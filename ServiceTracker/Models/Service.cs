using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTracker.Models
{
    public class Service
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string Name { get; set; }

        public ICollection<BusinessService> BusinessServiceses { get; set; }
    }
}
