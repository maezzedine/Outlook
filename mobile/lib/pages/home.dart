import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/services/localizations.dart';
import 'package:flutter_redux/flutter_redux.dart';

class _ViewModel {
  final Volume volume;
  final Issue issue;

  _ViewModel({
    @required this.issue,
    @required this.volume
  });

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;
  
  @override
  int get hashCode => 0;
}

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        StoreConnector<OutlookState, _ViewModel>(
          distinct: true,
          converter: (state) => _ViewModel(issue: state.state.issue, volume: state.state.volume),
          builder: (context, viewModel) => Padding(
            padding: EdgeInsets.symmetric(vertical: 10),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Text(
                  "${OutlookAppLocalizations.of(context).translate('volume')} ${viewModel.volume?.number} | ${viewModel.volume?.fallYear} - ${viewModel.volume?.springYear}",
                  style: TextStyle(
                    fontSize: 28,
                  ),
                ),
                Padding(
                  padding: EdgeInsets.all(10),
                  child: Container(height: 1, color: Theme.of(context).backgroundColor,),
                ),
                Text(
                  "${OutlookAppLocalizations.of(context).translate('issue')} ${viewModel.issue?.number}",
                  style: TextStyle(fontSize: 25),
                )
              ]
              ,
            ),
          ),
        ),
      ],
    );
  }
}