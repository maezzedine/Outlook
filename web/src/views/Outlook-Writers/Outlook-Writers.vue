<template>
    <div class="writers">
        <div class="title">{{$store.getters['language/Language'].writers}}</div>
        <ol>
            <li v-for="writer in Writers" v-if="writer.language == $store.getters['language/Language'].num">
                {{writer.name}}
            </li>
        </ol>
        
    </div>
</template>

<script lang="ts" src="./Outlook-Writers.ts"></script>
<style lang="scss" src="./Outlook-Writers.scss" scoped></style>