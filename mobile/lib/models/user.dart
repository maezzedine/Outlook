class User {
  String firstName;
  String lastName;

  User.fromJson(Map<String, dynamic> json) :
    firstName = json['firstName'],
    lastName = json['lastName'];
}