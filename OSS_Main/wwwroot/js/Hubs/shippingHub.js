const connection = new signalR.HubConnectionBuilder()
    .withUrl("/shippingHub")
    .build();

// Khi kết nối thành công
connection.start().then(() => {
    console.log("Connected to SignalR! in SHIPPING HUB");
}).catch(err => console.error("SignalR Connection Error: ", err));

connection.on("UpdateStatus", function (action, customerDTO) {
    location.reload();
});