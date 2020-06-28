import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/pages/home.dart';
import 'package:dynamic_theme/dynamic_theme.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:provider/provider.dart';

class App extends StatelessWidget {
  const App({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final currentTheme = DynamicTheme.of(context).data.brightness;
    final appLanguage = Provider.of<AppLanguage>(context);
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
          children: <Widget>[
            Padding(
              padding: EdgeInsets.all(30),
              child: Align(
                alignment: Alignment.centerLeft,
                child: SvgPicture.asset(
                  'assets/svgs/logo.svg',
                  width: 50,
                  color: Theme.of(context).textTheme.overline.color,
                ),
              ),
            ),
            Align(
              alignment: Alignment.centerLeft,
              child: Padding(
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