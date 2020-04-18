<template>
    <nav id="navbar" class="navbar" :class="$store.getters.Language['window-expand']">

        <!-- <logo> -->
        <div @click="$emit('expansion-toggled')">
            <svg-outlook></svg-outlook>
        </div>
        <!-- </logo> -->

        <!-- <search bar> -->
        <div class="d-none d-sm-flex col-4 form-control">
            <input class="form-control" type="text" :placeholder="$store.getters.Language.search" />
            <i class="fas fa-search"></i>
        </div>
        <!-- </search bar> -->

            
        <!-- <options> -->
        <div class="options">

            <!-- <Home> -->
            <router-link to="/" class="btn-group d-sm-none" :data-title="$store.getters.Language.home">
                <button class="archive">
                    <i class="fas fa-home"></i>
                </button>
            </router-link>
            <!-- </Home> -->

            <!-- <Archives> -->
            <router-link to="/Archives" class="btn-group" :data-title="$store.getters.Language.archives">
                <button class="archive">
                    <i class="fas fa-archive"></i>
                </button>
            </router-link>
            <!-- </Archives> -->

            <!-- <Change Language> -->
            <button class="archive" :data-title="$store.getters.Language.language">
                <i class="fas fa-language" @click="$emit('language-toggled')"></i>
            </button>
            <!-- </Change Language> -->

            <!-- <Meet Outlook> -->
            <router-link to='/'>
                <div class="btn mx-sm-0 mx-md-2">{{$store.getters.Language.aboutUs}}</div>
            </router-link>
            <!-- </Meet Outlook> -->

        </div>
        <!-- </options> -->
    </nav>
</template>

<script lang="ts" src="./Navbar.ts"></script>
<style lang="scss" src="./Navbar.scss" scoped></style>
