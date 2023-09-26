using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        IUserRepository userRepository;

        public UserService() { 
            userRepository = new UserRepository();
        }

        public void Register(UserRegistrationData userRegistrationData) 
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userRegistrationData.LastName))
                throw new ArgumentNullException();
            
            if (String.IsNullOrEmpty(userRegistrationData.Email))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userRegistrationData.Password))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentException();
            
            if (userRegistrationData.Password.Length < 8)
                throw new ArgumentException();

            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new UserIsAlreadyExistException();

            var userEntity = new UserEntity();

            userEntity.firstName = userRegistrationData.FirstName;
            userEntity.lastName = userRegistrationData.LastName;
            userEntity.email = userRegistrationData.Email;
            userEntity.password = userRegistrationData.Password;
                
            if (this.userRepository.Create(userEntity) == 0) 
                throw new Exception();

        }
    
        public User Authentificate(UserAuthentificationData userAuthentificationData)
        {
            var FindUserEntity = userRepository.FindByEmail(userAuthentificationData.Email);
            if (FindUserEntity is null) 
                throw new UserNotFoundException();
            if (FindUserEntity.password != userAuthentificationData.Password)
                throw new PasswordIsIncorrectException();
            return ConstructUserModel(FindUserEntity);
        }

        public User ConstructUserModel(UserEntity userEntity)
        {

            return new User(userEntity.id,
                            userEntity.firstName,
                            userEntity.lastName,
                            userEntity.password,
                            userEntity.email,
                            userEntity.photo,
                            userEntity.favourite_movie,
                            userEntity.favourite_book);
        }

        public User FindByEmail(string email) {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();
            return ConstructUserModel(findUserEntity);
                }

        public void Update(User user)
        {
            var updateUserEntity = new UserEntity()
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                password = user.Password,
                email = user.Email, 
                photo = user.Photo,
                favourite_book = user.FavouriteBook,
                favourite_movie = user.FavouriteMovie
            };

            if (userRepository.Update(updateUserEntity) == 0)
                throw new Exception();
        }
    }
}
