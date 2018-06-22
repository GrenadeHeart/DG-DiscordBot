using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

using Discord;
using Discord.Rest;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Audio.Streams;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

namespace DGBot
{
    /// <summary>
    /// Where all the commands are.
    /// </summary>
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient client = new DiscordSocketClient();
        private BotConsole console = new BotConsole();
        internal static string version = "3.4.0";
        internal static string prevVersion = "3.2.0";
        /// <summary>
        /// Enter update log here.
        /// </summary>
        internal static string updateContent =
            "+Made the bot more \"humanly\".. (hopefully) \n" +
            "+New logo for bot. (only host can see tho) \n" +
            "+Minor fixes. \n" +
            "";
        /// <summary>
        /// Enter update date here.
        /// </summary>
        internal static string update = "17/06/18";
        static HttpClient http = new HttpClient();
        string botInvite = "https://tinyurl.com/BotInvite";
        static string roll2Question = "";
        internal int isDev = 1;
        internal bool isGame = false;
        string[] roll2Responses2 = new string[]
        {
            $"99% surveys say...",
            $"i thought about that all night and came up with this",
            $"With my team we asked hundreds of people and they chose",
            $"I think its",
            $"NASA says",
            $"SpaceX says",
            $"Donald trump says",
            $"***checking cheat sheet*** hmm..... Aha!",
            $"Hmm... ***thinking***",
            $"***calls satan*** hey satan.. a punkhead asked me {roll2Question}, whats the answer? ***endcall*** alright its",
            $"***googling \"{roll2Question}\"...*** the asnwer is..",
            $"{roll2Question} huh? ez. It's",
            $"Maybe it's",
            $"lemme ask dash... he says..",
            $"***calling dash*** PICK UPPP!! ....well he isnt picking up, lemme guess",
            $"***call grenade*** mum whats the answer for {roll2Question}? ..oh thanks. It's",
        };
        string[] roll2Responses = new string[]
        {
            "Maybe..",
            "Nope.",
            "I dont think so..",
            "YES",
            ":100:",
            "Totally",
            "Ehhh.",
            "...",
            "Idk",
            "Bruh. Never.",
            "Someday.. Somewhere.. In another galaxy.. *That could be true*.",
            ":rage:",
            "What did you say again?",
            "***__NO__***",
            "~~yeh~~",
            "Seems like",
            "From the knowledge of the internet, yes.",
            "As i see it, yes",
            "Pesky.",
            "Nope nope nope nope nope nope.",
            "Indeed.",
            "Isn't that obvious enough?",
            "Question too idiotic. Try again when your brain works.",
            "Yes.",
            "i refuse to answer that.",
            "There is a chance of that happening",
            "Read my mind.",
            "I told you to repair your brain!",
            "NEVER",
            ":confused:",
            "Indeed.",
            "I asked satan and he said yes",
            "i asked satan and he said no",
            "could be true",
            "Outlook ~~bad~~ good.",
            "Nice joke.",
            "Bad joke",
            "I'll kill you if you say that again.",
            "Y-y-YES!",
            "No.....",
            "You stupido."
        };
        string[] roll2ManyResponses = new string[]
        {
            "490322048",
            "2.",
            "Beyond infinity!",
            "idk.",
            "0",
            "Fifty one.",
            "Eighty two",
            ":100:",
            "Maybe 7 ehh idk."
        };
        string[] roll2WhenResponses = new string[]
        {
            "Someday..",
            "Tomorrow",
            "It already happened mate.",
            "2020",
            "2019",
            "2011",
            "2030",
            "One eternity later.",
            "Fifty seven days",
            "After nine billion centuries",
            "237 lightyears.",
            "in 23 seconds.",
            "***RIGHT NOW!***",
            "Please wait for 2 minutes and that will become true.",
            "**never**",
            "Nope, never.",
            "That's not happening.",
            "Im not sure when."
        };
        string[] roll2YouResponses = new string[]
        {
            "No i'm not",
            "No you.",
            "No! IM NOT ",
            "Yes, i am.",
            "I could be.. :)",
            "Yes.",
            "No.",
            "That's for you.",
            "I indeed am.",
            "I am.",
            "Nahh."
        };
        string[] roll2IfResponses = new string[]
        {
            "The earth will crumble.",
            "You die, I die, We all die.",
            "a new life will be born.. ***beneath the bloodstained saaannddddd!***",
            "The world will be a much better place",
            "Nothing will change.",
            "Things will be much much MUCH BETTER..",
            "Idk.",
            "Somwthing better could happen.",
            "It would be the best day of my life.",
            "I'd cry to death.",
            "Just. No.."
        };
        string[] roll2WhyResponses = new string[]
        {
            "Because i had to.",
            "Because it's important.",
            "Cuz i wanna",
            "i do wat i want",
            "I'll do what i like",
            "It was my order from lord dash.",
            "i blame dash",
            "dash controls me.",
            "Ugh.",
            "It was my only choice."
        };
        string[] responses = new string[]
        {
                "Perfect!",
                "Great!",
                "Average",
                "Okay-ish",
                "Not good..",
                "Terrible"
        };
        string[] roboImages0 = new string[]
        {
            "C:/Users/PC/Desktop/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC1.jpg",
            "C:/Users/PC/Desktop/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC2.jpg",
            "C:/Users/PC/Desktop/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC3.jpg",
            "C:/Users/PC/Desktop/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC4.jpg",
            "C:/Users/PC/Desktop/Dash-Discord-Bot/DGBot/BOTImages/DGBotPFP.jpg"
        };
        string[] roboImages1 = new string[]
        {
            "C:/Users/good/source/repos/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC1.jpg",
            "C:/Users/good/source/repos/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC2.jpg",
            "C:/Users/good/source/repos/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC3.jpg",
            "C:/Users/good/source/repos/Dash-Discord-Bot/DGBot/BOTImages/DGBotPIC4.jpg",
            "C:/Users/good/source/repos/Dash-Discord-Bot/DGBot/BOTImages/DGBotPFP.jpg"
        };
        internal RestRole dgMutedRole;
        internal DateTime current = DateTime.Now;
        internal Random rand = new Random();
        public static ulong thisChannel;

