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
    public class RegistrationView : IHelper
    {
        private UserService userService;

        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Панель регистрации нового пользователя:");
            Console.WriteLine();
            Console.WriteLine("\tИмя пользователя - любая непустая строчка");
            Console.WriteLine("\tФамилия пользователя - любая непустая строчка");
            Console.WriteLine("\tЭлектронная почта - непустая строчка в формате \"example@mail.adress\"");
            Console.WriteLine("\tПароль - любая строка длиной не менее 8 символов");
            Console.WriteLine();
        }
        


        public RegistrationView(UserService _userService)
        {
            userService = _userService;
        }

        public void Show()
        {
            Help();

            UserRegistrationData userRegistrationData = new UserRegistrationData();

            Console.WriteLine("Начата процедура регистрации нового пользователя");
            bool RegistrationIsFinished = false;
            do
            {
                Console.WriteLine("Введите имя:");
                userRegistrationData.FirstName = Console.ReadLine();

                Console.WriteLine("Введите фамилию:");
                userRegistrationData.LastName = Console.ReadLine();

                Console.WriteLine("Введите адрес электронной почты:");
                userRegistrationData.Email = Console.ReadLine();

                Console.WriteLine("Введите пароль (длина пароля должна быть не менее 8 символов):");
                userRegistrationData.Password = Console.ReadLine();

                try
                {
                    userService.Register(userRegistrationData);
                    Console.WriteLine("Новый пользователь зарегистирирован");
                    RegistrationIsFinished = true;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Регистрация пользователя завершена с ошибкой:");
                    Console.WriteLine("Пустые строки недопустимы");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Регистрация пользователя завершена с ошибкой:");
                    Console.WriteLine("Введены некорректные данные");
                }
                catch (UserIsAlreadyExistException)
                {
                    Console.WriteLine("Регистрация пользователя завершена с ошибкой:");
                    Console.WriteLine("Пользователь с таким почтовым адресом уже существует");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Регистрация пользователя завершена с ошибкой:");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (!RegistrationIsFinished)
                    {
                        Console.WriteLine("Ввести данные пользователя заново? \n\n\tYes - повторить ввод\n\tЛюбое другое значение - закончить авторизацию");
                        RegistrationIsFinished = !(Console.ReadLine().ToUpper() == "YES");
                    }
                }
            }
            while (!RegistrationIsFinished);
        }


    }
}
