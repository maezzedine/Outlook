import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/models/category.dart';

const API_URL = '192.168.50.104:5000';
const SERVER_URL = '192.168.50.104:5001';

const CategoryColors = {
  Brightness.light: {
    'News': Color(0xFFff777c),
    "Lifestyle": Color(0xFFbf82fa),
    "Opinion": Color(0xFF8fb9ff),
    "Art": Color(0xFFc0a38a),
    "Culture": Color(0xFFfae566),
    "Personality": Color(0xFFbf82fa),
    "Gender": Color(0xFF66d5ff),
    "Society": Color(0xFFcee86f),
    "Cover": Color(0xFFB3EAFF),
    "Other": Color(0xFF6de0c3)
  },
  Brightness.dark: {
    'News': Color(0xFFa47235),
    "Lifestyle": Color(0xFF80329b),
    "Opinion": Color(0xFF4f73af),
    "Art": Color(0xFF6a503a),
    "Culture": Color(0xFFa59735),
    "Personality": Color(0xFF9233af),
    "Gender": Color(0xFF2f8aaf),
    "Society": Color(0xFF78941f),
    "Cover": Color(0xFF263940),
    "Other": Color(0xFF4f5d6e)
  }
};