import { ActionContext } from 'vuex';
import state from '../state';
import { ArticleScoreChange } from '../../models/articleScoreChange';
import { ArticleCommentChange } from '../../models/articleCommentChange';
import articleObject from '../../models/articleObject';
import { ArticleFavoriteChange } from '../../models/articleFavoriteChange';

const state = {
    article: articleObject
}

const mutations = {
    setArticle(state: state, article: articleObject) {
        state.article = article;
    },
    updateArticleRate(state: state, articleScore: ArticleScoreChange) {
        if (state.article.id == articleScore.articleId) {
            state.article.rate = articleScore.rate;
            state.article.numberOfVotes = articleScore.numberOfVotes;
        }
    },
    updateArticleComments(state: state, articleComments: ArticleCommentChange) {
        if (state.article.id == articleComments.articleId) {
            state.article.comments = articleComments.comments;
        }
    },
    updateArticleFavorites(state: state, articleFavorites: ArticleFavoriteChange) {
        if (state.article.id == articleFavorites.articleId) {
            state.article.numberOfFavorites = articleFavorites.numberOfFavorites;
        }
    }
}

const actions = {
    setArticle(context: ActionContext<state, state>, article: articleObject) {
        context.commit('setArticle', article);
    },
    updateArticleRate(context: ActionContext<state, state>, articleScore: ArticleScoreChange) {
        context.commit('updateArticleRate', articleScore);
    },
    updateArticleComments(context: ActionContext<state, state>, articleComments: ArticleCommentChange) {
        context.commit('updateArticleComments', articleComments);
    },
    updateArticleFavorites(context: ActionContext<state, state>, articleFavorites: ArticleFavoriteChange) {
        context.commit('updateArticleFavorites', articleFavorites);
    },
}

const getters = {
    Article: (state: state) => {
        return state.article;
    }
}

export default {
    state,
    mutations,
    actions,
    getters
}