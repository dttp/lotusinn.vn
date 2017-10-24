using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace LotusInn.Core.Models
{
    public class MailInfo
    {
        public List<FileAttachmentInfo> FileAttachmentInfos { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public AlternateView AlternateView { get; set; }

    }

    public class FileAttachmentInfo
    {
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}
