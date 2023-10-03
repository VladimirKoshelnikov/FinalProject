using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        private FriendRepository friendRepository;
        public UserService userService;
        
        public IEnumerable<User> GetFriendsByUserId(int userId)
        {
            return friendRepository.FindAllByUserId(userId)
                    .Select(findedFriendEntity => userService.FindById(findedFriendEntity.friendId));
        }

        public void AddFriend(UserAddingFriendData userAddingFriendData)
        {
            
            var findUser = userService.FindById(userAddingFriendData.UserId);
            var findFriend = userService.FindByEmail(userAddingFriendData.FriendEmail);

            if (findUser is null | findUser is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                   userId = findUser.Id,
                   friendId = findFriend.Id
            };


            if (friendRepository.IsUsersAlreadyIsFriend(friendEntity))
                throw new FriendIsAlredyAddedException();

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

        public bool IsFriendshipExist(UserAddingFriendData userAddingFriendData)
        {
            var findFriend = userService.FindByEmail(userAddingFriendData.FriendEmail);
            var findUser = userService.FindById(userAddingFriendData.UserId);

            var friendEntity = new FriendEntity()
            {
                userId = findUser.Id,
                friendId = findFriend.Id

            };
            return friendRepository.IsUsersAlreadyIsFriend(friendEntity);
        }

        public void DeleteFriendByEmail(int userId, string email)
        {
            int friendId = userService.GetUserIdByEmail(email);

            FriendEntity friendEntity = new FriendEntity
            {
                userId = userId,
                friendId = friendId
            };

            if (!friendRepository.IsUsersAlreadyIsFriend(friendEntity))
                throw new FriendshipIsNotFoundException();

            if (friendRepository.Delete(friendEntity) == 0)
                throw new Exception();

        }



        public FriendService(UserService _userService)
        {
            friendRepository = new FriendRepository();
            userService = _userService;
        }

    }
}
