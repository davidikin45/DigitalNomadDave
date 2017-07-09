(function (homeController) {

    var data = require("../data");

    homeController.init = function (app){
        app.get("/", function (req, res) {

            data.getNoteCategories(function (err, results) {
                res.render("index", {
                    title: "Note Board",
                    error: err,
                    categories: results,
                    newCatError: req.flash("newCatName")
                });
            });     
        });

        app.get("/note/:categoryName", function (req, res) {

            var categoryName = req.params.categoryName;

            data.getNoteCategories(function (err, results) {
                res.render("notes", {
                    title: categoryName
                });
            });
        });

        app.post("/newCategory", function (req, res) {
            //body will contain post data in form encoded data. Make sure app.use(express.urlencoded()); has been executed
            var categoryName = req.body.categoryName;

            data.createNewCategory(categoryName, function (err) {
                if (err)
                {
                    console.log(err);
                    req.flash("newCatName", err);
                    res.redirect("/");
                }
                else {
                    res.redirect("/notes/" + categoryName);
                }
            });
        });
    };

})(module.exports);