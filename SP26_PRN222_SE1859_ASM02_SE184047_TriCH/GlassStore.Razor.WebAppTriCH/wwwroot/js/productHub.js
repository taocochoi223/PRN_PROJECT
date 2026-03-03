"use strict";

// Kết nối tới ProductHub trên server
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/productHub")
    .build();

// Lắng nghe event "ProductDeleted" từ server
// Khi có sản phẩm bị xóa → tìm row theo data-id và xóa khỏi table
connection.on("ProductDeleted", function (productId) {
    var row = document.querySelector("tr[data-id='" + productId + "']");
    if (row) {
        row.remove();
        console.log("SignalR: Đã xóa sản phẩm id=" + productId + " khỏi table.");
    }
});

// Khởi động kết nối
connection.start()
    .then(function () {
        console.log("SignalR: Đã kết nối tới /productHub");
    })
    .catch(function (err) {
        console.error("SignalR Error: " + err.toString());
    });
