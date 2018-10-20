using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ConsoleApp1.myClass;
using System.Data;

namespace ConsoleApp1
{

    class Program
    {
        private koneksi con;

        public class getData
        {
            private koneksi con;
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

        }

        static void Main(string[] args)
        {

            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-' , '(' ,')' };

            string full_text = " Ferry Buntut dari kericuhan yang terjadi di Universitas Kanjuruhan Kota Malang (Unikama), aktivitas perkuliahan di kampus ini pun " +
                "terpaksa dihentikan untuk sementara. Keputusan diambil oleh pihak kampus sampai situasi kembali normal. " +
                "Perkuliahan kita hentikan sementara, pascaterjadi peristiwa tadi, ungkap Rektor Unikama Pieter Sahertian kepada wartawan. " +
                "Pieter menambahkan, bukan hanya menghentikan perkuliahan, pihaknya juga berencana mengubah akses keluar masuk kampus menjadi satu pintu. " +
                "Selama ini, ada dua pintu gerbang yang dijadikan akses keluar masuk oleh civitas akademika Unikama, yaitu di Jalan S Supriyadi dan di sisi utara " +
                "kampus yang menjadi sasaran amuk mahasiswa pagi tadi. saat ini masih ada sejumlah mahasiswa yang memilih bertahan di dalam kampus usai bentrokan terjadi. Rata-rata dari mereka adalah yang tidak terlibat bentrokan" +
                 "Aparat kepolisian masih bersiaga di lokasi kampus.Ruang rektorat turut disegel sampai situasi internal Unikama dipastikan kondusif " +
                 "Meski tetap meragukan perubahan iklim, namun Presiden Amerika Serikat Donald Trump mengatakan bahwa dirinya percaya ada sesuatu sedang terjadi. " +
                 "Saya pikir ini bukan hoaks, tapi ke depannya iklim pasti kembali berubah seperti sedia kala, ujar Presiden Trump dalam wawancara dengan program, yang ditayangkan oleh stasiun televisi CBS, Minggu malam. " +
                 "Dikutip dari Time.com pada Senin, Donald Trump mengatakan dia tidak ingin merugikan AS dalam menanggapi perubahan iklim. " +
                 " Saya pikir mungkin ada perbedaan pendapat. Tetapi saya tidak tahu bahwa itu (perubahan iklim) buatan manusia. Saya akan mengatakan ini: saya tidak ingin memberikan triliunan dolar. Saya tidak ingin kehilangan jutaan pekerjaan bagi seluruh rakyat Amerika " +
                 "Tidak lama berselang setelahnya, Donald Trump berbalik mengatakan dia bercanda tentang hubungan AS dan China, meski bertahun-tahun sejak itu dia " +
                 "terus menyebut pemanasan global sebagai tipuan. Trump, yang dijadwalkan pergi mengunjungi korban Badai Michael di negara bagian Georgia dan Florida pada hari Senin, juga menyatakan keraguan atas temuan para ilmuwan tentang keterkaitan perubahan iklim dan badai kuat. ";

            string text = "kehilangan kemandian kemudian kedudukan";

            System.Console.WriteLine($"Original text: '{full_text}'");
            string[] arrayWord;
            string[] kataSambung = { "yang" ,"tersebut","diduga" ,"sebelah","disekitar","berupa","sebelumnya","terjadi","dibandingkan" ,"penyebabnya","sekitar","pada" , "biasa" ,
                "dan" ,"dengan", "serta" ,"atau" ,"tetapi" ,"namun",
                "sedangkan","sebaliknya" ,"atau" ,"melainkan" ,"hanya", "bahkan","malah","lagipula","apalagi","jangankan"
            ,"kecuali","hanya","lalu","kemudian" ,"selanjutnya","yaitu" ,"yakni","bahwa","adalah","ialah"
            ,"jadi","karena itu","oleh sebab itu" ,"sebab","karena","kalau","jikalau","jika","bila","asal","sewaktu","sebelum"
            ,"sesudah","tatkala","sampai","hingga","sehingga","untuk","guna","laksana"};

            string[] words = full_text.Split(delimiterChars);
            System.Console.WriteLine($"{full_text.Length} words in text:");
            var count = new getData();
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
                arrayWord[i] = full_text.Split(delimiterChars)[i].ToString().ToLower();
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

            Console.WriteLine("TERM KATA YANG DIDAPAT==================================================");
            for (int wir = 0; wir < arrayWord.Length; wir++)
            {
                if (!arrayWord[wir].Equals(""))
                {
                    Console.WriteLine("[" + arrayWord[wir] + "]");
                }
            }
            Console.WriteLine("=================SUKSES==========================");
            Console.ReadKey();
        }
    }
}
