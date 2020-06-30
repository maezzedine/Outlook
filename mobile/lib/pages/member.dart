import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/article-thumbnail.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/member.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:mobile/services/localizations.dart';

class _ViewModel {
  final List<Article> articles;

  _ViewModel({@required this.articles});

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class MemberPage extends StatelessWidget {
  final Member member;

  MemberPage({@required this.member});

  @override
  Widget build(BuildContext context) {
    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (state) => _ViewModel(articles: state.state.articles),
      builder: (context, viewModel) {
        var articles = viewModel.articles.where((a) => a.writer.id == member.id);
        
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
                            '${articles.length}',
                            style: Theme.of(context).textTheme.bodyText2
                              .merge(TextStyle(fontSize: 18)),
                          )
                        ),
                      ],
                    )
                  ],
                ),
              )
            ]..addAll(articles.map((a) => ArticleThumbnail(article: a))),
          ),
        );
      },
    );
  }
}