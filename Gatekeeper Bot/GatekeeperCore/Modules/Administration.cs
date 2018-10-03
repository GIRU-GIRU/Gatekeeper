using Discord.Commands;
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
        [Command("commencepurge")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        private async Task ThePurge()
        {
            var allUsers = Context.Guild.Users;
            var noobRole = Helpers.ReturnRole(Context.Guild, "noob");
            int purgeReminderCount = 0;
            int purgeFinalCount = 0;
            ITextChannel theNoobGateChannel = Context.Guild.GetChannel(Config.TheNoobGateChannel) as ITextChannel;

            await Context.Channel.SendMessageAsync($"The Purge is commencing in 5 seconds, cleansing all shitters from Melee Slasher");
            await theNoobGateChannel.SendMessageAsync($"@here you're about to get fucking cleaned out you worthless fucking trash");

            Task.Delay(5000).Wait();

            foreach (var user in allUsers)
            {
                if (user.Roles.Contains(noobRole))
                {
                    await Context.Channel.SendMessageAsync($"{user.Username} has been cleansed");
                    await theNoobGateChannel.SendMessageAsync($"{user.Username} has been cleansed");
                    await user.KickAsync();
                    purgeReminderCount++;
                    purgeFinalCount++;
                }
                if (purgeReminderCount == 5)
                {
                    await Context.Channel.SendMessageAsync($"The Purge is commencing, cleansing all shitters from Melee Slasher");
                    purgeReminderCount = 0;
                }
            }

            if (purgeFinalCount == 0)
            {
                await Context.Channel.SendMessageAsync("Melee Slasher is currently clean");
                return;
            }

            var thePurgeEmbed = new EmbedBuilder();
            thePurgeEmbed.WithTitle($"⭕          A total of {purgeFinalCount} shitters were cleansed.        ⭕");
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


    }
}
