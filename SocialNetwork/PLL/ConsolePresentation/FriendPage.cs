using SocialNetwork.BLL.Exceptions;
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
    public class FriendPage : IHelper, IFriendPage

    {
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
                    case "GETMYFRIENDS":
                        GetMyFriends();
                        break;
                    case "ADDFRIEND":
                        AddFriend();
                        break;
                    case "DELETEFRIEND":
                        DeleteFriend();
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
            Console.WriteLine("Навигационная панель вкладки \"Друзья\"");
            Console.WriteLine();

            Console.WriteLine("");
            Console.WriteLine("Показать весь список друзей \"GetMyFriends\"");
            Console.WriteLine("Добавить пользователя в друзья \"AddFriend\"");
            Console.WriteLine("Удалить пользователя из друзей \"DeleteFriend\"");
            Console.WriteLine("Вызвать меню помощи \"Help\"");
            Console.WriteLine("Перейти на главную страницу \"Undo\"");
            Console.WriteLine("");
        }

        public void AddFriend()
        {
            Console.WriteLine("Введите Email друга");
            string email = Console.ReadLine();

            UserAddingFriendData userAddingFriendData = new UserAddingFriendData()
            {
                UserId = user.Id,
                FriendEmail = email
            };
            try
            {
                friendService.AddFriend(userAddingFriendData);
                Console.WriteLine($"Пользователь с адресом {email} добавлен в список ваших друзей");
            }
            catch (UserNotFoundException ex) {
                Console.WriteLine($"Пользователь с адресом {email} не найден");
            }
        }

        public void GetMyFriends()
        {
            var friendList = friendService.GetFriendsByUserId(user.Id);
            if (friendList.Count() == 0)
            {
                Console.WriteLine("У вас нет друзей. Тогда мб го за пивом?");
            }
            else
            {
                Console.WriteLine("Список друзей:");
                Console.WriteLine("Email \t\t\t\t ФИО");
                foreach (User friend in friendList)
                {
                    Console.WriteLine($"{friend.Email}\t\t {friend.LastName} {friend.FirstName}");
                }
            }

            
        }

        public void DeleteFriend()
        {
            Console.WriteLine("Введите Email будущего бывшего друга");
            string email = Console.ReadLine();

            friendService.DeleteFriendByEmail(user.Id, email);
            Console.WriteLine($"Пользователь с адресом {email} удален из списка ваших друзей");
           
        }
        public FriendPage(UserService _userService, FriendService _friendService) 
        {
            userService = _userService;
            friendService = _friendService; 
        }
    }
}
