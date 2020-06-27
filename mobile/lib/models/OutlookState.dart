import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/services/api.dart';

class OutlookState {
  Map<String, dynamic> language;

  OutlookState({
    @required this.language,
  });
}

Future<OutlookState> initialOutlookState() async {
  return OutlookState(language: await getLanguage('ar'));
}