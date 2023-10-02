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
        public static WelcomePage welcomePage;
        public static LoginPage loginPage;
        public static RegistrationPage registrationPage;
        public static MainPage mainPage;
        public static FriendPage friendPage;
        public static ChangeInfoPage changeInfoPage;
        public static ConversationPage conversationPage;
        public static DialogPage dialogPage;

        public static UserService userService;
        public static MessageService messageService;
        public static FriendService friendService;

        static void Main(string[] args)
        {
            userService = new UserService();
            messageService = new MessageService(userService);
            friendService = new FriendService(userService);


            Console.WriteLine("Добро пожаловать в социальную сеть.");
            welcomePage = new WelcomePage();
            loginPage = new LoginPage(userService);
            registrationPage = new RegistrationPage(userService);
            mainPage = new MainPage(userService);
            friendPage = new FriendPage(userService, friendService);
            conversationPage = new ConversationPage(messageService, userService, friendService);
            changeInfoPage = new ChangeInfoPage(userService);
            dialogPage = new DialogPage(messageService);

            welcomePage.Show();


        }
    }
}