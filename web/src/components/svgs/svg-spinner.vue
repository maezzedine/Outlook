<template>
    <svg xmlns="http://www.w3.org/2000/svg" 
         xmlns:xlink="http://www.w3.org/1999/xlink" 
         class="svg-spinner"
         width="200px" height="200px" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">

        <path d="M10 50A40 40 0 0 0 90 50A40 42 0 0 1 10 50" 
              stroke="none"transform="rotate(358.556 50.0014 51.0014)">

            <animateTransform attributeName="transform" type="rotate" dur="1s" 
                              repeatCount="indefinite" keyTimes="0;1" 
                              values="0 50 51;360 50 51"></animateTransform>
        </path>
    </svg>
</template>

<style lang="scss" scoped>
    .svg-spinner {
        margin: auto; 
        display: block; 
        shape-rendering: auto;
    }
    
    path {
        fill: var(--fg-3);
    }
</style>