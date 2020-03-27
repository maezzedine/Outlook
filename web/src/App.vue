<template>
    <div id="app">
        <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang" @set-issue="setIssue"
                        :language="Language"/>

        <outlook-sidebar @theme-toggled="toggleTheme"
                         :language="Language"></outlook-sidebar>
        <!--<Home />-->
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
