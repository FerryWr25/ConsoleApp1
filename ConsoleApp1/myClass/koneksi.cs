using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace ConsoleApp1.myClass
{
    class koneksi
    {
        NpgsqlConnection con;
        NpgsqlCommand command;

        public koneksi()
        {
            this.openConnection();
        }

        public void openConnection()
        {
            if (this.con == null)
            {
                // buat koneksi baru
                var connectionString = "Server=127.0.0.1;Port=5432;Database=Skripsiku; User Id=postgres; Password = 'admin' ;Pooling=False;";
                this.con = new NpgsqlConnection(connectionString);
            }

            // buka koneksi
            if (this.con.State == ConnectionState.Closed)
            {
                this.con.Open();
                NpgsqlConnection.ClearAllPools();}
        }
        public void stopAccess()
        {
            NpgsqlConnection.ClearAllPools();
            this.con.Close();
        }

        public void closeConnection()
        {
            if (this.con.State == ConnectionState.Open)
            {
                // tutup koneksi
                this.con.Close();
            }
        }

        public void excequteQuery(string query)
        {
            // buat command baru
            this.command = new NpgsqlCommand(query, this.con);
            this.command.CommandType = CommandType.Text;

            // eksekusi query
            this.command.ExecuteNonQuery();
            this.closeConnection();
        }

        public DataTable getResult(string query)
        {
            // buat command baru
            this.command = new NpgsqlCommand(query, this.con);
            this.command.CommandType = CommandType.Text;

            // baca data
            NpgsqlDataReader reader;
            reader = this.command.ExecuteReader();

            // menampung hasil query
            DataTable result = new DataTable();
            result.Load(reader);
            this.closeConnection();
            // mengembalikan data table berisi hasil query
            return result;
        }
    }
}