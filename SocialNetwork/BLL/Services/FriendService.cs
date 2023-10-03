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
            var findUser = userService.FindByEmail(userAddingFriendData.FriendEmail);
            if (findUser is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                userId = userAddingFriendData.UserId,
                friendId = findUser.Id
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

        public void DeleteFriendByEmail(string email)
        {
            int friendId = userService.GetUserIdByEmail(email);

            if (friendRepository.Delete(friendId) == 0)
                throw new Exception();

        }

        public FriendService(UserService _userService)
        {
            friendRepository = new FriendRepository();
            userService = _userService;
        }

    }
}
