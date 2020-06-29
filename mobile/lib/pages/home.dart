import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/article-thumbnail.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/services/localizations.dart';
import 'package:flutter_redux/flutter_redux.dart';

class _ViewModel {
  final Volume volume;
  final Issue issue;
  final List<Article> articles;

  _ViewModel({
    @required this.issue,
    @required this.volume,
    @required this.articles
  });

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;
  
  @override
  int get hashCode => 0;
}

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final currentLanguage = OutlookAppLocalizations.of(context).translate('language');

    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (state) => _ViewModel(
        issue: state.state.issue, 
        volume: state.state.volume,
        articles: state.state.articles
      ),
      builder: (context, viewModel) =>
        (viewModel.articles != null)?
        ListView(
          children: <Widget>[
            Padding(
              padding: EdgeInsets.symmetric(vertical: 10),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Text(
                    "${OutlookAppLocalizations.of(context).translate('volume')} ${viewModel.volume?.number} | ${viewModel.volume?.fallYear} - ${viewModel.volume?.springYear}",
                    style: TextStyle(
                      fontSize: 28,
                    ),
                  ),
                  Padding(
                    padding: EdgeInsets.all(10),
                    child: Container(height: 1, color: Theme.of(context).backgroundColor,),
                  ),
                  Text(
                    "${OutlookAppLocalizations.of(context).translate('issue')} ${viewModel.issue?.number}",
                    style: TextStyle(fontSize: 25),
                  ),
                ],
              ),
            ),
          ]..addAll(viewModel.articles
              ?.where((a) => a.category.language == currentLanguage)
              ?.map<ArticleThumbnail>((a) => ArticleThumbnail(article: a))),
        ) : Container()
    );
  }
}