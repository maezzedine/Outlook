<template>
    <div id="sidebar" :class="[$store.getters.Language['sidebar-box-shadow'], $store.getters.Language['sidebar-hide']]" >

        <div class="main">
            <table>
                <router-link :to="{ name: 'home' }">
                    <th><i class="fas fa-home"></i></th>
                    <td>{{$store.getters.Language.mainmenu}}</td>
                </router-link>
                <router-link to="/PDF">
                    <th><i class="fas fa-file-pdf"></i></th>
                    <td>{{$store.getters.Language.pdf}}</td>
                </router-link>
            </table>
        </div>

        <!--<Categories>-->
        <div class="main">
            <span>{{$store.getters.Language.categories}}</span>
            <table>
                <router-link v-for="cat in Categories" v-if="showCategory(cat)"
                                :to="{ name: 'category', params: { id: cat.id } }">
                    <th><i class="fontawesome" :class="getCategoryIcon(cat)"></i></th>
                    <td>{{cat.categoryName}}<span class="badge badge-secondary m-2" v-if="cat.articlesCount != 0">{{cat.articlesCount}}</span></td>
                </router-link>
            </table> 
        </div>
        <!--</Categories>-->

        <!--<About>-->
        <div class="main">
            <span>{{$store.getters.Language.about}}</span>
            <table>
                <router-link to="/writers">
                    <th><i class="fas fa-feather-alt"></i></th>
                    <td>{{$store.getters.Language.writers}}</td>
                </router-link>
            </table>      
        </div>
        <!--</About>-->

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