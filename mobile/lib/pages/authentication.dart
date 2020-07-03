import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:mobile/models/OutlookState.dart';
import 'package:mobile/models/user.dart';
import 'package:mobile/redux/actions.dart';
import 'package:mobile/services/localizations.dart';
import 'package:mobile/services/api.dart';
import 'package:redux/redux.dart';
import 'package:flutter_redux/flutter_redux.dart';

class _ViewModel {
  final Store<OutlookState> store;
  final User user;

  _ViewModel({
    @required this.store,
    @required this.user
  });

  @override
  bool operator ==(Object other) =>
    identical(this, other) ||
      other is _ViewModel &&
      runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class Authentication extends StatefulWidget {
  @override
  _AuthenticationState createState() => _AuthenticationState();
}

class _AuthenticationState extends State<Authentication> {
  var _login = true;
  var loading = false;
  var isAuthenticated;
  List<String> errorMessages = <String>[];
  final _loginFormKey = GlobalKey<FormState>();
  final _registerFormKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return StoreConnector<OutlookState, _ViewModel>(
        distinct: true,
        converter: (store) {
          isAuthenticated = store.state.user?.token != null && store.state.user?.token != '';
          return _ViewModel(store: store, user: store.state.user);
        },
        builder: (context, viewModel) =>
          (isAuthenticated)? 
            profile(context, store: viewModel.store) : 
            (_login)? 
              login(context, store: viewModel.store) : 
              register(context, store: viewModel.store)
    );
  }

  Widget login(BuildContext context, { Store<OutlookState> store }) {
    var usernameController = TextEditingController();
    var passwordController = TextEditingController();

    return Center(
      child: Card(
        child: Padding(
          padding: EdgeInsets.all(20),
          child: Form(
            key: _loginFormKey,
            autovalidate: true,
            child: ListView(
              shrinkWrap: true,
              children: <Widget>[
                if (errorMessages.length != 0)
                  Column(
                    children: errorMessages.map((e) => Text(e, style: TextStyle(color: Theme.of(context).errorColor))).toList(),
                  ),
                TextFormField(
                  controller: usernameController,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('username')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('username-required') : null,
                ),
                TextFormField(
                  controller: passwordController,
                  obscureText: true,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('password')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('password-required') : null,
                ),
                RaisedButton(
                  child: Text(OutlookAppLocalizations.of(context).translate('login')),
                  color: Theme.of(context).canvasColor,
                  onPressed: () {
                    if (_loginFormKey.currentState.validate())
                    {
                      signIn(
                        usernameController.text.replaceAll(' ', ''), 
                        passwordController.text.replaceAll(' ', '')
                      )
                      .then((value) async {
                        var user = User(username: usernameController.text, token: value['access_token']);
                        store.dispatch(SetUserAction(user: user));

                        final prefs = await SharedPreferences.getInstance();
                        prefs.setString('outlook-user', json.encode(user.toJson()));

                        setState(() {
                          isAuthenticated = true;
                        });
                      })
                      .catchError((e) {
                        setState(() {
                          errorMessages = <String>[OutlookAppLocalizations.of(context).translate('invalid-login')];
                        });
                      });
                    }
                  },
                ),
                Text(
                  '${OutlookAppLocalizations.of(context).translate('dont-have-account')} ',
                  style: TextStyle(
                    fontSize: 15
                  ),
                  softWrap: true,
                  textAlign: TextAlign.center,
                ),
                InkWell(
                  onTap: () { 
                    setState(() {
                      _login = false;
                      errorMessages = <String>[];
                    });
                  },
                  child: Text(
                    OutlookAppLocalizations.of(context).translate('register-here'),
                    style: TextStyle(
                      fontSize: 17,
                      color: Theme.of(context).textTheme.button.color
                    ),
                    softWrap: true,
                    textAlign: TextAlign.center,
                  ),
                ),
              ],
            ),
          )
        )
      )
    );
  }

