<template>
    <div class="about">
        <div class="about-pannel">
            <div class="title">{{$store.getters.Language['outlook']}}</div>
            <table>
                <tr>
                    <th>{{$store.getters.Language['version']}}</th>
                    <td>1.0.0</td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['author']}}</th>
                    <td><a href="https://github.com/mezdn">{{$store.getters.Language['author-name']}}</a></td>
                </tr>
                <tr>
                    <th>{{$store.getters.Language['contact']}}</th>
                    <td>{{$store.getters.Language['info-email']}}</td>
                </tr>
            </table>
            <a href="https://github.com/mezdn/Outlook"><svg-github /></a> 
            <div> 2020 &copy;</div>
        </div>
    </div>
</template>

<script lang="ts" src="./About.ts"></script>
<style lang="scss" src="./About.scss" scoped></style>