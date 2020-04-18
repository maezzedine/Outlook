<template>
    <div class="Archives">
        <p>{{$store.getters['language/Language']['volume']}} {{Volume['volumeNumber']}} | {{$store.getters['language/Language']['issue']}} {{Issue['issueNumber']}}</p>
        <table>
            <tr>
                <th>{{$store.getters['language/Language']['selectVolume']}}</th>
                <td>
                    <select v-model="Volume">
                        <option v-for="volume in Volumes" :value="volume">
                            {{volume['volumeNumber']}} | {{volume['fallYear']}} - {{volume['springYear']}}
                        </option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>{{$store.getters['language/Language']['selectIssue']}}</th>
                <td>
                    <select v-model="Issue">
                        <option v-for="issue in Issues" :value="issue">
                            {{issue['issueNumber']}}
                        </option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
</template>

<script lang="ts" src="./Archive.ts"></script>
<style lang="scss" src="./Archive.scss" scoped></style>