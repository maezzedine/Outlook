import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/category.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/pages/home.dart';
import 'package:dynamic_theme/dynamic_theme.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:mobile/services/localizations.dart';
import 'package:provider/provider.dart';
import 'package:redux/redux.dart';
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
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    final currentTheme = DynamicTheme.of(context).data.brightness;
    final appLanguage = Provider.of<AppLanguage>(context);
    final currentLanguage = OutlookAppLocalizations.of(context).translate('language');
    final tabs = [
      'assets/svgs/home.svg',
      'assets/svgs/archive.svg',
      'assets/svgs/signup.svg',
      'assets/svgs/signup.svg',
    ];

    return Scaffold(
      drawer: Drawer(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          mainAxisSize: MainAxisSize.max,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Padding(
              padding: EdgeInsets.all(10),
              child: SvgPicture.asset(
                'assets/svgs/logo.svg',
                width: 50,
                color: Theme.of(context).textTheme.overline.color,
              ),
            ),
            StoreConnector<OutlookState, _ViewModel>(
              distinct: true,
              converter: (state) => _ViewModel(categories: state.state.categories),
              builder: (context, viewModel) => Flexible(
                  child: ListView(
                    children: viewModel.categories
                    ?.where((c) => c.language.toLowerCase() == currentLanguage.toLowerCase())
                    ?.map<ListTile>((c) => ListTile(
                        leading: SvgPicture.asset(
                          'assets/svgs/${c.tag.toLowerCase()}.svg',
                          width: 25,
                          color: Theme.of(context).textTheme.bodyText1.color
                        ),
                        title: Text(c.name),
                        subtitle: Text(_ArticleLanguageCount(context, c.articles.length)),
                      ))
                    ?.toList(),
                  ),
                )
            ),
            Padding(
              padding: EdgeInsets.all(10),
              child: FlatButton(
                child: SvgPicture.asset(
                  currentTheme == Brightness.light? 'assets/svgs/moon.svg' : 'assets/svgs/sun.svg',
                  width: 40,
                  color: Theme.of(context).textTheme.overline.color
                ),
                onPressed: () {
                  if (currentTheme == Brightness.light)
                    DynamicTheme.of(context).setBrightness(Brightness.dark);
                  else 
                    DynamicTheme.of(context).setBrightness(Brightness.light);
                }
              ),
            ) 
          ],
        )
      ),
      body: DefaultTabController(
        length: tabs.length,
        child: NestedScrollView(
          headerSliverBuilder: (context, innerBoxIsScrolled) {
            return [
              SliverAppBar(
                actions: <Widget>[
                  FlatButton(
                    child: SvgPicture.asset(
                      'assets/svgs/languages.svg',
                      width: 20,
                      color: Theme.of(context).textTheme.bodyText2.color,
                    ),
                    onPressed: () {
                      if (appLanguage.appLocale == Locale('ar'))
                        appLanguage.changeLanguage(Locale('en'));
                      else
                        appLanguage.changeLanguage(Locale('ar'));
                    },
                  )
                ],
              ),
              SliverPersistentHeader(
                pinned: true,
                delegate: _SliverAppBarDelegate(
                  TabBar(
                    indicator: _AppBarDecorator(color: Theme.of(context).accentColor, count: tabs.length),
                    tabs: <Widget>[
                      for (final svg in tabs) 
                        Tab(icon: SvgPicture.asset(svg, width: 20, color: Theme.of(context).textTheme.bodyText2.color))
                    ],
                  )
                ),
              )
            ];
          },
          body: TabBarView(
            children: <Widget>[
              Home(),
              // Archives(),
              Home(),
              Home(),
              Home(),
            ],
          ),
        )
      ) 
    );
  }
}

class _SliverAppBarDelegate extends SliverPersistentHeaderDelegate {
  final TabBar tabBar;

  _SliverAppBarDelegate(this.tabBar);
  
  @override
  Widget build(BuildContext context, double shrinkOffset, bool overlapsContent) {
    return Container(
      color: Theme.of(context).appBarTheme.color,
      child: tabBar,
    );
  }

  @override
  double get maxExtent => tabBar.preferredSize.height;

  @override
  double get minExtent => tabBar.preferredSize.height;

  @override
  bool shouldRebuild(SliverPersistentHeaderDelegate oldDelegate) => true;
}

class _AppBarDecorator extends Decoration {
  final BoxPainter painter;
  final int count;

  _AppBarDecorator({@required Color color, @required this.count})
    : painter = _AppBarBoxPainter(color, count);

  @override
  BoxPainter createBoxPainter([onChanged]) => painter;
}

class _AppBarBoxPainter extends BoxPainter {
  final Paint _paint;
  final int count;

  _AppBarBoxPainter(Color color, this.count)
    : _paint = Paint()
      ..color = color
      ..isAntiAlias = true;

  @override
  void paint(Canvas canvas, Offset offset, ImageConfiguration configuration) {
    var rect = Rect.fromCenter(center: Offset(offset.dx + configuration.size.width / 2, offset.dy + configuration.size.height), height: 3, width: 100);
    canvas.drawRect(rect, _paint);
  }

}

String _ArticleLanguageCount(BuildContext context ,int count) {
  switch (count) {
    case 0: return '';
    case 1: return '${OutlookAppLocalizations.of(context).translate('one-article')}';
    case 2: return '${OutlookAppLocalizations.of(context).translate('two-articles')}';
    default: return '$count ${OutlookAppLocalizations.of(context).translate('articles')}';
  }
}