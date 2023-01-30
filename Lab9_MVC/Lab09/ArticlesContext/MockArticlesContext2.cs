using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab09.ArticlesContext;
using Lab09.ViewModels;


namespace Lab09.ArticlesContext2
{
    public class MockArticlesContext2: IArticlesContext
    {
        Dictionary<int, Article> articles = new Dictionary<int, Article>()
        {
            {0, new Article(0, "Burger", 25.5, new DateTime(2023, 7, 15), Category.Food) },
            {1, new Article(1, "Cola", 5.99, new DateTime(2025, 7, 15), Category.Drink) } 
        };
        public void AddArticle(Article article)
        {
            int nextId = 0;
            if (articles.Count > 0)
                nextId = articles.Max(a => a.Key) + 1;
            article.Id = nextId;
            articles.Add(nextId, article);
        }
        public Article GetArticle(int id)
        {                        
            return articles[id];
        }

        public IEnumerable<Article> GetArticles()
        {
            return articles.Values.ToList();
        }

        public void RemoveArticle(int id)
        {            
            articles.Remove(id);
        }

        public void UpdateArticle(Article article)
        {
            articles[article.Id] = article;                        
        }
    }
}
