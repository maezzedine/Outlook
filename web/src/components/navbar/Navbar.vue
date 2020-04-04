<template>
    <nav id="navbar" class="navbar">

        <!-- <logo> -->
        <div @click="$emit('expansion-toggled')">
            <svg-outlook></svg-outlook>
        </div>
        <!-- </logo> -->

        <!-- <search bar> -->
        <div class="d-none d-sm-flex col-4 form-control">
            <input class="form-control" type="text" :placeholder="language.search" />
            <i class="fas fa-search"></i>
        </div>
        <!-- </search bar> -->

            
        <!-- <archives> -->
        <div class="archives">
               
            <!-- <volumes dropdown> -->
            <div class="btn-group mx-sm-0 mx-md-1">
                <button type="button" class="archive" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" :data-title="language.volumes">
                    <i class="fas fa-archive"></i>
                </button>
                <div class="dropdown-menu dropdown-menu-right">
                    <button v-for="volume in Volumes" class="dropdown-item" type="button" @click="setVolume(volume)" :class="{choice : volume.id == Volume.id}">
                        {{language.volume}} {{volume.volumeNumber}} | {{volume.fallYear}} - {{volume.springYear}}</button>
                </div>
            </div>
            <!-- </volumes dropdown> -->
                
            <!-- <issues dropdown> -->
            <div class="btn-group mx-sm-0 mx-md-1">
                <button type="button" class="archive" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" :data-title="language.issue">
                    <i class="fas fa-newspaper"></i>
                </button>
                <div class="dropdown-menu dropdown-menu-right">
                    <button v-for="issue in Issues" class="dropdown-item" type="button" @click="setIssue(issue)" :class="{choice : issue.id == Issue.id}">
                        {{language.issue}} {{issue.issueNumber}}</button>
                </div>
            </div>
            <!-- </issues dropdown> -->

            <!-- <Change Language> -->
            <button class="archive" :data-title="language.language">
                <i class="fas fa-globe" @click="$emit('language-toggled')"></i>
            </button>
            <!-- </Change Language> -->

            <!-- <Meet Outlook> -->
            <div class="btn mx-sm-0 mx-md-2">{{language.about}}</div>
            <!-- </Meet Outlook> -->


        </div>
        <!-- </archives> -->
    </nav>
</template>

<script lang="ts" src="./Navbar.ts"></script>
<style lang="scss" src="./Navbar.scss" scoped></style>
