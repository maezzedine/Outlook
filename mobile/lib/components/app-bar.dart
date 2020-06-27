import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:provider/provider.dart';
import 'package:flutter_svg/flutter_svg.dart';

SliverAppBar outlookAppBar(BuildContext context, List<String> svgs) {
  final appLanguage = Provider.of<AppLanguage>(context);

  return SliverAppBar(
    floating: true,
    actions: <Widget>[
      FlatButton(
        child: SvgPicture.asset(
          'assets/svgs/signin.svg',
          width: 20,
          color: Theme.of(context).textTheme.bodyText2.color,
        ),
        onPressed: () {
          
        },
      ),
      FlatButton(
        child: SvgPicture.asset(
          'assets/svgs/signup.svg',
          width: 20,
          color: Theme.of(context).textTheme.bodyText2.color,
        ),
        onPressed: () {
          
        },
      ),
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
  );
}