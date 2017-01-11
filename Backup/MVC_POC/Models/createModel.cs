using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC_POC.Models;
using System.Web.Mvc;


namespace MVC_POC.Models
{
    public class createModel
    {
        private string firstName;
        private string lastName;
        private Int16 age;
        private DateTime dob;
        private string address;
        private List<Country> country;
        private SelectList state;
        private GenderEnum gender;
        private Int32 id;
        private string selectedCntry;


        [Required(ErrorMessage = "Country cannot be empty")]
        public string SelectedCntry
        {
            get { return selectedCntry; }
            set { selectedCntry = value; }
        }
        private string selectedState;

        public string SelectedState
        {
            get { return selectedState; }
            set { selectedState = value; }
        }

        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }
        
        [Display(Name="First Name:-")]
        [Required(ErrorMessage="First Name cannot be empty")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }


        [Display(Name = "Last Name:-")]
        [Required(ErrorMessage = "Last Name cannot be empty")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


        [Display(Name = "Age:-")]
        [Required(ErrorMessage = "Age cannot be empty")]
        [Range(18,50)]
        public Int16 Age
        {
            get { return age; }
            set { age = value; }
        }

        [Display(Name = "DOB:-")]
        [Required(ErrorMessage = "DOB cannot be empty")]
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        [Display(Name = "Address:-")]
        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        [Display(Name = "Country:-")]
       // [Required(ErrorMessage = "Country cannot be empty")]
        public List<Country> Country
        {
            get { return country; }
            set { country = value; }
        }

        [Display(Name = "State:-")]
       // [Required(ErrorMessage = "State cannot be empty")]
        public SelectList State
        {
            get { return state; }
            set { state = value; }
        }

        [Display(Name = "Gender:-")]
        [Required(ErrorMessage = "Gender cannot be empty")]
        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

    }


    public class Country
    {
        public string Id { get; set; }
        public string countryName { get; set; }
    }
    public class State
    {
        public string Id { get; set; }
        public string countryId { get; set; }
        public string stateName { get; set; }
    }
}