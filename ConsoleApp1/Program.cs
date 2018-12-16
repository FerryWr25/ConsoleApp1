using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ConsoleApp1.myClass;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    
    class Program
    {
       
        static void Main(string[] args)
        {
            koneksi con = new koneksi();
            ReadJson readJsonRun = new ReadJson();
            stemmingTala tala = new stemmingTala();
            string idDokument = null;
            for (int i = 1 ; i < 2; i++)    
            {
                con.openConnection();
                string textJson = null;
                int run = i;
                idDokument = readJsonRun.getId_Dokument(run);
                textJson = readJsonRun.readtheJsonOffline(run);
                tala.runStemming_Tala(textJson);
                tala.getFrekunsiKata(idDokument);
                con.stopAccess();
            }
            Console.WriteLine("selesai Semua.....................");
            Console.Read();
            //  Console.ReadKey();
           // tala.cekKepunyaan("sandalnya");
            //tala.replace_Akhiran("sandalnya");
            //tala.replace_Akhiran("sandalmu");
           //tala.replace_Awalan1("menyatu");
            //Console.Read();
        }

    }
}

