import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/article-thumbnail.dart';
import 'package:mobile/models/member.dart';
import 'package:mobile/services/api.dart';
import 'package:mobile/services/localizations.dart';

class MemberPage extends StatelessWidget {
  final int memberId;

  MemberPage({@required this.memberId});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: fetchMember(memberId),
      builder: (context, snapchot) {
        if (snapchot.hasData) {
          Member member = snapchot.data;
          return Container(
            child: ListView(
              children: <Widget>[
                Container(
                  padding: EdgeInsets.symmetric(horizontal: 20),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      Container(
                        width: double.infinity,
                        decoration: BoxDecoration(border:
                          Border(bottom: BorderSide(
                            color: Theme.of(context).textTheme.bodyText1.color,
                          ))),
                        child: Text(
                          member.name,
                          textAlign: TextAlign.end,
                          style: TextStyle(
                            fontSize: 25,
                            color: Theme.of(context).textTheme.bodyText1.color
                          ),
                        ),
                      ),
                      Row(
                        children: <Widget>[
                          Text(
                            '${OutlookAppLocalizations.of(context).translate('position')}:',
                            style: Theme.of(context).textTheme.bodyText1,
                          ),
                          Padding(
                            padding: EdgeInsets.symmetric(horizontal: 5),
                            child: Text(
                              '${member.position}',
                              style: Theme.of(context).textTheme.bodyText2
                                .merge(TextStyle(fontSize: 18)),
                            ),
                          ),
                        ],
                      ),
                      if (member.position == 'Junior Editor' || member.position == 'رئيس قسم')
                      Row(
                        children: <Widget>[
                          Text(
                            '${OutlookAppLocalizations.of(context).translate('category')}:',
                            style: Theme.of(context).textTheme.bodyText1,
                          ),
                          Padding(
                            padding: EdgeInsets.symmetric(horizontal: 5),
                            child: Text(
                              '${member.category.name}',
                              style: Theme.of(context).textTheme.bodyText2
                                .merge(TextStyle(fontSize: 18)),
                            ),
                          ),
                        ],
                      ),
                      Row(
                        children: <Widget>[
                          Text(
                            '${OutlookAppLocalizations.of(context).translate('number-of-articles')}',
                            style: Theme.of(context).textTheme.bodyText1,
                          ),
                          Padding(
                            padding: EdgeInsets.symmetric(horizontal: 5),
                            child: Text(
                              '${member.numberOfArticles}',
                              style: Theme.of(context).textTheme.bodyText2
                                .merge(TextStyle(fontSize: 18)),
                            )
                          ),
                        ],
                      )
                    ],
                  ),
                )
              ]..addAll(member.articles.map((a) => ArticleThumbnail(article: a))),
            ),
          );
        } else if (snapchot.hasError) {
          throw Exception('Failed to load member.');
        } else {
          return Center(child: CircularProgressIndicator());
        }
      },
    );
  }
}