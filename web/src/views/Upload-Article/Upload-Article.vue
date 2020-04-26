<template>
    <div class="upload-article">
        <div class="title">{{$store.getters.Language['contribute-to-outlook']}}</div>
        <div class="body">
            {{$store.getters.Language['write-for-outlook-question']}}
            <p>{{$store.getters.Language['good-news']}}<span>{{$store.getters.Language['you-can-upload']}}</span></p>
            <div class="form">
                <div class="instructions">
                    <div class="instructions-title">{{$store.getters.Language['to-upload']}}</div>
                    <div class="instructions-details">
                        {{$store.getters.Language['upload-your-document']}}
                        <ul>
                            <li>{{$store.getters.Language['your-name']}}</li>
                            <li>{{$store.getters.Language['upload-picture']}}</li>
                            <li>{{$store.getters.Language['upload-title']}}</li>
                            <li>{{$store.getters.Language['upload-subtitle']}}</li>
                            <li>{{$store.getters.Language['upload-content']}}</li>
                        </ul>
                    </div>
                    <div class="note">{{$store.getters.Language['upload-note']}}</div>
                </div>
                <div class="vertical-seperator d-none d-sm-block"></div>
                <form @submit.prevent="sendFile">
                    <div>
                        <input id="file" type="file" @change="updateFile($event.target.files[0])" accept="application/pdf" multiple="false"/>
                        <button type="submit" class="btn">{{$store.getters.Language['submit']}}</button>
                        <i class="fas fa-check w-100 text-center" style="color: forestgreen" v-if="ProcessCompleted"></i>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Upload-Article.ts"></script>
<style lang="scss" src="./Upload-Article.scss" scoped></style>