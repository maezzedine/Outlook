import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/services.dart';

class OutlookAppLocalizations {
  final Locale locale;

  OutlookAppLocalizations(this.locale);

  static OutlookAppLocalizations of (BuildContext context) {
    return Localizations.of<OutlookAppLocalizations>(context, OutlookAppLocalizations);
  }

  static const LocalizationsDelegate<OutlookAppLocalizations> delegate = _OutlookAppLocalizationsDelegate();

  Map<String, String> _localizedValues;

  Future<bool> load() async {
    String jsonString = await rootBundle.loadString('assets/languages/${locale.languageCode}.json');
    Map<String, dynamic> jsonMap = json.decode(jsonString);

    _localizedValues = jsonMap.map((key, value) {
      return MapEntry(key, value.toString());
    });

    return true;
  }

  String translate(String key) {
    return _localizedValues[key];
  }
}

class _OutlookAppLocalizationsDelegate extends LocalizationsDelegate<OutlookAppLocalizations> {
  const _OutlookAppLocalizationsDelegate();

  @override
  bool isSupported(Locale locale) {
    return ['ar', 'en'].contains(locale.languageCode);
  }
  
    @override
  Future<OutlookAppLocalizations> load (Locale locale) async {
    OutlookAppLocalizations localizations = OutlookAppLocalizations(locale);
    await localizations.load();
    return localizations;
  }
  
  @override
  bool shouldReload(_OutlookAppLocalizationsDelegate old) => false;
}