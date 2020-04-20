<template>
    <div class="auth">
        <div class="w-100">{{token}}</div>
        <div class="w-100">{{error}}</div>
        <div class="form">
            <span>{{$store.getters.Language['new-user']}} <router-link :to="{ name: 'register'}">{{$store.getters.Language['register']}}</router-link> {{$store.getters.Language['here']}}</span>
            <table>
                <tr>
                    <th>{{$store.getters.Language['username']}}:</th>
                    <td><input v-model="Model.username" /></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['password']}}:</th>
                    <td><input v-model="Model.password" type="password" /></td>
                </tr>
            </table>
            <button class="btn title" @click="login">{{$store.getters.Language['login']}}</button>
        </div>
    </div>
</template>

<script lang="ts" src="./Login.ts"></script>
<style lang="scss" src="./Authentication.scss" scoped></style>