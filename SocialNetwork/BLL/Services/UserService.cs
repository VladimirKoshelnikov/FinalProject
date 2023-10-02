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
        IFriendRepository friendRepository;
        MessageService messageService;

        public UserService() 
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
        }

        private void CheckCriticalData(UserEntity userEntity) {
            if (String.IsNullOrEmpty(userEntity.firstName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userEntity.lastName))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userEntity.email))
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(userEntity.password))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(userEntity.email))
                throw new ArgumentException();

            if (userEntity.password.Length < 8)
                throw new ArgumentException();

        }

        public void Register(UserRegistrationData userRegistrationData) 
        {
           

            var userEntity = new UserEntity();

            userEntity.firstName = userRegistrationData.FirstName;
            userEntity.lastName = userRegistrationData.LastName;
            userEntity.email = userRegistrationData.Email;
            userEntity.password = userRegistrationData.Password;

            CheckCriticalData(userEntity);


            if (userRepository.FindByEmail(userEntity.email) != null)
                throw new UserIsAlreadyExistException();

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

        public User ConstructUserModel(UserEntity userEntity) { 

            return new User(userEntity.id,
                          userEntity.firstName,
                          userEntity.lastName,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favouriteMovie,
                          userEntity.favouriteBook
                          );
        }

        public User FindByEmail(string email) {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();
            return ConstructUserModel(findUserEntity);
                }

        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();
            return ConstructUserModel(findUserEntity);
        }

        public int GetUserIdByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();
            return findUserEntity.id;
        }

        public string GetUserEmailById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();
            return findUserEntity.email;
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
                favouriteBook = user.FavouriteBook,
                favouriteMovie = user.FavouriteMovie
            };

            CheckCriticalData(updateUserEntity);

            if (userRepository.Update(updateUserEntity) == 0)
                throw new Exception();
        }
    }
}
