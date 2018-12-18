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
using System.Threading;
using Gatekeeper.Personality;
using Gatekeeper.Modules;

namespace GIRUBotV3.Modules
{
    public class UserJoined : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            if (Context.Message.Channel.Id != Config.TheNoobGateChannel) return;

            var insult = await Insults.GetInsult();
           
            Random rnd = new Random();
            var aggressString = await WarmWelcome.GetAggressNoobString(Context.Message.Author as SocketGuildUser, rnd);
            await Context.Channel.SendMessageAsync(aggressString);
        }
      
        public static async Task UserJoinedServer(SocketGuildUser guildUser)
        {

            var guildUserIGuildUser = guildUser as IGuildUser;
            var chnl = guildUser.Guild.GetChannel(Config.TheNoobGateChannel) as ITextChannel;
            var mainchnl = guildUser.Guild.GetChannel(Config.MeleeSlasherMainChannel) as ITextChannel;

            var insult = await Insults.GetInsult();
            Random rnd = new Random();

            var noobRole = Helpers.FindRole(guildUser, UtilityRoles.Noob);
            await guildUser.AddRoleAsync(noobRole);

            if (CommandToggles.WelcomeMessages)
            {
                var welcomeMessageMain = await WarmWelcome.GetWelcomeArrayMain(guildUser, rnd);
                await mainchnl.SendMessageAsync(welcomeMessageMain);
            }

            ITextChannel logChannel = guildUser.Guild.GetChannel(Config.UserJoinedLogChannel) as ITextChannel;
            await logChannel.SendMessageAsync($"{guildUser.Username}#{guildUser.Discriminator} joined Melee Slasher. UserID = {guildUser.Id}");        
            var messageInfo = "Write +help for instructions to get inside to Melee Slasher.";
            var greetingMessage = await WarmWelcome.GetWelcomeArrayNoobGate(guildUser, rnd) + "\n" + messageInfo;
      
            _ = Task.Run(async () => await GreetUser(chnl, greetingMessage));
        }

        private static async Task GreetUser(ITextChannel chnl, string greetingMessage)
        {
            Task.Delay(15000).Wait();
            await chnl.SendMessageAsync(greetingMessage);
        }


    } 
}
