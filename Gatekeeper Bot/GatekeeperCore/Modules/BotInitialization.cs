using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;
using GIRUBotV3.Personality;
using System.Threading.Tasks;
using System.Linq;
using GIRUBotV3;
using GIRUBotV3.Modules;

namespace Gatekeeper.Modules
{
    public class BotInitialization : ModuleBase<SocketCommandContext>
    {
        private static DiscordSocketClient _client;

        public BotInitialization(DiscordSocketClient client)
        {
            _client = client;
        }

        private static int noobRoleCount = 0;
        private static string randomTargetNoob;
        public static async Task StartUpMessages()
        {
            var chnl = _client.GetChannel(Config.MeleeSlasherMainChannel) as ITextChannel;
            await chnl.SendMessageAsync("Gatekeeper initializing...");
            Task.Delay(500).Wait();

            await chnl.SendMessageAsync("...");
            Task.Delay(500).Wait();
            await chnl.SendMessageAsync("Ready.");

            var noobChnl = _client.GetChannel(Config.TheNoobGateChannel);
            var noobChnlUsers = noobChnl.Users;
            var noobRole = Helpers.ReturnRole(chnl.Guild as SocketGuild, "noob");

            //cout noob role
            foreach (var item in noobChnlUsers)
            {
                var user = item as SocketGuildUser;
                if (user.Roles.Where(x => x.Id == noobRole.Id).Count() == 1)
                {
                    randomTargetNoob = user.Username;
                    noobRoleCount++;
                }
            }

            if (noobRoleCount == 0)
            {
                await chnl.SendMessageAsync("Melee Slasher is currently clean of noob filth");
            }
            else if (noobRoleCount == 1)
            {
                await chnl.SendMessageAsync($"There is one noob for me to deal with, and it's {randomTargetNoob}");
            }
            else
            {
                await chnl.SendMessageAsync($"There are {noobRoleCount} noobs for me to deal with, the first being { randomTargetNoob}");
            }
           
        }
    }
}
