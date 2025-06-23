using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.View
{
    public class UserAddToFriendView
    {
        FriendService friendService;

        public UserAddToFriendView(FriendService friendService)
        {
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            Console.Write("Введите почту друга:");
            var email = Console.ReadLine();

            this.friendService.AddFriend(new FriendAdd() { id_User = user.Id, emailFriend = email});

            Console.WriteLine("Друг добавлен");
        }

    }
}
