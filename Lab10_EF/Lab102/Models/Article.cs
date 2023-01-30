using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab102.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 999999)]
        public float Price { get; set; }

        [Display(Name = "Image")]
        public string imagePath { get; set; } 

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
