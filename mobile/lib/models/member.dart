import 'package:mobile/models/category.dart';

class Member {
  int id;
  String name;
  String language;
  String position;
  int numberOfArticles;
  Category category;

  Member.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    name = json['name'],
    language = json['language'],
    position = json['position'],
    numberOfArticles = json['numberOfArticles'],
    category = json['category'] != null? Category.fromJson(json['category']) : null;
}