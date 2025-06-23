using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.View
{
    public class UserDeleteFriendView
    {
        FriendService friendService;

        public UserDeleteFriendView(FriendService friendService)
        {
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            var listFriends = this.friendService.GetAllFriend(user.Id);

            int i = 1;
            foreach (var friend in listFriends)
            {
                Console.WriteLine("{0}.   Email:{1}",i,friend.email);
                i++;
            }

            Console.WriteLine();
            Console.Write("Введите номер каго нужно удалить:");
            var numberFriend =  Console.ReadLine();

            int idFriend = 0;
            int.TryParse(numberFriend, out idFriend);

            this.friendService.DeleteFriend(listFriends[idFriend - 1]);
        }
    }
}
