import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/member.dart';
import 'package:mobile/models/topStats.dart';
import 'package:mobile/models/user.dart';
import 'package:mobile/models/volume.dart';

class OutlookState {
  final List<Volume> volumes;
  final List<Issue> issues;
  final Volume volume;
  final Issue issue;
  final List<Category> categories;
  final List<Article> articles;
  final TopStats topStats;
  final List<Member> writers;
  final User user;

  OutlookState({
    @required this.issues,
    @required this.volumes,
    @required this.issue,
    @required this.volume,
    @required this.categories,
    @required this.articles,
    @required this.topStats,
    @required this.writers,
    @required this.user
  });

  OutlookState.initialSatte() : 
    issues = null, 
    volumes = null, 
    issue = null, 
    volume = null, 
    categories = null,
    articles = null,
    topStats = null,
    writers = null,
    user = null;
}

typedef void SetStateCallback();