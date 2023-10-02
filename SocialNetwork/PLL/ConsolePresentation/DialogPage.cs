using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.ConsolePresentation
{
    public class DialogPage : IHelper, IDialogPage
    {
        User user;
        User friend;
        MessageService messageService;


        public void SendMessage(string content)
        {
            MessageSendingData messageSendingData = new MessageSendingData();
            messageSendingData.Content = content;
            messageSendingData.SenderId = user.Id;
            messageSendingData.RecipientEmail = friend.Email;
        }

        public void Show(User _user, User _friend)
        {
            user = _user;
            friend = _friend;

            var dialog = messageService.GetFullConversation(user.Id, friend.Id);

            foreach (Message message in dialog)
            {
                DateTime dateTime = new DateTime();
                dateTime.AddTicks(message.DateTimeSend);

                Console.WriteLine($"Сообщение от {message.SenderEmail} к {message.RecipientEmail} от {dateTime.ToLongDateString}");
            }

        }

        public void Help()
        {
            throw new NotImplementedException();
        }

        public DialogPage(MessageService _messageService)
        {
            messageService = _messageService;
        }
    }
}
