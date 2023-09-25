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
            throw new NotImplementedException();
        }

        public IEnumerable<MessageEntity> FindByRecipientId(int recipientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MessageEntity> FindBySenderId(int senderId)
        {
            throw new NotImplementedException();
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
