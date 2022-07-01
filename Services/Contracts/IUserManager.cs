﻿using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IUserManager
    {
        public Task<AuthResponse> RegisterAsync(ProfileRegistrationDTO user);
        public Task<AuthResponse> LoginAsync   (ProfileLoginDTO user);
        public Task               DeleteAsync  (Guid userID);
    }
}