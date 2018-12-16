using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ConsoleApp1.myClass;
using System.Data;
namespace ConsoleApp1.myClass
{
    class stemmingTala
    {

        string[] arrayWord;
        string[] Array_frekuensi, countFrekuensi;
        int[] Array_frekuensiCopy;
        int[] Array_frekuensiResult;
        private string[] value;
        koneksi con = new koneksi();

        public int getCOunt()
        {
            this.con = new koneksi();
            string query = "SELECT COUNT(*) FROM public.katadasar";
            DataTable result = this.con.getResult(query);
            int count = result.Rows.Count;
            return count;
        }

        public bool runVSM_DB(string data, string id, int tf)
        {
            int status = 0;
            string idTerm;
            this.con = new koneksi();
            con.openConnection();
            string cek_KesediaanTerm = "SELECT * FROM public.\"Term\" where \"Term\"='" + data + "';";
            DataTable result = this.con.getResult(cek_KesediaanTerm);
            con.stopAccess();
            if (result.Rows.Count == 0)
            {
                //simpan Term baru
                con.openConnection();
                this.con = new koneksi();
                string queryTerm = "INSERT INTO public.\"Term\"(\"Term\", \"DF\") VALUES ('" + data + "','1');";
                con.excequteQuery(queryTerm);
                con.stopAccess();

                //baca idTermnya
                idTerm = cariID_Term(data);
                insert_ToBobot(id, idTerm, tf);
                con.stopAccess();

            }
            else // ketika sama
            {
                //baca idTermnya
                idTerm = cariID_Term(data);
                //update nilai df kata-n
                int dfBefore = Convert.ToInt32(getValueDF(data));
                int data_update = (dfBefore + 1);
                updateDF(data, Convert.ToString(data_update));
                insert_ToBobot(id, idTerm, tf);
                con.stopAccess();
            }
            //simpan dokument
            return true;

        }
        public double count_IDF(string id_term)
        {
            //hitung idf
            double n_Doc = Convert.ToDouble(get_LongDoc());
            double n_DF = Convert.ToDouble(getDF_Term(id_term));
            double IDF = Math.Round(Math.Log10(n_Doc / n_DF), 3);
            Console.WriteLine("Log (" + n_Doc + "/" + n_DF + ")" + " = " + IDF);
            //set IDF
            return IDF;
        }
        public double count_TF_IDF(string id_term, string idBobotTerm)
        {
            //hitung idf
            double TF_Term = Convert.ToDouble(getTF_Term(idBobotTerm));
            double IDF_Term = Convert.ToDouble(getIDF_Term(id_term));
            double TF_IDF = Math.Round(TF_Term * IDF_Term, 3);
            Console.WriteLine(TF_Term + " * " + IDF_Term + " = " + TF_IDF);
            //set IDF
            return TF_IDF;
        }
        public string getTF_Term(string idBobotTerm)
        {
            con.openConnection();
            int getTF_Term = 0;
            string QgetDF_Term = "SELECT \"Tf\" FROM public.\"bobotTerm\" where \"id_bobotTerm\"='" + idBobotTerm + "';";
            DataTable result = this.con.getResult(QgetDF_Term);
            getTF_Term = Convert.ToInt32(result.Rows[0]["Tf"]);
            con.stopAccess();
            return getTF_Term.ToString();
        }
        public string getIDF_Term(string idTerm)
        {
            con.openConnection();
            double getIDF_Term = 0;
            string QgetDF_Term = "SELECT \"IDF\"  FROM public.\"Term\" where \"id_Term\"='" + idTerm + "';";
            DataTable result = this.con.getResult(QgetDF_Term);
            getIDF_Term = Convert.ToDouble(result.Rows[0]["IDF"]);
            con.stopAccess();
            return getIDF_Term.ToString();

        }

