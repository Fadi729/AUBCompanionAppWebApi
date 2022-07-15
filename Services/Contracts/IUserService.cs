﻿using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IUserService
    {
        public Task<AuthResponse> RegisterAsync(ProfileRegistrationDTO user, CancellationToken cancellationToken);
        public Task<AuthResponse> LoginAsync   (ProfileLoginDTO user       , CancellationToken cancellationToken);
        public Task               DeleteAsync  (Guid userID                , CancellationToken cancellationToken);
    }
}
