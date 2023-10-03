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


        public void SendMessage()
        {
            Console.WriteLine("Введите текст сообщения");
            string content = Console.ReadLine();
            Message message = new Message(content, user.Email, friend.Email, DateTime.Now.Ticks);

            messageService.SendMessage(message);
            Console.WriteLine("Сообщение отправлено");
        }

        public void GetFullDialog()
        {
            var dialog = messageService.GetFullConversation(user.Id, friend.Id);

            foreach (Message message in dialog)
            {
                DateTime dateTime = new DateTime(message.DateTimeSend);
                Console.WriteLine($"Сообщение от {message.SenderEmail} к {message.RecipientEmail} от {dateTime.ToLongDateString()} {dateTime.ToLongTimeString()}");
                Console.WriteLine($"\t {message.Content}");
                Console.WriteLine();
            }

        }


        public void Show(User _user, User _friend)
        {
            user = _user;
            friend = _friend;

            Help();

            while (true)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "HELP":
                        Help();
                        break;
                    case "GETFULLDIALOG":
                        GetFullDialog();
                        break;
                    case "SENDMESSAGE":
                        SendMessage();
                        break;
                    case "UNDO":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }

        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine($"Вы назодитесь в окне диалога с вашим другом {friend.LastName} {friend.FirstName}") ;
            Console.WriteLine();
            Console.WriteLine("\tПомощь в использовании \"Help\"");
            Console.WriteLine("\tПосмотреть всю переписку \"GetFullDialog\"");
            Console.WriteLine("\tОтправить сообщение \"SendMessage\"");

            Console.WriteLine("\tВернуться на главную \"Undo\"");
            Console.WriteLine();

        }

        public DialogPage(MessageService _messageService)
        {
            messageService = _messageService;
        }
    }
}
