<template>
    <div id="sidebar">

        <div class="main">
            <table>
                <tr>
                    <th><i class="fas fa-home"></i></th>
                    <td>{{language.mainmenu}}</td>
                </tr>
                <tr>
                    <th><i class="fas fa-file-pdf"></i></th>
                    <td>{{language.pdf}}</td>
                </tr>
            </table>
        </div>

        <!--<Categories>  -->
        <div class="main">
            <span>{{language.categories}}</span>
            <table>
                <tr v-for="cat in Categories" v-if="showCategory(cat)">
                    <th><i class="fontawesome" :class="getCategoryIcon(cat)"></i></th>
                    <td>{{cat.categoryName}}</td>
                </tr>
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