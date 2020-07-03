class User {
  String username;
  String firstName;
  String lastName;
  String token;
  String email;
  String password;

  User({
    this.firstName,
    this.lastName,
    this.token,
    this.email,
    this.username,
    this.password
  });

  User.fromJson(Map<String, dynamic> json) :
    firstName = json['firstName'],
    lastName = json['lastName'],
    username = json['username'],
    email = json['email'],
    token = json['token'];

  Map<String, String> toJson() => 
    {
      'firstName': firstName,
      'lastName': lastName,
      'username': username,
      'email': email,
      'password': password,
      'token': token
    };
}