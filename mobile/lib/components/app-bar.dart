import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:provider/provider.dart';
import 'package:flutter_svg/flutter_svg.dart';

List<Widget> outlookAppBar(BuildContext context, List<String> svgs) {
  final appLanguage = Provider.of<AppLanguage>(context);

  return <Widget> [
    SliverAppBar(
      floating: true,
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
          indicator: _AppBarDecorator(backgroundColor: Theme.of(context).accentColor, count: svgs.length),
          tabs: <Widget>[
            for (final svg in svgs) 
              Tab(icon: SvgPicture.asset(svg, width: 20, color: Theme.of(context).textTheme.bodyText2.color))
          ],
        ),
      ),
    )
  ] ;
}

class _SliverAppBarDelegate extends SliverPersistentHeaderDelegate {
  final TabBar tabBar;

  _SliverAppBarDelegate(this.tabBar);
  
  @override
  Widget build(BuildContext context, double shrinkOffset, bool overlapsContent) {
    return Container(
      color: Theme.of(context).appBarTheme.color,
      child: tabBar
    );
  }

  @override
  double get maxExtent => tabBar.preferredSize.height;

  @override
  double get minExtent => tabBar.preferredSize.height;

  @override
  bool shouldRebuild(SliverPersistentHeaderDelegate oldDelegate) => false;
}

class _AppBarDecorator extends Decoration {
  final BoxPainter painter;
  final int count;

  _AppBarDecorator({@required Color backgroundColor, @required this.count})
    : painter = _AppBarBoxPainter(backgroundColor, count);

  @override
  BoxPainter createBoxPainter([onChanged]) => painter;
}

class _AppBarBoxPainter extends BoxPainter {
  final Paint _paint;
  final int count;

  _AppBarBoxPainter(Color backgroundColor, this.count)
    : _paint = Paint()
      ..color = backgroundColor
      ..isAntiAlias = true;

  @override
  void paint(Canvas canvas, Offset offset, ImageConfiguration configuration) {
    var rect = Rect.fromCenter(center: Offset(offset.dx + configuration.size.width / 2, offset.dy + configuration.size.height), height: 3, width: 100);
    canvas.drawRect(rect, _paint);
  }

}