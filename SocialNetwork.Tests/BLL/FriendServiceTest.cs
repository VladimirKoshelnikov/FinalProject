using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests.BLL
{
    internal class FriendServiceTest
    {
        UserService userService;
        FriendService friendService;

        [SetUp]
        public void SetUp()
        {
            userService = new UserService();
            friendService = new FriendService(userService);
        }

        public static IEnumerable<UserAddingFriendData> GetIncorrectFriendship()
        {
            yield return new UserAddingFriendData()
            {
                UserId = -1,
                FriendEmail = "Jack@Daniels.com"
            };

            yield return new UserAddingFriendData()
            {
                UserId = -1,
                FriendEmail = "JackDaniels.com"
            };

            yield return new UserAddingFriendData()
            {
                UserId = 7,
                FriendEmail = "JackDaniels.com"
            };
        }

        [Test]
        public void GetFriendsByUserIdTest()
        {
            
            Assert.IsEmpty(friendService.GetFriendsByUserId(-1));
        }


        [Test]
        public void AddExistingFriendTest()
        {
            var existingFriendship= new UserAddingFriendData()
            {
                UserId = 7,
                FriendEmail = "Jack@Daniels.com"
            };
            Assert.Catch<FriendIsAlredyAddedException>(() => friendService.AddFriend(existingFriendship));
        }

        [Test]
        public void AddIncorrectFriendTest()
        {
            var incorrectFriendship = GetIncorrectFriendship();
            foreach (var friend in incorrectFriendship) {
                Assert.Catch<UserNotFoundException>(() => friendService.AddFriend(friend));
                }

        }

        [Test]
        public void AddDeleteСorrectFriendTest()
        {
            var сorrectFriendship = new UserAddingFriendData()
            {
                UserId = 1,
                FriendEmail = "bk@vk.ru"
            };
            bool isFriendshipExist;

            friendService.AddFriend(сorrectFriendship);
            isFriendshipExist = friendService.IsFriendshipExist(сorrectFriendship);
            Assert.IsTrue(isFriendshipExist);

            friendService.DeleteFriendByEmail(сorrectFriendship.UserId, сorrectFriendship.FriendEmail);
            isFriendshipExist = friendService.IsFriendshipExist(сorrectFriendship);
            Assert.IsFalse(isFriendshipExist);
        }
    }
}
