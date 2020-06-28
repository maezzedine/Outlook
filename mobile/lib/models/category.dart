import 'package:mobile/models/article.dart';
import 'package:mobile/models/member.dart';

class Category {
  int id;
  String tag;
  String language;
  String name;
  List<Article> articles;
  List<Member> editors;

  Category.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    tag = json['tag'],
    language = json['language'],
    name = json['name'],
    articles = json['articles']?.map<Article>((a) => Article.fromJson(a))?.toList(),
    editors = json['editors']?.map<Member>((e) => Member.fromJson(e))?.toList();
}