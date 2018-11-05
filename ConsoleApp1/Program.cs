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

        static void Main(string[] args)
        {
            string textJson = null;
            ReadJson readJsonRun = new ReadJson();
            textJson = readJsonRun.readtheJson();
            stemmingTala stemTala = new stemmingTala();
            stemTala.runStemming_Tala(textJson);
            stemTala.getValue_Tala();
            Console.ReadKey();
        }
    }
}
