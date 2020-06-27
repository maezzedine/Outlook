import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:dynamic_theme/dynamic_theme.dart';

AppBar outlookAppBar(BuildContext context) {
  final currentTheme = DynamicTheme.of(context).data.brightness;
  
  return AppBar(
    title: Text('AUB Outlook'),
    actions: <Widget>[
      FlatButton(
        child: Icon(Icons.lightbulb_outline),
        onPressed: () {
          if (currentTheme == Brightness.light)
            DynamicTheme.of(context).setBrightness(Brightness.dark);
          else 
            DynamicTheme.of(context).setBrightness(Brightness.light);
        }
      )
    ],
  );
}