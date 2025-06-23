using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        MessageService messageService;
        IUserRepository userRepository;

        public UserService() 
        {
            userRepository = new UserRepository();
            messageService = new MessageService();
        }

        public void Register(UserRegistrationData userRegistrationData)
        {

            if(String.IsNullOrEmpty(userRegistrationData.FirstName))
            {
                throw new ArgumentNullException();
            }

            if (String.IsNullOrEmpty(userRegistrationData.LastName))
            {
                throw new ArgumentNullException();
            }

            if (String.IsNullOrEmpty(userRegistrationData.Email))
            {
                throw new ArgumentNullException();
            }

            if (String.IsNullOrEmpty(userRegistrationData.Password))
            {
                throw new ArgumentNullException();
            }

            if(userRegistrationData.Password.Length < 8)
            {
                throw new ArgumentNullException();
            }

            if(!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            {
                throw new ArgumentNullException();
            }

            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
            {
                throw new ArgumentNullException();
            }

            var userEntity = new UserEntity()
            {
                firstName = userRegistrationData.FirstName,
                lastName = userRegistrationData.LastName,
                email = userRegistrationData.Email,
                password = userRegistrationData.Password
            };

            if(this.userRepository.Create(userEntity) == 0)
            {
                throw new Exception();
            }

        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null)
            {
                throw new UserNotFoundException();
            }

            return ConstructUserModel(findUserEntity);
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updateTableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                password = user.Password,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if(this.userRepository.Update(updateTableUserEntity) == 0)
            {
                throw new Exception();
            }
        }

        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageService.GetIncomingMessagesByUserId(userEntity.id);

            var outcomingMessages = messageService.GetOutcomingMessagesByUserId(userEntity.id);

            return new User(userEntity.id,
                          userEntity.firstName,
                          userEntity.lastName,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book,
                          incomingMessages,
                          outcomingMessages
                          );
        }

    }
}
