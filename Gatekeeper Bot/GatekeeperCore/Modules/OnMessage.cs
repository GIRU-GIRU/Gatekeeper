﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GIRUBotV3.Models;
using GIRUBotV3.Personality;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GIRUBotV3.Modules
{
    public class OnMessage
    {
        private static DiscordSocketClient _client;
        public OnMessage(DiscordSocketClient client)
        {
            _client = client;
        }

        private static Regex regexInviteLinkDiscord = new Regex(@"(https?:\/\/)?(www\.)?(discord\.(gg|io|me|li)|discordapp\.com\/invite)\/.+[a-z]");
        public async Task MessageContainsAsync(SocketMessage arg)
        {
            //ignore ourselves, check for null
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            if (message.Author.IsBot || Helpers.IsRole("Moderator", context.User as SocketGuildUser)) return;
            if (message.MentionedUsers.Count > 8)
            {
                IGuildUser targetUser = context.Guild.GetUser(message.Author.Id) as IGuildUser;
                IRole moderators = Helpers.ReturnRole(context.Guild, UtilityRoles.Moderator);
                var mutedRole = Helpers.ReturnRole(context.Guild, UtilityRoles.Muted);
                ITextChannel adminlogchannel = context.Guild.GetChannel(Config.AuditChannel) as ITextChannel;

                await targetUser.AddRoleAsync(mutedRole);
                await context.Channel.SendMessageAsync($"stay small {message.Author.Mention}, no spam in the server you little shitter");            
                await adminlogchannel.SendMessageAsync($"{targetUser.Username}#{targetUser.DiscriminatorValue} has been auto muted for mass mention, please investigate {moderators.Mention}");
            }

            if (regexInviteLinkDiscord.Match(message.Content).Success & !Helpers.IsRole("Moderator", message.Author as SocketGuildUser))
            {

                var insult = await Insults.GetInsult();
                await context.Message.DeleteAsync();
                await context.Channel.SendMessageAsync($"{context.User.Mention}, don't post invite links {insult}");
            }
        }


        public async Task UpdatedMessageContainsAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var messageAfter = after as SocketUserMessage;
            var context = new SocketCommandContext(_client, messageAfter);
            if (messageAfter.Author.IsBot || Helpers.IsRole("Moderator", context.User as SocketGuildUser)) return;
            if (regexInviteLinkDiscord.Match(messageAfter.Content).Success)
            {
                var insult = await Insults.GetInsult();
                await context.Message.DeleteAsync();
                await context.Channel.SendMessageAsync($"{context.User.Mention}, don't post invite links {insult}");
            }
            if (messageAfter.MentionedUsers.Count > 8)
            {
                IGuildUser targetUser = context.Guild.GetUser(messageAfter.Author.Id) as IGuildUser;
                IRole moderators = Helpers.ReturnRole(context.Guild, UtilityRoles.Moderator);
                var mutedRole = Helpers.ReturnRole(context.Guild, UtilityRoles.Muted);
                ITextChannel adminlogchannel = context.Guild.GetChannel(Config.AuditChannel) as ITextChannel;

                await targetUser.AddRoleAsync(mutedRole);
                await context.Channel.SendMessageAsync($"stay small {messageAfter.Author.Mention}, no spam in the server you little shitter");
                await adminlogchannel.SendMessageAsync($"{targetUser.Username}#{targetUser.DiscriminatorValue} has been auto muted for mass mention, please investigate {moderators.Mention}");
            }
        }
    }
}
