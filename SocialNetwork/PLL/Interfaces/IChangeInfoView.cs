using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Interfaces
{
    public interface IChangeInfoView
    {
        public void Show(ref User user);
        public void ChangeParameters();
        public void SaveChanges();
        public void DropChanges();

    }
}
