import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';

class Member {
  int id;
  String name;
  String language;
  String position;
  int numberOfArticles;
  Category category;
  List<Article> articles;

  Member.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    name = json['name'],
    language = json['language'],
    position = json['position'],
    numberOfArticles = json['numberOfArticles'],
    category = json['category'] != null? Category.fromJson(json['category']) : null,
    articles = json['articles']?.map<Article>((a) => Article.fromJson(a))?.toList();
}