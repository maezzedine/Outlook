import 'dart:convert';

import 'package:flutter/services.dart';

Future<Map<String, dynamic>> getLanguage(String abbreviation) async {
  String languageJsonString = await rootBundle.loadString('assets/languages/$abbreviation.json');
  return json.decode(languageJsonString);
}