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

namespace GIRUBotV3.Modules
{
    public class UserJoined : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            var dubjoyEmoji = Helpers.FindEmoji(Context.User as SocketGuildUser, "dubjoy");
            var noobGateChannel = Context.Guild.GetChannel(Config.TheNoobGateChannel);
            var chnl = noobGateChannel as ITextChannel;
            var insult = await Insults.GetInsult();


            string[] aggressArray = new string[]
            {
               $"lmfao why you asking me for help? {insult}.",
               $"yea keep asking for help",
               $"yea that's right keep asking for help",
               $"rly love it when some {insult} begs me for help",
               $"lmfao, asking for help like some loser",
               $"hahah no1 gonna help u fucking retard",
               $"get fucked {insult} aint no1 gonna help u LMFAO",
               $"ur irritating",
               $"{dubjoyEmoji} keep crying for help",
               $"you're alone here, nobody even wants you",
               $"LOL you actually asked for help {dubjoyEmoji}",
               $"fucking weak {insult} asking for help,",
               $"?.. you think i was seriously going to help you",
               $"stfu, i wasn't serious about helping",
               $"you expect some warm welcome or something ? wtf u think this place is {dubjoyEmoji}",
               $"ugh",
               $"hahaha why u crying for help {insult}",
               $"lmfao, u think some1 gonna help u ?",
               $"nobody gonna help u son",
               $"cant u do things by urself? why are u such a pathetic loser that needs help",
               $"why don't you ask your mommy for help instead you fucking {insult} {dubjoyEmoji}",
               $"why don't you ask your mommy for help instead you fucking {insult}",
               $"nah",
               $"no",
               $"na",
               $"cry more {insult}",
               $"maybe later.. {dubjoyEmoji}",
               $"{dubjoyEmoji}",
               $"{dubjoyEmoji} u actually asked for help"

            };
            Random rnd = new Random();
            int pull = rnd.Next(aggressArray.Length);
            await chnl.SendMessageAsync(aggressArray[pull].ToString());
        }
      
        public static async Task UserJoinedServer(SocketGuildUser guildUser)
        {
            // casting
            var guildUserIGuildUser = guildUser as IGuildUser;
            var noobGateChannel = guildUser.Guild.GetChannel(Config.TheNoobGateChannel);
            var chnl = noobGateChannel as ITextChannel;

            // assigning noob role
            var noobRole = Helpers.FindRole(guildUser, "noob");
            await guildUser.AddRoleAsync(noobRole);

            // welcoming
            var insult = await Insults.GetInsult();
            Random rnd = new Random();
            string[] welcomeArray = new string[]
            {
               $"{guildUser.Mention}, welcome to the server {insult}.",
               $"new fucking {insult} {guildUser.Mention} has joined the server.",
               $"Welcome, {guildUser.Mention}.",
               $"what's up {guildUser.Mention}.",
               $"Hello {insult}. {guildUser.Mention},",
               $"Greetings {guildUser.Mention}.",
               $"{guildUser.Mention} has just joined the server, check the new {insult}. ",
               $"User {guildUser.Mention} has connected - someone see to this {insult}.",

            };
            var messageStateYourRegion = ""; //"Firstly - state your region; Europe, North America, Russia, Oceania, South America or Africa.";
            var messageInfo = "Write +help for instructions to get inside to Melee Slasher.";

            int pull = rnd.Next(welcomeArray.Length);
            string welcomeMessage = welcomeArray[pull].ToString();

            Task.Delay(15000).Wait();

            await chnl.SendMessageAsync(welcomeMessage + "\n" + messageStateYourRegion + "\n" + messageInfo);
        }


        //[Command("saytest")]
        //private async Task SaTest([Remainder]string message)
        //{
        //    var guildUser = Context.User as IGuildUser;
        //    ITextChannel logChannel = await guildUser.Guild.GetChannelAsync(492381877630402572) as ITextChannel;

        //    if (guildUser.JoinedAt.HasValue)
        //    {
        //        string[] dateArray = guildUser.JoinedAt.GetValueOrDefault().ToString("dd/MM/yyyy hh:mm").Split(" ");
        //        userJoinedDate = dateArray[0] + ", at " + dateArray[1];
        //    }
        //    await logChannel.SendMessageAsync($"{guildUser.Username}#{guildUser.Discriminator} joined Melee Slasher on {userJoinedDate}. UserID = {guildUser.Id}");
        //}



    } 
}
