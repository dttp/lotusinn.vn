using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Lotusinn.Service;
using LotusInn.Core;
using LotusInn.Model;

namespace LotusInn.Web.APIControllers
{
    public class AlbumController : BaseApiController
    {
        [AcceptVerbs("POST")]
        public PhotoAlbum Insert(PhotoAlbum album)
        {
            var svc = new AlbumService();
            return svc.Insert(album);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(PhotoAlbum album)
        {
            var svc = new AlbumService();
            svc.Update(album);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("GET")]
        public PhotoAlbum GetById(string id)
        {
            var svc = new AlbumService();
            return svc.GetById(id);
        }

        [AcceptVerbs("GET")]
        public List<PhotoAlbum> GetByHouseId(string houseId)
        {
            var svc = new AlbumService();
            return svc.GetByHouseId(houseId);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new AlbumService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SetThumbnail(string albumId)
        {
            var svc = new AlbumService();
            svc.SetThumbnail(albumId, HttpContext.Current.Request.Files[0]);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UploadImages(string albumId)
        {
            var svc = new AlbumService();
            svc.UploadImages(albumId, HttpContext.Current.Request.Files);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateImage(ImageItem image)
        {
            var svc = new AlbumService();
            svc.UpdateImage(image);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage RemoveImage(string albumId, string imageId)
        {
            var svc = new AlbumService();
            svc.RemoveImage(albumId, imageId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
