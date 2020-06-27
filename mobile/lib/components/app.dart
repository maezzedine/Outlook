import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-bar.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:mobile/services/localizations.dart';
import 'package:provider/provider.dart';

class App extends StatelessWidget {
  const App({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final appLanguage = Provider.of<AppLanguage>(context);

    return Scaffold(
      appBar: outlookAppBar(context),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Drawer(
            child: Text('Hello'),
          ),
          Text(OutlookAppLocalizations.of(context).translate('greetings')),
          FlatButton(
            child: Icon(Icons.language),
            onPressed: () {
              if (appLanguage.appLocale == Locale('ar'))
                appLanguage.changeLanguage(Locale('en'));
              else
                appLanguage.changeLanguage(Locale('ar'));
            },
          )
        ],
      ),
    );
  }
}