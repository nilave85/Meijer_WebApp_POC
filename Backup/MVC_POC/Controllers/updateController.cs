using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using MVC_POC.Models;

namespace MVC_POC.Controllers
{
    public class updateController : Controller
    {
        //
        // GET: /update/

        public ActionResult update()
        {
            this.GetLstName();
            return View();
        }

        private void GetLstName()
        {
            updateModal upmdl = new updateModal();
            upmdl.FirstName = this.GetNames("first_name");
            upmdl.LastName = this.GetNames("last_name");
            ViewBag.fstnme = new SelectList(upmdl.FirstName);
            ViewBag.lstnme = new SelectList(upmdl.LastName);
        }

        //
        // GET: /update/Details/5
        
        public ActionResult Details(FormCollection fc)
        {
            List<updateModal> mdl = new List<updateModal>();           
            string firstname = fc["fstnme"].ToString();
            string lstname = fc["lstnme"].ToString();
            this.GetData(firstname, lstname,ref mdl);
            this.GetLstName();
            //return View(mdl);
            return(View("update",mdl));
        }
       
       
        //
        // GET: /update/Edit/5
 
        public ActionResult Edit(int id)
        {
            createModel crtMdl = new createModel();
            this.GetData(id, ref crtMdl);
            

            createModel objcountrymodel = new createModel();
            objcountrymodel.Country = new List<Country>();
            objcountrymodel.Country = GetAllState();
            
            crtMdl.Country = objcountrymodel.Country;
            
            return View(crtMdl);
        }



        [HttpPost]
        public ActionResult Edit(createModel updtMdl,FormCollection fc)
        {

            //if (ModelState.IsValid == false)
            //{
            //    createModel objcountrymodel = new createModel();
            //    objcountrymodel.Country = new List<Country>();
            //    objcountrymodel.Country = GetAllState();
            //    return View(objcountrymodel);
            //}
            //else
            //{
                updtMdl.SelectedCntry = Convert.ToString(fc[7]);
                updtMdl.SelectedState = Convert.ToString(fc[9]);
                this.updData(updtMdl);
                ModelState.Clear();
                createModel objcountrymodel = new createModel();
                objcountrymodel.Country = new List<Country>();
                objcountrymodel.Country = GetAllState();
                return View(objcountrymodel);
            //}
        }


        private string GetConnection()
        {
            string connStr = null;
            connStr = ConfigurationManager.AppSettings["DB-Connection"];
            return connStr;
        }


        private List<string> GetNames(string field)
        {

            List<string> name=new List<string>();
            using (SqlConnection sqlcon = new SqlConnection(this.GetConnection()))
            {

                try
                {                   
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = CommandType.Text;
                    string sql = "select distinct " + field + " from dbo.details";
                    sqlcmd.CommandText = sql;
                    DataTable dtl = new DataTable(field);
                    sqlcon.Open();    
                    sqlcmd.Connection = sqlcon;                       
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    sda.Fill(dtl);                                       
                    sqlcmd.ExecuteNonQuery();                    
                    System.Diagnostics.Debug.WriteLine("done");
                    name = dtl.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("got error" + e.Message);
                }
            }

            return name;
        }

        private void GetData(string fstName, string lstName,ref List<updateModal> mdl)
        {

            List<displayModel> dspmdl = new List<displayModel>();            
            using (SqlConnection sqlcon = new SqlConnection(this.GetConnection()))
            {

                try
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = CommandType.Text;
                    string sql =@"select details.Id,details.first_name,details.last_name,details.age,details.dob,details.address,details.country,details.state,details.gender from dbo.details where details.first_name=@fn and details.last_name=@ln";
                    sqlcmd.CommandText = sql;
                    sqlcmd.Parameters.Add(new SqlParameter("fn", fstName));
                    sqlcmd.Parameters.Add(new SqlParameter("ln", lstName));
                    DataTable dtl = new DataTable("DataSelected");
                    sqlcon.Open();
                    sqlcmd.Connection = sqlcon;
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    sda.Fill(dtl);
                    sqlcmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("done");
                    foreach (DataRow dr in dtl.Rows)
                    {

                        mdl.Add(new updateModal(                       
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

                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("got error" + e.Message);
                }
            }           
            return ;
        }


        private void GetData(int id,ref createModel crtMdl)
        {

            List<displayModel> dspmdl = new List<displayModel>();
            using (SqlConnection sqlcon = new SqlConnection(this.GetConnection()))
            {

                try
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = CommandType.Text;
                    string sql = @"select details.Id,details.first_name,details.last_name,details.age,details.dob,details.address,details.country,details.state,details.gender from dbo.details where details.Id=@id ";
                    sqlcmd.CommandText = sql;
                    sqlcmd.Parameters.Add(new SqlParameter("id", id));                   
                    DataTable dtl = new DataTable("DataSelected");
                    sqlcon.Open();
                    sqlcmd.Connection = sqlcon;
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    sda.Fill(dtl);
                    sqlcmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("done");
                    foreach (DataRow dr in dtl.Rows)
                    {

                                  crtMdl.Id= Convert.ToInt32(dr[0]);
                                  crtMdl.FirstName= dr[1].ToString();
                                  crtMdl.LastName= dr[2].ToString();
                                  crtMdl.Age= Convert.ToInt16(dr[3]);
                                  crtMdl.Dob= Convert.ToDateTime(dr[4]);
                                  crtMdl.Address= dr[5].ToString();
                                  crtMdl.SelectedCntry= dr[6].ToString();
                                  crtMdl.SelectedState= dr[7].ToString();
                                  crtMdl.Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), dr[8].ToString());                         
                    }

                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("got error" + e.Message);
                }
            }
            return;
        }

