<template>
    <div class="Archives">
        <p>
            <template v-if="Volume != null">
                {{$store.getters.Language['volume']}} {{Volume['number']}}
            </template> 
            <template v-if="Issue != null">
                | {{$store.getters.Language['issue']}} {{Issue['number']}}
            </template>
        </p>
        <table>
            <tr>
                <th>{{$store.getters.Language['selectVolume']}}</th>
                <td>
                    <select v-model="Volume">
                        <option v-for="volume in Volumes" :value="volume">
                            {{volume.number}} | {{volume.fallYear}} - {{volume.springYear}}
                        </option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>{{$store.getters.Language['selectIssue']}}</th>
                <td>
                    <select v-model="Issue">
                        <option v-for="issue in Issues" :value="issue">
                            {{issue.number}}
                        </option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
</template>

<script lang="ts" src="./Archive.ts"></script>
<style lang="scss" src="./Archive.scss" scoped></style>