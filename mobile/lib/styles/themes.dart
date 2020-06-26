import 'package:flutter/material.dart';

final lightTheme = ThemeData(
  brightness: Brightness.light,
  backgroundColor: Color(0xFFf7f9fa), // Background
  canvasColor: Color(0xFFf2f2f5), // Side
  accentColor: Color(0xFF99e3ff),
  appBarTheme: AppBarTheme(
    color: Color(0xFFfefefe)
  ),
  primaryColor: Color(0xFFFFFEFEFE),
  textTheme: TextTheme(
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
  backgroundColor: Color(0xFF1e2934),
  canvasColor: Color(0xFF101921),
  accentColor: Color(0xFF162024),
  appBarTheme: AppBarTheme(
    color: Color(0xFF0c1115)
  ),
  textTheme: TextTheme(
    bodyText1: TextStyle(
      color: Color(0xFFAAAAAA)
    ),
    bodyText2: TextStyle(
      color: Color(0xFF808080)
    ),
  )
);