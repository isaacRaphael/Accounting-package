using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingPackage.Models
{
    public class TransRegViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Credit { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
