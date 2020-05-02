<template>
    <div class="change-password">
        <form class="form" @submit.prevent="changePassword">
            <div class="error" v-if="errors.length > 0">
                <ul>
                    <li v-for="error in errors">{{error}}</li>
                </ul>
            </div>
            <div class="text-success" v-if="succeeded">{{$store.getters.Language['password-changed']}}</div>
            <table>
                <tr>
                    <th>{{$store.getters.Language['old-password']}}:</th>
                    <td><input v-model="Model.oldPassword" type="password" autocomplete="on" /></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['new-password']}}:</th>
                    <td><input v-model="Model.newPassword" type="password" autocomplete="on" /></td>
                </tr>
            </table>
            <button class="btn title" type="submit">{{$store.getters.Language['submit']}}</button>
        </form>
    </div>
</template>

<script lang="ts" src="./ChangePassword.ts"></script>
<style lang="scss" src="./Authentication.scss"></style>