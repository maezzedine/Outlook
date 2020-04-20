<template>
    <div class="top-voted-articles" v-if="model != null">
        <span><i :class="model.titleIcon"></i> {{model.title}} <i :class="model.titleIcon"></i></span>

        <ol>
            <li v-for="item in model.items" v-if="item.language == $store.getters.Language.num">
                <router-link :to="{ name: model.component, params: { id: item.id } }">
                    {{item[model.itemTitle]}}
                    <span>{{item[model.field]}} <i :class="model.itemIcon"></i></span>
                </router-link>
            </li>
        </ol>
    </div>
</template>

<script lang="ts" src="./TopStats.ts"></script>
<style lang="scss" src="./TopStats.scss"></style>