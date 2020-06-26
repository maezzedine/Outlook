import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/styles/themes.dart';

class _ViewModel {
  final VoidCallback onPressed;

  _ViewModel({
    @required this.onPressed
  });

  @override
  bool operator ==(Object other) => 
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

final outlookAppBar =
  AppBar(
    title: Text('AUB Outlook'),
    actions: <Widget>[
      StoreConnector<OutlookState, _ViewModel>(
        distinct: true,
        converter: (state) => _ViewModel(onPressed: () => state.dispatch(
            SetThemeAction(
              theme: darkTheme
              // theme: state.state.theme == lightTheme 
              // ? darkTheme 
              // : lightTheme
            )
          )
        ),
        builder: (context, viewModel) => FlatButton(
          onPressed: () {
            print('Pressed');
            viewModel.onPressed();
          },
          child: Icon(Icons.invert_colors),
        ),
      ),
      StoreConnector<OutlookState, _ViewModel>(
        distinct: true,
        converter: (state) => _ViewModel(onPressed: () => state.dispatch(
            SetLanguageAction(
              abbreviation: 'en'
            )
          )
        ),
        builder: (context, viewModel) => FlatButton(
          onPressed: () {
            viewModel.onPressed();
          },
          child: Icon(Icons.invert_colors),
        ),
      )
    ],
  );
