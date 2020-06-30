import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/member.dart';
import 'package:mobile/models/topStats.dart';
import 'package:mobile/models/volume.dart';

abstract class OutlookAction {
  @override
  String toString() {
    return '$runtimeType';
  }
}

class SetIssueAction extends OutlookAction {
  final Issue issue;
  SetIssueAction({@required this.issue});
}

class SetIssuesAction extends OutlookAction {
  final List<Issue> issues;
  SetIssuesAction({@required this.issues});
}

class SetVolumeAction extends OutlookAction {
  final Volume volume;
  SetVolumeAction({@required this.volume});
}

class SetVolumesAction extends OutlookAction {
  final List<Volume> volumes;
  SetVolumesAction({@required this.volumes});
}

class SetCategoriesAction extends OutlookAction {
  final List<Category> categories;
  SetCategoriesAction({@required this.categories});
}

class SetArticlesAction extends OutlookAction {
  final List<Article> articles;
  SetArticlesAction({@required this.articles});
}

class SetTopStatsAction extends OutlookAction {
  final TopStats topStats;
  SetTopStatsAction({@required this.topStats});
}

class SetWritersAction extends OutlookAction {
  final List<Member> writers;
  SetWritersAction({@required this.writers});
}