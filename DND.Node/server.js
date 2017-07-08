'use strict';
var http = require('http');
var express = require('express');

var app = express();

//Setup the View Engine
//app.set("view engine", "jade");
app.set("view engine", "vash"); //razor

app.get("/", function (req, res) {
    //res.writeHead(200, { 'Content-Type': 'text/plain' });

    var msgs = require('./msgs.js');
    res.render("index", { title: "test" });

    //var func = require('./func.js');
    //res.send(func());

    //var obj = require('./obj.js');
    //var a = new obj();

    //var logger = require('./logger');
    //logger.log('This is from the logger');
    //res.send(a.first);
});

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


