using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

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
