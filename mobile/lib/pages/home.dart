import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/services/localizations.dart';

class Home extends StatelessWidget {
  // const Home({Key key}) : super(key: key);

  final greetings = ['Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello','Hello'];

  @override
  Widget build(BuildContext context) {
    // return SliverList(
    //   delegate: SliverChildBuilderDelegate(
    //     (context, index) => (index < greetings.length)? ListTile(title: Text(greetings[index])) : null
    //   ),
    // );
    
    
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