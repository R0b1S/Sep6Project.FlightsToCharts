// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// HubConnections
var sharedHubConnection = new signalR.HubConnectionBuilder().withUrl('/sharedHub').withAutomaticReconnect().build();
var airlinesHubConnection = new signalR.HubConnectionBuilder().withUrl('/airlinesHub').withAutomaticReconnect().build();
var airportsHubConnection = new signalR.HubConnectionBuilder().withUrl('/airportsHub').withAutomaticReconnect().build();
var flightsHubConnection = new signalR.HubConnectionBuilder().withUrl('/flightsHub').withAutomaticReconnect().build();
var planesHubConnection = new signalR.HubConnectionBuilder().withUrl('/planesHub').withAutomaticReconnect().build();
var weatherHubConnection = new signalR.HubConnectionBuilder().withUrl('/weatherHub').withAutomaticReconnect().build();
var assignmentHubConnection = new signalR.HubConnectionBuilder().withUrl('/assignmentHub').withAutomaticReconnect().build();

// Start connection
//sharedHubConnection.start().then(function () {
//   //document.getElementById('sendButton').disabled = false;
//   sharedHubConnection.invoke('GetAllBlobsMetaData').catch(function (err) {
//      return console.error(err.toString());
//   });
//}).catch(function (err) {
//   return console.error(err.toString());
//});

//airlinesHubConnection.start().then(function () {
//   //document.getElementById('sendButton').disabled = false;
//   airlinesHubConnection.invoke('SendMessage', 'jebko', 'sprava').catch(function (err) {
//      return console.error(err.toString());
//   });
//}).catch(function (err) {
//   return console.error(err.toString());
//});

//// Hub events
//sharedHubConnection.on('SendBlobsMetadata', function (data) {

//   var datax = JSON.parse(data);
//   console.log(JSON.parse(datax.Data));
//});

//airlinesHubConnection.on('ReceiveMessage', function (user, message) {
//   console.log(user, message);
//});

// Other

//// create load panel
//var loadPanel = $('#loadPanel').dxLoadPanel({
//   shadingColor: 'rgba(0,0,0,0.4)',
//   position: { of: '#test' },
//   visible: false,
//   showIndicator: true,
//   showPane: true,
//   shading: true,
//   closeOnOutsideClick: false,
//}).dxLoadPanel('instance');

//// start loading
//loadPanel.show();
