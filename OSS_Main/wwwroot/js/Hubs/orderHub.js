const connectionOrder = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

// Khi kết nối thành công
connectionOrder.start().then(() => {
    console.log("Connected to SignalR in ORDER HUB!");
}).catch(err => console.error("SignalR Connection Error: ", err));

connectionOrder.on("NewOrder", function (action, customerDTO) {
    location.reload();
});