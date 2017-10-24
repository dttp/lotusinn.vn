using System.IO;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace LotusInn.Web.APIControllers
{
    public class BaseApiController : ApiController
    {
        public TResult GetData<TResult>(string fileName)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data", fileName);
            if (!File.Exists(filePath)) return default(TResult);
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var streamReader = new StreamReader(fs))
                {
                    var jsonSerializer = new JsonSerializer();
                    return (TResult) jsonSerializer.Deserialize(streamReader, typeof (TResult));
                }
            }
        }

        public void SaveData<TInput>(string fileName, TInput input)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data", fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var streamWriter = new StreamWriter(fs))
                {
                    var jsonSerializer = new JsonSerializer {Formatting = Formatting.Indented};
                    jsonSerializer.Serialize(streamWriter, input);
                }
            }
        }

        public string LoadTemplate(string templateName)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"app\data\templates", templateName);
            return File.ReadAllText(filePath);
        }
    }
}