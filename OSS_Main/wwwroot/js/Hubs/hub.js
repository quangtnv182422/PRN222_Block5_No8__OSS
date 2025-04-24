const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

// Khi kết nối thành công
connection.start().then(() => {
    console.log("Connected to SignalR!");
}).catch(err => console.error("SignalR Connection Error: ", err));

connection.on("ConfirmOrder", function (action, customerDTO) {
    let message = "";
    switch (action) {
        case "confirm":
            location.reload();
            break;
        case "edit":
            location.reload();
            break;
        case "delete":
            location.reload();
            break;
        case "details":
            location.reload();
            break;
    }
    console.log(message);
});