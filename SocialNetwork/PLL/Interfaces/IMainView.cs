﻿using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Interfaces
{
    public interface IMainView
    {
        public void Show(User user);
        public void ShowMyInfo();
        public void Logout();
    }
}
