const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

// Khi kết nối thành công
connectionNoti.start().then(() => {
    console.log("Connected to SignalR!");
}).catch(err => console.error("SignalR Connection Error: ", err));

connectionNoti.on("ConfirmOrder", function (action, customerDTO) {
    let message = "";
    switch (action) {
        case "confirm":
            location.reload();
            break;
        case "cancel":
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