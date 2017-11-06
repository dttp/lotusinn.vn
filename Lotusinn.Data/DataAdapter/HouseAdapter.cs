using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LotusInn.Core;
using LotusInn.Model;

namespace Lotusinn.Data.DataAdapter
{
    public class HouseAdapter : BaseAdapter
    {
        private const string SP_HOUSE_GETALL = "HouseSelect";
        private const string SP_HOUSE_GETBYID = "HouseGetById";
        private const string SP_HOUSE_INSERT = "HouseInsert";
        private const string SP_HOUSE_UPDATE = "HouseUpdate";
        private const string SP_HOUSE_DELETE = "HouseDelete";

        public List<House> GetAll()
        {
            var list = Call(SP_HOUSE_GETALL, null, DataReader.ReadHouses);

            var articleAdapter = new ArticleAdapter();
            foreach (var house in list)
            {
                house.Article = articleAdapter.GetById(house.Article.Id);
            }

            return list;
        }

        public House GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id)
            };

            var house = Call(SP_HOUSE_GETBYID, param, DataReader.ReadHouses).FirstOrDefault();
            if (house != null)
            {
                house.Article = new ArticleAdapter().GetById(house.Article.Id);
            }
            return house;
        }

        public House Insert(House house)
        {
            house.Id = IdHelper.Generate();

            var article = new Article
            {
                Name = house.Id,
                Description = "Article for house " + house.Name,
                Content = string.Empty,
                Title = "Article for house " + house.Name
            };

            var articleAdapter = new ArticleAdapter();
            article = articleAdapter.Insert(article);

            house.Article = article;

            var param = new[]
            {
                new SqlParameter("@id", house.Id),
                new SqlParameter("@name", house.Name),
                new SqlParameter("@address", house.Address),
                new SqlParameter("@latitude", house.Latitude),
                new SqlParameter("@longitude", house.Longitude),
                new SqlParameter("@order", house.Order),
                new SqlParameter("@articleId", house.Article.Id),
                new SqlParameter("@thumbnail", house.Thumbnail),
            };

            Call(SP_HOUSE_INSERT, param);
            return house;
        }

        public void Update(House house)
        {
            var param = new[]
            {
                new SqlParameter("@id", house.Id),
                new SqlParameter("@name", house.Name),
                new SqlParameter("@address", house.Address),
                new SqlParameter("@latitude", house.Latitude),
                new SqlParameter("@longitude", house.Longitude),
                new SqlParameter("@order", house.Order),
                new SqlParameter("@thumbnail", house.Thumbnail),
            };

            Call(SP_HOUSE_UPDATE, param);

            var articleAdapter = new ArticleAdapter();
            articleAdapter.Update(house.Article);
        }

        public void Delete(string id)
        {            
            var param = new[]
            {
                new SqlParameter("@id", id)
            };
            Call(SP_HOUSE_DELETE, param);
        }
    }
}
