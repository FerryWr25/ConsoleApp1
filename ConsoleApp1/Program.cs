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

            stemmingTala stemTala = new stemmingTala();
            stemTala.runStemming_Tala(full_text);
            stemTala.getValue_Tala();
            Console.ReadKey();
        }
    }
}
