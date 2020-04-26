<template>
    <div class="article">
        <div v-if="!loading"  class="category" 
                :style="[{background: getCategoryColor(Article.categoryTagName)}]">
            {{Article.category.categoryName}} <span></span>
        </div>

        <div class="article-body">
            <div class="title">
                <template v-if="!loading">{{Article.title}}</template>
            </div>
            <div class="subtitle">
                <template v-if="!loading">{{Article.subtitle}}</template>
            </div>

            <template v-if="!loading">
                <img v-if="Article.picturePath != null" :src="APP_URL + Article.picturePath" :class="$store.getters.Language['article-image']"/>
            </template>

            <div class="writer">
                <router-link :to="{ name: 'member', params: { id: Article.memberID } }" v-if="!loading">{{Article.member.name}} | {{Article.member.positionName}}</router-link>
            </div>

            <div id="article-text-body"></div>

            <div v-if="!loading" class="footer">

                <span class="rate" :class="[{ ratedUp : Article.ratedByUser == 1 }]" @click="rateUp">
                    <i class="fas fa-angle-up"></i>
                </span>

                {{Article.rate}}

                <span class="rate" :class="[{ ratedDown : Article.ratedByUser == 2 }]" @click="rateDown">
                    <i class="fas fa-angle-down"></i>
                </span>

                <span>
                    {{$store.getters.Language.votes}}: {{Article.numberOfVotes}}
                </span>

                <span v-if="Article.comments != undefined">
                    {{$store.getters.Language.comments}}: {{Article.comments.length}}
                </span>

                <span class="row m-0 p-0 star" @click="favoriteArticle">
                    <span class="unfavorited"><i class="far fa-star"></i></span>
                    <span class="favorited"><i class="fas fa-star"></i></span>
                    {{Article.numberOfFavorites}}
                </span>


                <div class="signature mx-3">
                    {{$store.getters.Language['made-with']}}  <i class="fas fa-heart"></i>  {{$store.getters.Language['by-outlook']}}
                </div>

                <div class="time"> {{getDateTime(Article.dateTime)}}</div>

                <div class="comment-section">
                    <div class="comments">
                        <form class="new-comment" @submit.prevent="addComment">
                            <template v-if="!$store.getters.IsAuthenticated">
                                <textarea v-model="Comment" autofocus :placeholder="$store.getters.Language['unauthorized-comment']" disabled />
                            </template>
                            <template v-else>
                                <textarea v-model="Comment" autofocus :placeholder="$store.getters.Language['comment']" />
                                <span><button type="submit" class="btn">{{$store.getters.Language['submit']}}</button></span>
                            </template>
                        </form>
                        <div class="comment" v-for="comment in Article.comments">
                            <div class="owner">{{comment.user.firstName}} {{comment.user.lastName}}</div>

                            {{comment.text}}
                            <div class="time">{{getDateTime(comment.dateTime)}}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts" src="./Outlook-Article.ts"></script>
<style lang="scss" src="./Outlook-Article.scss" scoped></style>