        private void updData(createModel updtMdl)
        {

            int result;
            using (SqlConnection sqlcon = new SqlConnection(this.GetConnection()))
            {

                try
                {
                    SqlCommand sqlcmd = new SqlCommand("operations", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@Id", SqlDbType.Int).Value = updtMdl.Id;
                    sqlcmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = updtMdl.FirstName;
                    sqlcmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = updtMdl.LastName;
                    sqlcmd.Parameters.Add("@age", SqlDbType.SmallInt).Value = updtMdl.Age;
                    sqlcmd.Parameters.Add("@dob", SqlDbType.Date).Value = updtMdl.Dob;
                    sqlcmd.Parameters.Add("@address", SqlDbType.VarChar).Value = updtMdl.Address;
                    sqlcmd.Parameters.Add("@country", SqlDbType.VarChar).Value = updtMdl.SelectedCntry;
                    sqlcmd.Parameters.Add("@state", SqlDbType.VarChar).Value = updtMdl.SelectedState;
                    sqlcmd.Parameters.Add("@gender", SqlDbType.Char).Value = updtMdl.Gender;
                    sqlcmd.Parameters.Add("@operations", SqlDbType.Int).Value =3;
                    /* adding output parameters */
                    SqlParameter outResult = new SqlParameter("@result", SqlDbType.VarChar);
                    outResult.Direction = ParameterDirection.Output;
                    outResult.Size = 1;
                    sqlcmd.Parameters.Add(outResult);
                    /*----------------*/

                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("done");
                    //result = (int)outResult.Value;
                    result = Convert.ToInt16(outResult.Value);
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("got error" + e.Message);
                }
            }

           
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult GetCityByStaeId(string stateid)
        {
            List<State> objcity = new List<State>();
            objcity = GetAllCity().Where(m => m.countryId == stateid).ToList();
            SelectList obgcity = new SelectList(objcity, "Id", "stateName", 0);
            return Json(obgcity);
        }
        // Collection for state
        public List<Country> GetAllState()
        {
            List<Country> objstate = new List<Country>();
            objstate.Add(new Country { Id = "Select Country", countryName = "Select Country" });
            objstate.Add(new Country { Id = "Country 1", countryName = "Country 1" });
            objstate.Add(new Country { Id = "Country 2", countryName = "Country 2" });
            objstate.Add(new Country { Id = "Country 3", countryName = "Country 3" });
            objstate.Add(new Country { Id = "Country 4", countryName = "Country 4" });
            return objstate;
        }
        //collection for city
        public List<State> GetAllCity()
        {
            List<State> objcity = new List<State>();
            objcity.Add(new State { Id = "State1-1", countryId = "Country 1", stateName = "State1-1" });
            objcity.Add(new State { Id = "State2-1", countryId = "Country 2", stateName = "State2-1" });
            objcity.Add(new State { Id = "State4-1", countryId = "Country 4", stateName = "State4-1" });
            objcity.Add(new State { Id = "State1-2", countryId = "Country 1", stateName = "State1-2" });
            objcity.Add(new State { Id = "State1-3", countryId = "Country 1", stateName = "State1-3" });
            objcity.Add(new State { Id = "State4-2", countryId = "Country 4", stateName = "State4-2" });
            objcity.Add(new State { Id = "State3-1", countryId = "Country 3", stateName = "State3-1" });
            return objcity;
        }
    }
}
