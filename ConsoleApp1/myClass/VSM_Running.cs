using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.myClass;

namespace ConsoleApp1.myClass
{
    class VSM_Running
    {
        stemmingTala tala = new stemmingTala();
        double hasil_TF_IDF_Query;
        public void getDokumen(string query)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-', '(', ')', '"', '`' };
            System.Console.WriteLine($"Original text: '{query}'");
            string[] words = query.Split(delimiterChars);
            getFrekunsiKata_fromQuery(words);
        }

        public string getIDF_Term(string term)
        {
            koneksi con = new koneksi();
            con.openConnection();
            double getIDF_Term = 0;
            string QgetDF_Term = "SELECT \"IDF\"  FROM public.\"Term\" where \"Term\"='" + term + "';";
            DataTable result = con.getResult(QgetDF_Term);
            getIDF_Term = Convert.ToDouble(result.Rows[0]["IDF"]);
            con.stopAccess();
            return getIDF_Term.ToString();

        }

        public double cekKetersediaan_Term(string kata)
        {
            koneksi con = new koneksi();
            con.openConnection();
            string query = "SELECT \"Term\"  FROM public.\"Term\" where \"Term\"='" + kata + "';";
            DataTable result = con.getResult(query);
            con.closeConnection();
            if (result.Rows.Count >= 1)
            {
                con.openConnection();
                double getIDF_Term = 0;
                string QgetDF_Term = "SELECT \"IDF\"  FROM public.\"Term\" where \"Term\"='" + kata + "';";
                DataTable result2 = con.getResult(QgetDF_Term);
                getIDF_Term = Convert.ToDouble(result2.Rows[0]["IDF"]);
                con.stopAccess();
                return getIDF_Term;

            }
            else
            {
                return 0;
            }

        }
        public double count_TF_IDF_Query(double Tf_Idf, double frekuensi)
        {
            double hasil = 0;
            hasil = frekuensi * Tf_Idf;
            return hasil;
        }

        public string cariID_Term(string data)
        {
            koneksi con = new koneksi();
            con.openConnection();
            string termCari = null;
            string termMasuk = "SELECT * FROM public.\"Term\" where \"Term\"='" + data + "';";
            DataTable result2 = con.getResult(termMasuk);
            if (result2.Rows.Count >= 1)
            {
                termCari = result2.Rows[0]["id_Term"].ToString();
                con.stopAccess();
                return termCari;
            }
            else
            {
                return "0";
            }

        }

        public double cariTF_IDF(string id_doc, string idTerm)
        {
            koneksi con = new koneksi();
            con.openConnection();
            double termCari;
            string termMasuk = "SELECT \"TF-IDF\" FROM public.\"bobotTerm\" where" +
                " \"idDokumen\"='" + id_doc + "' and \"id_Term\"='" + idTerm + "';";
            DataTable result2 = con.getResult(termMasuk);
            if (result2.Rows.Count >= 1)
            {
                termCari = Convert.ToDouble(result2.Rows[0]["TF-IDF"]);
                con.stopAccess();
                return termCari;
            }
            else
            {
                return 0;
            }

        }

        public void getDoc_MengandungQuery(string query)
        {
            string queryPakek = cariID_Term(query);
            double [] hasilPembilang_Perkata;
            double [] hasilPenyebut_Perkata;
            double [] hasilpembilang_Query;


            if (queryPakek.Equals("0"))
            {
                Console.WriteLine("Tidak ada dokument yang memakai kata " + query + "");
                Console.WriteLine("======================================================================");
            }
            else
            {
                koneksi con = new koneksi();
                con.openConnection();
                string queryDF = "SELECT \"idDokumen\", \"id_Term\", \"TF-IDF\" FROM public.\"bobotTerm\" where" +
                    " \"id_Term\"='" + queryPakek + "'";
                DataTable result = con.getResult(queryDF);

                string[] dataID_Term = new string[result.Rows.Count];
                double[] hasilTF_IDF_Perkata = new double[dataID_Term.Length];
                string[] dataTF_IDF_Term = new string[result.Rows.Count];
                for (int i = 0; i < result.Rows.Count; i++)
                {

                    dataTF_IDF_Term[i] = result.Rows[i]["idDokumen"].ToString();
                    hasilTF_IDF_Perkata[i] = Convert.ToDouble(result.Rows[i]["TF-IDF"]);
                    hasilPembilang_Perkata = new double[result.Rows.Count];
                    hasilPenyebut_Perkata = new double[result.Rows.Count];
                    if ((result.Rows.Count - 1) == i)
                    {
                        Console.WriteLine("dokument yang memakai kata " + query + " sebanyak " + result.Rows.Count +"yaitu "+ dataID_Term[i]);
                        Console.WriteLine("======================================================================");
                        double TF_IDFperKata = cariTF_IDF(dataID_Term[i], queryPakek);

                        for (int w = 0; w < result.Rows.Count; w++)
                        {
                            hasilPembilang_Perkata[w] = hasilTF_IDF_Perkata[w] * hasil_TF_IDF_Query;
                            Console.WriteLine("nilai pembilang doc " + (w + 1) + " = " + hasilPembilang_Perkata[w]);
                            Console.WriteLine("======================================================================");
                            double hasilTotal_Pembilang =+ hasilPembilang_Perkata[w];
                        }
                    }
                    Console.WriteLine("dokument yang memakai kata " + query + " sebanyak " + result.Rows.Count + "yaitu " + dataID_Term[i]);
                }
                con.stopAccess();
            }

        }

        public void getFrekunsiKata_fromQuery(string[] data)
        {
            int[] frekuensi = new int[data.Length];
            int i, j, ctr;
            Console.WriteLine(data.Length + " Panjangnya");
            for (i = 0; i < data.Length; i++)
            {
                frekuensi[i] = -1;
            }
            for (i = 0; i < data.Length; i++)
            {
                ctr = 1;
                for (j = i + 1; j < data.Length; j++)
                {
                    if (data[i].Equals(data[j]))
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
            for (i = 0; i < data.Length; i++)

            {
                if (frekuensi[i] != 0)

                {
                    if (!data[i].Equals(""))
                    {
                        if (data[i].Length > 2)
                        {
                            Console.WriteLine("kata " + data[i] + " muncul : " + frekuensi[i]);
                            double TF_IDF_Query = Convert.ToDouble(cekKetersediaan_Term(data[i]));
                            hasil_TF_IDF_Query = count_TF_IDF_Query(TF_IDF_Query, Convert.ToDouble(frekuensi[i]));
                            Console.WriteLine("nilai TF-IDF Query " + data[i] + " = " + hasil_TF_IDF_Query);
                            Console.WriteLine("Lanjut Hitung Pembilang pada cosine similarity");
                            getDoc_MengandungQuery(data[i]);

                        }
                    }
                }

            }

        }

    }
}
