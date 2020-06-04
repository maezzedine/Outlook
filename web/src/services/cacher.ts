import { ApiObject } from '../models/apiObject';

export class Cacher {
    initializeVolume(volumes: Array<ApiObject>) {
        var localVolumeId = sessionStorage.getItem('Outlook-Volume');

        if (localVolumeId != null) {
            for (var i = 0; i < volumes.length; i++) {
                if (volumes[i]['id'] == localVolumeId) {
                    return volumes[i];
                }
            }
        }
        var volume = volumes[volumes.length - 1];
        // Cache the volume id
        sessionStorage.setItem('Outlook-Volume', volume['id']);

        return volume;
    }

    initializeIssue(issues: Array<ApiObject>) {
        var localIssueId = sessionStorage.getItem('Outlook-Issue');

        if (localIssueId != null) {
            try {
                for (var i = 0; i < issues.length; i++) {
                    if (issues[i]['id'] == localIssueId) {
                        return issues[i];
                    }
                }
            } catch (e) { }
        }
        if (issues.length != 0) {
            var issue = issues[issues.length - 1];
            sessionStorage.setItem('Outlook-Issue', issue['id']);
            return issue;
        }
        return null;
    }
}

export const cacher = new Cacher();