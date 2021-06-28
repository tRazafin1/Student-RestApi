using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_RestApi.Models
{
    public class Student
    {
        public Student()
        {
            this.name = "";
            this.details = "";
        }
        public int id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
    }
}
