using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeSystemMVC.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public string ClassTitle { get; set; }
        public string RoomNumber { get; set; }
        public string Description { get; set; }

    }
}
