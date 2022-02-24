﻿using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password) =>
            await usersRepository.Authenticate(usernameOrEmail, password);

        public async Task<AuthenticateModel> Register(User user) => await this.usersRepository.Register(user);
    }
}
