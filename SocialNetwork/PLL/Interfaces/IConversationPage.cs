using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Interfaces
{
    internal interface IConversationPage
    {
        public void Show(User user);
        public void ShowAllDialogs();
        public void OpenDialog(string friendEmail);
    }
}