        public int getDF_Term(string id_term)
        {
            con.openConnection();
            int getDF_Term;
            string QgetDF_Term = "SELECT \"DF\" FROM public.\"Term\" where \"id_Term\"='" + id_term + "';";
            DataTable result = this.con.getResult(QgetDF_Term);
            getDF_Term = Convert.ToInt32(result.Rows[0]["DF"]);
            con.stopAccess();
            return getDF_Term;
        }
        public int get_LongDoc()
        {
            int getNumber;
            con.openConnection();
            this.con = new koneksi();
            string queryDF = "SELECT count (*) as dipakek_nDoc FROM public.\"bobotTerm\" group by \"idDokumen\";";
            DataTable result = this.con.getResult(queryDF);
            getNumber = Convert.ToInt32(result.Rows.Count);
            con.stopAccess();
            return getNumber;
        }
        public void updateDF(string term, string ndata)
        {
            con.openConnection();
            this.con = new koneksi();
            string queryDF = "UPDATE public.\"Term\" set \"DF\"=" + ndata + " WHERE \"Term\"='" + term + "';";
            con.excequteQuery(queryDF);
            con.stopAccess();
        }
        public void updateIDF(double value, string idTerm)
        {
            con.openConnection();
            this.con = new koneksi();
            string queryDF = "UPDATE public.\"Term\" SET \"IDF\"= '" + value + "' WHERE \"id_Term\"='" + idTerm + "';";
            con.excequteQuery(queryDF);
            con.stopAccess();
        }
        public void updateTF_IDF(double value, string idBobotTerm)
        {
            con.openConnection();
            this.con = new koneksi();
            string queryTF_IDF = "UPDATE public.\"bobotTerm\" SET \"TF-IDF\"='" + value + "' WHERE \"id_bobotTerm\"='" + idBobotTerm + "';";
            con.excequteQuery(queryTF_IDF);
            con.stopAccess();
        }

        public string getValueDF(string term)
        {
            string valueCari = null;
            this.con = new koneksi();
            con.openConnection();
            string termMasuk = "SELECT \"DF\"  FROM public.\"Term\" where \"Term\"='" + term + "';";
            DataTable result = this.con.getResult(termMasuk);
            valueCari = result.Rows[0]["DF"].ToString();
            con.stopAccess();
            return valueCari;

        }
        public void simpanTermBaru(string term)
        {
            con.openConnection();
            this.con = new koneksi();
            string queryTerm = "INSERT INTO public.\"Term\"(\"Term\") VALUES ('" + term + "');";
            con.excequteQuery(queryTerm);
            con.stopAccess();
        }
        public string cariID_Term(string data)
        {
            string termCari = null;
            this.con = new koneksi();
            con.openConnection();
            string termMasuk = "SELECT * FROM public.\"Term\" where \"Term\"='" + data + "';";
            DataTable result2 = this.con.getResult(termMasuk);
            termCari = result2.Rows[0]["id_Term"].ToString();
            con.stopAccess();
            return termCari;
        }
        public void insert_ToBobot(string idDoc, string idTerm, int tf)
        {
            this.con = new koneksi();
            con.openConnection();
            string queryInsert_Dokument = "INSERT INTO public.\"bobotTerm\"(\"idDokumen\", \"id_Term\", \"Tf\") VALUES ('" + idDoc + "', '" + idTerm + "', " + tf + ");";
            con.excequteQuery(queryInsert_Dokument);
            con.stopAccess();
        }
        public void setIDF()
        {
            con.openConnection();
            this.con = new koneksi();
            string queryDF = "SELECT \"id_Term\"  FROM public.\"Term\";";
            DataTable result = this.con.getResult(queryDF);
            string[] dataID_Term = new string[result.Rows.Count];
            for (int i = 0; i < result.Rows.Count; i++)
            {

                dataID_Term[i] = result.Rows[i]["id_Term"].ToString();
                Console.WriteLine(dataID_Term[i] + " =====================================");
                double value = count_IDF(dataID_Term[i]);
                updateIDF(value, dataID_Term[i]);

            }
            con.stopAccess();

        }
        public void setTF_IDF()
        {
            con.openConnection();
            this.con = new koneksi();
            string queryIDF = "SELECT \"id_bobotTerm\", \"id_Term\"  FROM public.\"bobotTerm\"";
            DataTable result = this.con.getResult(queryIDF);
            string[] dataID_Term = new string[result.Rows.Count];
            string[] dataID_BOBOT_Term = new string[result.Rows.Count];
            for (int i = 0; i < result.Rows.Count; i++)
            {

                dataID_Term[i] = result.Rows[i]["id_Term"].ToString();
                dataID_BOBOT_Term[i] = result.Rows[i]["id_bobotTerm"].ToString();
                Console.WriteLine(dataID_Term[i] + " =====================================");
                double value = count_TF_IDF(dataID_Term[i], dataID_BOBOT_Term[i]);
                updateTF_IDF(value, dataID_BOBOT_Term[i]);
            }
            con.stopAccess();

        }


