using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;

namespace Base_Bot
{
    public class BotCommands : ApplicationCommandModule
    {

        [SlashCommand("add-points", "Adds base points")]
        [SlashRequirePermissions(Permissions.Administrator)]
        public async Task AddPoints (
            InteractionContext ctx,
            [Option("User", "Enter user")] DiscordUser user,
            [Option("Points", "Enter points")] long points)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"{user.Mention} получает {points} BasePoints"));
        }

        [SlashCommand("deprive-points", "Deprive base points")]
        [SlashRequirePermissions(Permissions.Administrator)]
        public async Task DeprivePoints(
            InteractionContext ctx,
            [Option("User", "Enter user")] DiscordUser user,
            [Option("Points", "Enter points")] long points)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"{user.Mention} лишается {points} BasePoints"));
        }


        [SlashCommand("add-points-vote", "Adds base points?")]
        [SlashRequirePermissions(Permissions.Administrator)]
        public async Task AddPointsVote (
            InteractionContext ctx,
            [Option("User", "Enter user")] DiscordUser user,
            [Option("Points", "Enter points")] long points)
        {
            int yesResult = 0, noResult = 0;

            var interactivity = ctx.Client.GetInteractivity();

            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var joinMassege = await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Выдать {points} поинтов {user.Mention}?"));
            
            var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMassege.CreateReactionAsync(yes).ConfigureAwait(false);
            await joinMassege.CreateReactionAsync(no).ConfigureAwait(false);

            TimeSpan time = new TimeSpan(0, 0, 10);

            var result = await interactivity.CollectReactionsAsync(joinMassege, time).ConfigureAwait(false);

            foreach (var list in result)
                if (list.Emoji == yes)
                    yesResult++;

                else if (list.Emoji == no)
                    noResult++;

            if (yesResult > noResult)
                await ctx.Channel.SendMessageAsync($"В результате голосования {user.Mention} получает {points} поинтов!").ConfigureAwait(false);
            else if (yesResult <= noResult)
                await ctx.Channel.SendMessageAsync($"В результате голосования {user.Mention} не получает {points} поинтов!").ConfigureAwait(false);

        }

        [SlashCommand("deprive-points-vote", "Deprive base points?")]
        [SlashRequirePermissions(Permissions.Administrator)]
        public async Task DecreasePointsVote(
            InteractionContext ctx,
            [Option("User", "Enter user")] DiscordUser user,
            [Option("Points", "Enter points")] long points)
        {
            int yesResult = 0, noResult = 0;

            var interactivity = ctx.Client.GetInteractivity();

            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var joinMassege = await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Лишить {points} поинтов {user.Mention}?"));

            var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMassege.CreateReactionAsync(yes).ConfigureAwait(false);
            await joinMassege.CreateReactionAsync(no).ConfigureAwait(false);

            TimeSpan time = new TimeSpan(0, 0, 10);

            var result = await interactivity.CollectReactionsAsync(joinMassege, time).ConfigureAwait(false);

            foreach (var list in result)
                if (list.Emoji == yes)
                    yesResult++;

                else if (list.Emoji == no)
                    noResult++;

            if (yesResult > noResult)
                await ctx.Channel.SendMessageAsync($"В результате голосования {user.Mention} лишается {points} поинтов!").ConfigureAwait(false);
            else if (yesResult <= noResult)
                await ctx.Channel.SendMessageAsync($"В результате голосования {user.Mention} не лишается {points} поинтов!").ConfigureAwait(false);

        }


        //[Command("respondmessage")]
        //[RequireRoles(RoleCheckMode.Any, "Админ")]
        //public async Task RespondEmoji(CommandContext ctx)
        //{
        //    var interactivity = ctx.Client.GetInteractivity();

        //    var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
        //    var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

        //    var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
        //    //if (message.Result.Emoji == "{😢}")
        //    if(message.Result.Emoji == yes)
        //        await ctx.Channel.SendMessageAsync("Бан нахуй");
        //    else
        //        await ctx.Channel.SendMessageAsync("");

        //}
        //[Command($"base")]
        //[RequireRoles(RoleCheckMode.Any, "Админ")]
        //public async Task BaseOrCringe(CommandContext ctx, string userName)
        //{
        //    var interactivity = ctx.Client.GetInteractivity();

        //    var user = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

        //    var joinEmbed = new DiscordEmbedBuilder
        //    {
        //        Title = "База или кринж?",
        //        ImageUrl = user.Result.Content,
        //        Color = DiscordColor.Red
        //    };

        //    var joinMassege = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

        //    var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
        //    var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

        //    await joinMassege.CreateReactionAsync(yes).ConfigureAwait(false);
        //    await joinMassege.CreateReactionAsync(no).ConfigureAwait(false);


        //}
    }
}
