using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab09.ViewModels;

namespace Lab09.ArticlesContext
{
    public interface IArticlesContext
    {
        IEnumerable<Article> GetArticles();
        Article GetArticle(int id);
        void AddArticle(Article article);
        void RemoveArticle(int id);
        void UpdateArticle(Article article);
    }
}