        public bool getKataDasar(string kata)
        {
            this.con = new koneksi();
            string query = "SELECT katadasar FROM public.katadasar where katadasar= '" + kata + "' ";
            DataTable result = this.con.getResult(query);
            con.closeConnection();
            if (result.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            con.stopAccess();

        }

        public int cekSandang(string kata)
        {
            int verified = 0;
            string[] test_1 = { "lah", "kah", "pun" };
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_1.Length; f++)
                {
                    if (kata.Length < 3)
                    {

                    }
                    else if (kata.Substring(kata.Length - 3).Equals(test_1[f]))
                    {
                        verified++;
                    }

                }
            }
            return verified;
        }

        public string replace_Sandang(string kata)
        {
            string replace = "";
            string[] test_1 = { "lah", "kah", "pun" };
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_1.Length; f++)
                {
                    if (kata.Substring(kata.Length - 3).Equals(test_1[f]))
                    {
                        replace = kata.Replace(kata.Substring(kata.Length - 3), string.Empty);
                        break;
                    }
                }
            }
            return replace;
        }
        public int cekAkhiran(string kata)
        {
            int verified = 0;
            string[] test_2 = { "ku", "nya", "mu", "kan", "an", "i" };
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_2.Length; f++)
                {
                    if (kata.Length < 3)
                    {

                    }
                    else if (kata.Substring(kata.Length - 3).Equals(test_2[f]))
                    {
                        verified++;
                    }
                    else if (kata.Substring(kata.Length - 2).Equals(test_2[f]))
                    {
                        verified++;
                    }
                }
            }
            return verified;
        }
        public string replace_Akhiran(string kata)
        {
            string replace = "";
            string[] test_2 = { "ku", "nya", "mu", "kan", "an", "i" };
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_2.Length; f++)
                {
                    if (kata.Substring(kata.Length - 3).Equals(test_2[f]))
                    {
                        replace = kata.Replace(kata.Substring(kata.Length - 3), string.Empty);
                        break;
                    }
                    else if (kata.Substring(kata.Length - 2).Equals(test_2[f]))//kenak replace kalau akhiran juga sama
                    {
                        replace = kata.Replace(kata.Substring(kata.Length - 2), string.Empty);
                        break;
                    }
                }
            }
            return replace;
        }
        public int cekawalan1(string kata)
        {
            int verified = 0;
            string[] test_2 = { "meng", "menya", "menyi" , "menyu","menye" , "menyo" ,"meny","men","mema","memi","memu","meme","memo",
            "mem","me","peng","penya","penyi","penyu","penye","penyo","peny","pen","pema","pemi","pemu","peme","pemo","pem","di","ter","ke","ber","bel","be","per","pel","pe"};
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_2.Length; f++)
                {
                    if (kata.Length < 3)
                    {
                        verified = 0;
                    }
                    else if (kata.Substring(0, 2).Equals(test_2[f]))
                    {
                        verified++;
                    }
                    else if (kata.Substring(0, 3).Equals(test_2[f]))
                    {
                        verified++;
                    }

                }
            }
            return verified;
        }
        public string replace_Awalan1(string kata)
        {
            string replace = "";
            string[] test_3 = { "meng", "menya", "menyi" , "menyu","menye" , "menyo" ,"meny","men","mema","memi","memu","meme","memo",
            "mem","me","peng","penya","penyi","penyu","penye","penyo","peny","pen","pema","pemi","pemu","peme","pemo","pem","di","ter","ke","ber","bel","be","per","pel","pe"};
            string[] test_3replace = { "", "s", "s", "s", "s", "s", "s", "t", "pa", "p", "p", "p", "p" ,"","","","s","s","s","s","s","s","",
            "p","p","p","p","p","","","","","","","","","",""};

            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_3.Length; f++)
                {
                    if (kata.Length < 5)
                    {
                        Console.WriteLine("kata ini kurang dari 5 karakter jadi tidak bisa dihilangkan awalannya ");
                    }
                    else
                    {
                        if (kata.Substring(0, 2).Equals(test_3[f]))
                        {
                            replace = kata.Replace(kata.Substring(0, 2), test_3replace[f]);
                            Console.WriteLine("2 kata sama ");
                            break;
                        }
                        else if (kata.Substring(0, 3).Equals(test_3[f]))
                        {
                            replace = kata.Replace(kata.Substring(0, 3), test_3replace[f]);
                            Console.WriteLine("3 kata sama ");
                            break;
                        }
                        else if (kata.Substring(0, 4).Equals(test_3[f]))
                        {
                            replace = kata.Replace(kata.Substring(0, 4), test_3replace[f]);
                            Console.WriteLine("4 kata sama ");
                            break;
                        }
                        else if (kata.Substring(0, 5).Equals(test_3[f]))
                        {
                            replace = kata.Replace(kata.Substring(0, 5), test_3replace[f]);
                            if (getKataDasar(replace) == true)
                            {
                                replace = kata;
                            }
                            else
                            {
                                Console.WriteLine("dia bukan kata dasar");
                                string[] alternative = { "sa", "si", "su", "se", "so" };
                                string[] temp = new string[alternative.Length];
                                for (int fer = 0; fer < temp.Length; fer++)
                                {
                                    temp[fer] = kata.Replace(kata.Substring(0, 5), alternative[fer]);
                                    Console.WriteLine(temp[fer]);
                                    if (getKataDasar(temp[fer]) == true)
                                    {
                                        replace = kata.Replace(kata.Substring(0, 5), alternative[fer]);
                                        Console.WriteLine(temp[fer]);
                                        break;
                                    }
                                    else
                                    {
                                        replace = kata.Replace(kata.Substring(0, 5), test_3replace[f]);
                                    }
                                }
                            }
                            Console.WriteLine("5 kata sama ");
                            break;
                        }
                    }

                }
            }
            return replace;
        }


        public bool getKataSambung(string kata)
        {
            this.con = new koneksi();
            string query = "SELECT kata FROM public.kata_sambung where kata =  '" + kata + "'";
            DataTable result = this.con.getResult(query);
            con.closeConnection();
            if (result.Rows.Count == 1)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        public void runStemming_Tala(string text)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-', '(', ')', '"', '`' };
            System.Console.WriteLine($"Original text: '{text}'");
            string[] words = text.Split(delimiterChars);
            System.Console.WriteLine($"{text.Length} words in text:");
            var count = new stemmingTala();
            int getData = count.getCOunt();
            int range = words.Length;
            arrayWord = new string[range];

            //kumpulan aturan tala Stemming//
            string[] test_1 = { "lah", "kah", "pun" };
            string[] test_2 = { "nya", "ku", "mu" };
            string[] test_3 = { "meng", "menya", "menyi" , "menyu","menye" , "menyo" ,"meny","men","mema","memi","memu","meme","memo",
            "memo","me","peng","penya","penyi","penyu","penye","penyo","peny","pen","pema","pemi","pemu","peme","pemo","pem","di","ter","ke"};
            string[] test_3replace = { "", "s", "s", "s", "s", "s", "s", "", "p", "p", "p", "p", "p" ,"","","","s","s","s","s","s","s","",
            "p","p","p","p","p","","","",""};

            string[,] test_22 = { { "meng", "" },{ "menya","s"} , { "menyi", "s" }, { "menyu", "s" }, { "menye", "s" }
            ,{ "menyo","s"} ,{ "meny","s"},{ "men","s"},{ "mema","p"},{ "memi","p"},{ "memu","p"},{ "meme","p"}
            ,{ "memo","p"},{ "mem",""},{ "me",""},{ "peng",""},{ "penya","s"},{ "penyi","s"},{ "penye","s"},{ "penyo","s"}
            ,{ "peny","s"},{ "pen",""},{ "pema","s"},{ "pemi","s"},{ "pemu","s"},{ "peme","s"},{ "pemo","s"},{ "pem",""}
            ,{ "di",""},{ "ter",""},{ "ke",""}};
            string[,] test_3a = { { "ber", "" }, { "bel", "" }, { "be", "" }, { "per", "" }, { "pel", "" }, { "pe", "" } };
            string[,] test_4das = { { "kan", "" }, { "an", "" }, { "i", "" } };

            //proses memasukkan kata kedalam array//
            for (int i = 0; i < words.Length; i++)
            {
                arrayWord[i] = text.Split(delimiterChars)[i].ToString().ToLower();
                Console.WriteLine("[" + arrayWord[i] + "]");
                //Proses filtering, atau menghilangkan kata sambung//

                if (count.getKataSambung(arrayWord[i]) == true)
                {
                    //proses menghilangkan wordList//
                    Console.WriteLine("[" + arrayWord[i] + "]" + "=> masuk kata sambung, kata dalam index ini dihapus");
                    arrayWord[i] = "";
                }
                //memeriksa apakah sudah kata dasar//
                else if (count.getKataDasar(arrayWord[i]) == true)
                {
                    arrayWord[i] = arrayWord[i];
                    this.con = new koneksi();
                    Console.WriteLine("[" + arrayWord[i] + "]" + "[ini kata dasar_ so Fix]");
                }
                //proses tala ke-1// hilangkan sandang kalau ada
                else if (count.cekSandang(arrayWord[i]) > 0)//cek apakah punyak sandang
                {
                    Console.WriteLine("[" + arrayWord[i] + "]" + " Masuk Proses Tala 1");
                    arrayWord[i] = count.replace_Sandang(arrayWord[i]);//proses hapus sandang

                    if (count.getKataDasar(arrayWord[i]) == true)
                    {
                        Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                    }
                    else if (count.cekAkhiran(arrayWord[i]) > 0)//cek akhirannya
                    {
                        Console.WriteLine("[" + arrayWord[i] + "]" + " Masuk Proses Tala 1");
                        arrayWord[i] = count.replace_Akhiran(arrayWord[i]);//proses hapus akhiran
                        if (count.getKataDasar(arrayWord[i]) == true)
                        {
                            Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                        }

                        else
                        {
                            Console.WriteLine("belum kata dasar " + "[" + arrayWord[i] + "]" + " Lanjut ke proses selanjutnya");
                        }
                    }
                    else
                    {
                        Console.WriteLine("belum kata dasar " + "[" + arrayWord[i] + "]" + " Lanjut ke proses selanjutnya");
                        if (count.cekAkhiran(arrayWord[i]) > 0)// cek apakah punyak akhiran
                        {
                            arrayWord[i] = count.replace_Akhiran(arrayWord[i]);//proses hapus akhiran
                            Console.WriteLine("diproses 2 jadi " + "[" + arrayWord[i] + "]");
                        }

                        else
                        {
                            Console.WriteLine("[" + arrayWord[i] + "]" + "Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1");
                            arrayWord[i] = count.replace_Awalan1(arrayWord[i]);
                            //runVSM_DB(arrayWord[i], id, getFrekunsiKata());//lakukan proses kelola DB
                            Console.WriteLine("Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1 menjadi " + "[" + arrayWord[i] + "]");
                        }
                    }
                }
                else if (count.cekAkhiran(arrayWord[i]) > 0)//cek akhirannya
                {
                    Console.WriteLine("[" + arrayWord[i] + "]" + " Masuk Proses Tala 1");
                    arrayWord[i] = count.replace_Akhiran(arrayWord[i]);//proses hapus akhiran
                    if (count.getKataDasar(arrayWord[i]) == true)
                    {
                        //runVSM_DB(arrayWord[i], id, getFrekunsiKata());//lakukan proses kelola DB
                        Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                    }
                    else
                    {
                        Console.WriteLine("belum kata dasar " + "[" + arrayWord[i] + "]" + " Lanjut ke proses selanjutnya");
                        if (count.getKataDasar(arrayWord[i]) == true)
                        {
                            Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                        }
                        else if (count.cekawalan1(arrayWord[i]) > 0)//cek punyak awalan kah ???
                        {
                            Console.WriteLine("[" + arrayWord[i] + "]" + "Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1");
                            arrayWord[i] = count.replace_Awalan1(arrayWord[i]);
                            //runVSM_DB(arrayWord[i], id, getFrekunsiKata());//lakukan proses kelola DB
                            Console.WriteLine("Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1 menjadi " + "[" + arrayWord[i] + "]");
                        }
                    }
                }
                else if (count.cekawalan1(arrayWord[i]) > 0)
                {
                    Console.WriteLine("[" + arrayWord[i] + "]" + "Tidak Punyak akhiran, maka dilakukan menghilangkan awalan");
                    arrayWord[i] = count.replace_Awalan1(arrayWord[i]);
                    //runVSM_DB(arrayWord[i], id, getFrekunsiKata());//lakukan proses kelola DB
                    Console.WriteLine("Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1 menjadi " + "[" + arrayWord[i] + "]");
                }
                else if (arrayWord[i].Length < 3)
                {
                    arrayWord[i] = "";
                }
                else
                {
                    arrayWord[i] = arrayWord[i];
                    //runVSM_DB(arrayWord[i], id, getFrekunsiKata());//lakukan proses kelola DB
                    Console.WriteLine("[" + arrayWord[i] + "]");
                }
                con.stopAccess();
            }


        }
        public string[] getValue_Tala2()
        {
            Console.WriteLine("=================TERM KATA YANG DIDAPAT=========================");
            int count = 0;
            List<string> my_List = new List<string>();
            for (int i = 0; i < arrayWord.Length; i++)
            {
                if (!arrayWord[i].Equals(""))
                {
                    count++;
                }

            }
            for (int i = 0; i < count; i++)
            {
                if (!arrayWord[i].Equals(""))
                {
                    value[i] = arrayWord[i];
                }
                Console.WriteLine(value[i] + "``");
            }
            Console.WriteLine("=================SUKSES==========================");

            return value;

        }
        public string getValue_Tala()
        {
            int[] frekuensi = new int[arrayWord.Length];
            string value = null;
            Console.WriteLine("TERM KATA YANG DIDAPAT==================================================");
            for (int wir = 0; wir < arrayWord.Length; wir++)
            {
                if (!arrayWord[wir].Equals(""))
                {
                    Console.WriteLine(arrayWord[wir]);
                    value = arrayWord[wir];
                }
            }
            Console.WriteLine("=================SUKSES==========================");
            return value;
        }

        public void getFrekunsiKata(string id)
        {
            int[] frekuensi = new int[arrayWord.Length];
            int i, j, ctr, countIDF = 0;
            Console.WriteLine(arrayWord.Length + " Panjangnya");
            for (i = 0; i < arrayWord.Length; i++)
            {
                frekuensi[i] = -1;
            }
            for (i = 0; i < arrayWord.Length; i++)
            {
                ctr = 1;
                for (j = i + 1; j < arrayWord.Length; j++)
                {
                    if (arrayWord[i].Equals(arrayWord[j]))
                    {
                        ctr++;
                        frekuensi[j] = 0;
                    }
                }
                if (frekuensi[i] != 0)
                {
                    frekuensi[i] = ctr;
                }
            }
            Console.Write("\nThe frequency of all elements of the array : \n");
            for (i = 0; i < arrayWord.Length; i++)

            {
                if (frekuensi[i] != 0)

                {
                    if (!arrayWord[i].Equals(""))
                    {
                        if (arrayWord[i].Length > 2)
                        {
                            runVSM_DB(arrayWord[i], id, frekuensi[i]);
                        }
                        else
                        {
                            Console.WriteLine(arrayWord[i] + " terlalu pendek, so gak dimasukkan");
                        }

                    }
                }
                if ((arrayWord.Length - 1) == i)
                {
                    Console.WriteLine("Proses menghitung IDF");
                    setIDF();
                    Console.WriteLine("Proses menghitung TF-IDF");
                    setTF_IDF();
                }

            }

        }


    }
}
