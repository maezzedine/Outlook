import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobile/components/app-scaffold.dart';
import 'package:mobile/models/article.dart';
import 'package:mobile/pages/category.dart';
import 'package:mobile/pages/member.dart';
import 'package:mobile/services/constants.dart';
import 'package:flutter_html/flutter_html.dart';

class ArticlePage extends StatefulWidget {
  final Article article;

  ArticlePage({@required this.article});

  @override
  _ArticlePageState createState() => _ArticlePageState();
}

class _ArticlePageState extends State<ArticlePage> {
  @override
  Widget build(BuildContext context) {
    return Container(
      child: ListView(
        children: <Widget>[
          Flex(
            direction: Axis.horizontal,
            children: <Widget>[
              Container(
                padding: EdgeInsets.symmetric(horizontal: 5),
                color: CategoryColors[Theme.of(context).brightness][widget.article.category.tag],
                child: InkWell(
                  onTap: () {
                    Navigator.push(context, MaterialPageRoute(
                      builder: (context) => AppScaffold(body: CategoryPage(categoryName: widget.article.category.name))
                    ));
                  },
                  child: Text(
                    widget.article.category.name,
                    style: TextStyle(
                      fontSize: 18,
                      color: Theme.of(context).canvasColor,
                    )
                  ),
                )
              )
            ],
          ),
          Padding(
            padding: EdgeInsets.all(10),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Text(
                  widget.article.title,
                  style: Theme.of(context).textTheme.bodyText1.merge(TextStyle(fontSize: 25)),
                ),
                if (widget.article.subtitle != null)
                  Text(
                    widget.article.subtitle,
                    style: Theme.of(context).textTheme.bodyText2.merge(TextStyle(fontSize: 20)),
                  ),
                if (widget.article.picturePath != null) Image.network('http://$SERVER_URL${widget.article.picturePath}'),
                InkWell(
                  onTap: () => Navigator.push(context, MaterialPageRoute(builder: (context) => AppScaffold(body: MemberPage(member: widget.article.writer)))),
                  child: Text(
                    '${widget.article.writer.name} | ${widget.article.writer.position} ',
                    style: Theme.of(context).textTheme.bodyText2.merge(
                      TextStyle(
                        fontSize: 18,
                        color: Theme.of(context).textTheme.headline4.color
                      )
                    ),
                  )
                ),
                Container(
                  height: 1,
                  color: Theme.of(context).backgroundColor,
                ),
                Html(data: widget.article.text)
              ],
            ),
          ),
        ],
      ),
    );
  }
}