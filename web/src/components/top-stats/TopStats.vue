<template>
    <div class="top-voted-articles" v-if="model != null">
        <span><svg-stats :icon="model.titleIcon"/> {{model.title}} <svg-stats :icon="model.titleIcon"/></span>

        <ol>
            <li v-for="item in model.items" v-if="item.language == $store.getters.Language.language">
                <router-link :to="{ name: model.component, params: { id: item.id } }">
                    {{item[model.itemTitle]}}
                    <span>{{item[model.field]}} <svg-stats :icon="model.itemIcon" /></span>
                </router-link>
            </li>
        </ol>
    </div>
</template>

<script lang="ts" src="./TopStats.ts"></script>
<style lang="scss" src="./TopStats.scss"></style>