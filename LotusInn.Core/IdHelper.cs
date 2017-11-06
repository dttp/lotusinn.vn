using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HashidsNet;

namespace LotusInn.Core
{
    public class IdHelper
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJJKLMNOPQRSTUVWXYZ0123456789";        
        public static string Generate(int maxSize = 20)
        {            
            var chars = Alphabet.ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            var size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);                 
            }
            return result.ToString();
        }

        public static string Encode(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var hashIds = new Hashids("UCMS");
            return hashIds.Encode(bytes.Select(b => (int)b));
        }

        public static string Decode(string encodedText)
        {
            var hashIds = new Hashids("UCMS");
            var intArr = hashIds.Decode(encodedText);
            return Encoding.UTF8.GetString(intArr.Select(i => (byte)i).ToArray());
        }
    }
}
