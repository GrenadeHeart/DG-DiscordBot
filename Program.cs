using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGBot
{
    public class Program : ModuleBase<SocketCommandContext>
    {
        public CommandHandler handler; // Create an instance of the CommandHandler.
        private DiscordSocketClient client; // Create a new client (bot).
        private IMessageChannel messageChannel;
        public static BotConsole console = new BotConsole();
        public static DateTime uptimeStart; // Declare uptimeStart for later use in MainAsync and Commands.cs.

        public static string Token;
        public static string Prefix;
        public static string DefPrefix;

        string MODE = "";

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult(); //START THE BOT

        #region Main
        /// <summary>
        /// Configure the bot.
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            uptimeStart = DateTime.Now; // Assign uptimeStart to the time bot is started on.
#if DEBUG
            await client.SetStatusAsync(UserStatus.DoNotDisturb); // Set a Do Not Disturb status for the bot.
                await client.SetGameAsync("in Debug Mode. (/help)", "https://discordapp.com/", StreamType.Twitch); // Set a playing status for the bot.
                MODE = "DEBUG"; // Sets the MODE to current running mode.
#else            //}
            //else
            //{
                await client.SetStatusAsync(UserStatus.Online); // Set an online status for the bot.
                await client.SetGameAsync("/help", "https://discordapp.com/", StreamType.Twitch); // Set a playing status for the bot.
                MODE = "RELEASE"; // Sets the MODE to current running mode.
            //}
#endif
            Console.WriteLine("Copyright (C) lord_Dash" + "\n" + $"Last updated: {Commands.update}");
            await console.LogMessage($"Started in mode: {MODE}");
            await console.OpenConfig();

            client.Log += console.Log; // Logs data on the Console.

            // INITIALIZE COMMAND HANDLING SERVICE
            handler = new CommandHandler();
            await handler.InitializeAsync(client);
            //


            // MAKE THE BOT LOGIN
            await client.LoginAsync(TokenType.Bot, console.token); // Login.
            await client.StartAsync(); // Start the bot.
            Token = console.token;
            Prefix = console.Prefix;
            DefPrefix = console.defPrefix;
            //

            string isCloseResponse = Console.ReadLine();

            if (isCloseResponse == "exit" || isCloseResponse == "end" || isCloseResponse == "close") // Checks if the user wants to close the bot.
            {
                await console.LogMessage("Exiting bot...");
                await client.LogoutAsync();
                await Task.Delay(2500); // DELAY THE TASK FOR 2.5 SECONDS
            }
            else if (isCloseResponse == "connect")
            {
                await console.TryConnect();
                isCloseResponse = Console.ReadLine();
                return;
                await Task.Delay(-1);
            }
            else if (isCloseResponse == "disconnect")
            {
                await console.TryDisconnect();
                isCloseResponse = Console.ReadLine();
                return;
                await Task.Delay(-1);
            }
            else if (isCloseResponse.Contains("send"))
            {
                string text = Console.ReadLine();
                await talk(text);
                return;
            }
            else
            {
                isCloseResponse = Console.ReadLine();
                return;
                await Task.Delay(-1);   // DELAY THE TASK UNTIL PROGRAM IS CLOSED
            }
        }
        #endregion
        public async Task talk(string text)
        {
            //var channel = client.GetTextChannel(Commands.thisChannel);
            
        }
    }
}
