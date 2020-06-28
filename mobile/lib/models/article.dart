
import 'package:mobile/models/category.dart';
import 'package:mobile/models/comment.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/member.dart';

class Article {
  int id;
  DateTime dateTime;
  String title;
  String subtitle;
  String picturePath;
  String text;
  int rate;
  int numberOfVotes;
  int numberOfFavorites;
  Category category;
  Issue issue;
  Member writer;
  List<Comment> comments;

  Article.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    dateTime = DateTime.parse(json['dateTime']),
    title = json['title'],
    subtitle = json['subtitle'],
    picturePath = json['picturePath'],
    text = json['text'],
    rate = json['rate'],
    numberOfVotes = json['numberOfVotes'],
    numberOfFavorites = json['numberOfFavorites'],
    category = json['category'] != null? Category.fromJson(json['category']): null,
    issue = json['issue'] != null? Issue.fromJson(json['issue']) : null,
    writer = json['writer'] != null? Member.fromJson(json['writer']) : null,
    comments = json['comments']?.map<Comment>((c) => Comment.fromJson(c))?.toList(); 
}