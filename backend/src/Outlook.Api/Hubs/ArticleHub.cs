using Microsoft.AspNetCore.SignalR;

namespace Outlook.Api.Hubs
{
    public class ArticleHub : Hub<IArticleHub>
    {
        //public async Task SendArticleEvent()
        //{
        //    await Clients.All.ArticleScoreChange(1, 1);
        //}
    }
}
