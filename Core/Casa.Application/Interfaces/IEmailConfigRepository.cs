﻿using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IEmailConfigRepository
    {
        Task<int> SaveEmailConfig(EmailConfig_Request parameters);

        Task<IEnumerable<EmailConfig_Response>> GetEmailConfigList(EmailConfig_Search parameters);

        Task<EmailConfig_Response?> GetEmailConfigById(int Id);
    }
}