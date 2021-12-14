﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QLNS
{
    class SQLDatabase
    {
        string connect = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLNSu;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable table = new DataTable();
        public void KetNoi()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
        }
        void NgatKetNoi()
        {
            conn.Close();
        }
        public void ThucThiKetNoi(string strsql)
        {
            KetNoi();
            cmd = new SqlCommand(strsql, conn);
            cmd.ExecuteNonQuery();
            NgatKetNoi();
        }
        public bool kttrungkhoa(string dauvao, string strsql)
        {
            bool ok = false;
            KetNoi();
            cmd = new SqlCommand(strsql, conn);
            var sqldr = cmd.ExecuteReader();
            while (sqldr.Read())
            {
                if (sqldr[0].ToString().ToLower() == dauvao.ToLower())
                    ok = true;
            }
            NgatKetNoi();
            return ok;
        }
    }
}