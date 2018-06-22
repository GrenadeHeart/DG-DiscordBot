using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace DGBot
{
    public class BotConsole : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient client = new DiscordSocketClient();
        public string token { get; set; }
        public string defPrefix { get; set; }
        public string Prefix { get; set; }
        public static IRole userJoinRole { get; set; }
        public static IRole botJoinedRole { get; set; }
        /// <summary>
        /// Log messages on Console.
        /// </summary>
        /// <param name="msg">The log message.</param>
        /// <returns></returns>
        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        /// <summary>
        /// Logs an error to the Console.
        /// </summary>
        /// <param name="errorMsg">The error to log.</param>
        /// <returns></returns>
        public Task LogError(string errorMsg)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} BotError     {errorMsg}");
            return Task.CompletedTask;
        }
        /// <summary>
        /// Deserializes the "config.json" file and loads variables.
        /// </summary>
        /// <returns></returns>
        public Task OpenConfig()
        {
            BotConfig json = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(@"C:\Users\good\source\repos\Dash-Discord-Bot\DGBot\config.json"));
            token = json.token;
            defPrefix = json.defPrefix;
            Prefix = json.Prefix;
            LogMessage("Successfully deserialized \"config.json\".");
            return Task.CompletedTask;
        }
        /// <summary>
        /// Logs a message on the Console.
        /// </summary>
        /// <param name="logMsg">The message to log.</param>
        /// <returns></returns>
        public Task LogMessage(string logMsg)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Message     {logMsg}");
            return Task.CompletedTask;
        }
        /// <summary>
        /// Adds a role to the user/bot joined if any specified.
        /// </summary>
        /// <returns></returns>
        public async Task UserJoinedRole(SocketGuildUser user)
        {
            if (!(userJoinRole == null && user.IsBot))
            {
                await user.AddRoleAsync(userJoinRole);
            }
            else if (!(botJoinedRole == null) && user.IsBot)
            {
                await user.AddRoleAsync(botJoinedRole);
            }
            else
            {
                await LogError("Target role not specified.");
            }
            //return Task.CompletedTask;
        }
        public Task TryConnect()
        {
            client.LoginAsync(TokenType.Bot, token);
            client.StartAsync();
            return Task.CompletedTask;
        }
        public Task TryDisconnect()
        {
            LogMessage("Manually Disconnecting...");
            client.LogoutAsync();
            client.StopAsync();
            LogMessage("Disconnected.");
            return Task.CompletedTask;
        }
    }
     public class BotConfig
    {
        public string token { get; set; }
        public string defPrefix { get; set; }
        public string Prefix { get; set; }
    }
}
