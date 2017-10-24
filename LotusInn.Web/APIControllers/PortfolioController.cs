using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Model;
using TP.Shared;

namespace LotusInn.Web.APIControllers
{
    public class PortfolioController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public Portfolio GetPortfolio()
        {
            return GetData<Portfolio>("portfolio.json");
        }
        [AcceptVerbs("POST")]
        public HttpResponseMessage SavePortfolio([FromBody] Portfolio portfolio)
        {
            SaveData("portfolio.json", portfolio);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UploadImages([FromUri]string albumId)
        {
            var uploadFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\portfolio", albumId);
            var portfolio = GetData<Portfolio>("portfolio.json");
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var httpPostedFile = HttpContext.Current.Request.Files[i];
                var item = new ImageItem()
                {
                    Id = ShortGuid.NewGuid(),
                    Name = httpPostedFile.FileName,
                    Description = "",
                    ImagePath = "/app/data/portfolio/" + albumId + "/" + httpPostedFile.FileName
                };
                var fileName = Path.Combine(uploadFolder, httpPostedFile.FileName);
                if (File.Exists(fileName)) File.Delete(fileName);
                httpPostedFile.SaveAs(fileName);
                var album = portfolio.Albums.Single(a => a.Id.Equals(albumId));
                album.Items.Add(item);
            }

            SaveData("portfolio.json", portfolio);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public PhotoAlbum CreateAlbum()
        {
            var album = new PhotoAlbum
            {
                Id = ShortGuid.NewGuid(),
                Name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"),
                Description = "Click to see more",
                Items = new List<ImageItem>()
            };
            var folder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\portfolio", album.Id);
            Directory.CreateDirectory(folder);
            var portfolio = GetData<Portfolio>("portfolio.json");
            portfolio.Albums.Add(album);
            SaveData("portfolio.json", portfolio);
            return album;
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SetThumbnail([FromUri] string albumId)
        {
            var uploadFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\portfolio", albumId);
            var portfolio = GetData<Portfolio>("portfolio.json");

            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var fileName = Path.Combine(uploadFolder, "thumb.png");
            if (File.Exists(fileName)) File.Delete(fileName);
            httpPostedFile.SaveAs(fileName);
            var album = portfolio.Albums.Single(a => a.Id.Equals(albumId));
            album.ThumbnailImage = "/app/data/portfolio/" + albumId + "/thumb.png";

            SaveData("portfolio.json", portfolio);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
