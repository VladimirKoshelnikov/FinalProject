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

            Help();

            while (true)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "HELP":
                        Help();
                        break;
                    case "SHOWDIALOGS":
                        ShowAllDialogs();
                        break;
                    case "OPENDIALOG":
                        OpenDialog();
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
            Console.WriteLine("Список доступных команд в панели информации социальной сети:");
            Console.WriteLine();
            Console.WriteLine("\tПомощь в использовании \"Help\"");
            Console.WriteLine("\tПосмотреть активные переписки\"ShowDialogs\"");
            Console.WriteLine("\tОткрыть переписку с пользователем \"OpenDialog\"");

            Console.WriteLine("\tВернуться на главную \"Undo\"");
            Console.WriteLine();


        }

        public void OpenDialog()
        {

            
            Console.WriteLine("Введите почту пользователя");
            try
            {
                string friendEmail = Console.ReadLine();
                User friend = userService.FindByEmail(friendEmail);
                Program.dialogPage.Show(user, friend);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка в поиске диалога пользователем");
            }

        }

        public void ShowAllDialogs()
        { 
            var dialogsEmail = messageService.GetIncomingMessagesByUserId(user.Id).Select(m => m.SenderEmail).Distinct().ToList();
            
            if (dialogsEmail.Count == 0)
            {
                Console.WriteLine("У вас нет активных переписок");
            }
            else
            {
                Console.WriteLine("Список адресов с которыми у вас есть диалог:");
                foreach (string email in dialogsEmail)
                {
                    Console.WriteLine(email);
                }
            }
            Console.WriteLine();
        }

        public ConversationPage(MessageService _messageSevice, UserService _userService, FriendService _friendService)
        {
            messageService = _messageSevice;
            userService = _userService;
            friendService = _friendService;
        }
    }
}
