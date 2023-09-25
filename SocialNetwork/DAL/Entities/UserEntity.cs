using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.DAL.Entities
{
    public class UserEntity
    {
        public int id { get; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        private string password { get; set; }
        public string email { get; set; }

        public delegate bool CheckData(string str);
        private bool CheckStringData(string str) => string.IsNullOrEmpty(str);
        private bool CheckEmailData(string str) => !new EmailAddressAttribute().IsValid(str);
        private bool CheckPasswordData(string str) => str.Length <= 8;

        private string GetCorrectString(string question, CheckData checkData)
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
