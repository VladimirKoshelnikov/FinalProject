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
                            values (:user_id, :friend_id)", friendEntity);
        }

        public int Delete(int id)
        {
            return Execute(@"delete from friends 
                            where friendId = :id_p", new {id_p = id});
        }

        public IEnumerable<FriendEntity> FindAllbyUserId(int userId)
        {
            return Query<FriendEntity>(@"select * from friends where userId = :user_id", new { user_id = userId });
        }
    }

    public interface IFriendRepository
    {
        int Create(FriendEntity friendEntity);
        IEnumerable<FriendEntity> FindAllbyUserId(int userId);
        int Delete(int id);
    }
}
