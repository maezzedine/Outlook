import 'package:flutter/cupertino.dart';
import 'package:mobile/models/issue.dart';

class Volume {
  int id;
  int number;
  int fallYear;
  int springYear;
  List<Issue> issues;

  Volume({
    @required this.id,
    @required this.number,
    @required this.fallYear,
    @required this.springYear,
    @required this.issues
  });

  Volume.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    number = json['number'],
    fallYear = json['fallYear'],
    springYear = json['springYear'];
}