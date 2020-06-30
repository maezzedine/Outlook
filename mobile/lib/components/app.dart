import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/pages/archives.dart';
import 'package:mobile/pages/home.dart';
import 'package:mobile/pages/top-stats.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';
import 'package:redux/redux.dart';

class App extends StatefulWidget {
  final Store store;
  const App({Key key, @required this.store}) : super(key: key);

  @override
  _AppState createState() => _AppState();
}

class _AppState extends State<App> {
  Volume volume;
  Issue issue;

  @override
  void initState() {
    super.initState();
    fetchVolumes().then((v) {
      volume = v.last;
      widget.store.dispatch(SetVolumesAction(volumes: v));
      widget.store.dispatch(SetVolumeAction(volume: volume));
      onVolumeChange(widget.store, volume.id, () => setState(() {}));
    });
  }

  @override
  Widget build(BuildContext context) {
    return AppScaffold(
      isMainScreen: true,
      body: TabBarView(
        children: <Widget>[
          Home(),
          Archives(),
          TopStatsPage(),
          Home(),
        ],
      ),
    );
  }
}