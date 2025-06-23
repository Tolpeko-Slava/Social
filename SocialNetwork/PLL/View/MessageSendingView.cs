using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.View
{
    public class MessageSendingView
    {
        private UserService userService;
        private MessageService messageService;

        public MessageSendingView(MessageService messageService, UserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        public void Show(User user)
        {
            var messageSendingData = new MessageSendingData();

            Console.Write("Введите email получателя: ");
            messageSendingData.RecipientEmail = Console.ReadLine()?.Trim();

            Console.WriteLine("Введите текст сообщения (макс. 5000 символов):");
            Console.WriteLine("(Для отмены оставьте поле пустым и нажмите Enter)");
            messageSendingData.Content = Console.ReadLine();

            // Проверка на отмену операции
            if (string.IsNullOrWhiteSpace(messageSendingData.Content))
            {
                Console.WriteLine("Отправка отменена.");
                return;
            }

            messageSendingData.SenderId = user.Id;

            try
            {
                messageService.SendMessage(messageSendingData);
                SuccessMessage.Show("Сообщение успешно отправлено!");

                // Обновляем данные пользователя
                user = userService.FindById(user.Id);
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Ошибка: пользователь с таким email не найден.");
            }
            catch (ArgumentException ex)
            {
                AlertMessage.Show($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                AlertMessage.Show($"Произошла ошибка при отправке: {ex.Message}");
                // Логирование ошибки (рекомендуется)
                // Logger.LogError(ex, "Ошибка отправки сообщения");
            }
        }

    }
}
