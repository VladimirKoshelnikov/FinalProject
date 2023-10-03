using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageRepository: BaseRepository, IMessageRepository
    {
        public int SendMessage(MessageEntity messageEntity)
        {
            return Execute(@"insert into messages(content, senderid, recipientid, datetime)

                            values(:content, :senderId, :recipientId, :datetime)", messageEntity);
        }

        public int DeleteById(int messageid)
        {
            return Execute(@"delete from messages

                            where id = :id_p", new { id_p = messageid });
        }

        public IEnumerable<MessageEntity> GetIncomingMessagesByUserId(int recipientId)
        {
            return Query<MessageEntity>(@"select * from messages
                                        where recipientId = :recipientId_p", new { recipientId_p = recipientId });
        }

        public IEnumerable<MessageEntity> GetOutcomingMessagesByUserId(int senderId)
        {
            return Query<MessageEntity>(@"select * from messages
                                        where senderId = :senderId_p", new { senderId_p = senderId });
        }

        public IEnumerable<MessageEntity> GetFullConversation(int senderId, int recipientId)
        {
            List<MessageEntity> MessagesFromUser = Query<MessageEntity>(@"select * from messages
                                        where senderId = :senderId_p and recipientId = :recipientId_p", new { senderId_p = senderId, recipientId_p = recipientId });
            
            List<MessageEntity> MessagesToUser = Query<MessageEntity>(@"select * from messages
                                        where senderId = :recipientId_p and recipientId = :senderId_p", new { senderId_p = senderId, recipientId_p = recipientId });

            var FullConversation = MessagesFromUser.Union(MessagesToUser).OrderBy(m =>m.datetime);

            return FullConversation;
        }

    }


    public interface IMessageRepository
    {
        public IEnumerable<MessageEntity> GetIncomingMessagesByUserId (int senderId);
        public IEnumerable<MessageEntity> GetOutcomingMessagesByUserId(int recipientId);

        public IEnumerable<MessageEntity> GetFullConversation(int senderId, int recipientId);

        public int SendMessage(MessageEntity messageEntity);
        public int DeleteById(int messageid);
    }
}
