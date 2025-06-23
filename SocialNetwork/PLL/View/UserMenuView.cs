using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.View
{
    public class UserMenuView
    {
        private UserService userService;

        public UserMenuView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine("Входящие сообщения: {0}", user.IncomingMessages.Count());
                Console.WriteLine("Исходящие сообщения: {0}", user.OutcomingMessages.Count());

                Console.WriteLine("1. Просмотреть информацию о моём профиле");
                Console.WriteLine("2. Редактировать мой профиль");
                Console.WriteLine("3. Добавить в друзья");
                Console.WriteLine("4. Удалить из друзей");
                Console.WriteLine("5. Написать сообщение");
                Console.WriteLine("6. Просмотреть входящие сообщения");
                Console.WriteLine("7. Просмотреть исходящие сообщения");
                Console.WriteLine("8. Выйти из профиля");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Program.userInfoView.Show(user);
                        break;
                    case "2":
                        Program.userDataUpdateView.Show(user);
                        break;
                    case "3":
                        Program.userAddToFriendView.Show(user);
                        break;
                    case "4":
                        Program.userDeleteFriendView.Show(user);
                        break;
                    case "5":
                        Program.messageSendingView.Show(user);
                        break;
                    case "6":
                        Program.userIncomingMessageView.Show(user.IncomingMessages);
                        break;
                    case "7":
                        Program.userOutcomingMessageView.Show(user.OutcomingMessages);
                        break;
                }
            }
        }
    }
}
