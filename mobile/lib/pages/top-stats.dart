import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/topStats.dart';
import 'package:mobile/pages/article.dart';
import 'package:mobile/pages/member.dart';
import 'package:mobile/services/api.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:mobile/services/localizations.dart';
import 'package:flutter_svg/flutter_svg.dart';

class _ViewModel {
  final TopStats topStats;

  _ViewModel({@required this.topStats});

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class TopStatsPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (state) => _ViewModel(topStats: state.state.topStats),
      builder: (context, viewModel) {
        var topStats = viewModel.topStats;
        return ListView(
          children: <Widget>[
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 20, vertical: 20),
              child: Card (
                color: Theme.of(context).appBarTheme.color,
                child: Column(
                  children: <Widget> [
                    Text(
                      OutlookAppLocalizations.of(context).translate('top-rated-articles'),
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Column(
                      children: topStats.topVotedArticles
                        ?.where((a) => a.category.language == OutlookAppLocalizations.of(context).translate('language'))
                        ?.map<ListTile>((a) => ListTile(
                          onTap: () => Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: ArticlePage(article: a)))),
                          title: Row(
                            children: <Widget>[
                              Expanded(
                                child: Text(
                                  '\u2022 ${a.title}',
                                  style: Theme.of(context).textTheme.bodyText2,
                                )
                              ), 
                              Padding(
                                padding: EdgeInsets.symmetric(horizontal: 5),
                                child: Text(a.rate.toString()),
                              ),
                              SvgPicture.asset(
                                'assets/svgs/thumb-up.svg',
                                width: 20,
                                color: Theme.of(context).textTheme.bodyText2.color,
                              )
                            ],
                          ),
                        ))
                        ?.toList(),
                    )
                  ]
                )
              ),
            ),
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 20),
              child: Card (
                color: Theme.of(context).appBarTheme.color,
                child: Column(
                  children: <Widget> [
                    Text(
                      OutlookAppLocalizations.of(context).translate('top-favorited-articles'),
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Column(
                      children: topStats.topFavoritedArticles
                        ?.where((a) => a.category.language == OutlookAppLocalizations.of(context).translate('language'))
                        ?.map<ListTile>((a) => ListTile(
                          onTap: () => Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: ArticlePage(article: a)))),
                          title: Row(
                            children: <Widget>[
                              Expanded(
                                child: Text(
                                  '\u2022 ${a.title}',
                                  style: Theme.of(context).textTheme.bodyText2)
                              ), 
                              Padding(
                                padding: EdgeInsets.symmetric(horizontal: 5),
                                child: Text(a.numberOfFavorites.toString()),
                              ),
                              SvgPicture.asset(
                                'assets/svgs/star-fill.svg',
                                width: 20,
                                color: Theme.of(context).textTheme.bodyText2.color,
                              )
                            ],
                          ),
                        ))
                        ?.toList(),
                    )
                  ]
                )
              ),
            ),
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 20, vertical: 20),
              child: Card (
                color: Theme.of(context).appBarTheme.color,
                child: Column(
                  children: <Widget> [
                    Text(
                      OutlookAppLocalizations.of(context).translate('top-writers'),
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Column(
                      children: topStats.topWriters
                        ?.where((a) => a.language == OutlookAppLocalizations.of(context).translate('language'))
                        ?.map<ListTile>((w) => ListTile(
                          onTap: () => Navigator.push(context, MaterialPageRoute(
                            builder: (context) => AppScaffold(body: MemberPage(member: w))
                          )),
                          title: Row(
                            children: <Widget>[
                              Expanded(
                                child: Text(
                                  '\u2022 ${w.name}',
                                  style: Theme.of(context).textTheme.bodyText2)
                              ), 
                              Padding(
                                padding: EdgeInsets.symmetric(horizontal: 5),
                                child: Text(w.numberOfArticles.toString()),
                              ),
                              SvgPicture.asset(
                                'assets/svgs/papers.svg',
                                width: 20,
                                color: Theme.of(context).textTheme.bodyText2.color,
                              )
                            ],
                          ),
                        ))
                        ?.toList(),
                    )
                  ]
                )
              ),
            ),
          ],
        );
      },
    );
  }
}