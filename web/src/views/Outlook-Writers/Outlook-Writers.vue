<template>
    <div class="writers">
        <div class="title">{{$store.getters.Language.writers}}</div>
        <ol>
            <li v-for="writer in Writers" v-if="writer.language == $store.getters.Language.language">
                <router-link :to="{ name: 'member', params: { id: writer.id } }">{{writer.name}}</router-link>
            </li>
        </ol>
        
    </div>
</template>

<script lang="ts" src="./Outlook-Writers.ts"></script>
<style lang="scss" src="./Outlook-Writers.scss" scoped></style>