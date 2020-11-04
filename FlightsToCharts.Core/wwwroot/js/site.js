// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

"use strict";

// create load panel
var loadPanel = $("#loadPanel").dxLoadPanel({
   shadingColor: "rgba(0,0,0,0.4)",
   position: { of: "#test" },
   visible: false,
   showIndicator: true,
   showPane: true,
   shading: true,
   closeOnOutsideClick: false,
}).dxLoadPanel("instance");

// start loading
loadPanel.show();

var connection = new signalR.HubConnectionBuilder().withUrl("/sharedHub").build();


connection.on("SendBlobsMetadata", function (data) {
   var response = JSON.parse(data);
   var datasource = JSON.parse(response.Data);
   //console.log(datasource);
   $("#gridContainer").dxDataGrid({
      showColumnLines: false,
      showRowLines: true,
      rowAlternationEnabled: true,
      dataSource: datasource,
      columns: ["Name", {
         dataField: "UploadDate",
         dataType: "date",
         format: 'dd.MM.yyyy HH:mm'  
      }, "LastModified", "Count"],
      showBorders: true
   });

   setTimeout(() => {
      // hide load panel
      loadPanel.hide();
   }, 4000);

});

connection.start().then(function () {
   //document.getElementById("sendButton").disabled = false;
   connection.invoke("GetAllBlobsMetaData").catch(function (err) {
      return console.error(err.toString());
   });
}).catch(function (err) {
   return console.error(err.toString());
});
