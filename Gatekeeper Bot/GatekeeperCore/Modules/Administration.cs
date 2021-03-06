﻿using Discord.Commands;
using Discord.WebSocket;
using Discord;
using GIRUBotV3.Personality;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Net;
using GIRUBotV3.Models;
using System.Linq;

namespace GIRUBotV3.Modules
{
    public class Administration : ModuleBase<SocketCommandContext>
    {
        [Command("warn")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        private async Task WarnUserCustom(IGuildUser user, [Remainder]string warningMessage)
        {
            try
            {
                await user.SendMessageAsync("You have been warned in Melee Slasher for: " + warningMessage);
                await Context.Channel.SendMessageAsync($"⚠      *** {user.Username} has received a warning.      ⚠***");
            }
            catch (HttpException ex)
            {
                await Context.Channel.SendMessageAsync($"{user.Mention}, {warningMessage}");
            }
        }
        [Command("stealthpootime")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        private async Task WarnUserpooCustom(IGuildUser user, [Remainder]string warningMessage)
        {
            try
            {
                await user.SendMessageAsync("You have been warned in Melee Slasher for: " + warningMessage);
                 var postedWarningMessage = await Context.Channel.SendMessageAsync($"⚠      *** {user.Username} has received a warning.      ⚠***");
   
                await Context.Message.DeleteAsync();
                await postedWarningMessage.DeleteAsync();
            }
            catch (HttpException ex)
            {
                var postedWarningMessage = await Context.Channel.SendMessageAsync($"{user.Mention}, {warningMessage}");

                await Context.Message.DeleteAsync();
                await postedWarningMessage.DeleteAsync();
            }
        }


        [Command("commencepurge")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        private async Task ThePurge()
        {
            var allUsers = Context.Guild.Users;
            var noobRole = Helpers.ReturnRole(Context.Guild, "noob");
            int purgeReminderCount = 0;
            int amountToBePurged = 0;
            
            ITextChannel theNoobGateChannel = Context.Guild.GetChannel(Config.TheNoobGateChannel) as ITextChannel;
            foreach (var item in allUsers)
            {
                if (item.Roles.Contains(noobRole))
                {
                    amountToBePurged++;
                }
            }
            if (amountToBePurged == 0)
            {
                await Context.Channel.SendMessageAsync("Melee Slasher is currently clean");
                return;
            }


            await Context.Channel.SendMessageAsync($"The Purge is commencing in 5 seconds, cleansing all shitters from Melee Slasher");
            await noobRole.ModifyAsync(x => x.Mentionable = true);
            await theNoobGateChannel.SendMessageAsync($"{noobRole.Mention} you're about to get fucking cleaned out you worthless fucking trash");
            await noobRole.ModifyAsync(x => x.Mentionable = false);

            await Task.Delay(5000);

            foreach (var user in allUsers)
            {
                if (user.Roles.Contains(noobRole))
                {
                    await Context.Channel.SendMessageAsync($"{user.Username} has been cleansed");
                    await theNoobGateChannel.SendMessageAsync($"{user.Username} has been cleansed");
                    await user.KickAsync();
                    purgeReminderCount++;
                    
                }
                if (purgeReminderCount == 5)
                {
                    await Context.Channel.SendMessageAsync($"The Purge is commencing, cleansing all shitters from Melee Slasher");
                    purgeReminderCount = 0;
                }
            }

            var thePurgeEmbed = new EmbedBuilder();
            thePurgeEmbed.WithTitle($"⭕          A total of {amountToBePurged} shitters were cleansed.        ⭕");
            thePurgeEmbed.ThumbnailUrl = "https://cdn.discordapp.com/attachments/300832513595670529/469942372034150440/detailed_helmet_discord_embed.png";
            thePurgeEmbed.WithColor(new Color(255, 0, 0));
            await Context.Channel.SendMessageAsync("", false, thePurgeEmbed.Build());
            return;
        }
        [Command("clearnoobgate")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        private async Task ClearNoobGate()
        {

        }

        [Command("say")]
        [RequireUserPermission(GuildPermission.ViewAuditLog)]
        private async Task SayInMain([Remainder]string message)
        {
            var chnl = Context.Guild.GetTextChannel(Config.MeleeSlasherMainChannel);
            await chnl.SendMessageAsync(message);
        }
        [Command("saynoob")]
        [RequireUserPermission(GuildPermission.ViewAuditLog)]
        private async Task SayInNoob([Remainder]string message)
        {
            var chnl = Context.Guild.GetTextChannel(Config.TheNoobGateChannel);
            await chnl.SendMessageAsync(message);
        }

        OverwritePermissions openWindow = new OverwritePermissions(readMessages: PermValue.Inherit);
        OverwritePermissions closeWindow = new OverwritePermissions(readMessages: PermValue.Deny);
        OverwritePermissions noTalk = new OverwritePermissions(sendMessages: PermValue.Deny);
        [Command("noobwindow open")]
        [RequireUserPermission(GuildPermission.ViewAuditLog)]
        private async Task NoobWindowOpen()
        {
            var chnl = Context.Guild.GetTextChannel(Config.TheNoobGateChannel);
            await chnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, openWindow);
            await chnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, noTalk);

            await Context.Channel.SendMessageAsync($"A window to the noob gate has been opened: {chnl.Mention}");
        }
        [Command("noobwindow close")]
        [RequireUserPermission(GuildPermission.ViewAuditLog)]
        private async Task NoobWindowclose()
        {
            var chnl = Context.Guild.GetTextChannel(Config.TheNoobGateChannel);
            await chnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, closeWindow);

            await Context.Channel.SendMessageAsync("The noob gate window has been closed");
        }
    }
}
