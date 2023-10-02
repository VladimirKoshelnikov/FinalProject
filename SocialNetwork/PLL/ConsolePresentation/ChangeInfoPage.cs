using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.ConsolePresentation
{
    public class ChangeInfoPage : IHelper, IChangeInfoPage
    {
        private User changedUser;
        private User sourceUser;
        UserService userService;
        private string GetNewParameter(string ParameterName)
        {
            Console.WriteLine($"Введите новое значение параметра {ParameterName}");
            string answer = Console.ReadLine();
            Console.WriteLine($"Новое значение параметра {ParameterName}: {answer}");
            return answer;
        }

        public void DropChanges()
        {
            changedUser = sourceUser;
            Console.WriteLine("Изменения сброшены");
        }

        public void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Список доступных команд в панели редактирования профиля:");
            Console.WriteLine();
            Console.WriteLine("\tПомощь в использовании: \"Help\"");
            Console.WriteLine("\tИзменить параметры: \"ChangeParameters\"");
            Console.WriteLine("\tСохранить изменения: \"SaveChanges\"");
            Console.WriteLine("\tУдалить изменения: \"DropChanges\"");
            Console.WriteLine("\tВернуться на стартовую страницу: \"Undo\"");
            Console.WriteLine();
        }

        public void SaveChanges()
        {
            try
            {
                userService.Update(changedUser);
                Console.WriteLine("Изменения успешно внесены");
                Console.WriteLine();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Изменения не внесены:");
                Console.WriteLine("Введены пустые значения в критичные строки");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Изменения не внесены:");
                Console.WriteLine("Введены некорректные данные");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Изменения не внесены по неизвестной причине:");
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ChangeParameters()
        {
            Console.WriteLine("Введите параметр для изменения:");

            Console.WriteLine("\tАдрес электронной почты: \"Email\"");
            Console.WriteLine("\tПароль: \"Password\"");
            Console.WriteLine("\tИмя: \"FirstName\"");
            Console.WriteLine("\tФамилия: \"LastName\"");
            Console.WriteLine("\tСсылка на фото: \"Photo\"");
            Console.WriteLine("\tЛюбимый фильм: \"FavouriteMovie\"");
            Console.WriteLine("\tЛюбимая книга: \"FavouriteBook\"");

            Console.WriteLine("\tЗавершить редактирование: \"StopEdit\"");
            Console.WriteLine();
            bool changingIsFinished = false;

            while (!changingIsFinished)
            {
                Console.WriteLine("Введите наименование параметра для изменения");
                switch (Console.ReadLine().ToUpper())
                {
                    case "EMAIL":
                        changedUser.Email = GetNewParameter("Email");
                        break;
                    case "PASSWORD":
                        changedUser.Password = GetNewParameter("Password");
                        break;
                    case "FIRSTNAME":
                        changedUser.FirstName = GetNewParameter("FirstName");
                        break;
                    case "LASTNAME":
                        changedUser.LastName = GetNewParameter("LastName");
                        break;
                    case "PHOTO":
                        changedUser.Photo = GetNewParameter("Photo");
                        break;
                    case "FAVOURITEBOOK":
                        changedUser.FavouriteBook = GetNewParameter("FavouriteBook");
                        break;
                    case "FAVOURITEMOVIE":
                        changedUser.FavouriteMovie = GetNewParameter("FavouriteMovie");
                        break;
                    case "STOPEDIT":
                        changingIsFinished = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестный параметр");
                        break;
                }
            }
        }

        public ChangeInfoPage(UserService _userService)
        {
            userService = _userService;
        }

        public void Show (ref User user )
        {
            sourceUser = user;
            changedUser = user;
            Help();

            Console.WriteLine();
            while (true)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "CHANGEPARAMETERS":
                        ChangeParameters();
                        break;

                    case "SAVECHANGES":
                        SaveChanges();
                        user = changedUser;
                        changedUser = null;
                        sourceUser = null;
                        return;

                    case "DROPCHANGES":
                        DropChanges();
                        break;

                    case "UNDO":
                        Console.WriteLine();
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}
