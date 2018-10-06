//using Discord;
//using Discord.Commands;
//using Discord.WebSocket;
//using GIRUBotV3.Models;
//using GIRUBotV3.Personality;
//using System;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace GIRUBotV3.Modules
//{
//    public class OnMessage
//    {
//        private static DiscordSocketClient _client;
//        public OnMessage(DiscordSocketClient client)
//        {
//            _client = client;
//        }

//        private static Regex regexInviteLinkDiscord = new Regex(@"(https?:\/\/)?(www\.)?(discord\.(gg|io|me|li)|discordapp\.com\/invite)\/.+[a-z]");
//        public async Task MessageContainsAsync(SocketMessage arg)
//        {
//            //ignore ourselves, check for null
//            var message = arg as SocketUserMessage;
//            var context = new SocketCommandContext(_client, message);

//            if (message.Author.IsBot || Helpers.IsRole("Moderator", context.User as SocketGuildUser)) return;
//        }


//        public async Task UpdatedMessageContainsAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
//        {

//        }
//    }
//}
