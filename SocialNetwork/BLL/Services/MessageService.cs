using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class MessageService
    {
        public IMessageRepository messageRepository;
        public UserService userService;

        public bool IsLengthMoreThanLimit(Message message)
        {
            return (message.Content.Length < 5000);
        }

        public bool isReceiverExist(string ReceiverEmail)
        {
            var findUserEntity = userService.FindByEmail(ReceiverEmail);
            return findUserEntity == null ? false : true; 
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Message>();

            messageRepository.GetIncomingMessagesByUserId(recipientId).ToList().ForEach(m =>
            {
                var sender = userService.FindById(m.senderId);
                var recipient= userService.FindById(m.recipientId);

                messages.Add(new Message(m.content, sender.Email, recipient.Email, m.datetime));
            });
            return messages;
        }

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Message>();

            messageRepository.GetOutcomingMessagesByUserId(senderId).ToList().ForEach(m =>
            {
                var sender = userService.FindById(m.senderId);
                var recipient = userService.FindById(m.recipientId);
                messages.Add(new Message( m.content, sender.Email, recipient.Email, m.datetime));
            });
            return messages;
        }

        public IEnumerable<Message> GetFullConversation(int senderId, int recipientId)
        {
            var messages = new List<Message>();

            messageRepository.GetFullConversation(senderId, recipientId).ToList().ForEach(m =>
            {
                var sender = userService.FindById(m.senderId);
                var recipient = userService.FindById(m.recipientId);

                messages.Add(new Message( m.content, sender.Email, recipient.Email, m.datetime));
            });
            return messages;
        }


        public void SendMessage(Message message)
        {
            if (!IsLengthMoreThanLimit(message))
            {
                throw new ArgumentException();
            }
            if (!isReceiverExist(message.RecipientEmail))
            {
                throw new UserNotFoundException();
            }
            MessageEntity messageEntity = new MessageEntity()
            {
                senderId = userService.GetUserIdByEmail(message.SenderEmail),
                recipientId = userService.GetUserIdByEmail(message.RecipientEmail),
                content = message.Content,
                datetime = message.DateTimeSend
            };

            messageRepository.SendMessage(messageEntity);
        }

        public Message ConstructMessageModel(MessageEntity messageEntity)
        {
            
             return new Message(
                                messageEntity.content,
                                userService.GetUserEmailById(messageEntity.senderId),
                                userService.GetUserEmailById(messageEntity.recipientId),
                                messageEntity.datetime);   
        }

        public MessageService(UserService _userService)
        {
            messageRepository = new MessageRepository();
            userService = _userService;
        }

    }
}
