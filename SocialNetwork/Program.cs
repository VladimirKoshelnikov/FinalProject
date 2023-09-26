using System;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork
{
    class Program
    {
        static void NavigationHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Панель навигации программы:");
            Console.WriteLine();
            Console.WriteLine("\tДля вывода подсказки введите \"Help\"");
            Console.WriteLine("\tДля входа введите \"Login\"");
            Console.WriteLine("\tДля регистрации в системе введите \"Registration\"");
            Console.WriteLine("\tДля выхода из программы введите \"Exit\"");
            Console.WriteLine();

        }

        static void UserRegistrationHelp()
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

                

        static void LoginHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Панель авторизации существующего пользователя:");
            Console.WriteLine();
            Console.WriteLine("\tЭлектронная почта - ваш адрес электронной почты в формате \"example@mail.adress\"");
            Console.WriteLine("\tПароль - ваш пароль, непустая строка длиной не менее 8 символов");
            Console.WriteLine();

        }


        static void Login()
        {
            LoginHelp();
            UserService userService = new UserService();
            UserAuthentificationData userAuthentificationData = new UserAuthentificationData();

            Console.WriteLine("Начата процедура авторизации существующего пользователя");
            bool AutorizationisFinished = false;
            do
            {
                Console.WriteLine("Введите адрес электронной почты:");
                userAuthentificationData.Email = Console.ReadLine();

                Console.WriteLine("Введите пароль:");
                userAuthentificationData.Email = Console.ReadLine();

                try
                {
                    userService.Authentificate(userAuthentificationData);
                    Console.WriteLine("Авторизация прошла успешно.");
                    AutorizationisFinished = true;
                }
                catch (UserNotFoundException)
                {
                    Console.WriteLine("Авторизация завершилась с ошибкой.");
                    Console.WriteLine("Пользователя с такой электронной почтой не существует");
                }
                catch (PasswordIsIncorrectException)
                {
                    Console.WriteLine("Авторизация завершилась с ошибкой.");
                    Console.WriteLine("Пользователя с такой электронной почтой не существует");
                }
                finally
                {
                    if (!AutorizationisFinished)
                    {
                        Console.WriteLine("Ввести данные пользователя заново? \n\n\tYes - повторить ввод\n\tЛюбое другое значение - закончить авторизацию");
                        AutorizationisFinished = !(Console.ReadLine().ToUpper() == "YES");
                    }
                }
            }
            while (!AutorizationisFinished);
            
        }

        static void Registration()
        {
            UserRegistrationHelp();

            UserService userService = new UserService();
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

    

        static void Network()
        {
            Console.WriteLine("Для входа введите \"Login\"");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в социальную сеть.");
            while (true)
            {
                NavigationHelp();
                string userInput = Console.ReadLine();
                switch (userInput.ToUpper())
                {
                    case "HELP":
                        NavigationHelp();
                        break;
                    case "LOGIN":
                        Login();
                        break;
                    case "REGISTRATION":
                        Registration();
                        break;
                    case "EXIT":
                        return;
                }
            }
        }
    }
}