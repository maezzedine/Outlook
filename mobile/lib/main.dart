import 'package:flutter/material.dart';
import 'package:mobile/components/app.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/redux/reducers.dart';
import 'package:mobile/styles/themes.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:redux/redux.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(OutlookApp());
}

class OutlookApp extends StatelessWidget {
  Store store;

  OutlookApp() {
    initialOutlookState().then((value) => store = Store<OutlookState>(outlookAppReducer, initialState: value));
  }
  
  @override
  Widget build(BuildContext context) {
    return StoreProvider<OutlookState>(
      store: store,
      child: MaterialApp(
        title: 'AUB Outlook',
        theme: lightTheme,
        home: App()
      )
    );
  }
}