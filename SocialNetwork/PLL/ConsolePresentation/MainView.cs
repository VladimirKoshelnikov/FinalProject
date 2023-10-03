using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.ConsolePresentation
{
    public class MainView : IHelper , IMainView
    {
        public UserService userService;
        public MessageService messageService;
        public User? user;


        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Список доступных команд в панели информации социальной сети:");
            Console.WriteLine();
            Console.WriteLine("\tПомощь в использовании \"Help\"");
            Console.WriteLine("\tПосмотреть информацию о моем профиле \"ShowMyInfo\"");
            Console.WriteLine("\tРедактировать мой профиль \"ChangeMyInfo\"");
            Console.WriteLine("\tПерейти на вкладку Мои друзья \"MyFriends\"");
            Console.WriteLine("\tПерейти на вкладку Диалоги \"MyConversations\"");
            Console.WriteLine("\tВыйти из аккаунта \"Logout\"");
            Console.WriteLine();
        }

        public void ShowMyInfo()
        {
            Console.WriteLine("Информация обо мне:");
            Console.WriteLine();
            Console.WriteLine($"\tИмя: {user.FirstName}");
            Console.WriteLine($"\tФамилия: {user.LastName}");
            Console.WriteLine($"\tАдрес электронной почты: {user.Email}");
            Console.WriteLine($"\tСсылка на фотографию: {user.Photo}");
            Console.WriteLine($"\tЛюбимый фильм: {user.FavouriteMovie}");
            Console.WriteLine($"\tЛюбимая книга: {user.FavouriteBook}");

        }

        public void Logout()
        {
            userService = null;
            user = null;
        }
        
        public MainView(UserService _userService)
        {
            userService = _userService;
        }

        public void Show(User _user)
        {
            user = _user;

            Console.WriteLine("Добро пожаловать в социальную сеть имени меня");
            Help();
            
            while (true)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "HELP":
                        Help();
                        break;
                    case "SHOWMYINFO":
                        ShowMyInfo();
                        break;
                    case "CHANGEMYINFO":
                        Program.changeInfoView.Show(ref user);
                        break;
                    case "MYFRIENDS":
                        Program.friendView.Show(user);
                        break;
                    case "MYCONVERSATIONS":
                        Program.conversationView.Show(user);
                        break;
                    case "LOGOUT":
                        Logout();
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        } 
    }
}
