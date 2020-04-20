<template>
    <div id="sidebar" :class="[$store.getters.Language['sidebar-box-shadow'], $store.getters.Language['sidebar-hide']]" >

        <div class="sidebar-body">
            <div class="main">
                <table>
                    <router-link :to="{ name: 'home' }">
                        <th><i class="fas fa-home"></i></th>
                        <td>{{$store.getters.Language.mainmenu}}</td>
                    </router-link>

                    <!--<Categories>-->
                    <tr><span>{{$store.getters.Language.categories}}</span></tr>
                    <router-link v-for="cat in Categories" v-if="showCategory(cat)"
                                 :to="{ name: 'category', params: { id: cat.id } }">
                        <th><i class="fontawesome" :class="getCategoryIcon(cat)"></i></th>
                        <td>{{cat.categoryName}}<p class="badge badge-secondary m-2" v-if="cat.articlesCount != 0">{{cat.articlesCount}}</p></td>
                    </router-link>
                    <!--</Categories>-->

                    <!--<About>-->
                    <tr><span>{{$store.getters.Language.about}}</span></tr>
                    <router-link :to="{ name: 'writers' }">
                        <th><i class="fas fa-feather-alt"></i></th>
                        <td>{{$store.getters.Language.writers}}</td>
                    </router-link>
                    <router-link :to="{ name: 'about' }">
                        <th><i class="fas fa-handshake"></i></th>
                        <td>{{$store.getters.Language.aboutUs}}</td>
                    </router-link>
                    <!--</About>-->
            
                    <!--<Authentication>-->
                    <tr><span><i class="fas fa-lock"></i></span></tr>
                    <router-link :to="{ name: 'login' }">
                        <th><i class="fas fa-user"></i></th>
                        <td>{{$store.getters.Language.login}}</td>
                    </router-link>
                    <router-link :to="{ name: 'register' }">
                        <th><i class="fas fa-user-plus"></i></th>
                        <td>{{$store.getters.Language.register}}</td>
                    </router-link>
                    <!--</Authentication>-->
                </table>
            </div>
        </div>

        <!-- <toggle theme> -->
        <div class="icon-btn" @click="$emit('theme-toggled')">
            <i v-if="theme == 'default-dark'" class="fas fa-sun"></i>
            <i v-if="theme == 'default'" class="fas fa-moon"></i>
        </div>
        <!-- </toggle theme> -->
    </div>
</template>

<script lang="ts" src="./Sidebar.ts"></script>
<style lang="scss" src="./Sidebar.scss" scoped></style>