<template>
    <div id="sidebar" :class="Language['sidebar-box-shadow']">

        <div class="main">
            <table>
                <router-link :to="{ name: 'home' }">
                    <th><i class="fas fa-home"></i></th>
                    <td>{{Language.mainmenu}}</td>
                </router-link>
                <router-link to="/PDF">
                    <th><i class="fas fa-file-pdf"></i></th>
                    <td>{{Language.pdf}}</td>
                </router-link>
            </table>
        </div>

        <!--<Categories>  -->
        <div class="main">
            <span>{{Language.categories}}</span>
            <table>
                <router-link v-for="cat in Categories" v-if="showCategory(cat)"
                             :to="{ name: 'category', params: { id: cat.id }, meta: { category: cat } }">
                    <th><i class="fontawesome" :class="getCategoryIcon(cat)"></i></th>
                    <td>{{cat.categoryName}}<span class="badge badge-secondary" v-if="cat.articlesCount != 0">{{cat.articlesCount}}</span></td>
                </router-link>
            </table>      
        </div>
        <!--</Categories>  -->

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