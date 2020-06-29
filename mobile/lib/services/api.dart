import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/services.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/services/constants.dart';

Future<Map<String, dynamic>> getLanguage(String abbreviation) async {
  String languageJsonString = await rootBundle.loadString('assets/languages/$abbreviation.json');
  return json.decode(languageJsonString);
}

Future<List<Volume>> fetchVolumes() async {
  var response = await http.get(_buildUrl(path: 'volumes'));

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((v) => Volume.fromJson(v)).toList();
  } else {
    throw Exception('Failed to load volumes.');
  }
}

Future<List<Issue>> fetchIssues(int volumeId) async {
  var response = await http.get(_buildUrl(path: 'issues/$volumeId'));

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((v) => Issue.fromJson(v)).toList();
  } else {
    throw Exception('Failed to load issues.');
  }
}

Future<List<Category>> fetchCategories(int issueId) async {
  var response = await http.get(_buildUrl(path: 'categories/$issueId'));

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((c) => Category.fromJson(c)).toList();
  } else {
    throw Exception('Failed to load categories.');
  }
}

Future<List<Article>> fetchArticle(int issueId) async {
  var response = await http.get(_buildUrl(path: 'articles/$issueId'));

  if (response.statusCode == 200) {
    Iterable jsonList = json.decode(response.body);
    return jsonList.map((a) => Article.fromJson(a)).toList();
  } else {
    throw Exception('Failed to load articles.');
  }
}

_buildUrl({String path, dynamic params}) =>
  Uri.http(API_URL, path, params);