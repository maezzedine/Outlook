import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/pages/home.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';
import 'package:redux/redux.dart';

class App extends StatefulWidget {
  final Store store;
  const App({Key key, @required this.store}) : super(key: key);

  @override
  _AppState createState() => _AppState();
}

class _AppState extends State<App> {
  Volume volume;
  Issue issue;
  List<Category> categories;
  List<Article> articles;

  @override
  void initState() {
    super.initState();
    fetchVolumes().then((v) {
      volume = v[v.length - 1];
      widget.store.dispatch(SetVolumeAction(volume: volume));
      fetchIssues(volume.id).then((i) {
        issue = i[i.length - 1];
        widget.store.dispatch(SetIssueAction(issue: issue));
        fetchCategories(issue.id).then((c) {
          categories = c;
          widget.store.dispatch(SetCategoriesAction(categories: c));
            setState(() { });
        });
        fetchArticle(issue.id).then((a) {
          articles = a;
          widget.store.dispatch(SetArticlesAction(articles: a));
          setState(() { });
        });
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      isMainScreen: true,
      body: TabBarView(
        children: <Widget>[
          Home(),
          // Archives(),
          Home(),
          Home(),
          Home(),
        ],
      ),
    );
  }
}