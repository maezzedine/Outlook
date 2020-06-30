import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';
import 'package:mobile/services/localizations.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:redux/redux.dart';

class _ViewModel {
  final List<Issue> issues;
  final Issue issue;
  final List<Volume> volumes;
  final Volume volume;
  final Store<OutlookState> store;

  _ViewModel({
    @required this.issues,
    @required this.issue,
    @required this.volumes,
    @required this.volume,
    @required this.store
  });

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class Archives extends StatefulWidget {
  Archives({Key key}) : super(key: key);

  @override
  _ArchivesState createState() => _ArchivesState();
}

class _ArchivesState extends State<Archives> {
  Volume volume;
  Issue issue;

  @override
  Widget build(BuildContext context) {
    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (store) => _ViewModel(
        issues: store.state.issues,
        issue: store.state.issue, 
        volumes: store.state.volumes, 
        volume: store.state.volume, 
        store: store
      ),
      builder: (context, viewModel) {
        // volume = viewModel.volumes.last;
        // issue = viewModel.issues.last;

        return Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Padding(
                padding: EdgeInsets.symmetric(vertical: 20),
                child:  Text(
                  '${OutlookAppLocalizations.of(context).translate('volume')} ${viewModel.volume.number} | ' +
                  '${OutlookAppLocalizations.of(context).translate('issue')} ${viewModel.issue.number}',
                  style: TextStyle(
                    color: Theme.of(context).textTheme.bodyText1.color,
                    fontSize: 25
                  )
                ),
              ),
              Card(
                child: Padding(
                  padding: EdgeInsets.symmetric(vertical: 20),
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Row(
                        mainAxisSize: MainAxisSize.max,
                        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                        children: <Widget>[
                          Text(
                            OutlookAppLocalizations.of(context).translate('volume'),
                            style: TextStyle(
                              color: Theme.of(context).textTheme.bodyText1.color,
                              fontSize: 25
                            )
                          ),
                          DropdownButton<Volume> (
                            value: volume,
                            icon: Icon(Icons.arrow_drop_down),
                            iconSize: 20,
                            elevation: 16,
                            underline: Container(
                              height: 2,
                              color: Colors.white,
                            ),
                            onChanged: (Volume value) {
                              setState(() {
                                volume = value;
                                viewModel.store.dispatch(SetVolumeAction(volume: volume));
                                onVolumeChange(viewModel.store, volume.id, () => setState(() {}));
                                issue = null;
                              });
                            },
                            items: viewModel.volumes?.map<DropdownMenuItem<Volume>>((v) => DropdownMenuItem<Volume>(
                              value: v, 
                              child: Text(
                                v.number.toString(),
                                style: TextStyle(
                                  color: Theme.of(context).textTheme.bodyText2.color,
                                  fontSize: 25
                                )
                              )))?.toList(),
                          )
                        ],
                      ),
                      Row(
                        mainAxisSize: MainAxisSize.max,
                        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                        children: <Widget>[
                          Text(
                            OutlookAppLocalizations.of(context).translate('issue'),
                            style: TextStyle(
                              color: Theme.of(context).textTheme.bodyText1.color,
                              fontSize: 25
                            )
                          ),
                          DropdownButton<Issue> (
                            value: issue,
                            icon: Icon(Icons.arrow_drop_down),
                            iconSize: 20,
                            elevation: 16,
                            underline: Container(
                              height: 2,
                              color: Colors.white,
                            ),
                            onChanged: (Issue value) {
                              setState(() {
                                issue = value;
                                viewModel.store.dispatch(SetIssueAction(issue: issue));
                                onIssueChange(viewModel.store, issue.id, () => setState(() {}));
                              });
                            },
                            items: viewModel.issues?.map<DropdownMenuItem<Issue>>((i) => DropdownMenuItem<Issue>(
                              value: i, 
                              child: Text(
                                i.number.toString(),
                                style: TextStyle(
                                  color: Theme.of(context).textTheme.bodyText2.color,
                                  fontSize: 25
                                )
                              )))?.toList(),
                          )
                        ],
                      )
                    ],
                  ),
                ),
              )
            ],
          ) 
        );
      }
    ); 
  }
}