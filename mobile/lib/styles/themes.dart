import 'package:flutter/material.dart';
import 'package:mobile/services/localizations.dart';

lightTheme(BuildContext context) => ThemeData(
  brightness: Brightness.light,
  backgroundColor: Color(0xFFf7f9fa), // Background
  canvasColor: Color(0xFFf2f2f5), // Side
  accentColor: Color(0xFF404040),
  appBarTheme: AppBarTheme(
    color: Color(0xFFfefefe)
  ),
  primaryColor: Color(0xFFFFFEFEFE),
  snackBarTheme: SnackBarThemeData(
    backgroundColor: Color(0xFF566573)
  ),
  fontFamily: OutlookAppLocalizations.of(context)?.locale?.toLanguageTag() == "ar"? 'Scheherazade': 'Baloo2',
  cardColor: Color(0xFFfefefe),
  buttonColor: Color(0xFF566573),
  textTheme: TextTheme(
    overline: TextStyle(
      color: Colors.black
    ),
    bodyText1: TextStyle(
      color: Color(0xFF262655),
      fontSize: 18
    ),
    bodyText2: TextStyle(
      color: Color(0xFF404040),
      fontSize: 15
    ),
    button: TextStyle(
      color: Color(0xFF00b9ff)
    )
  ),
);

darkTheme(BuildContext context) => ThemeData(
  brightness: Brightness.dark,
  canvasColor: Color(0xFF293540),
  accentColor: Color(0xFFa1acb0),
  appBarTheme: AppBarTheme(
    color: Color(0xFF0c1115),
  ),
  snackBarTheme: SnackBarThemeData(
    backgroundColor: Color(0xFFABB2B9)
  ),
  buttonColor: Color(0xFFABB2B9),
  fontFamily: OutlookAppLocalizations.of(context)?.locale?.toLanguageTag() == "ar"? 'Scheherazade': 'Baloo2',
  cardColor: Color(0xFF0c1115),
  textTheme: TextTheme(
    overline: TextStyle(
      color: Colors.grey
    ),
    bodyText1: TextStyle(
      color: Colors.white,
      fontSize: 18
    ),
    bodyText2: TextStyle(
      color: Color(0xFF808080),
      fontSize: 15
    ),
    button: TextStyle(
      color: Color(0xFF33c7ff)
    )
  )
);