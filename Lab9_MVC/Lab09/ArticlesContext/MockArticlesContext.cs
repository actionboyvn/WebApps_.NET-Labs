using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab09.ViewModels;

namespace Lab09.ArticlesContext
{
    public class MockArticlesContext : IArticlesContext
    {
        List<Article> articles = new List<Article>()
        {
            new Article(0, "Burger", 25.5, new DateTime(2023, 7, 15), Category.Food),
            new Article(1, "Cola", 5.99, new DateTime(2025, 7, 15), Category.Drink),

        };
        public void AddArticle(Article article)
        {
            int nextNumber = 0;
            if (articles.Count > 0)
                nextNumber = articles.Max(a => a.Id) + 1;
            article.Id = nextNumber;
            articles.Add(article);
        }

        public Article GetArticle(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Article> GetArticles()
        {           
            return articles;
        }

        public void RemoveArticle(int id)
        {
            Article articleToRemove = articles.FirstOrDefault(a => a.Id == id);
            if (articleToRemove != null)
                articles.Remove(articleToRemove);
        }

        public void UpdateArticle(Article article)
        {
            Article articleToUpdate = articles.FirstOrDefault(a => a.Id == article.Id);
            articles = articles.Select(a => (a.Id == article.Id) ? article : a).ToList();
        }
    }
}
