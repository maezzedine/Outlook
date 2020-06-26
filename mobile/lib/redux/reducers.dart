import 'package:flutter/material.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';

Map<String, dynamic> languageReducer(Map<String, dynamic> state, OutlookAction action) {
  if (action is SetLanguageAction) {
    Map<String, dynamic> language;
    getLanguage(action.abbreviation).then((value) => language = value);
    return language;
  }
  return state;
}

ThemeData themeReducer(ThemeData state, OutlookAction action) {
  if (action is SetThemeAction) return action.theme;
  return state;
}

OutlookState outlookAppReducer(state, action) {
  return new OutlookState(
    language: languageReducer(state.language, action),
    theme: themeReducer(state.theme, action)
  );
}