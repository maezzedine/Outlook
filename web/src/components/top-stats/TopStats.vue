<template>
    <div class="top-voted-articles" v-if="model != null">
        <span><i :class="model.titleIcon"></i> {{model.title}} <i :class="model.titleIcon"></i></span>

        <ol>
            <li v-for="item in model.items" v-if="item.language == $store.getters['language/Language'].num">
                <div>
                    {{item[model.itemTitle]}}
                    <span>{{item[model.field]}} <i :class="model.itemIcon"></i></span>
                </div>
            </li>
        </ol>
    </div>
</template>

<script lang="ts" src="./TopStats.ts"></script>
<style lang="scss" src="./TopStats.scss"></style>