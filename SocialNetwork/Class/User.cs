using SocialNetwork.Interfaces;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.Class
{
    public class User : IUser
    {
        public int id { get; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public delegate bool CheckData(string str);
        public bool CheckStringData(string str) => String.IsNullOrEmpty(str);
        public bool CheckEmailData(string str) => new EmailAddressAttribute().IsValid(str);
        public bool CheckPasswordData(string str) => str.Length >= 8;

        public string GetCorrectString(string question, CheckData checkData)
        {
            Console.WriteLine(question);
            string answer = Console.ReadLine();

            if (checkData(answer))
                throw new ArgumentNullException();
            
            return answer;
        }

        public void GetNewUser()
        {
            firstName = GetCorrectString("Введите ваше имя", CheckStringData);
            lastName = GetCorrectString("Введите ваше имя", CheckStringData);
            email = GetCorrectString("Введите вашу электронную почту", CheckEmailData);
            password = GetCorrectString("Введите пароль", CheckPasswordData);

        }
    }
}
