<template>
    <div class="sidebar">

        <div class="main">
            <div class="side-tab">
                <i class="edge"></i>
                <a class="tab">{{language.mainmenu}}</a>
            </div>
            <div class="side-tab">
                <i class="edge"></i>
                <a class="tab"><i class="fas fa-file-pdf mx-1"></i> {{language.pdf}} </a>
            </div>
        </div>

        <!--<Categories>  -->
        <div class="main">
            <span>{{language.categories}}</span>
            <div class="side-tab" v-for="cat in Categories" v-if="showCategory(cat)" >
                <i class="edge"></i>
                <a class="tab">{{cat.categoryName}}<br /></a>
            </div>        
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