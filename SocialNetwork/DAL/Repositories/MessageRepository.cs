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
        public int Create(MessageEntity messageEntity)
        {
            return Execute(@"insert into messages(content, senderid, recipientid)

                            values(:content, :sender_id, :recipient_id)", messageEntity);
        }

        public int DeleteById(int messageid)
        {
            return Execute(@"delete from messages

                            where :id = id_p", new {id_p = messageid});
        }

        public IEnumerable<MessageEntity> FindByRecipientId(int recipientId)
        {
            return Query<MessageEntity>(@"select * from messages
                                        where recipient_id = :recipientId_p", new { recipientId_p = recipientId });
        }

        public IEnumerable<MessageEntity> FindBySenderId(int senderId)
        {
            return Query<MessageEntity>(@"select * from messages
                                        where sender_id = :senderId_p", new { senderId_p = senderId });
        }
      
    }
    public interface IMessageRepository
    {
        int Create(MessageEntity messageEntity);
        IEnumerable<MessageEntity> FindBySenderId(int senderId);
        IEnumerable<MessageEntity> FindByRecipientId(int recipientId);

        int DeleteById(int messageid);
    }
}
