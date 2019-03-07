using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeSystemMVC.Models
{
    public class SearchTerm
    {
        [Required]
        public string SearchName { get; set; }
    }
}
