﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/HubKitchen").build();

connection.start().then(function () {
    console.log("connected");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("KitchenReload", function () {
    window.location.reload();
});

