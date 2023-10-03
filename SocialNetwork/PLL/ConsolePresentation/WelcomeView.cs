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
    public class WelcomeView : IHelper, IWelcomeView
    {
        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Help()
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

        public void Show()
        {
            Help();
            while (true)
            {
                Console.WriteLine("Вы находитесь на стартовой странице приложения");
                string userInput = Console.ReadLine();
                switch (userInput.ToUpper())
                {
                    case "HELP":
                        Help();
                        break;
                    case "LOGIN":
                        Program.loginView.Show();
                        break;
                    case "REGISTRATION":
                        Program.registrationView.Show();
                        break;
                    case "EXIT":
                        Console.WriteLine("До свидания. Ждем вас снова");
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}
