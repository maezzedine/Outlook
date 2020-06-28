import 'package:flutter/material.dart';

final lightTheme = ThemeData(
  brightness: Brightness.light,
  backgroundColor: Color(0xFFf7f9fa), // Background
  canvasColor: Color(0xFFf2f2f5), // Side
  accentColor: Color(0xFFfefefe),
  appBarTheme: AppBarTheme(
    color: Color(0xFFfefefe)
  ),
  primaryColor: Color(0xFFFFFEFEFE),
  textTheme: TextTheme(
    overline: TextStyle(
      color: Colors.black
    ),
    bodyText1: TextStyle(
      color: Color(0xFF262655)
    ),
    bodyText2: TextStyle(
      color: Color(0xFF404040)
    ),
  ),
);

final darkTheme = ThemeData(
  brightness: Brightness.dark,
  canvasColor: Color(0xFF293540),
  accentColor: Color(0xFFa1acb0),
  appBarTheme: AppBarTheme(
    color: Color(0xFF0c1115),
  ),
  textTheme: TextTheme(
    overline: TextStyle(
      color: Colors.grey
    ),
    bodyText1: TextStyle(
      color: Color(0xFFAAAAAA)
    ),
    bodyText2: TextStyle(
      color: Color(0xFF808080)
    ),
  )
);