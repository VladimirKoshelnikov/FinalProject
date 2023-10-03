using SocialNetwork;
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
    public class LoginView : IHelper , ILoginView
    {
        User user;
        UserService userService;
        UserAuthentificationData userAuthentificationData;


        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Панель авторизации существующего пользователя:");
            Console.WriteLine();
            Console.WriteLine("\tЭлектронная почта - ваш адрес электронной почты в формате \"example@mail.adress\"");
            Console.WriteLine("\tПароль - ваш пароль, непустая строка длиной не менее 8 символов");
            Console.WriteLine();
        }

        public LoginView(UserService _userService)
        {
            userService = _userService;
        }

        public void Show()
        {
            Help();
            
            userService = new UserService();
            userAuthentificationData = new UserAuthentificationData();

            Console.WriteLine("Начата процедура авторизации существующего пользователя");
            bool AutorizationIsSuccessfull= false;
            bool AutorizationIsFinished = false;
            do
            {
                Console.WriteLine("Введите адрес электронной почты:");
                userAuthentificationData.Email = Console.ReadLine();

                Console.WriteLine("Введите пароль:");
                userAuthentificationData.Password = Console.ReadLine();

                try
                {
                    user = userService.Authentificate(userAuthentificationData);
                    Console.WriteLine("Авторизация прошла успешно.");
                    AutorizationIsSuccessfull = true;
                    AutorizationIsFinished = true;
                }
                catch (UserNotFoundException)
                {
                    Console.WriteLine("Авторизация завершилась с ошибкой.");
                    Console.WriteLine("Пользователя с такой электронной почтой не существует");
                }
                catch (PasswordIsIncorrectException)
                {
                    Console.WriteLine("Авторизация завершилась с ошибкой.");
                    Console.WriteLine("Пароль введен неверно");
                }
                finally
                {
                    if (!AutorizationIsFinished)
                    {
                        Console.WriteLine("Ввести данные пользователя заново? \n\n\tYes - повторить ввод\n\tЛюбое другое значение - закончить авторизацию");
                        AutorizationIsFinished = !(Console.ReadLine().ToUpper() == "YES");
                    }
                }
            }
            while (!AutorizationIsFinished);

            if (AutorizationIsSuccessfull)
            {
                Program.mainView.Show(user);
            }
        }
    }
}
