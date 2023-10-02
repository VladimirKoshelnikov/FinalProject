using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{

    public class User
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string FavouriteMovie { get; set; }
        public string FavouriteBook { get; set; }

        public IEnumerable<Message> IncomingMessages { get; }
        public IEnumerable<Message> OutgoingMessages { get; }

        public IEnumerable<User> Friends { get; }

        public User(
            int id,
            string firstName,
            string lastName,
            string password,
            string email,
            string photo,
            string favouriteMovie,
            string favouriteBook
            )
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Email = email;
            this.Photo = photo;
            this.FavouriteMovie = favouriteMovie;
            this.FavouriteBook = favouriteBook;
        }
    }
}
