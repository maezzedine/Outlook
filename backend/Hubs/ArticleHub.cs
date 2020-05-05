using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class ArticleHub : Hub<IArticleHub>
    {
        //public async Task SendArticleEvent()
        //{
        //    await Clients.All.ArticleScoreChange(1, 1);
        //}
    }
}
