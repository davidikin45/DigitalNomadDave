'use strict';
var http = require('http');
var express = require('express');
var app = express();
var controllers = require('./controllers');

//1. Setup the View Engine
//app.set("view engine", "jade");
app.set("view engine", "vash"); //razor

//2. set the public static resource folder
app.use(express.static(__dirname + "/public"));

//3. Map the routes
controllers.init(app);


app.get("/api/users", function (req, res) {
    res.set("Content-Type", "text/plain");
    res.send({ name:"Shawn", isValid: true, group: "Admin"});
});

var server = http.createServer(app);
var port = process.env.PORT || 1337;
server.listen(port, function () {
    var host = server.address().address
    var port = server.address().port

    console.log("Example app listening at http://%s:%s", host, port)
})


