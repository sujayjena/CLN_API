﻿using CLN.Application.Interfaces;
using CLN.Domain.Entities;
using Microsoft.Extensions.Options;
using CLN.Application.Helpers;
using CLN.Application.Models;

namespace CLN.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IJwtUtilsRepository jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last().SanitizeValue()!;
            UsersLoginSessionData? usersData = await jwtUtils.ValidateJwtToken(token);

            if (usersData != null)
            {
                // attach account to context on successful jwt validation
                context.Items["SessionData"] = usersData;
            }
            var vSessionManager = new SessionManager();

            await _next(context);
        }
    }
}
