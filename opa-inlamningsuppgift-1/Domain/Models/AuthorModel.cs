using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }

        public AuthorModel(string firstName, string lastName, string category)
        {
            FirstName = firstName;
            LastName = lastName;
            Category = category;
        }
    }
}
