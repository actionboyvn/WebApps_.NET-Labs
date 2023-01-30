using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lab102.ViewModels
{
    public class ArticleCreateViewModel
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

        public int? CategoryId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? FormFile { get; set; }
    }
}
