﻿using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Ritsukage.Library.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ritsukage.Discord.Services
{
    [Service]
    public class FirstCommingRole
    {
        readonly DiscordSocketClient _discord;

        public FirstCommingRole(IServiceProvider services)
        {
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _discord.UserJoined += UserJoined;
        }

        async Task UserJoined(SocketGuildUser user)
        {
            var t = await Database.Data.Table<DiscordGuildSetting>().ToArrayAsync();
            var data = t.Where(x => x.Guild == Convert.ToInt64(user.Guild.Id)).FirstOrDefault();
            if (data != null && data.FirstCommingRole > 0 && !user.IsBot && !user.IsWebhook)
                await user.AddRoleAsync(user.Guild.GetRole(Convert.ToUInt64(data.FirstCommingRole)));
        }

    }
}
