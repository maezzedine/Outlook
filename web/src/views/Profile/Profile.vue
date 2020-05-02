<template>
    <div class="profile" v-if="!loading">
        <div class="name">{{User.firstName}} {{User.lastName}}</div>
        <div class="row m-0">
            <div class="options">
                <router-link :to="{ 'name': 'change-password' }">{{$store.getters.Language['change-password']}}</router-link>
                <router-link :to="{ 'name': 'favorited-articles' }">{{$store.getters.Language['favorited-articles']}}</router-link>
            </div>
            <router-view class="sub-window"/>
        </div>
    </div>
</template>

<script lang="ts" src="./Profile.ts"></script>
<style lang="scss" src="./Profile.scss" scoped></style>