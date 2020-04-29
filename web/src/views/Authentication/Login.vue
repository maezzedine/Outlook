<template>
    <div class="auth">
        <form class="form" @submit.prevent="login">
            <div class="error" v-if="errors.length > 0">
                <ul>
                    <li v-for="error in errors">{{error}}</li>
                </ul>
            </div>
            <div class="text-success" v-if="signInSuccessfuly">{{$store.getters.Language['signin-successfully']}}</div>
            <span>{{$store.getters.Language['new-user']}} <router-link :to="{ name: 'register'}">{{$store.getters.Language['register']}}</router-link> {{$store.getters.Language['here']}}</span>
            <table>
                <tr>
                    <th>{{$store.getters.Language['username']}}:</th>
                    <td><input v-model.trim="Model.username" autofocus autocomplete="on"/></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['password']}}:</th>
                    <td><input v-model.trim="Model.password" type="password" autocomplete="on"/></td>
                </tr>
            </table>
            <button class="btn title" type="submit">{{$store.getters.Language['login']}}</button>
        </form>
    </div>
</template>

<script lang="ts" src="./Login.ts"></script>
<style lang="scss" src="./Authentication.scss" scoped></style>