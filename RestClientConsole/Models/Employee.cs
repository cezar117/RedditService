using System;
using System.Collections.Generic;

#nullable disable

namespace RestClientConsole.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rfc { get; set; }
        public DateTime BornDate { get; set; }
        public int Status { get; set; }
    }
}
