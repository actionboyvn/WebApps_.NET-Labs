using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab09.ViewModels
{
    public enum Category { Food, Drink, Chemical, Tool}
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Article's name")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive numbers allowed.")]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        public Category Category { get; set; }

        public Article() { }
        public Article(int id, string name, double price, DateTime expiryDate, Category category)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.ExpiryDate = expiryDate;
            this.Category = category;
        }
    }
}
