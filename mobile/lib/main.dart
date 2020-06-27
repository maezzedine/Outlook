import 'package:flutter/material.dart';
import 'package:mobile/components/app.dart';
import 'package:mobile/services/appLanguage.dart';
import 'package:mobile/services/localizations.dart';
import 'package:mobile/styles/themes.dart';
import 'package:dynamic_theme/dynamic_theme.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:provider/provider.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  AppLanguage appLanguage = AppLanguage();
  await appLanguage.fetchLocale();
  runApp(OutlookApp(appLanguage: appLanguage));
}

class OutlookApp extends StatelessWidget {
  final AppLanguage appLanguage;

  OutlookApp({
    @required this.appLanguage
  });
  // Store store;

  // OutlookApp() {
  //   initialOutlookState().then((value) => store = Store<OutlookState>(outlookAppReducer, initialState: value));
  // }
  @override
  Widget build(BuildContext context) {
    return DynamicTheme(
      defaultBrightness: Brightness.dark,
      data: (brightness) => brightness == Brightness.light
        ? lightTheme
        : darkTheme,
      themedWidgetBuilder: (context, theme) => 
        ChangeNotifierProvider<AppLanguage>(
          create: (_) => appLanguage,
          child: Consumer<AppLanguage>(
            builder: (context, model, child) =>
              MaterialApp(
                title: "AUB Outlook",
                localizationsDelegates: [
                  OutlookAppLocalizations.delegate,
                  GlobalMaterialLocalizations.delegate,
                  GlobalWidgetsLocalizations.delegate,
                  GlobalCupertinoLocalizations.delegate
                ],
                supportedLocales: [
                  const Locale('ar', 'LB'),
                  const Locale('en', 'US'),
                ],
                locale: model.appLocale,
                theme: theme,
                home: App()
              )
          )
        )
    );
  }
}