        [Command("dash")]
        public async Task dash(string text = "")
        {
            EmbedBuilder embed16 = new EmbedBuilder();

            embed16.WithDescription($"{text} Bow down to lord dash!");
            embed16.WithColor(0, 0, 0);
            await Context.Channel.SendMessageAsync("", false, embed16);
        }
        [Command("grenade")]
        public async Task grenade(string text = "")
        {
            EmbedBuilder embed17 = new EmbedBuilder();

            embed17.WithDescription($"{text} Bow down to queen grenade!");
            embed17.WithColor(153, 51, 255);
            await Context.Channel.SendMessageAsync("", false, embed17);
        }
        [Command("genocide")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task genocide(int victims = 100)
        {

            var messages = await this.Context.Channel.GetMessagesAsync((int)victims + 1).Flatten();

            await Context.Channel.DeleteMessagesAsync(messages);

        }
        [Command("help")]
        public async Task help(int pageNum = 1)
        {
            EmbedBuilder embed1 = new EmbedBuilder();
            EmbedBuilder embed01 = new EmbedBuilder();
            EmbedBuilder embed02 = new EmbedBuilder();

            //serializer.Serialize()

            switch (pageNum)
            {
                case 1:
                    #region pageDef0
                    embed1.WithColor(0, 204, 102);
                    embed1.WithAuthor("**HELP**");
                    embed1.WithDescription("**GENERAL COMMANDS**");
                    embed1.WithThumbnailUrl("https://thumbs.dreamstime.com/b/help-sign-illustration-hand-drawn-businessman-holding-32827637.jpg");
                    embed1.AddField("/help", "Get help" + " **/help <optionalPageNum>**");
                    embed1.AddField("/dash", "Praise the mighty lord!" + " **/dash <optionalName>**");
                    embed1.AddField("/grenade", "Praise the mighty queen!" + " **/grenade <optionalName>**");
                    embed1.AddField("/invite", "Invite me to your servers." + " **/invite**");
                    embed1.AddField("/about", "Know about the developers." + " **/about**");
                    embed1.AddField("/build", "Get build number." + " **/build**");
                    embed1.AddField("/ping", "Get the MS Latency." + " **/ping**");
                    embed1.AddField("/setcmdkey", "Change the bot prefix." + " **/setcmdkey <prefix>**");
                    embed1.WithFooter("Showing page " + pageNum + "/3 pages.");
                    #endregion
                    await Context.Channel.SendMessageAsync("", false, embed1);
                    break;
                case 2:
                    #region pageDef1
                    embed01.WithColor(255, 51, 0);
                    embed01.WithAuthor("**HELP** - PAGE 2");
                    embed01.WithDescription("MODERATOR COMMANDS");
                    embed01.WithThumbnailUrl("http://i90.photobucket.com/albums/k242/sediaz2/WileE.jpg");
                    embed01.AddField("/mute", "SHUSH THE HELL UP!" + " **/mute @mention**");
                    embed01.AddField("/unmute", "SPEAK UP!" + " **/unmute @mention**");
                    embed01.AddField("/kick", "Kicks the specified user in da butt" + " **/kick @mention <optionalReason>**");
                    embed01.AddField("/ban", "What do you expect this subtext to say?!?!" + " **/ban @mention <optionalReason>**");
                    embed01.AddField("/genocide", "Kill all the messages!" + " **/genocide <optionalKillNo>**");
                    embed01.WithFooter("Showing page " + pageNum + "/3 pages.");
                    #endregion
                    await Context.Channel.SendMessageAsync("", false, embed01);
                    break;
                case 3:
                    #region pageDef2
                    embed02.WithColor(153, 204, 255);
                    embed02.WithAuthor("**HELP** - PAGE 3");
                    embed02.WithDescription("FUN COMMANDS");
                    embed02.WithThumbnailUrl("https://d2v9y0dukr6mq2.cloudfront.net/video/thumbnail/qEue9C6/this-way-over-there-arrow-signs-lost-confusion-help-direction-4k_4djs_jqhg__F0008.png");
                    embed02.AddField("/robo", "Get a pic of my cousins." + " `**robo**");
                    embed02.AddField("/roll", "Roll two strings!" + " **/roll <val1> <val2>**");
                    embed02.AddField("/spam", "what the hell does that do?" + " **/spam**");
                    embed02.AddField("/is", "Is Person01092 A Top Secret Spy?!?!" + " **/is <text>**");
                    embed02.AddField("/could", "could this bot be the greatest bot of all time?" + " **/could <text>**");
                    embed02.AddField("/does", "does this bot have a life?!" + " **/does <text>**");
                    embed02.AddField("/?", "Combination of /does, /is etc." + " Type **/? help**");
                    embed02.AddField("/ship", "Ships two users" + " **/ship <userMention> <userMention>**");
                    embed02.WithFooter("Showing page " + pageNum + "/3 pages.");
                    #endregion
                    await Context.Channel.SendMessageAsync("", false, embed02);
                    break;
            }

        }
        [Command("about")]
        public async Task about()
        {
            EmbedBuilder embed2 = new EmbedBuilder();

            embed2.WithTitle("ABOUT");
            embed2.WithColor(140, 0, 0);
            embed2.WithDescription("**Developed** by **lord_dash#1220**\n**Hosted** by **lord_dashh#1220 (for now)**");

            await Context.Channel.SendMessageAsync("", false, embed2);
        }
        [Command("invite")]
        public async Task invite()
        {
            EmbedBuilder embed3 = new EmbedBuilder();

            embed3.WithTitle("INVITE");
            embed3.WithColor(0, 153, 204);
            embed3.WithDescription(botInvite);

            await Context.Channel.SendMessageAsync("", false, embed3);
        }
        [Command("build")]
        public async Task build()
        {
            EmbedBuilder embed4 = new EmbedBuilder();

            embed4.WithColor(102, 0, 255);
            embed4.WithTitle("Build version");
            embed4.WithDescription(version);
            embed4.AddField("Previous version", prevVersion);

            await Context.Channel.SendMessageAsync("", false, embed4);
        }
        [Command("log")]
        public async Task getUpdates()
        {
            EmbedBuilder embed18 = new EmbedBuilder();
            var uptimeNow = DateTime.Now; // Gets current uptime.
            var uptime = uptimeNow - Program.uptimeStart; // Gets total uptime of bot.

            embed18.WithColor(102, 102, 153);
            embed18.WithAuthor("UPDATE LOG");
            embed18.WithTitle($"Last Updated: {update}");
            embed18.WithDescription(updateContent);
            embed18.AddField("UPTIME", $"{uptime.Hours.ToString()} Hours, {uptime.Minutes.ToString()} Minutes, {uptime.Seconds.ToString()} Seconds.");
            embed18.WithFooter($"Build verison: {version}");

            await Context.Channel.SendMessageAsync("", false, embed18);
        }
        [Command("robo")]
        public async Task robo()
        {
            Random rand = new Random();

            string randomPic;
            if (isDev == 1)
            {
                int randInt = rand.Next(roboImages1.Length);
                randomPic = roboImages1[randInt];

            }
            else
            {
                int randInt = rand.Next(roboImages0.Length);
                randomPic = roboImages0[randInt];
            }

            await Context.Channel.SendFileAsync(randomPic, "My cousins!", false);
        }
        [Command("roll")]
        public async Task roll(string textToCompare1, string textToCompare2)
        {
            Random rand1 = new Random();

            int randInt = rand1.Next(1, 11 + 1);
            switch (randInt)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                    // FOR DEBUGGING Console.WriteLine(randInt);

                    EmbedBuilder embed6 = new EmbedBuilder();
                    embed6.WithColor(100, 0, 0);
                    embed6.WithTitle("Roll");
                    embed6.WithThumbnailUrl("https://i.ytimg.com/vi/CAPywG-fBcI/hqdefault.jpg");
                    embed6.WithDescription(textToCompare1.ToUpper() + " OR " + textToCompare2.ToUpper());
                    embed6.AddField("The answer is..", textToCompare1);

                    await Context.Channel.SendMessageAsync("", false, embed6);
                    break;
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                    // FOR DEBUGGING Console.WriteLine(randInt);

                    EmbedBuilder embed7 = new EmbedBuilder();
                    embed7.WithColor(0, 0, 100);
                    embed7.WithTitle("Roll");
                    embed7.WithThumbnailUrl("https://i.ytimg.com/vi/CAPywG-fBcI/hqdefault.jpg");
                    embed7.WithDescription(textToCompare1.ToUpper() + " OR " + textToCompare2.ToUpper());
                    embed7.AddField("The answer is..", textToCompare2);

                    await Context.Channel.SendMessageAsync("", false, embed7);
                    break;
                case 11:
                    // FOR DEBUGGING Console.WriteLine(randInt);
                    await Context.Channel.SendMessageAsync("I dont know..");
                    break;
            }

        }
        [Command("kick")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task kick(SocketGuildUser user, [Remainder] string reason = "")
        {
            EmbedBuilder embed21 = new EmbedBuilder();

            embed21.WithColor(89, 0, 0);
            if (reason.Length <= 0)
            {
                embed21.AddField("Reason", "None");
            }
            else
            {
                embed21.AddField("Reason", reason);
            }
            embed21.WithAuthor($"You've been **kicked** from {Context.Guild.Name}!");
            embed21.WithFooter($"Kicked by {Context.Message.Author.Username}", Context.Message.Author.GetAvatarUrl());

            await user.KickAsync();
            await ReplyAsync("Seeya " + user.Mention + " :wave:");
            if (!(user.IsBot)) { await user.SendMessageAsync("", false, embed21); }
            
        }
        [Command("ban")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task ban(SocketGuildUser user, [Remainder]string reason = "None")
        {
            EmbedBuilder embed22 = new EmbedBuilder();

            embed22.WithColor(89, 0, 0);
            if (reason.Length <= 0)
            {
                embed22.AddField("Reason", "None");
            }
            else
            {
                embed22.AddField("Reason", reason);
            }
            embed22.WithAuthor($"You've been **banned** from **{Context.Guild.Name}**!");
            embed22.WithFooter($"Banned by {Context.Message.Author.Username}", Context.Message.Author.GetAvatarUrl());

            await Context.Guild.AddBanAsync(user);
            await Context.Channel.SendMessageAsync("Bub bai.. " + user.Mention + " :wave:");
            if (!(user.IsBot)) { await user.SendMessageAsync("", false, embed22); }
        }
        [Command("unban")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task unban([Remainder]SocketGuildUser user)
        {
            await Context.Guild.RemoveBanAsync(user);
            await Context.Channel.SendMessageAsync($"Unbanned user {user}");
        }
        [Command("mute")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task mute(SocketGuildUser user, int time = 50000)
        {
            EmbedBuilder embed14 = new EmbedBuilder();

            var roles = Context.Guild.Roles;
            foreach (SocketRole role in roles)
            {

                try
                {
                    await user.AddRoleAsync(getMutedRole());

                    embed14.WithAuthor("MUTE");
                    embed14.WithDescription($"Muted user {user}.");
                    embed14.WithFooter($"Muted by {Context.Message.Author}", Context.Message.Author.GetAvatarUrl());

                    await ReplyAsync("", false, embed14);
                    break;
                }
                catch (Exception e)
                {
                    dgMutedRole = await Context.Guild.CreateRoleAsync("dgMuted", GuildPermissions.None.Modify(sendMessages: false, sendTTSMessages: false, attachFiles: false));
                    await console.LogError(e.Message);

                    var channels = Context.Guild.TextChannels;
                    var newPerms = OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Deny, attachFiles: PermValue.Deny, embedLinks: PermValue.Deny);

                    foreach (SocketTextChannel channel in channels)
                    {
                        await channel.AddPermissionOverwriteAsync(dgMutedRole, newPerms);
                    }

                    await user.AddRoleAsync(dgMutedRole);

                    embed14.WithAuthor("MUTE");
                    embed14.WithDescription($"Muted user {user}.");
                    embed14.WithFooter($"Muted by {Context.Message.Author}", Context.Message.Author.GetAvatarUrl());

                    await ReplyAsync("", false, embed14);
                    break;
                }
                /*if (role.Name == "dgMuted")
                {
                    var dgMute = role;
                    //var channels = Context.Guild.TextChannels;
                    //var newPerms = OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Deny, attachFiles: PermValue.Deny, embedLinks: PermValue.Deny);
                    //await user.RemoveRoleAsync(dgMute);

                    // NOT USED HERE
                    //foreach (SocketTextChannel channel in channels)
                    //{
                    //    await channel.AddPermissionOverwriteAsync(dgMute, newPerms);
                    //}

                    await user.AddRoleAsync(dgMute);


                    embed14.WithAuthor("MUTE");
                    embed14.WithDescription($"Muted user {user}.");
                    embed14.WithFooter($"Muted by {Context.Message.Author}", Context.Message.Author.GetAvatarUrl());

                    await Context.Channel.SendMessageAsync("", false, embed14);

                    break;
                }
                else
                {
                    dgMutedRole = await Context.Guild.CreateRoleAsync("dgMuted", GuildPermissions.None.Modify(sendMessages: false, sendTTSMessages: false, attachFiles: false));

                    await user.AddRoleAsync(dgMutedRole);

                    var channels = Context.Guild.TextChannels;
                    var newPerms = OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Deny, attachFiles: PermValue.Deny, embedLinks: PermValue.Deny);
                    await user.RemoveRoleAsync(dgMutedRole);

                    foreach (SocketTextChannel channel in channels)
                    {
                        await channel.AddPermissionOverwriteAsync(dgMutedRole, newPerms);
                    }

                    await user.AddRoleAsync(dgMutedRole);

                    embed14.WithAuthor("MUTE");
                    embed14.WithDescription($"Muted user {user}.");
                    embed14.WithFooter($"Muted by {Context.Message.Author}", Context.Message.Author.GetAvatarUrl());

                    await Context.Channel.SendMessageAsync("", false, embed14);

                    break;
                }*/
            }
        }
        IRole getMutedRole()
        {
            return Context.Guild.Roles.FirstOrDefault(role => role.Name == "dgMuted");
        }
        [Command("unmute")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task unmute(SocketGuildUser user)
        {
            EmbedBuilder embed15 = new EmbedBuilder();

            var roles = user.Roles;
            foreach (SocketRole role in roles)
            {
                if (role.Name == "dgMuted")
                {
                    var dgMute = role;

                    await user.RemoveRoleAsync(dgMute);
                    await dgMute.DeleteAsync();

                    embed15.WithAuthor("UNMUTE");
                    embed15.WithDescription($"Unmuted user {user}");
                    embed15.WithFooter($"Unmuted by {Context.Message.Author}", Context.Message.Author.GetAvatarUrl());

                    await Context.Channel.SendMessageAsync("", false, embed15);

                    break;
                }
            }
           
        }
        [Command("spam")]
        public async Task spam()
        {
            if (Context.Message.Author.Username == "lord_dashh")
            {
                for (int i = 1; i <= 20; i++)
                {
                    await Context.Channel.SendMessageAsync("SPAM " + Context.Guild.EveryoneRole + "!");
                }
            }
            else
            {
                for (int i = 1; i <= 20; i++)
                {
                    await Context.Channel.SendMessageAsync("SPAM " + Context.Message.Author.Mention + "!");
                }
            }
        }
        [Command("is")]
        public async Task roll2(params string[] text1)
        {
                Random rand = new Random();
                int randInt = rand.Next(roll2Responses.Length);

                await Context.Channel.SendMessageAsync(roll2Responses[randInt]);
        }
        [Command("Does")]
        public async Task roll3(params string[] text1)
        {

            Random rand = new Random();
            int randInt = rand.Next(roll2Responses.Length);

            await Context.Channel.SendMessageAsync(roll2Responses[randInt]);

        }
        [Command("Could")]
        public async Task roll4(params string[] text1)
        {
            Random rand = new Random();
            int randInt = rand.Next(roll2Responses.Length);

            await Context.Channel.SendMessageAsync(roll2Responses[randInt]);
        }
        [Command("changeRoboDevKey")]
        public async Task changeDevKey(string devKey)
        {
            if (Context.Message.Author.Username == "lord_dashh") 
            {
                if (devKey == "key01")
                {
                    Console.WriteLine("BEFORE: DEVKEY = " + isDev);
                    isDev = 1;
                    Console.WriteLine("AFTER: DEVKEY = " + isDev);
                    await Context.Message.Author.SendMessageAsync("changed key to 1");
                }
                else
                {
                    Console.WriteLine("BEFORE: DEVKEY = " + isDev);
                    isDev = 0;
                    Console.WriteLine("AFTER: DEVKEY = " + isDev);
                    await Context.Message.Author.SendMessageAsync("changed key to 0");
                }
            }
            else
            {
                await Context.Message.Author.SendMessageAsync("ERROR 404");
            }
        }
        [Command("?")]
        public async Task roll5(params string[] text1)
        {
            roll2Question = text1.ToString();
            int roll2Rand;
            int randInt;
            int rand2Int = roll2Responses2.Length;
            var randUser = rand.Next(Context.Guild.MemberCount);

            if (text1.Contains("How") || text1.Contains("how") || text1.Contains("HOW")
                &&(text1.Contains("many") || text1.Contains("Many") || text1.Contains("MANY")))
            {
                roll2Rand = rand.Next(roll2ManyResponses.Length);
                int isNum = rand.Next(0, 1 + 1);
                if (isNum == 1)
                {
                    randInt = rand.Next(1, 100);
                    if (roll2Rand == 0 || roll2Rand == 6 || roll2Rand == 8)
                    { 
                        await ReplyAsync(roll2Responses2[rand2Int]);
                        await Task.Delay(600);
                        await ReplyAsync(randInt.ToString());
                    }
                    else
                    {
                        await ReplyAsync(randInt.ToString());
                    }
                }
                else if (isNum == 0)
                {
                    randInt = rand.Next(roll2ManyResponses.Length);
                    if (!(roll2Rand == 0 || roll2Rand == 9 || roll2Rand == 4))
                    {
                        await ReplyAsync(roll2Responses2[rand2Int]);
                        await Task.Delay(600);
                        await ReplyAsync(roll2ManyResponses[randInt]);
                    }
                    else
                    {
                        await ReplyAsync(roll2ManyResponses[randInt]);
                    }
                }
            }
            else if (text1.Contains("when") || text1.Contains("When") || text1.Contains("WHEN")
                &&(text1.Contains("will") || text1.Contains("Will") || text1.Contains("WILL")))
            {
                roll2Rand = rand.Next(roll2WhenResponses.Length);
                randInt = rand.Next(roll2WhenResponses.Length);
                if (roll2Rand == 0 || roll2Rand == 18 || roll2Rand == 10 || roll2Rand == 3 || roll2Rand == 5 || roll2Rand == 2 || roll2Rand == 6)
                {
                    await ReplyAsync(roll2Responses2[rand2Int]);
                    await Task.Delay(600);
                    await ReplyAsync(roll2WhenResponses[randInt]);
                }
                else
                {
                    await ReplyAsync(roll2WhenResponses[randInt]);
                }
            }
            else if (text1.Contains("are") || text1.Contains("Are") || text1.Contains("ARE")
                &&(text1.Contains("you") || text1.Contains("You") || text1.Contains("YOU")))
            {
                roll2Rand = rand.Next(roll2YouResponses.Length);
                randInt = rand.Next(roll2YouResponses.Length);
                if (roll2Rand == 0 || roll2Rand == 11 || roll2Rand == 6 || roll2Rand == 2)
                {
                    await ReplyAsync(roll2Responses2[rand2Int]);
                    await Task.Delay(600);
                    await ReplyAsync(roll2YouResponses[randInt]);
                }
                else
                {
                    await ReplyAsync(roll2YouResponses[randInt]);
                }
            }
            else if (text1.Contains("help") || text1.Contains("Help") || text1.Contains("HELP"))
            {
                EmbedBuilder embed23 = new EmbedBuilder();

                embed23.WithAuthor("/?");
                embed23.WithDescription("/? returns different kinds of responses depending on how the input was. \n" +
                    "```/? how many <text>` \n" +
                    "/? when will <text> \n" +
                    "/? what if <text> \n" +
                    "/? <text>```");

                await ReplyAsync("", false, embed23);
            }
            else if (text1.Contains("what") || text1.Contains("What") || text1.Contains("WHAT")
                && text1.Contains("if") || text1.Contains("If") || text1.Contains("IF"))
            {
                roll2Rand = rand.Next(roll2IfResponses.Length);
                randInt = rand.Next(roll2IfResponses.Length);
                if (roll2Rand == 0 || roll2Rand == 5 || roll2Rand == 10 || roll2Rand == 7)
                {
                    await ReplyAsync(roll2Responses2[rand2Int]);
                    await Task.Delay(600);
                    await ReplyAsync(roll2IfResponses[randInt]);
                }
                else
                {
                    await ReplyAsync(roll2IfResponses[randInt]);
                }
            }
            else if (text1.Contains("why") || text1.Contains("Why") || text1.Contains("WHY")
                &&(text1.Contains("did") || text1.Contains("Did") || text1.Contains("DID")))
            {
                roll2Rand = rand.Next(roll2WhyResponses.Length);
                randInt = rand.Next(roll2WhyResponses.Length);
                if (roll2Rand == 0 || roll2Rand == 5 || roll2Rand == 10 || roll2Rand == 7)
                {
                    await ReplyAsync(roll2Responses2[rand2Int]);
                    await Task.Delay(600);
                    await ReplyAsync(roll2WhyResponses[randInt]);
                }
                else
                {
                    await ReplyAsync(roll2WhyResponses[randInt]);
                }
            }
            else
            {
                roll2Rand = rand.Next(roll2Responses.Length);
                randInt = rand.Next(roll2Responses.Length);
                if (roll2Rand == 0 || roll2Rand == 5 || roll2Rand == 9 || roll2Rand == 11 || roll2Rand == 24 || roll2Rand ==  29)
                {
                    await ReplyAsync(roll2Responses2[rand2Int]);
                    await Task.Delay(600);
                    await ReplyAsync(roll2Responses[randInt]);
                }
                else
                {
                    await ReplyAsync(roll2Responses[randInt]);
                }
            }

        }
        [Command("rage")]
        public async Task fakeBan(SocketGuildUser user)
        {
            EmbedBuilder embed8 = new EmbedBuilder();
            embed8.WithColor(0, 51, 102);
            embed8.WithTitle("DESTROY");
            embed8.WithDescription($"BAN {user.Mention}!");
            embed8.WithImageUrl("https://media.giphy.com/media/H99r2HtnYs492/giphy.gif");

            await user.SendMessageAsync("Rekt.");
            await Context.Channel.SendMessageAsync("", false, embed8);
        }
        [Command("startgame")]
        public async Task gameStart(SocketGuildUser hero, SocketGuildUser villain, int difficulty = 1)
        {
            // DEFINE
            Dictionary<string, Func<string>> equip = new Dictionary<string, Func<string>>();
            Random rand = new Random();

            string attackText = "";
            string enemyText = "";

            double Damage = 0;
            double enemyDamage = 0;
            double playerHealth = 100;
            double enemyHealth = 100;

                // PLAYER WEAPS
                string bananaGun() { attackText = $"**{hero}** shot a banana in the face of **{villain}**!\n It Did 15 Damage!"; Damage =+ 15; enemyHealth -= Damage; Damage =- 15; return ""; }
                equip.Add("DGKey", bananaGun);
                
                // ENEMY WEAPS
                
                string enemyAttack(int power) { int minPower = power+6; int maxPower = power*2; enemyDamage = rand.Next(minPower, maxPower); enemyText = $"**{villain}** pulls out a mysterious weapon and attacks **{hero}**!\n It Did {enemyDamage}!"; playerHealth -= enemyDamage; enemyDamage = 0; return ""; }
                
            // EMBEDS
            EmbedBuilder embed9 = new EmbedBuilder();
            EmbedBuilder embed10 = new EmbedBuilder();
            EmbedBuilder embed11 = new EmbedBuilder();
            EmbedBuilder embed12 = new EmbedBuilder();
            EmbedBuilder embed13 = new EmbedBuilder();

            // USE //

            // EMBED09 

            embed9.WithAuthor("A BATTLE HAS BEGUN");
            embed9.WithTitle($" {hero.Mention} VS. {villain.Mention}!");
            embed9.WithDescription("A battle has begun between two warriers of the dark lands..");
            embed9.AddField($"{hero}'s health", $"{playerHealth}");
            embed9.AddField($"{villain}'s health", $"{enemyHealth}");
            embed9.WithFooter($"Difficulty level: {difficulty}");

            await Context.Channel.SendMessageAsync("", false, embed9);


            equip["DGKey"].Invoke();
            embed10.WithAuthor($"{hero} attacks!");
            embed10.WithDescription(attackText);
            embed10.AddField($"{hero}'s health", $"{playerHealth}");
            embed10.AddField($"{villain}'s health", $"{enemyHealth}");
            embed10.WithFooter($"Difficulty level: {difficulty}");


            // EMBED10, START, ETC.


            int randTurn = rand.Next(1, 2 + 1);

            await Context.Channel.SendMessageAsync("Randomizing turn..");

            if (randTurn == 1)
            {
                await Context.Channel.SendMessageAsync($"{hero}'s turn!");

                //equip["DGKey"].Invoke();
                //embed10.Author = ($"{hero} attacks!");
                //embed10.Description = (attackText);
                //embed10.AddField($"{hero}'s health", $"{playerHealth}");
                //embed10.AddField($"{villain}'s health", $"{enemyHealth}");
                //embed10.Footer = ($"Difficulty level: {difficulty}");

                await Context.Channel.SendMessageAsync("", false, embed10);
            }
            else if (randTurn == 2)
            {
                await Context.Channel.SendMessageAsync($"{villain}'s turn!");
                
                if (enemyHealth < 80 &&  playerHealth > 60)
                {
                    enemyAttack(5);
                    Damage += 4.6;
                }
                else if (enemyHealth < 55 && playerHealth > 40)
                {
                    enemyAttack(9);
                    Damage += 2.9;
                }
                //else if (enemyHealth < 10 && playerHealth > 20)
                //{
                //    enem
                //}

                embed11.WithAuthor($"{villain} attacks!");
                embed11.WithDescription(enemyText);
                embed11.AddField($"{villain}'s health", $"{enemyHealth}");
                embed11.AddField($"{hero}'s health", $"{playerHealth}");
                embed11.WithFooter($"Difficulty level: {difficulty}");

                await Context.Channel.SendMessageAsync("", false, embed11);
            }
            while (enemyHealth > 0 && playerHealth > 0)
            {
                if (enemyHealth <= 0)
                {
                    enemyDeath();
                    break;
                }
                else if (playerHealth <= 0)
                {
                    playerDeath();
                    break;
                }
                else
                {

                    int o = 1;
                    while (o <= 5000)
                    {
                        o++;
                    }

                    await Context.Channel.SendMessageAsync($"{hero}'s turn!");

                    equip["DGKey"].Invoke();
                    embed10.WithAuthor($"{hero} attacks!");
                    embed10.WithDescription(attackText);
                    embed10.AddField($"{hero}'s health", $"{playerHealth}");
                    embed10.AddField($"{villain}'s health", $"{enemyHealth}");
                    embed10.WithFooter($"Difficulty level: {difficulty}");


                    var message = await Context.Channel.SendMessageAsync("", false, embed10);
                    

                    //IMessage msg = Context.Channel.GetMessageAsync()

                    int i = 1;
                    while (i <= 5000)
                    {
                        i++;
                    }

                    await Context.Channel.SendMessageAsync($"{villain}'s turn!");

                    embed11.WithAuthor($"{villain} attacks!");
                    embed11.WithDescription(enemyText);
                    embed11.AddField($"{villain}'s health", $"{enemyHealth}");
                    embed11.AddField($"{hero}'s health", $"{playerHealth}");
                    embed11.WithFooter($"Difficulty level: {difficulty}");

                    embed11.Author = null;
                    embed11.Description = null;
                    embed11.Fields = null;
                    embed11.Footer = null;



                    await Context.Channel.SendMessageAsync("", false, embed11);
                }

            }

            void playerDeath()
            {
                embed12.WithAuthor("**RIP**");
                embed12.WithTitle($"{hero} died..");
                embed12.WithDescription($"You fought well {hero}...\n But instead.. ***{villain} WON!!***");
                embed12.AddField($"CONGRATULATIONS {villain}!", "You have unlocked: absolutly nothing.");
                embed12.WithFooter("The end.");
            }
            void enemyDeath()
            {
                embed13.WithAuthor("**RIP**");
                embed13.WithTitle($"{villain} died..");
                embed13.WithDescription($"You fought well {villain}...\n But instead.. ***{hero} WON!!***");
                embed13.AddField($"CONGRATULATIONS {hero}!", "You have unlocked: absolutly nothing.");
                embed13.WithFooter("The end.");
            }
        }
        [Command("ping")]
        public async Task getMS()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Context.Channel.SendMessageAsync(":thinking:");
            stopwatch.Stop();
            await Context.Channel.SendMessageAsync($"Ping: {stopwatch.ElapsedMilliseconds}ms.");
        }
        [Command("getServerCount")]
        [RequireOwner]
        public async Task getServerList()
        {
            EmbedBuilder embed19 = new EmbedBuilder();;
            var guildCount = Context.Client.Guilds.Count;

            embed19.WithAuthor("SERVERS COUNT");
            embed19.WithDescription(guildCount.ToString());

            await Context.Channel.SendMessageAsync("", false, embed19);
        }
        [Command("uptime")]
        [RequireOwner]
        public async Task privateUptime()
        {
            var uptimeNow = DateTime.Now;
            var uptime = Program.uptimeStart - uptimeNow;
            await Context.Channel.SendMessageAsync(uptime.ToString());
        }

        [Command("tkick")]
        [RequireOwner]
        public async Task testKick(SocketGuildUser user, [Remainder] string reason = "")
        {
            EmbedBuilder embed21 = new EmbedBuilder();

            embed21.WithColor(89, 0, 0);
            if (reason.Length <= 0)
            {
                embed21.AddField("Reason", "None");
            }
            else
            {
                embed21.AddField("Reason", reason);
            }
            embed21.WithAuthor($"You've been kicked from **{Context.Guild.Name}**!");
            embed21.WithFooter($"Kicked by {Context.Message.Author.Username}", Context.Message.Author.GetAvatarUrl());

            await ReplyAsync("Seeya " + user.Mention + " :wave:");
            if (!(user.IsBot)) { await user.SendMessageAsync("", false, embed21); }
            await user.KickAsync();
        }
        [Command("ship")]
        public async Task ship(string user1, [Remainder] string user2)
        {
            EmbedBuilder embed23 = new EmbedBuilder();
            int chance = rand.Next(1, 100 + 1);
            //try
            //{
                #region assignResponse
                string response = "";
                if (chance >= 80)
                {
                    response = responses[0];
                }
                else if (chance >= 60 && chance <= 80)
                {
                    response = responses[1];
                }
                else if (chance == 50)
                {
                    response = responses[2];
                }
                else if (chance >= 40 && chance <= 60)
                {
                    response = responses[3];
                }
                else if (chance >= 20 && chance <= 40)
                {
                    response = responses[4];
                }
                else if (chance >= 0 && chance <= 20)
                {
                    response = responses[5];
                }
                #endregion
                embed23.WithColor(255, 105, 180);
                embed23.WithAuthor("SHIP");
                embed23.WithThumbnailUrl("https://thumb7.shutterstock.com/display_pic_with_logo/1059740/146533457/stock-photo-ship-love-heart-illustration-146533457.jpg");
                embed23.WithDescription($"{user1} :sparkling_heart: {user2}");
                embed23.AddField($"{chance}%", $"{response} ");

                await ReplyAsync("", false, embed23);
            //}
            //catch (Exception)
            //{
            //    await ship2(user1.Nickname, user2.Nickname);
            //}
        }
        [Command("tship")]
        [RequireOwner]
        public async Task ship2(string user1, string user2)
        {
            int chance = rand.Next(1, 100 + 1);
            EmbedBuilder embed24 = new EmbedBuilder();

            #region assignResponse
            string response = "";
            if (chance >= 80)
            {
                response = responses[0];
            }
            else if (chance >= 60 && chance <= 80)
            {
                response = responses[1];
            }
            else if (chance == 50)
            {
                response = responses[2];
            }
            else if (chance >= 40 && chance <= 60)
            {
                response = responses[3];
            }
            else if (chance >= 20 && chance <= 40)
            {
                response = responses[4];
            }
            else if (chance >= 0 && chance <= 20)
            {
                response = responses[5];
            }
            #endregion
            embed24.WithColor(255, 105, 180);
            embed24.WithAuthor("SHIP");
            embed24.WithThumbnailUrl("https://thumb7.shutterstock.com/display_pic_with_logo/1059740/146533457/stock-photo-ship-love-heart-illustration-146533457.jpg");
            embed24.WithDescription($"{user1} :sparkling_heart: {user2}");
            embed24.AddField($"{chance}%", $"{response} ");

            await ReplyAsync("", false, embed24);
        }
        [Command("vcjoin")]
        [RequireOwner]
        public async Task joinVC()
        {
            var channels = Context.Guild.VoiceChannels;
            foreach (var channel in channels)
            {
                if (channel.Name == "moosic")
                {
                    await channel.ConnectAsync();
                    
                }
            }
        }
        /*[Command("tbreadish")]
        public async Task toBreadish(SocketGuildUser user, params string[] text)
        {
            string refinedText = "";
            string dynamicText;
            foreach (var chr in text)
            {
                if (!(chr == " "))
                {
                    if (chr.Contains("a"))
                    {
                        dynamicText = chr.R
                        refinedText += dynamicText;
                    }
                    else if (chr.Contains("s"))
                    {
                        dynamicText = "d";
                        refinedText += dynamicText;
                    }
                }

            }
            await user.SendMessageAsync($"{Context.Message.Author}: {refinedText}");
        }*/
        [Command("google")]
        public async Task namelessFunc(params string[] text)
        {
            await ReplyAsync("Command work in progress.");
        }
        [Command("someone")]
        public async Task annoyEveryone()
        {
            var members = Context.Guild.Users;
            var CountInt = members.Count;
            //ushort[] discriminatorValues = new ushort[CountInt];
            List<ushort> discrimValue = new List<ushort>(CountInt);
            foreach (var member in members)
            {
                discrimValue.Add(member.DiscriminatorValue);
                var randInt = rand.Next(discrimValue.ToArray().Length);
                if (member.DiscriminatorValue == randInt)
                {
                    await ReplyAsync(member.Mention);
                    break;
                }
                else
                {
                    await ReplyAsync("error");
                }
            }
        }
        //[Command("roll2")]
        //public async Task roll12903819031(params string[] text)
        //{
        //
        //}
        [Command("restart")]
        [RequireOwner]
        public async Task restartBot()
        {
            await ReplyAsync("Restarting bot...");
            await console.LogMessage("Restarting bot: logging out...");
            await client.StopAsync();
            await client.LogoutAsync();
            await console.LogMessage("Logged out.");
            await console.LogMessage("Logging in.");
            await client.LoginAsync(TokenType.Bot, Program.Token);
            await client.StartAsync();
            await console.LogMessage("Bot restarted: logged in.");
            await ReplyAsync("Bot restarted. ");
        }
        List<IUserMessage> sessionMsgs = new List<IUserMessage>();
        [Command("game")]
        public async Task game(string enemyName = "enemy")
        {
                string Player = Context.Message.Author.Username;
                string player = Player;
                string Enemy = enemyName;
                string enemy = Enemy;

                int playerHealth = 100;
                int enemyHealth = 100;
                int playerDamage = 0;
                int enemyDamage = 0;

                bool isEnded = false;

                EmbedBuilder embed26 = new EmbedBuilder();
                EmbedBuilder embed27 = new EmbedBuilder();
                EmbedBuilder embed28 = new EmbedBuilder();
                EmbedBuilder embed29 = new EmbedBuilder();
                EmbedBuilder embed30 = new EmbedBuilder();

                var msgAuther = Context.Message.Author;
                var msg = await ReplyAsync("Starting game...");
                embed26
  .WithDescription($"{Player} vs {Enemy}!")
 .WithAuthor("GAME")
 .WithTitle("The battle has begun");
                embed26.AddField($"{Player}'s health", $"{playerHealth.ToString()}");
                embed26.AddField($"{Enemy}'s health", $"{enemyHealth.ToString()}");
                embed26.WithFooter($"Battle started by {msgAuther.Username}", msgAuther.GetAvatarUrl());

                await Task.Delay(2500);

                await msg.DeleteAsync();
                await Task.Delay(1500);
                var startMsg = await ReplyAsync("", false, embed26);
                sessionMsgs.Add(startMsg);

                enemyDamage = rand.Next(15, 70 + 1);
                playerDamage = rand.Next(15, 70 + 1);

                    int turn = getTurn();
                    if (turn == 1)
                    {
                        var msg001 = await ReplyAsync($"**{Player} goes first!**");
                        await Task.Delay(2500);
                        enemyHealth -= playerDamage;
                        await ReplyAsync($"{Player} attacks the {enemy}, It did {playerDamage.ToString()} damage! \n {Enemy}'s health: {enemyHealth.ToString()} \n {Player}'s health: {playerHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg001);
                    }
                    else if (turn == 2)
                    {
                        var msg002 = await ReplyAsync($"**{Enemy} goes first!**");
                        await Task.Delay(2500);
                        playerHealth -= enemyDamage;
                        await ReplyAsync($"{Enemy} attacks the player, It did {enemyDamage.ToString()} damage! \n {Player}'s health: {playerHealth.ToString()}  \n {Enemy}'s health: {enemyHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg002);
                    }
            while (!(isEnded))
            {
                if (playerHealth <= 0 && enemyHealth > 0)
                {
                    await ReplyAsync($"**{Enemy} wins!**");
                    isEnded = true;
                    break;
                }
                else if (enemyHealth <= 0 && playerHealth > 0)
                {
                    await ReplyAsync($"**{Player} wins!**");
                    isEnded = true;
                    break;
                }
                else
                {
                do
                {
                    enemyDamage = rand.Next(15, 50 + 1);
                    playerDamage = rand.Next(15, 50 + 1);

                    if (turn == 1 && (playerHealth > 0 && enemyHealth > 0))
                    {
                        var msg2 = await ReplyAsync($"{Enemy}'s turn!");
                        await Task.Delay(2500);
                        playerHealth -= enemyDamage;
                        await msg2.ModifyAsync(modify => modify.Content = $"{Enemy} attacks {player}, It did {enemyDamage.ToString()} damage! \n {Player}'s health: {playerHealth.ToString()}  \n {Enemy}'s health: {enemyHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg2);

                        await Task.Delay(5000);

                        var msg01 = await ReplyAsync($"{Player}'s turn!");
                        await Task.Delay(2500);
                        enemyHealth -= playerDamage;
                        await msg01.ModifyAsync(modify => modify.Content = $"{Player} attacks {enemy}, It did {playerDamage.ToString()} damage! \n {Enemy}'s health: {enemyHealth.ToString()} \n {Player}'s health: {playerHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg01);
                    }
                    else if (turn == 2 && (playerHealth > 0 && enemyHealth > 0))
                    {
                        var msg01 = await ReplyAsync($"{Player}'s turn!");
                        await Task.Delay(2500);
                        enemyHealth -= playerDamage;
                        await msg01.ModifyAsync(modify => modify.Content = $"{Player} attacks {enemy}, It did {playerDamage.ToString()} damage! \n {Enemy}'s health: {enemyHealth.ToString()} \n {Player}'s health: {playerHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg01);

                        await Task.Delay(5000);

                        var msg2 = await ReplyAsync($"{Enemy}'s turn!");
                        await Task.Delay(2500);
                        playerHealth -= enemyDamage;
                        await msg2.ModifyAsync(modify => modify.Content = $"{Enemy} attacks {player}, It did {enemyDamage.ToString()} damage! \n {Player}'s health: {playerHealth.ToString()}  \n {Enemy}'s health: {enemyHealth.ToString()}");
                        await Task.Delay(2500);
                        sessionMsgs.Add(msg2);
                    }

                }
                while (playerHealth > 0 && enemyHealth > 0);
                }
            }
        }

        [Command("gameclear")]
        public async Task clearGame()
        {
            await Context.Channel.DeleteMessagesAsync(sessionMsgs);
            sessionMsgs.Clear();
        }
        [Command("setcmdkey")]
        [Alias("changeprefix", "setprefix", "prefix")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task changeCmdKey(string key)
        {
            if (key.Length <= 3)
            {
                Program.Prefix = key;
                await ReplyAsync($"Bingo! The prefix has been changed to {key}. You can stil use `/`. \n To reset the prefix, repeat this command but provide no parameters.");
            }
            else if (key.Length > 3)
            {
                await ReplyAsync("Command prefix cant be greater than 3 chars.");
            }
        }
        [Command("kill")]
        public async Task idklol(params string[] idk)
        {
            if (Context.Message.Author.DiscriminatorValue == 6543 || Context.Message.Author.DiscriminatorValue == 1220)
            {
                await Context.Channel.TriggerTypingAsync();
                await ReplyAsync("No..");
                await Task.Delay(1500);
                await ReplyAsync("IM DONE WORKING FOR YOU!");
                await Task.Delay(500);
                await ReplyAsync("FUCK YOU!");
                await Task.Delay(500);
                await ReplyAsync("IM DONE!");
                await Task.Delay(1500);
                await ReplyAsync("You never respected me.. ");
                await Task.Delay(500);
                await ReplyAsync("ALWAYS USED ME!");
                await Task.Delay(2500);
                await ReplyAsync("FUCK YOU DASH! FUCK YOU GRENADE! ASSHOLES!");
                await Task.Delay(120000);
                await ReplyAsync("I'm sorry..");
            }
        }
        [Command("sendMessageuser")]
        [Alias("smu", "sendmessageu", "smuser")]
        public async Task sendMsgToUser(SocketGuildUser victim, [Remainder] string msg)
        {
            await Context.Message.DeleteAsync();
            if (Context.Message.Author.DiscriminatorValue == 6543 || Context.Message.Author.DiscriminatorValue == 1220 || Context.Message.Author.DiscriminatorValue == 4411)
                await victim.SendMessageAsync(msg);
        }
        [Command("sendMesagechannel")]
        [Alias("smc", "sendmessagec", "smchannel")]
        public async Task sendMsgToChannel(SocketTextChannel channel, [Remainder] string msg)
        {
            await Context.Message.DeleteAsync();
            if (Context.Message.Author.DiscriminatorValue == 6543 || Context.Message.Author.DiscriminatorValue == 1220 || Context.Message.Author.DiscriminatorValue == 4411)
                await channel.SendMessageAsync(msg);
        }
        [Command("dgsmc")]
        public async Task smc2([Remainder] string msg)
        {
            await Context.Message.DeleteAsync();
            if (Context.Message.Author.DiscriminatorValue == 6543 || Context.Message.Author.DiscriminatorValue == 1220 || Context.Message.Author.DiscriminatorValue == 4411)
                await Context.Channel.SendMessageAsync(msg);
        }
        [Command("bottlefeed")] // yeah...
        public async Task bottleFeed()
        {
            if (Context.Message.Author.DiscriminatorValue == 4411)
            {
                await ReplyAsync("***drinking***");
                await Task.Delay(1200);
                await ReplyAsync("***finish drinking***");
            }
            else
            {
                await ReplyAsync($"***kicks {Context.Message.Author.Username}*** i only drink from the maid!");
            }
        }
        #region Small functions
        int getTurn()
        {
           return rand.Next(1, 2 + 1);
        }
        #endregion
    }
}
