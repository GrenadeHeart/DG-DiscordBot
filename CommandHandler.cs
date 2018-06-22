using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace DGBot
{
    /// <summary>
    /// Handles the commands given in the guild/channel.
    /// </summary>
    public class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService service;
        BotConsole console;

        /// <summary>
        /// Initalizes the Command Handling Service.
        /// </summary>
        /// <param name="client">The bot.</param>
        /// <returns></returns>
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            console = new BotConsole(); // Creates a new BotConsole.
            _client = client; // Assigns the class variable "_client"  to the local variable "client".
            service = new CommandService(); // Creates a new command service.
            await service.AddModulesAsync(Assembly.GetEntryAssembly()); // I'n not sure what this does.
            _client.MessageReceived += HandleCommandAsync; // As far as i think: Everytime a message is received, HandeCommandAsync is invoked with socketMsg being the message.
        }

        /// <summary>
        /// Handles the given command.
        /// </summary>
        /// <param name="s">The command.</param>
        /// <returns></returns>
        private async Task HandleCommandAsync(SocketMessage socketMsg)
        {
            var userMsg = socketMsg as SocketUserMessage; // Sets 's' to msg as a "SocketUserMessage".
            if (userMsg == null) return; // If message is null then returns to the start of the task.
            var context = new SocketCommandContext(_client, userMsg); // Create a local context.
            if (context.User.IsBot) return; // If message is by a bot then returns to the start of the task.

            int argPos = 0;
            if (userMsg.HasStringPrefix(Program.DefPrefix, ref argPos) // Checks if the command is actually a command. (contains '/' at start of message).
                || userMsg.HasMentionPrefix(_client.CurrentUser, ref argPos) // OR the user @mentions the bot.
                || userMsg.HasStringPrefix(Program.Prefix, ref argPos)) // OR the the user uses a custom prefix.
            {
                var result = await service.ExecuteAsync(context, argPos); // I'm not sure what this means.
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) // If the result isn't a success AND isn't an unknown command.
                {
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Command     {result.ErrorReason}"); // Writes the error on the Console.
                    await context.Channel.SendMessageAsync(context.Message.Author.Mention + " " + result.ErrorReason); // Sends the error in the channel.
                }
                else if (!result.IsSuccess && result.Error == CommandError.UnknownCommand) // If the result isn't a success AND IS an unknown command.
                {
                    await context.Channel.SendMessageAsync(context.Message.Author.Mention + " Unknown command. Type /help for command list."); // Send the error in the channel.
                }
            }
        }
    }
}
