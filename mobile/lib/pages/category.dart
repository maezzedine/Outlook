import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/article-thumbnail.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/services/constants.dart';
import 'package:mobile/services/localizations.dart';
import 'package:flutter_redux/flutter_redux.dart';

class _ViewModel {
  final List<Category> categories;

  _ViewModel({@required this.categories});

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class CategoryPage extends StatelessWidget {
  final String categoryName;

  CategoryPage({@required this.categoryName});

  @override
  Widget build(BuildContext context) {
    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (state) => _ViewModel(categories: state.state.categories),
      builder: (context, viewModel) {
        var category = viewModel.categories.firstWhere((c) => c.name == categoryName);
        
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
                          color: CategoryColors[Theme.of(context).brightness][category.tag],
                        ))),
                      child: Text(
                        category.name,
                        textAlign: TextAlign.end,
                        style: TextStyle(
                          fontSize: 25,
                          color: CategoryColors[Theme.of(context).brightness][category.tag]
                        ),
                      ),
                    ),
                    Text(
                      '${OutlookAppLocalizations.of(context).translate('number-of-articles')} ${category.articles.length}',
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Text(
                      '${OutlookAppLocalizations.of(context).translate('junior-editors')}',
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: category.editors.map((e) => Text(
                        '-  ${e.name}',
                        style: Theme.of(context).textTheme.bodyText2
                      )).toList()
                    )
                  ],
                ),
              )
            ]..addAll(category.articles.map((a) => ArticleThumbnail(article: a))),
          ),
        );
      }
    );
  }
}