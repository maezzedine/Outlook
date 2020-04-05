<template>
    <nav id="navbar" class="navbar">

        <!-- <logo> -->
        <div @click="$emit('expansion-toggled')">
            <svg-outlook></svg-outlook>
        </div>
        <!-- </logo> -->

        <!-- <search bar> -->
        <div class="d-none d-sm-flex col-4 form-control">
            <input class="form-control" type="text" :placeholder="language.search" />
            <i class="fas fa-search"></i>
        </div>
        <!-- </search bar> -->

            
        <!-- <options> -->
        <div class="options">

            <!-- <Archives> -->
            <router-link to="/Archives" class="btn-group" :data-title="language.archives">
                <button class="archive" :data-title="language.volumes">
                    <i class="fas fa-archive"></i>
                </button>
            </router-link>
            <!-- </Archives> -->

            <!-- <Change Language> -->
            <button class="archive" :data-title="language.language">
                <i class="fas fa-globe" @click="$emit('language-toggled')"></i>
            </button>
            <!-- </Change Language> -->

            <!-- <Meet Outlook> -->
            <router-link to='/'>
                <div class="btn mx-sm-0 mx-md-2">{{language.about}}</div>
            </router-link>
            <!-- </Meet Outlook> -->

        </div>
        <!-- </options> -->
    </nav>
</template>

<script lang="ts" src="./Navbar.ts"></script>
<style lang="scss" src="./Navbar.scss" scoped></style>
