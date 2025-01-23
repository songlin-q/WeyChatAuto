using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeyChatAuto.Common;
using WeyChatAuto.Model;


namespace WeyChatAuto.DBHelper
{
    public static class DbHelper
    {
        private static string dbPath = System.AppDomain.CurrentDomain.BaseDirectory + "WeyChatAutoDB.db";
        private static SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbPath);
        public static string TableDbReal = "T_FriendList";//表1

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CreateDataBase()
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    SQLiteConnection.CreateFile(dbPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("新建数据库文件" + dbPath + "失败：" + ex.Message);
            }
        }

        public static void NewTable(string tableName, List<string> Columns)
        {
            if (sqliteConn.State != System.Data.ConnectionState.Open) sqliteConn.Open();
            string Column = "";
            for (int i = 0; i < Columns.Count; i++)
            {
                Column += Columns[i] + ",";
            }
            Column = Column.Substring(0, Column.Length - 1);
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = sqliteConn;
            cmd.CommandText = " CREATE TABLE " + tableName + "(" + Column + ")";
            cmd.ExecuteNonQuery();
            sqliteConn.Close();
        }

        public static List<string[]> Query<T>(T model, string TableName, string where = "")
        {
            List<string[]> datas = new List<string[]>();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = sqliteConn;
                sqliteConn.Open();
                cmd.CommandText = "select * from " + TableName;
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                System.Reflection.PropertyInfo[] properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    throw new Exception("类属性长度为零");
                }
                foreach (DataRow dd in dt.Rows)
                {
                    string[] data = new string[properties.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = dd[i].ToString();
                    }
                    datas.Add(data);
                }
            }
            sqliteConn.Close();
            return datas;
        }


        public static DataTable QueryDt<T>(string TableName, Expression<Func<T, bool>> exp = null)
        {
            DataTable dt = new DataTable();

            var whereStr = ExpressHelper.DealExpress(exp);

            var sqlstr = $"select * from {TableName}";

            if (!string.IsNullOrWhiteSpace(whereStr))
            {
                sqlstr += $" where {whereStr}";
            }



            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = sqliteConn;
                sqliteConn.Open();
                cmd.CommandText = sqlstr;
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);
            }
            sqliteConn.Close();
            return dt;
        }
    }
}
