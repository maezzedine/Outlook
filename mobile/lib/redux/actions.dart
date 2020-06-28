import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
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

class SetVolumeAction extends OutlookAction {
  final Volume volume;
  SetVolumeAction({@required this.volume});
}

class SetCategoriesAction extends OutlookAction {
  final List<Category> categories;
  SetCategoriesAction({@required this.categories});
}