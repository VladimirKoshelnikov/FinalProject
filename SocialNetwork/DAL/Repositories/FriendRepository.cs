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

        public int Delete(FriendEntity friendEntity)
        {
            return Execute(@"delete from friends 
                            where userId = :userId and friendId = :friendId", friendEntity);
        }

        public IEnumerable<FriendEntity> FindAllByUserId(int userId)
        {
            return Query<FriendEntity>(@"select * from friends where userId = :user_id", new { user_id = userId });
        }
        
        public bool IsUsersAlreadyIsFriend(FriendEntity friendEntity)
        {
            var FriendshipEntity = QueryFirstOrDefault<FriendEntity>(@"select * from friends where userId = :userId and friendId = :friendId", friendEntity);

            return FriendshipEntity != null;
        }
    }

    public interface IFriendRepository
    {
        public int Create(FriendEntity friendEntity);
        public IEnumerable<FriendEntity> FindAllByUserId(int userId);
        public int Delete(FriendEntity friendEntity);
    }
}
