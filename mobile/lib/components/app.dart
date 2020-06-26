import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:mobile/components/app-bar.dart';
import 'package:mobile/models/OutlookState.dart';

class _ViewModel {
  final String text;

  _ViewModel({
    @required this.text
  });

  @override
  bool operator ==(Object other) =>
    identical(this, other) || 
      other is _ViewModel &&
      runtimeType == other.runtimeType && 
      text == other.text;

  @override
  int get hashCode => 0;
}

class App extends StatelessWidget {
  const App({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: outlookAppBar,
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Text('hello', style: TextStyle(fontSize: 40)),
          StoreConnector<OutlookState, _ViewModel>(
            distinct: true,
            converter: (store) => new _ViewModel(text: store.state.language['greetings']),
            builder: (context, viewModel) => Text(viewModel.text),
          ),
        ],
      ),
    );
  }
}