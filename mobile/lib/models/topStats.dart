import 'package:mobile/models/article.dart';
import 'package:mobile/models/member.dart';

class TopStats {
  List<Article> topVotedArticles;
  List<Article> topFavoritedArticles;
  List<Member> topWriters;

  TopStats({
    this.topFavoritedArticles,
    this.topVotedArticles,
    this.topWriters
  });
}