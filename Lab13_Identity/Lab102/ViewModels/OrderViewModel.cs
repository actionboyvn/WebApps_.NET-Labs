
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lab102.Models;

namespace Lab102.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public IEnumerable<Article> Products { get; set; }
        [Required]
        public Dictionary<int, int> Quantities { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
