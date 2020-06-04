<template>
    <div class="article">
        <router-link v-if="!loading" class="category" :to="{ name: 'category', params: { id: $store.getters.Article.category.id } }"
                :style="[{background: getCategoryColor($store.getters.Article.category.tag)}]">
            {{$store.getters.Article.category.name}} <span></span>
        </router-link>

        <div class="article-body">
            <div class="title">
                <template v-if="!loading">{{$store.getters.Article.title}}</template>
            </div>
            <div class="subtitle">
                <template v-if="!loading">{{$store.getters.Article.subtitle}}</template>
            </div>

            <template v-if="!loading">
                <img v-if="$store.getters.Article.picturePath != null" :src="APP_URL + $store.getters.Article.picturePath" :class="$store.getters.Language['article-image']"/>
            </template>

            <div class="writer">
                <router-link :to="{ name: 'member', params: { id: $store.getters.Article.writer.id } }" v-if="!loading">{{$store.getters.Article.writer.name}} | {{$store.getters.Article.writer.position}}</router-link>
            </div>

            <div id="article-text-body"></div>

            <div v-if="!loading" class="footer">

                <div @click="rateUp"><svg-arrow-up class="rate" /></div>
                {{$store.getters.Article.rate}}
                <div @click="rateDown"><svg-arrow-down class="rate" /></div>

                <span>
                    {{$store.getters.Language.votes}}: {{$store.getters.Article.numberOfVotes}}
                </span>

                <span v-if="$store.getters.Article.comments != undefined">
                    {{$store.getters.Language.comments}}: {{$store.getters.Article.comments.length}}
                </span>

                <span class="row m-0 p-0 star" @click="favoriteArticle">
                    <svg-star-empty class="mt-0 unfavorited" />
                    <svg-star-fill class="mt-0 favorited" />
                    {{$store.getters.Article.numberOfFavorites}}
                </span>


                <div class="signature mx-3">
                    {{$store.getters.Language['made-with']}}  <svg-heart />  {{$store.getters.Language['by-outlook']}}
                </div>

                <div class="time"> {{getDateTime($store.getters.Article.dateTime)}}</div>

                <div class="comment-section">
                    <div class="comments">
                        <form class="new-comment" @submit.prevent="addComment">
                            <template v-if="!$store.getters.IsAuthenticated">
                                <textarea v-model="Comment" :placeholder="$store.getters.Language['unauthorized-comment']" disabled />
                            </template>
                            <template v-else>
                                <textarea v-model="Comment" :placeholder="$store.getters.Language['comment']" />
                                <span><button type="submit" class="btn">{{$store.getters.Language['submit']}}</button></span>
                            </template>
                        </form>
                        <div class="comment" v-for="comment in $store.getters.Article.comments">
                            <div class="comment-header">
                                <div class="owner">
                                    {{comment.user.firstName}} {{comment.user.lastName}}
                                </div>
                                <div class="delete" @click="deleteComment(comment.id)" v-if="comment.user.userName == $store.getters.User.username"><svg-close /></div>
                            </div>
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
