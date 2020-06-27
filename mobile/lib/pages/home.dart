import 'package:flutter/cupertino.dart';
import 'package:mobile/services/localizations.dart';

class Home extends StatelessWidget {
  const Home({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Align(
          alignment: Alignment.center,
          child: Text(
            OutlookAppLocalizations.of(context).translate('greetings'),
            style: TextStyle(fontSize: 30)
          ),
        ),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
        Padding(padding: EdgeInsets.all(40), child: Text('Hello')),
      ],
    );
  }
}