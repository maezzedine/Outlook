import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/issue.dart';
import 'package:mobile/models/volume.dart';
import 'package:mobile/services/api.dart';
import 'package:mobile/services/localizations.dart';

class Archives extends StatefulWidget {
  Archives({Key key}) : super(key: key);

  @override
  _ArchivesState createState() => _ArchivesState();
}

class _ArchivesState extends State<Archives> {
  Future<List<Volume>> volumes;
  // Future<List<Issue>> issues;
  Volume volume;
  // Issue issue;

  @override
  void initState() {
    super.initState();
    volumes = fetchVolumes().then((_volumes) {
      volume = _volumes[_volumes.length -1];
      // issues = fetchIssues(volume.id).then((_issues) {
      //   issue = _issues[_issues.length - 1];
      //   return _issues;
      // });
      return volumes;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Container(
       child: Column(
         children: <Widget>[
            FutureBuilder(
              future: volumes,
              builder: (context, snapshot) {
                if (snapshot.hasData) {
                  List<Volume> _volumes = snapshot.data;
                  volume = _volumes[_volumes.length - 1];
                  List<DropdownMenuItem<Volume>> menuItems = _volumes.map<DropdownMenuItem<Volume>>((v) => 
                    DropdownMenuItem<Volume>(
                      value: v,
                      child: Text(v.number.toString()))
                    ).toList();

                  return Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      Text(OutlookAppLocalizations.of(context).translate('volume')),
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
                          });
                        },
                        items: menuItems,
                      )
                    ],
                  );
                } else if (snapshot.hasError) {
                  return Text(snapshot.error);
                }
                return Center(child: CircularProgressIndicator());
              },
            ),
            // FutureBuilder(
            //   future: issues,
            //   builder: (context, snapshot) {
            //     if (snapshot.hasData) {
            //       List<Issue> _issues = snapshot.data;
            //       issue = _issues[_issues.length - 1];
            //       List<DropdownMenuItem<Issue>> menuItems = _issues.map<DropdownMenuItem<Issue>>((i) => 
            //         DropdownMenuItem<Issue>(
            //           value: i,
            //           child: Text(i.number.toString()))
            //         ).toList();

            //       return Row(
            //         mainAxisAlignment: MainAxisAlignment.center,
            //         children: <Widget>[
            //           Text(OutlookAppLocalizations.of(context).translate('issue')),
            //           DropdownButton<Issue> (
            //             value: issue,
            //             icon: Icon(Icons.arrow_drop_down),
            //             iconSize: 20,
            //             elevation: 16,
            //             underline: Container(
            //               height: 2,
            //               color: Colors.white,
            //             ),
            //             onChanged: (Issue value) {
            //               setState(() {
            //                 issue = value;
            //               });
            //             },
            //             items: menuItems,
            //           )
            //         ],
            //       );
            //     } else if (snapshot.hasError) {
            //       return Text(snapshot.error);
            //     }
            //     return Center(child: CircularProgressIndicator());
            //   },
            // ),
            // Text(volume?.number.toString())
         ],
       ) 
    );
  }
}