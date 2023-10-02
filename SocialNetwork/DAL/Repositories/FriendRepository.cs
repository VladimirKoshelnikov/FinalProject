using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{

    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public int Create(FriendEntity friendEntity)
        {
            return Execute(@"insert into friends 
                            (userId, friendId) 
                            values (:userId, :friendId)", friendEntity);
        }

        public int Delete(int id)
        {
            return Execute(@"delete from friends 
                            where friendId = :id_p", new {id_p = id});
        }

        public IEnumerable<FriendEntity> FindAllByUserId(int userId)
        {
            return Query<FriendEntity>(@"select * from friends where userId = :user_id", new { user_id = userId });
        }

        
    }

    public interface IFriendRepository
    {
        public int Create(FriendEntity friendEntity);
        public IEnumerable<FriendEntity> FindAllByUserId(int userId);
        public int Delete(int id);
    }
}
