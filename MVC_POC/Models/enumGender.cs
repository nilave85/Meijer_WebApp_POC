using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_POC.Models
{
    public enum GenderEnum
    {
        [Display(Name="Male")]
        MALE,
        [Display(Name = "Female")]
        FEMALE
    };
}