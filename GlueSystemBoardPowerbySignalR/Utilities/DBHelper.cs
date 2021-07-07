using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GlueSystemBoardPowerbySignalR.Utilities
{
    public static class DbHelper
    {
        //public static string connectionString = "server=192.168.1.20;database=atedatabase;uid=sa;pwd=mestest";
        public static string connectionString = "server=192.168.1.10;database=atedatabase;uid=sa;pwd=Atetest";

        /// <summary>执行不带参数的增删改SQL语句或存储过程 ，返回受影响的行数</summary>
        public static int ExecuteNonQuery(string cmdText)
        {
            int res = 0;//受影响的行数
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        res = cmd.ExecuteNonQuery();//执行Sql语句并受影响的行数
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return res;
        }

        /// <summary>  执行带参数的增删改SQL语句或存储过程，返回受影响的行数</summary>
        public static int ExecuteNonQuery(string cmdText, params object[] paras)
        {
            int res = 0;//受影响的行数
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddRange(paras);//添加参数数组
                        if (paras != null)
                        {
                            for (int i = 0; i < paras.Length; i++)
                            {
                                cmd.Parameters.AddWithValue("@" + i, paras[i]);
                            }
                        }
                        res = cmd.ExecuteNonQuery();//执行Sql语句并受影响的行数
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return res;
        }

        /// <summary> 执行不带参数的查询SQL语句或存储过程，返回DataTable对象</summary>
        public static DataTable ExecuteQueryDataTable(string cmdText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            dt.Load(sdr);
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return dt;
        }

        /// <summary> 执行带参数的查询SQL语句或存储过程，返回DataTable对象</summary>
        public static DataTable ExecuteQueryDataTable(string cmdText, params object[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddRange(paras);
                        if (paras != null)
                        {
                            for (int i = 0; i < paras.Length; i++)
                            {
                                cmd.Parameters.AddWithValue("@" + i, paras[i]);
                            }
                        }
                        using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            dt.Load(sdr);
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return dt;
        }

        /// <summary> 执行不带参数的查询SQL语句或存储过程，返回DataSet对象</summary>
        public static DataSet ExecuteQueryDataSet(string cmdText)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds, "ds");
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return ds;
        }

        /// <summary> 执行带参数的查询SQL语句或存储过程，返回DataSet对象</summary>
        public static DataSet ExecuteQueryDataSet(string cmdText, params object[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();//打开数据库链接
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddRange(paras);
                        if (paras != null)
                        {
                            for (int i = 0; i < paras.Length; i++)
                            {
                                cmd.Parameters.AddWithValue("@" + i, paras[i]);
                            }
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds, "ds");
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)//判断连接是否处于打开状态
                    {
                        conn.Close();//关闭与数据库的链接
                    }
                }
            }
            return ds;
        }

        /// <summary>查询数据是否存在</summary>
        public static bool ExecuteDataIsExistByData(string sqlStr)
        {
            bool iss = false;
            DataSet ds = ExecuteQueryDataSet(sqlStr);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].Rows.Count > 0) iss = true;
            }
            return iss;
        }

        /// <summary>查询数据是否存在 </summary>
        public static bool ExecuteDataIsExistByData(string sqlStr, params object[] paras)
        {
            bool iss = false;
            DataSet ds = ExecuteQueryDataSet(sqlStr, paras);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].Rows.Count > 0) iss = true;
            }
            return iss;
        }

        /// <summary>查询增删改数据操作是否成功 </summary>
        public static bool ExecuteDataIsExistByInt(string sqlStr)
        {
            int ds = ExecuteNonQuery(sqlStr);
            bool iss = ds > 0 ? true : false;
            return iss;
        }

        /// <summary>查询增删改数据操作是否成功 </summary>
        public static bool ExecuteDataIsExistByInt(string sqlStr, params object[] paras)
        {
            int ds = ExecuteNonQuery(sqlStr, paras);
            bool iss = ds > 0 ? true : false;
            return iss;
        }
    }
}