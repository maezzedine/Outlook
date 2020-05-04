<template>
    <div class="email-confirmation">
        <div class="title">{{$store.getters.Language['confirm-email']}}</div>
        <div class="details">
            {{$store.getters.Language['check-email-confirmation']}} <span>no-reply@auboutlook.com</span>
            <p>{{$store.getters.Language['check-spam']}}</p>
        </div>
        <div class="resend-verification">
            {{$store.getters.Language['resend-verificaton']}} <button @click="resendVerification">{{$store.getters.Language['here']}}</button> {{$store.getters.Language['to-resend']}}
        </div>
    </div>
</template>

<script lang="ts" src="./Email-Confirmation.ts"></script>
<style lang="scss" src="./Email-Confirmation.scss" scoped></style>