﻿(function (homeController) {

    var data = require("../data");

    homeController.init = function (app){
        app.get("/", function (req, res) {

            data.getNoteCategories(function (err, results) {
                 res.render("index", { title: "test1", error : err, categories: results });
            });     
        });
    };
})(module.exports);