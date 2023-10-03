using System;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.PLL.ConsolePresentation;
using SocialNetwork.PLL.Interfaces;

namespace SocialNetwork
{
    public class Program
    {
        public static WelcomeView welcomeView;
        public static LoginView loginView;
        public static RegistrationView registrationView;
        public static MainView mainView;
        public static FriendView friendView;
        public static ChangeInfoView changeInfoView;
        public static ConversationView conversationView;
        public static DialogView dialogView;

        public static UserService userService;
        public static MessageService messageService;
        public static FriendService friendService;

        static void Main(string[] args)
        {
            userService = new UserService();
            messageService = new MessageService(userService);
            friendService = new FriendService(userService);


            Console.WriteLine("Добро пожаловать в социальную сеть.");
            welcomeView = new WelcomeView();
            loginView = new LoginView(userService);
            registrationView = new RegistrationView(userService);
            mainView = new MainView(userService);
            friendView = new FriendView(userService, friendService);
            conversationView = new ConversationView(messageService, userService, friendService);
            changeInfoView = new ChangeInfoView(userService);
            dialogView = new DialogView(messageService);

            welcomeView.Show();


        }
    }
}