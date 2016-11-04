using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.Collections.Generic;

namespace BountteWebAPIServices.Models
{
    public class DataAccess : IDataAccess
    {
        string Constr = ConfigurationManager.ConnectionStrings["Bountte"].ConnectionString;
        public DataTable getDataTable(string sql, List<SqlParameter> spt)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Constr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                if(spt != null)
                {
                    foreach(SqlParameter param in spt)
                    {
                        cmd.Parameters.Add(param);
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

               
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        public object getScalar(string sql)
        {
            object userObje = new object();
            SqlConnection sCon = new SqlConnection(Constr);
            try
            {
                sCon.Open();
                SqlCommand cmd = new SqlCommand(sql, sCon);
                userObje = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sCon.Close();
            }

            return userObje;
        }

        public int insertUpdateDelete(string sql, List<SqlParameter> spList)
        {
            SqlConnection con = new SqlConnection(Constr);
            int numRows=0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                if(spList != null)
                {
                    foreach(SqlParameter param in spList)
                    {
                        cmd.Parameters.Add(param);
                    }

                    numRows = cmd.ExecuteNonQuery();
                }
               
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return numRows;
        }



        //test table to be removed later
        public DataTable testDataTable(string sql)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Constr);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}