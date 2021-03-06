﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class AuthenticationDetails
    {
        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }

        [Required(ErrorMessage="Please enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool LoginFailed { get; set; }
    }
}