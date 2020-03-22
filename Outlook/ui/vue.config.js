module.exports = {
  "css": {
    "loaderOptions": {
      "sass": {
        "prependData": "\n                    @import \"@/styles/_variables.scss\"; \n                    @import \"@/styles/_queries.scss\";\n                "
      }
    }
  },
  "transpileDependencies": [
    "vuetify"
  ]
}