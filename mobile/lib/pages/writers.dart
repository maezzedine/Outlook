import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_redux/flutter_redux.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/member.dart';
import 'package:mobile/pages/member.dart';
import 'package:mobile/services/localizations.dart';

class _ViewModel {
  final List<Member> writers;

  _ViewModel({@required this.writers});

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class Writers extends StatelessWidget {
  const Writers({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    
    return StoreConnector<OutlookState, _ViewModel>(
      distinct: true,
      converter: (state) => _ViewModel(writers: state.state.writers),
      builder: (context, viewModel) {
        var writers = viewModel.writers.where((w) => w.language == OutlookAppLocalizations.of(context).translate('language')).toList();
        var length = writers.length;
        var midIndex = length ~/ 2;

        return Container(
          child: ListView(
            children: <Widget>[
              Container(
                padding: EdgeInsets.symmetric(horizontal: 20),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Container(
                      width: double.infinity,
                      decoration: BoxDecoration(border:
                        Border(bottom: BorderSide(
                          color: Theme.of(context).textTheme.bodyText1.color,
                        ))),
                      child: Text(
                        OutlookAppLocalizations.of(context).translate('writers'),
                        textAlign: TextAlign.end,
                        style: TextStyle(
                          fontSize: 25,
                          color: Theme.of(context).textTheme.bodyText1.color,
                        ),
                      ),
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: <Widget>[
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: writers.getRange(0, midIndex)
                            .map((w) => InkWell(
                              child: Container(
                                padding: EdgeInsets.symmetric(vertical: 5),
                                child: Text(
                                  '\u2022 ${w.name}',
                                  style: Theme.of(context).textTheme.bodyText1,
                                ),
                              ),
                              onTap: () => Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: MemberPage(memberId: w.id)))),
                            )).toList(),
                        ),
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: writers.getRange(midIndex, length)
                            .map((w) => InkWell(
                              child: Container(
                                padding: EdgeInsets.symmetric(vertical: 5),
                                child: Text(
                                  '\u2022 ${w.name}',
                                  style: Theme.of(context).textTheme.bodyText1,
                                ),
                              ),
                              onTap: () => Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: MemberPage(memberId: w.id)))),
                            )).toList(),
                        ),
                      ],
                    )
                  ],
                ),
              )
            ]
          ),
        );
      }
    );
  }
}