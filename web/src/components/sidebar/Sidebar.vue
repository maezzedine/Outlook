<template>
    <div id="sidebar" :class="[$store.getters.Language['sidebar-box-shadow'], $store.getters.Language['sidebar-hide']]" >

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
                <router-link :to="{ name: 'upload-article' }">
                    <th><i class="fas fa-hand-holding"></i></th>
                    <td>{{$store.getters.Language['contribute-to-outlook']}}</td>
                </router-link>
                <!--</About>-->

                <tr><span>{{$store.getters.Language.theme}}</span></tr>
                <tr @click="$emit('theme-toggled')" class="theme">
                    <th class="icon-btn">
                        <i v-if="theme == 'default-dark'" class="fas fa-sun"></i>
                        <i v-if="theme == 'default'" class="fas fa-moon"></i>
                    </th>
                    <td>
                        <template v-if="theme == 'default-dark'">{{$store.getters.Language['light-mode']}}</template>
                        <template v-if="theme == 'default'">{{$store.getters.Language['dark-mode']}}</template>
                    </td>
                </tr>
            </table>

            <!-- <toggle theme> -->
            
            <!-- </toggle theme> -->
        </div>

        
    </div>
</template>

<script lang="ts" src="./Sidebar.ts"></script>
<style lang="scss" src="./Sidebar.scss" scoped></style>