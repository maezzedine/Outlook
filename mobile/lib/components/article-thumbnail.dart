import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/pages/article.dart';
import 'package:mobile/services/constants.dart';

class ArticleThumbnail extends StatelessWidget {
  const ArticleThumbnail({@required this.article});

  final Article article;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: 10),
      child: FlatButton(
        onPressed: () {
          Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: ArticlePage(article: article))));
        },
        child: Card(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              Padding(
                padding: EdgeInsets.only(top: 5),
                child: Container(
                  padding: EdgeInsets.symmetric(horizontal: 5),
                  color: CategoryColors[Theme.of(context).brightness][article.category.tag],
                  child: Text(
                    article.category.name,
                    style: TextStyle(
                      color: Theme.of(context).canvasColor,
                    ),
                  )
                ),
              ),
              Padding(
                padding: EdgeInsets.all(10),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                      article.title,
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Row(
                      children: <Widget>[
                        Text(
                          '- ${article.writer.name}: ',
                          style: Theme.of(context).textTheme.bodyText2.merge(
                            TextStyle(color: Theme.of(context).textTheme.headline4.color)
                          ),
                        ),
                        Text(
                          article.subtitle?? '',
                          style: Theme.of(context).textTheme.bodyText2,
                        )
                      ],
                    ),
                    if (article.picturePath != null) Image.network('http://$SERVER_URL${article.picturePath}')
                  ],
                ),
              ),
            ],
          ),
        ),
      )
    );
  }
}