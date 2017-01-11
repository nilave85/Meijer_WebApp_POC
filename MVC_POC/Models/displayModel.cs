using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_POC.Models
{
    public class displayModel
    {

        private int id;        
        private string firstName;
        private string lastName;
        private Int16 age;
        private DateTime dob;
        private string address;
        private string country;
        private string state;
        private GenderEnum gender;

        public displayModel(int id,string firstName,string lastName,Int16 age,DateTime dob,string address,string country,string state,string gender)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Dob = dob;
            this.Address = address;
            this.Country = country;
            this.State = state;
            this.Gender =(GenderEnum) Enum.Parse(typeof(GenderEnum), gender);
            
        }

        public displayModel()
        {
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

       
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


    
        public Int16 Age
        {
            get { return age; }
            set { age = value; }
        }

    
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        
        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        
        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }


    }
}