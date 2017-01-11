using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using MVC_POC.Models;
using System.Configuration;

namespace MVC_POC.Controllers
{
    public class displayController : Controller
    {
        //
        // GET: /display/

        public ActionResult Display()
        {
            displayModel mdl = new displayModel();
            DataTable dtl = new DataTable("Data");
            this.DBOperations(mdl, 1,ref dtl);
            List<displayModel> enuMdl = new List<displayModel>();
            foreach (DataRow dr in dtl.Rows)
            {
                enuMdl.Add(new displayModel(
                                               Convert.ToInt32(dr[0]),
                                               dr[1].ToString(),
                                               dr[2].ToString(),
                                               Convert.ToInt16(dr[3]),
                                               Convert.ToDateTime(dr[4]),
                                               dr[5].ToString(),
                                               dr[6].ToString(),
                                               dr[7].ToString(),
                                               (dr[8].ToString())
                                             )
                           );
            }            
            return View(enuMdl);
        }                                     
             

        private string GetConnection()
        {
            string connStr = null;
            connStr = ConfigurationManager.AppSettings["DB-Connection"];
            return connStr;
        }


        private int DBOperations(displayModel modal, int operations,ref DataTable dtl)
        {

            int result = 0;
            using (SqlConnection sqlcon = new SqlConnection(this.GetConnection()))
            {

                try
                {
                    SqlCommand sqlcmd = new SqlCommand("operations", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                    sqlcmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = modal.FirstName;
                    sqlcmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = modal.LastName;
                    sqlcmd.Parameters.Add("@age", SqlDbType.SmallInt).Value = modal.Age;
                    sqlcmd.Parameters.Add("@dob", SqlDbType.Date).Value = modal.Dob;
                    sqlcmd.Parameters.Add("@address", SqlDbType.VarChar).Value = modal.Address;
                    sqlcmd.Parameters.Add("@country", SqlDbType.VarChar).Value = modal.Country;
                    sqlcmd.Parameters.Add("@state", SqlDbType.VarChar).Value = modal.State;
                    sqlcmd.Parameters.Add("@gender", SqlDbType.Char).Value = modal.Gender;
                    sqlcmd.Parameters.Add("@operations", SqlDbType.Int).Value = operations;
                    /* adding output parameters */
                    SqlParameter outResult = new SqlParameter("@result", SqlDbType.VarChar);
                    outResult.Direction = ParameterDirection.Output;
                    outResult.Size = 1;
                    sqlcmd.Parameters.Add(outResult);
                    /*----------------*/

                    sqlcon.Open();
                    if (operations == 1)
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                        sda.Fill(dtl);
                    }
                    else
                    {
                        sqlcmd.ExecuteNonQuery();
                    }
                    System.Diagnostics.Debug.WriteLine("done");
                    //result = (int)outResult.Value;
                    result = Convert.ToInt16(outResult.Value);
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("got error" + e.Message);
                }
            }

            return result;
        }
       
    }
}
