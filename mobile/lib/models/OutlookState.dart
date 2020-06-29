import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';

class OutlookState {
  final Volume volume;
  final Issue issue;
  final List<Category> categories;
  final List<Article> articles;

  OutlookState({
    @required this.issue,
    @required this.volume,
    @required this.categories,
    @required this.articles
  });

  OutlookState.initialSatte() : 
    issue = null, 
    volume = null, 
    categories = null,
    articles = null;
}