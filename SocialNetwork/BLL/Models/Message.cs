using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderEmail { get; }
        public string RecipientEmail { get; }
        public long DateTimeSend {  get; set; }
        


        public Message(
            int id,
            string content,
            string senderEmail,
            string recipientEmail,
            long datetime) 
        {
            Id = id;
            Content = content;
            SenderEmail = senderEmail;
            RecipientEmail = recipientEmail;
            DateTimeSend = datetime;
        }
    }
}
