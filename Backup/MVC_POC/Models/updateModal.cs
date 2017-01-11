using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_POC.Models
{
    public class updateModal
    {

        private List<string> firstName;
        private List<string> lastName;

        private int id;        
        private string fstName;
        private string lstName;
        private Int16 age;
        private DateTime dob;
        private string address;
        private string country;
        private string state;
        private GenderEnum gender;

        public updateModal(int id,string firstName,string lastName,Int16 age,DateTime dob,string address,string country,string state,string gender)
        {
            this.Id = id;
            this.fstName = firstName;
            this.lstName = lastName;
            this.Age = age;
            this.Dob = dob;
            this.Address = address;
            this.Country = country;
            this.State = state;
            this.Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), gender);
        }

        public updateModal()
        {
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        
        public string FstName
        {
            get { return fstName; }
            set { fstName = value; }
        }

       
        public string LstName
        {
            get { return lstName; }
            set { lstName = value; }
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


        public List<string> LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public List<string> FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
    }
}