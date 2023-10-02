using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Interfaces;
using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.ConsolePresentation
{
    public class ConversationPage : IHelper, IConversationPage
    {
        private MessageService messageService;
        private UserService userService;
        private FriendService friendService;
        private User user;

        public void Show(User _user)
        {
            user = _user;
        }
        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Список доступных команд в панели информации социальной сети:");
            Console.WriteLine();
            Console.WriteLine("\tПомощь в использовании \"Help\"");
            Console.WriteLine("\tПосмотреть информацию о моем профиле \"ShowDialogs\"");
            Console.WriteLine("\tРедактировать мой профиль \"OpenDialog\"");

            Console.WriteLine("\tВернуться на главную \"Undo\"");
            Console.WriteLine();


        }

        public void OpenDialog(string friendEmail)
        {
            int friendId = userService.GetUserIdByEmail(friendEmail);

            var dialog = messageService.GetFullConversation(user.Id, friendId);
            
            foreach (Message message in dialog)
            {
                DateTime dateTime = new DateTime();
                dateTime.AddTicks(message.DateTimeSend);

                Console.WriteLine($"Сообщение от {message.SenderEmail} к {message.RecipientEmail} от {dateTime.ToLongDateString}");
            }

        }

        public void ShowAllDialogs()
        { 
            var dialogsEmail = messageService.GetIncomingMessagesByUserId(user.Id).Select(m => m.SenderEmail).Distinct().ToList();
            Console.WriteLine("Список адресов с которыми у вас есть диалог:");
            foreach(string email in dialogsEmail)
            {
                Console.WriteLine(email);
            }
            
        
        }

        public ConversationPage(MessageService _messageSevice, UserService _userService, FriendService _friendService)
        {
            messageService = _messageSevice;
            userService = _userService;
            friendService = _friendService;
        }
    }
}
