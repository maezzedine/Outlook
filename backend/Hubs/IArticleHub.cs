using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Hubs
{
    public interface IArticleHub
    {
        Task ArticleScoreChange(int articleId, int rate, int numberOfVotes);
        Task ArticleCommentChange(int articleId, List<Comment> comments);
        Task ArticleFavoriteChange(int articleId, int numberOfFavorites);
    }
}
