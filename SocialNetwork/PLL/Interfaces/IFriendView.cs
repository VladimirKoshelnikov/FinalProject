using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Interfaces
{
    internal interface IFriendView
    {
        public void Show(User user);
        public void AddFriend();
        public void GetMyFriends();
        public void DeleteFriend();

    }
}
