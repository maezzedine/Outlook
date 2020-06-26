import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/services/api.dart';
import 'package:mobile/styles/themes.dart';

class OutlookState {
  Map<String, dynamic> language;
  ThemeData theme = lightTheme;

  OutlookState({
    @required this.language,
    this.theme
  });
}

Future<OutlookState> initialOutlookState() async {
  return OutlookState(language: await getLanguage('ar'));
}