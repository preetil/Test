using System;
using System.ComponentModel.DataAnnotations;

namespace MvcDemoApp.Models
{
    public class EditEmployeeViewModel
    {

        [Required(ErrorMessage = "First name cannot be null")]
        public string Fname { get; set; }
        
        public char Minit { get; set; }
        [Required(ErrorMessage = "LastName cannot be null")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "SSN is a required field")]
        [RegularExpression(@"^(?!000)([0-6]\d{2}|7([0-6]\d|7[012]))([ -]?)(?!00)\d\d\3(?!0000)\d{4}$")]
        public string Ssn { get; set; }
        public DateTime Bdate { get; set; }
        public string Address { get; set; }
        public char Sex { get; set; }
        public int Salary { get; set; }

        public string Super_ssn { get; set; }
        [Range(1,1000, ErrorMessage = "Department No should be in range 1- 1000")]
        public int Dno { get; set; }

    }
}