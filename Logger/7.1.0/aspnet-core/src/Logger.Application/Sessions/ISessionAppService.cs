﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Logger.Sessions.Dto;

namespace Logger.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
