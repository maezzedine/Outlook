<template>
    <div class="auth">
        <div class="w-100">{{token}}</div>
        <div class="w-100">{{error}}</div>
        <div class="form">
            <span>{{$store.getters.Language['old-user']}} <router-link :to="{ name: 'login'}">{{$store.getters.Language['login']}}</router-link> {{$store.getters.Language['here']}}</span>
            <table>
                <tr>
                    <th>{{$store.getters.Language['first-name']}}:</th>
                    <td><input v-model="Model.firstName" /></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['last-name']}}:</th>
                    <td><input v-model="Model.lastName" /></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['username']}}:</th>
                    <td><input v-model="Model.username" /></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['password']}}:</th>
                    <td><input v-model="Model.password" type="password" /></td>
                </tr>
            </table>
            <button class="btn title" @click="register">{{$store.getters.Language['register']}}</button>
        </div>
    </div>
</template>

<script lang="ts" src="./Register.ts"></script>
<style lang="scss" src="./Authentication.scss" scoped></style>