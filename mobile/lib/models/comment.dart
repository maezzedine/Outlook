import 'package:mobile/models/article.dart';
import 'package:mobile/models/user.dart';

class Comment {
  int id;
  DateTime dateTime;
  String text;
  User user;
  Article article;

  Comment.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    dateTime = DateTime.parse(json['dateTime']),
    text = json['text'],
    user = json['user'] != null? User.fromJson(json['user']) : null,
    article = json['article'] != null? Article.fromJson(json['article']): null;
}