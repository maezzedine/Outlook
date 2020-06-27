import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

abstract class OutlookAction {
  @override
  String toString() {
    return '$runtimeType';
  }
}

class SetLanguageAction extends OutlookAction {
  // final Map<String, dynamic> language;
  // SetLanguageAction({@required this.language});
  final String abbreviation;
  SetLanguageAction({@required this.abbreviation});
}