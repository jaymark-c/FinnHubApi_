//Get id values and setup websocket

const tokenApi = document.getElementById("token-api").textContent.trim();
console.log(tokenApi)
const webSocket = new WebSocket(`wss://ws.finnhub.io?token=${tokenApi}`);
var tokenSymbol = document.getElementById("token-symbol").textContent;
console.log(tokenSymbol + "oks")
//Open a connection

webSocket.addEventListener('open', function (event) {
    webSocket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': tokenSymbol }))
});

//Listen for messages

webSocket.addEventListener('message', function (event) {

    //if received error
    //if (event.data.type = "error") {
      //  console.log(event)
      //  $("#token-price").text(event.data.msg);
       // return;
    //}

    var eventData = JSON.parse(event.data);
    if (eventData) {
        if (eventData.data) {
            var updatedPrice = JSON.parse(event.data).data[0].p;

            $("#token-price").text(updatedPrice.toFixed(2));
        }
    }
});


// Unsubscribe to API call
var unsubscribe = function (tokenSymbol) {
    webSocket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': tokenSymbol}))
}

//load the unsubscribe when a window is closed
window.onunload = function () {
    unsubscribe(tokenSymbol);
};