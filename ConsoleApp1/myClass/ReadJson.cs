using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace ConsoleApp1.myClass
{
    class ReadJson
    {
        public string news { get; set; }

        public string readtheJson()
        {
            string dokument = null;
            string json = new WebClient().DownloadString("http://localhost:44300/read/News/5e28142a49e45a6d3426f3b9047a35ca");
            ReadJson[] ferr = JsonConvert.DeserializeObject<ReadJson[]>(json);
            string[] dataJsonnya = new string[ferr.Length];
            int count = 1;
            foreach (var data in ferr)
            {
                if (count == 25)
                {
                    dokument = data.news; 
                }
                count++;
            }
            return dokument;
        }
    }

}
