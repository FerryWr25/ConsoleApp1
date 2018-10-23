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
        private koneksi con;
        string[] arrayWord;

        public int getCOunt()
        {
            this.con = new koneksi();
            string query = "SELECT COUNT(*) FROM public.katadasar";
            DataTable result = this.con.getResult(query);
            int count = result.Rows.Count;
            return count;
        }
        public bool getKataDasar(string kata)
        {
            this.con = new koneksi();
            string query = "SELECT katadasar FROM public.katadasar where katadasar= '" + kata + "' ";
            DataTable result = this.con.getResult(query);
            if (result.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int cekSandang(string kata)
        {
            int verified = 0;
            string[] test_1 = { "lah", "kah", "pun" };
            for (int i = 0; i < kata.Length; i++)
            {
                for (int f = 0; f < test_1.Length; f++)
                {
                    if (kata.Substring(kata.Length - 3).Equals(test_1[f]))
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
                    if (kata.Substring(kata.Length - 3).Equals(test_2[f]))
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
                    if (kata.Substring(0, 2).Equals(test_2[f]))
                    {
                        verified++;
                    }
                    else if (kata.Substring(0, 3).Equals(test_2[f]))
                    {
                        verified++;
                    }
                    else if (kata.Substring(0, 4).Equals(test_2[f]))
                    {
                        verified++;
                    }
                    else if (kata.Substring(0, 5).Equals(test_2[f]))
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
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-', '(', ')' };

            
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
                        Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                    }
                    else
                    {
                        Console.WriteLine("belum kata dasar " + "[" + arrayWord[i] + "]" + " Lanjut ke proses selanjutnya");
                        if (count.getKataDasar(arrayWord[i]) == true)
                        {
                            Console.WriteLine("sudah kata dasar jadi " + "[" + arrayWord[i] + "]");
                        }
                        else if (count.cekawalan1(arrayWord[i]) > 1)//cek punyak awalan kah ???
                        {
                            Console.WriteLine("[" + arrayWord[i] + "]" + "Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1");
                            arrayWord[i] = count.replace_Awalan1(arrayWord[i]);
                            Console.WriteLine("Tidak Punyak akhiran, maka dilakukan menghilangkan awalan 1 menjadi " + "[" + arrayWord[i] + "]");
                        }
                    }
                }
                else
                {
                    arrayWord[i] = arrayWord[i];
                    Console.WriteLine("[" + arrayWord[i] + "]");
                }
            }


        }
        public string getValue_Tala()
        {
            string value = null;
            Console.WriteLine("TERM KATA YANG DIDAPAT==================================================");
            for (int wir = 0; wir < arrayWord.Length; wir++)
            {
                if (!arrayWord[wir].Equals(""))
                {
                    Console.WriteLine("[" + arrayWord[wir] + "]");
                    value = arrayWord[wir];
                }
            }
            Console.WriteLine("=================SUKSES==========================");
            Console.ReadKey();
            Console.ReadKey();
            return value;
        }
    }
}
