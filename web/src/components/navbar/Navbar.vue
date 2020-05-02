<template>
    <nav id="navbar" class="navbar" :class="$store.getters.Language['window-expand']">

        <!-- <logo> -->
        <div @click="$emit('expansion-toggled')">
            <svg-outlook></svg-outlook>
        </div>
        <!-- </logo> -->
            
        <!-- <options> -->
        <div class="options">

            <template v-if="!$store.getters.IsAuthenticated">
                <!-- <Login> -->
                <router-link :to="{ name: 'login' }" class="btn-group">
                    <button class="option">
                        <i class="fas fa-user"></i>
                    </button>
                </router-link>
                <!-- </Login> -->
                <!-- <Register> -->
                <router-link :to="{ name: 'register' }" class="btn-group">
                    <button class="option">
                        <i class="fas fa-user-plus"></i>
                    </button>
                </router-link>
                <!-- </Register> -->
            </template>

            <template v-else>
                <!-- <Logout> -->
                <router-link :to="{ 'name': 'profile' }">{{$store.getters.User.username}}</router-link>
                <button class="option" @click="logout">
                    <i class="fas fa-sign-out-alt"></i>
                </button>
                <!-- </Logout> -->
            </template>

            <!-- <Home> -->
            <router-link :to="{ name: 'home' }" class="btn-group d-sm-none">
                <button class="option">
                    <i class="fas fa-home"></i>
                </button>
            </router-link>
            <!-- </Home> -->

            <!-- <Archives> -->
            <router-link :to="{ name: 'archives' }" class="btn-group">
                <button class="option">
                    <i class="fas fa-archive"></i>
                </button>
            </router-link>
            <!-- </Archives> -->

            <!-- <Change Language> -->
            <button class="option">
                <i class="fas fa-language" @click="$emit('language-toggled')"></i>
            </button>
            <!-- </Change Language> -->

        </div>
        <!-- </options> -->
    </nav>
</template>

<script lang="ts" src="./Navbar.ts"></script>
<style lang="scss" src="./Navbar.scss" scoped></style>
