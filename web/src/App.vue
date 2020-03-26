<template>
    <div id="app">
        <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang"
                        :language="Language"/>
        <!--<Home />-->
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
