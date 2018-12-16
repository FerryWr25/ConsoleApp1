using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Data;

namespace ConsoleApp1.myClass
{
    class ReadJson
    {
        public string news { get; set; }
        public string id { get; set; }

        public string readtheJsonOnline(int verified)
        {
            string dokument = null;
            string json = new WebClient().DownloadString("http://localhost:44300/read/News/5e28142a49e45a6d3426f3b9047a35ca");
            ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(json);
            int count = 1;
            foreach (var berita in ferr)
            {
                if (count == verified)
                {
                    dokument = berita.news;
                }
                count++;
            }
            return dokument;
        }
        public string readtheJsonOffline(int verified)
        {
            String path = @"C:\Users\eliteglobal-pc\source\repos\ConsoleApp1\ConsoleApp1\Dokumen\konten.json";
            string dokument = null;
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonpakek = sr.ReadToEnd();
                ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(jsonpakek);
                int count = 1;
                foreach (var berita in ferr)
                {
                    if (count == verified)
                    {
                        dokument = berita.news;
                    }
                    count++;
                }
                return dokument;
            }
        }
        public string getId_Dokument(int verified)
        {
            String path = @"C:\Users\eliteglobal-pc\source\repos\ConsoleApp1\ConsoleApp1\Dokumen\konten.json";
            string getId = null;
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonpakek = sr.ReadToEnd();
                ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(jsonpakek);
                int count = 1;
                foreach (var berita in ferr)
                {
                    if (count == verified)
                    {
                        getId = berita.id;
                    }
                    count++;
                }
                Console.WriteLine(getId);
                return getId;
            }
        }

        public DataTable displayJson()
        {
            String path = @"C:\Users\eliteglobal-pc\source\repos\ConsoleApp1\ConsoleApp1\Dokumen\konten.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                var table = JsonConvert.DeserializeObject<DataTable>(json);
                return table;
            }
        }
        public int getLong()
        {
            string json = new WebClient().DownloadString("http://localhost:44300/read/News/5e28142a49e45a6d3426f3b9047a35ca");
            ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(json);
            string[] dataJsonnya = new string[ferr.Length];
            int count = 1;
            foreach (var data in ferr)
            {
                count++;
            }
            return count;
        }
        public string seacrhData_onJson(string seacrh)
        {
            string dokument = null;
            string json = new WebClient().DownloadString("http://localhost:44300/read/News/5e28142a49e45a6d3426f3b9047a35ca");
            ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(json);
            foreach (var berita in ferr)
            {
                if (seacrh.Equals(berita.news.Split(' ')))
                {
                    dokument = berita.news;
                    Console.WriteLine(berita.news);
                    Console.ReadKey();
                }
            }
            return dokument;
        }
      
    }
}