  Widget register(BuildContext context, { Store<OutlookState> store }) {
    var firstnameController = TextEditingController();
    var familynameController = TextEditingController();
    var emailController = TextEditingController();
    var usernameController = TextEditingController();
    var passwordController = TextEditingController();

    firstnameController.text = 'Mohammed';
    familynameController.text = 'Ezzedine';
    emailController.text = 'mezdn@outlook.com';
    usernameController.text = 'mezdnn';
    passwordController.text = 'mezdnn';

    return Center(
      child: Card(
        child: Padding(
          padding: EdgeInsets.all(20),
          child: Form(
            key: _registerFormKey,
            child: ListView(
              shrinkWrap: true,
              children: <Widget>[
                if (errorMessages.length != 0)
                  Column(
                    children: errorMessages.map((e) => Text(e, style: TextStyle(color: Theme.of(context).errorColor))).toList(),
                  ),
                TextFormField(
                  controller: firstnameController,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('first-name')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('first-name-required'): null,
                ),
                TextFormField(
                  controller: familynameController,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('last-name')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('last-name-required'): null,
                ),
                TextFormField(
                  controller: emailController,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('email'),
                    helperText: OutlookAppLocalizations.of(context).translate('aub-email-prefered')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('email-required'): null,
                ),
                TextFormField(
                  controller: usernameController,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('username')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('username-required'): null,
                ),
                TextFormField(
                  controller: passwordController,
                  obscureText: true,
                  decoration: InputDecoration(
                    fillColor: Colors.white,
                    hintText: OutlookAppLocalizations.of(context).translate('password')
                  ),
                  validator: (value) => (value.isEmpty)? OutlookAppLocalizations.of(context).translate('password-required'): null,
                ),
                if (loading)
                  LinearProgressIndicator(),
                RaisedButton(
                  child: Text(OutlookAppLocalizations.of(context).translate('register')),
                  onPressed: () {
                    if (_registerFormKey.currentState.validate()) {
                      setState(() {
                        loading = true;
                        errorMessages = <String>[];
                      });
                      signUp(
                        User(
                          username: usernameController.text.replaceAll(' ', ''),
                          password: passwordController.text.replaceAll(' ', ''),
                          firstName: firstnameController.text,
                          lastName: familynameController.text,
                          email: emailController.text
                        )
                      ).then((value) {
                        setState(() {
                          _login = true;
                          Scaffold.of(context).showSnackBar(
                            SnackBar(
                              content: Text(OutlookAppLocalizations.of(context).translate('confirm-email')),
                              duration: Duration(seconds: 2),
                            )
                          );
                        });
                      }).catchError((error) {
                        setState(() {
                          var errorList = json.decode(error);
                          List<String> errorKeys = errorList.keys.toList();
                          for (var key in errorKeys) {
                            for (var error in errorList[key]) {
                              errorMessages.add(error);
                            }
                          }
                        });
                      }).whenComplete(() {
                        setState(() {
                          loading = false;
                        });
                      });
                    }
                  },
                  color: Theme.of(context).canvasColor,
                ),
                Text(
                  '${OutlookAppLocalizations.of(context).translate('have-account')} ',
                  style: TextStyle(
                    fontSize: 15
                  ),
                  textAlign: TextAlign.center,
                  softWrap: true,
                ),
                InkWell(
                  onTap: () {
                    setState(() {
                      errorMessages = <String>[];
                      _login = true;
                    });
                  },
                  child: Text(
                    OutlookAppLocalizations.of(context).translate('login-here'),
                    style: TextStyle(
                      fontSize: 17,
                      color: Theme.of(context).textTheme.button.color
                    ),
                    textAlign: TextAlign.center,
                    softWrap: true,
                  ),
                ),
              ],
            ),
          )
        )
      )
    );
  }

  Widget profile(BuildContext context,{ Store<OutlookState> store }) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        RaisedButton(
          child: Text('Logout'),
          onPressed: () {
            setState(() {
              store.dispatch(SetUserAction(user: null));
              isAuthenticated = false;

              SharedPreferences.getInstance().then((value) {
                final prefs = value;
                prefs.remove('outlook-user');
              });
            });
          },
        )
      ],
    );
  }
}