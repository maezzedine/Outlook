import 'dart:convert';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/user.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/pages/archives.dart';
import 'package:mobile/pages/authentication.dart';
import 'package:mobile/pages/home.dart';
import 'package:mobile/pages/top-stats.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/api.dart';
import 'package:redux/redux.dart';
import 'package:shared_preferences/shared_preferences.dart';

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
    getUserFromSharedPreferences().then((u) => widget.store.dispatch(SetUserAction(user: u)));
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
          Authentication(),
        ],
      ),
    );
  }
}

Future<User> getUserFromSharedPreferences() async {
  
  final prefs = await SharedPreferences.getInstance();
  final userJsonString = prefs.getString('outlook-user');
  Map<String, dynamic> jsonUser = userJsonString != null? json.decode(userJsonString) : null;
  if (jsonUser == null) return null;

  var jsonToken = decodeJwtToken(jsonUser['token']);
  if (jsonToken != null) {
    var expirayDateString = jsonToken['exp'];
    var expiryDate = DateTime.fromMillisecondsSinceEpoch(expirayDateString * 1000);
    var now = DateTime.now();
    var diff = expiryDate.millisecondsSinceEpoch - now.millisecondsSinceEpoch;
    if (Duration(milliseconds: diff).inHours > 24) {
      return User.fromJson(jsonUser);
    }
  }
  return null;
}

Map<String, dynamic> decodeJwtToken(String token) {
  if (token == null) return null;

  final parts = token.split('.');
  if (parts.length != 3) throw Exception('Invalid token');

  var header = _decodeBase64(parts[1]);
  return json.decode(header);
}

String _decodeBase64(String str) {
  String output = str.replaceAll('-', '_').replaceAll('+', '/');

  switch (output.length % 4) {
    case 0:
      break;
    case 2:
      output += '==';
      break;
    case 3:
      output += '=';
      break;
    default:
      throw Exception('Illegal base64url string.');
  }

  return utf8.decode(base64Url.decode(output));
}
