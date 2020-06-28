import 'package:flutter/cupertino.dart';
import 'package:mobile/models/volume.dart';

class Issue {
  int id;
  int number;
  String arabicTheme;
  String englishTheme;
  Volume volume;

  Issue({
    @required this.id,
    @required this.number,
    @required this.arabicTheme,
    @required this.englishTheme,
    @required this.volume
  });

  Issue.fromJson(Map<String, dynamic> json) :
    id = json['id'],
    number = json['number'],
    arabicTheme = json['arabicTheme'],
    englishTheme = json['englishTheme'],
    volume = (json['volume'] != null)? Volume.fromJson(json['volume']) : null;
}