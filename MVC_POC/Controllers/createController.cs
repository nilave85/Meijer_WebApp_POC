using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_POC.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;




namespace MVC_POC.Controllers
{
    public class createController : Controller
    {
        //
        // GET: /create/
        

        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult addDetails(createModel addModel,FormCollection fc)
        {

            if (ModelState.IsValid == false)
            {
                createModel objcountrymodel = new createModel();
                objcountrymodel.Country = new List<Country>();
                objcountrymodel.Country = GetAllState();
                return View(objcountrymodel);

            //    return View();
            }
            else
            {
                //addModel.SelectedCntry=Convert.ToString(fc[6]);
                addModel.SelectedState=Convert.ToString(fc[7]);

                this.DBOperations(addModel, 4);
            ModelState.Clear();
            createModel objcountrymodel = new createModel();
            objcountrymodel.Country = new List<Country>();
            objcountrymodel.Country = GetAllState();
            return View(objcountrymodel);
            }
            

        }


        [System.Web.Mvc.HttpGet]
        public ActionResult addDetails()
        {
            createModel objcountrymodel = new createModel();
            objcountrymodel.Country = new List<Country>();
            objcountrymodel.Country = GetAllState();
            return View(objcountrymodel);

          // return View();
        }


        private string GetConnection()
        {
            string connStr = null;
            connStr = ConfigurationManager.AppSettings["DB-Connection"];
            return connStr;
        }


        private int DBOperations(createModel modal,int operations)
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
                    sqlcmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value =modal.LastName;
                    sqlcmd.Parameters.Add("@age", SqlDbType.SmallInt).Value = modal.Age;
                    sqlcmd.Parameters.Add("@dob", SqlDbType.Date).Value = modal.Dob;
                    sqlcmd.Parameters.Add("@address", SqlDbType.VarChar).Value = modal.Address;
                    sqlcmd.Parameters.Add("@country", SqlDbType.VarChar).Value = modal.SelectedCntry;
                    sqlcmd.Parameters.Add("@state", SqlDbType.VarChar).Value = modal.SelectedState;
                    sqlcmd.Parameters.Add("@gender", SqlDbType.Char).Value = modal.Gender;
                    sqlcmd.Parameters.Add("@operations", SqlDbType.Int).Value = operations;
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

            return result;
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
            objcity.Add(new State { Id ="State1-1", countryId = "Country 1", stateName = "State1-1" });
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