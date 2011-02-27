using System;

namespace MvcDemoApp.Models
{
    public class EditEmployeeViewModel
    {
        public string Fname { get; set; }
        public char Minit { get; set; }
        public string Lname { get; set; }
        public string Ssn { get; set; }
        public DateTime Bdate { get; set; }
        public string Address { get; set; }
        public char Sex { get; set; }
        public int Salary { get; set; }
        public string Super_ssn { get; set; }
        public int Dno { get; set; }

    }
}