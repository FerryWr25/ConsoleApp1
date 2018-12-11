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
            ReadJson readJsonRun = new ReadJson();
            stemmingTala tala = new stemmingTala();
            for (int i = 24; i < 25; i++)
            {
                string textJson = null;
                textJson = readJsonRun.readtheJsonOffline(i + 1);
                tala.runStemming_Tala(textJson);
                tala.getValue_Tala();
                tala.getFrekunsiKata();
            }
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

