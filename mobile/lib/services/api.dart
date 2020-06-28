import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';

final String API = 'http://192.168.50.104:5000';

Future<Map<String, dynamic>> getLanguage(String abbreviation) async {
  String languageJsonString = await rootBundle.loadString('assets/languages/$abbreviation.json');
  return json.decode(languageJsonString);
}

Future<List<Volume>> fetchVolumes() async {
  var response = await http.get('$API/volumes');

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((v) => Volume.fromJson(v)).toList();
  } else {
    throw Exception('Failed to load volumes.');
  }
}

Future<List<Issue>> fetchIssues(int volumeId) async {
  var response = await http.get('$API/issues/$volumeId');

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((v) => Issue.fromJson(v)).toList();
  } else {
    throw Exception('Failed to load issues.');
  }
}

Future<List<Category>> fetchCategories(int issueId) async {
  var response = await http.get('$API/categories/$issueId');

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((c) => Category.fromJson(c)).toList();
  } else {
    throw Exception('Failed to load categories.');
  }
}