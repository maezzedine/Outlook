import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/member.dart';
import 'package:mobile/models/topStats.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/redux/actions.dart';

Issue issueReducer(Issue state, OutlookAction action) {
  if (action is SetIssueAction) return action.issue;
  return state;
}

List<Issue> issuesReducer(List<Issue> state, OutlookAction action) {
  if (action is SetIssuesAction) return action.issues;
  return state;
}

Volume volumeReducer(Volume state, OutlookAction action) {
  if (action is SetVolumeAction) return action.volume;
  return state;
}

List<Volume> volumesReducer(List<Volume> state, OutlookAction action) {
  if (action is SetVolumesAction) return action.volumes;
  return state;
}

List<Category> categoryReducer(List<Category> state, OutlookAction action) {
  if (action is SetCategoriesAction) return action.categories;
  return state;
}

List<Article> articleReducer(List<Article> state, OutlookAction action) {
  if (action is SetArticlesAction) return action.articles;
  return state;
}

TopStats topStatsReducer(TopStats stat, OutlookAction action) {
  if (action is SetTopStatsAction) return action.topStats;
  return stat;
}

List<Member> writersReducer(List<Member> state, OutlookAction action) {
  if (action is SetWritersAction) return action.writers;
  return state;
}

OutlookState outlookAppReducer(state, action) {
  return new OutlookState(
    issues: issuesReducer(state.issues, action),
    volumes: volumesReducer(state.volumes, action),
    issue: issueReducer(state.issue, action),
    volume: volumeReducer(state.volume, action),
    categories: categoryReducer(state.categories, action),
    articles: articleReducer(state.articles, action),
    topStats: topStatsReducer(state.topStats, action),
    writers: writersReducer(state.writers, action)
  );
}