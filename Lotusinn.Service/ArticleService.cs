using Lotusinn.Data.DataAdapter;
using LotusInn.Model;

namespace Lotusinn.Service
{
    public class ArticleService
    {
        public Article GetById(string id)
        {
            var articleAdapter = new ArticleAdapter();
            return articleAdapter.GetById(id);
        }

        public Article GetByName(string name)
        {
            var articleAdapter = new ArticleAdapter();
            return articleAdapter.GetByName(name);
        }

        public void Update(Article article)
        {
            var articleAdapter = new ArticleAdapter();
            articleAdapter.Update(article);
        }

        public Article Insert(Article article)
        {
            var articleAdapter = new ArticleAdapter();
            return articleAdapter.Insert(article);
        }

        public void Delete(string id)
        {
            var articleAdapter = new ArticleAdapter();
            articleAdapter.Delete(id);
        }
    }
}
