using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        IFriendRepository friendRepository;
        IUserRepository userRepository;

        public FriendService() 
        {
            friendRepository = new FriendRepository();
            userRepository = new UserRepository();
        }

        public void AddFriend(FriendAdd friendAdd)
        {
            if(String.IsNullOrEmpty(friendAdd.emailFriend))
                throw new UserNotFoundException();

            var userFriend = userRepository.FindByEmail(friendAdd.emailFriend);

            var addNewFriend = new FriendEntity()
            {
                user_id = friendAdd.id_User,
                friend_id = userFriend.id
            };

            if (this.friendRepository.Create(addNewFriend) == 0)
                throw new Exception();
        }

        public void DeleteFriend(FriendShow friendShow)
        {
            if(this.friendRepository.Delete(friendShow.ID) == 0)
                throw new Exception();
        }

        public List<FriendShow> GetAllFriend(int id)
        {

            var listFriends = new List<FriendShow>();

            this.friendRepository.FindAllByUserId(id).ToList().ForEach(m =>
            {
                var friend = this.userRepository.FindById(m.friend_id);

                listFriends.Add(new FriendShow(m.id,friend.email));
            });


            return null;
        }

    }
}
