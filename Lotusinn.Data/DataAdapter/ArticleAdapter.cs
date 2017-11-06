using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data.DataAdapter
{
    public class ArticleAdapter : BaseAdapter
    {
        private const string SP_ARTICLE_GETBYID = "ArticleGetById";
        private const string SP_ARTICLE_GETBYNAME = "ArticleGetByName";
        private const string SP_ARTICLE_INSERT = "ArticleInsert";
        private const string SP_ARTICLE_UPDATE = "ArticleUpdate";
        private const string SP_ARTICLE_DELETE = "ArticleDelete";

        public Article GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id)
            };
            return Call(SP_ARTICLE_GETBYID, param, DataReader.ReadArticles).FirstOrDefault();
        }

        public Article GetByName(string name)
        {
            var param = new[]
            {
                new SqlParameter("@name", name)
            };
            return Call(SP_ARTICLE_GETBYNAME, param, DataReader.ReadArticles).FirstOrDefault();
        }

        public Article Insert(Article article)
        {
            article.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", article.Id),
                new SqlParameter("@name", article.Name),
                new SqlParameter("@title", article.Title),
                new SqlParameter("@description", article.Description),
                new SqlParameter("@content", article.Content),
            };

            Call(SP_ARTICLE_INSERT, param);
            return article;
        }

        public void Update(Article article)
        {
            var param = new[]
            {
                new SqlParameter("@id", article.Id),
                new SqlParameter("@name", article.Name ?? string.Empty),
                new SqlParameter("@title", article.Title ?? string.Empty),
                new SqlParameter("@description", article.Description ?? string.Empty),
                new SqlParameter("@content", article.Content ?? string.Empty),
            };
            Call(SP_ARTICLE_UPDATE, param);
        }


        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id)
            };
            Call(SP_ARTICLE_DELETE, param);
        }
    }
}
