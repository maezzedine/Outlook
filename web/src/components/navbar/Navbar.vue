<template>
    <div>
        <nav class="navbar">

            <!-- <logo> -->
            <div @click="$emit('theme-toggled')">
                <svg-outlook></svg-outlook>
            </div>
            <!-- </logo> -->

            <!-- <search bar> -->
            <div class="d-none d-sm-flex form-control col-3">
                <input type="text" class="" placeholder="Search">
                <span class="input-group-text" id="addon-wrapping"><i class="fas fa-search"></i></span>
            </div>
            <!-- </search bar> -->

            
            <!-- <archives> -->
            <div class="archives">
               
                

                <!-- <volumes dropdown> -->
                <div class="btn-group mx-sm-0 mx-md-1">
                    <button type="button" class="archive" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" title="Volumes">
                        <i class="fas fa-archive"></i>
                    </button>
                    <div class="dropdown-menu dropdown-menu-right">
                        <button v-for="volume in Volumes" class="dropdown-item" type="button" @click="setVolume(volume)" :class="{choice : volume.id == Volume.id}">V. {{volume.volumeNumber}} | {{volume.fallYear}} - {{volume.springYear}}</button>
                    </div>
                </div>
                <!-- </volumes dropdown> -->
                
                <!-- <issues dropdown> -->
                <div class="btn-group mx-sm-0 mx-md-1">
                    <button type="button" class="archive" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" title="Issues">
                        <i class="far fa-newspaper"></i>
                    </button>
                    <div class="dropdown-menu dropdown-menu-right">
                        <button v-for="issue in Issues" class="dropdown-item" type="button" @click="setIssue(issue)" :class="{choice : issue.id == Issue.id}">Issue {{issue.issueNumber}}</button>
                    </div>
                </div>
                <!-- </issues dropdown> -->

                <!-- <Meet Outlook> -->
                <div class="btn mx-sm-0 mx-md-2">Meet Outlook</div>
                <!-- </Meet Outlook> -->
                <div class="btn mx-sm-0 mx-md-2">Arabic</div>

            </div>
            <!-- </archives> -->

        </nav>
    </div>
</template>

<script lang="ts" src="./Navbar.ts"></script>
<style lang="scss" src="./Navbar.scss"></style>
