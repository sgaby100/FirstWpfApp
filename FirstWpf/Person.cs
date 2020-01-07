using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWpf
{
    public class Person
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

       
      
        public string Fullinfo
        {
            get 
            {
                return $"{UserName} {Password}";
            }
           
        }

    }
}
