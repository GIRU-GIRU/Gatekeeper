﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gatekeeper.Modules
{

    public static class CommandToggles
    {
        public static bool WelcomeMessages = true;
    }

    public class Toggles : ModuleBase<SocketCommandContext>
    {
        [Command("welcomemessages on")]
        private async Task WelcomeMessagesOn()
        {
            CommandToggles.WelcomeMessages = true;
            await Context.Channel.SendMessageAsync("Welcome messages turned on");
        }

        [Command("welcomemessages off")]
        private async Task WelcomeMessagesOff()
        {
            CommandToggles.WelcomeMessages = false;
            await Context.Channel.SendMessageAsync("Welcome messages turned off");
        }
    }
}
