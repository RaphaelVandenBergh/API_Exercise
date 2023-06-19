using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Exercise.Domain.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<User>? Users { get; set; }
    }
}
