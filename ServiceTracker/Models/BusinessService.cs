using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTracker.Models
{
    public class BusinessService
    {
        public int BusinessTypeId { get; set; }
        public int ServiceId { get; set; }

        public Service Service { get; set; }
        public BusinessType BusinessType { get; set; }
    }
}
