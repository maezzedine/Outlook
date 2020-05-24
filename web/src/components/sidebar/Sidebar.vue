<template>
    <div id="sidebar" :class="[$store.getters.Language['sidebar-box-shadow'], $store.getters.Language['sidebar-hide']]">
       
        <div class="main">
            <div class="close">
                <button @click="$emit('expansion-toggled')"><svg-close /></button>
            </div>

            <router-link :to="{ name: 'home' }">
                <svg-home class="header" />
                <div class="detail">{{$store.getters.Language.mainmenu}}</div>
            </router-link>

            <!--<Categories>-->
            <label class="table-row"><span>{{$store.getters.Language.categories}}</span></label>
            <router-link v-for="cat in Categories" v-if="showCategory(cat)"
                         :to="{ name: 'category', params: { id: cat.id } }">
                <svg-category :icon="getCategoryIcon(cat)" class="header" :articlesCount="cat.articles.length" />
                <div class="detail">{{cat.categoryName}}</div>
            </router-link>
            <!--</Categories>-->
            <!--<About>-->
            <label class="table-row"><span>{{$store.getters.Language['about-outlook']}}</span></label>
            <router-link :to="{ name: 'writers' }">
                <svg-quill class="header" />
                <div class="detail">{{$store.getters.Language.writers}}</div>
            </router-link>

            <router-link :to="{ name: 'meet-outlook' }">
                <svg-meeting class="header" />
                <div class="detail">{{$store.getters.Language.aboutUs}}</div>
            </router-link>

            <router-link :to="{ name: 'upload-article' }">
                <svg-contribute class="header" />
                <div class="detail">{{$store.getters.Language['contribute-to-outlook']}}</div>
            </router-link>

            <router-link :to="{ name: 'about' }">
                <svg-info class="header" />
                <div class="detail">{{$store.getters.Language['about']}}</div>
            </router-link>
            <!--</About>-->
            <!-- <toggle theme> -->
            <label class="table-row"><span>{{$store.getters.Language.theme}}</span></label>
            <div class="table-row theme" @click="$emit('theme-toggled')">
                <div class="header icon-btn">
                    <svg-sun v-if="theme == 'default-dark'" />
                    <svg-moon v-if="theme == 'default'" />
                </div>
                <div class="detail">
                    <template v-if="theme == 'default-dark'">
                        {{$store.getters.Language['light-mode']}}
                    </template>
                    <template v-if="theme == 'default'">
                        {{$store.getters.Language['dark-mode']}}
                    </template>
                </div>
            </div>
            <!-- </toggle theme> -->
        </div>


    </div>
</template>

<script lang="ts" src="./Sidebar.ts"></script>
<style lang="scss" src="./Sidebar.scss" scoped></style>