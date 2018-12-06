using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Gatekeeper.Modules;
using GIRUBotV3.Modules;
using GIRUBotV3.Personality;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace GIRUBotV3
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            var bot = program.RunBotAsync();
            bot.Wait();   
        }


        public DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private BotInitialization _botInitialization;

        public async Task RunBotAsync()
        {
            string botToken = Config.BotToken;
            var _HttpClient = new HttpClient();

            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _botInitialization = new BotInitialization(_client);
          
            
            _services = new ServiceCollection()
                 .AddSingleton(_commands)
                 .BuildServiceProvider();
   
            _client.UserJoined += UserJoined.UserJoinedServer;
            _client.Ready += BotInitialization.StartUpMessages;

            _client.Log += Log;
            
            await RegisterCommandAsync();
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(Config.CommandPrefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                switch (result.Error)
                {
                    default:
                        break;
                }                   
            }   
        }
        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
  
    }
}
