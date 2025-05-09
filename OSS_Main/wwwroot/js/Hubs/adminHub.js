const connection = new signalR.HubConnectionBuilder()
    .withUrl("/adminHub")
    .build();
connection.start();
connection.on("UpdateAccount", function () {
    if (location.pathname == "/AdminAccount/AccountList") {
        location.reload();
    }
})