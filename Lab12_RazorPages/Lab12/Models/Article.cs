using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lab12.Models
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

        public string? imagePath { get; set; } = null;
        //imagePath is image's unique name
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [NotMapped]
        public string Image =>
            imagePath is null ? $"/upload/default.png" : $"/upload/{imagePath}";

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? FormFile { get; set; }
    }
}
