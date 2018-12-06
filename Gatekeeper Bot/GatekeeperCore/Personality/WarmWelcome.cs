using Discord.WebSocket;
using GIRUBotV3.Modules;
using GIRUBotV3.Personality;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gatekeeper.Personality
{
    public static class WarmWelcome
    {
        public static async Task<string> GetWelcomeArrayMain(SocketGuildUser guildUser, Random rnd)
        {
            var insult = await Insults.GetInsult();
            string[] welcomeArrayMain = new string[]
            {
               $"{guildUser.Mention} has joined Melee Slasher, the {insult} is sitting in the shitter lobby",
               $"{guildUser.Mention} has joined the server, they are now waiting in the noob gate",
               $"{guildUser.Mention} is now waiting in the noob gate",
               $"{guildUser.Mention} join server guys 😃😃😃, they now wait in the noob gate",
               $"some {insult} called {guildUser.Mention} is now sitting in the noob gate",
               $"{guildUser.Mention} has just joined the server, waiting in the noob gate for attending to",
               $"{guildUser.Mention} has connected to the server, they're now sat in the noob gate",
            };
            int pull = rnd.Next(welcomeArrayMain.Length);
            return welcomeArrayMain[pull].ToString();
        }

        public static async Task<string> GetWelcomeArrayNoobGate(SocketGuildUser guildUser, Random rnd)
        {
            var insult = await Insults.GetInsult();

            string[] welcomeArrayNoobGate = new string[]
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

            int pull = rnd.Next(welcomeArrayNoobGate.Length);
            return welcomeArrayNoobGate[pull].ToString();

        }

        public static async Task<string> GetAggressNoobString(SocketGuildUser guildUser, Random rnd)
        {
            var dubjoyEmoji = Helpers.FindEmoji(guildUser, "dubjoy");
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
            int pull = rnd.Next(aggressArray.Length);
            return aggressArray[pull].ToString();
        }

    }
}

