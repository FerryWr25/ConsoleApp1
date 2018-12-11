using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.myClass;
using System.Data;

namespace ConsoleApp1.myClass
{

    class bayyesClassifier
    {
        string[] datasetPolitik = { "pilkada","hukum","presiden","negara","kader",
            "partai","capres","wapres","politik","regulasi","pimpin","dewan","majelis","rapat","paspampres",
        "fraksi","kampanye","demokrasi","kampanye"};
        int countPolitik;
        string[] tempPolitik_huruf;
        int[] tempPolitik_jumlah;
        stemmingTala stemRun = new stemmingTala();
        ReadJson rdJson_Run = new ReadJson();
        string[] tempData;
        
    }
}
