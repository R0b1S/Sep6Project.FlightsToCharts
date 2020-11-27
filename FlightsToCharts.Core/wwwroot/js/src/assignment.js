window.onload = () => {
   // utils
   // create load panel
   var loadPanel = $('#loadPanel').dxLoadPanel({
      shadingColor: 'rgba(0,0,0,0.4)',
      position: { of: '#gridContainer' },
      visible: false,
      showIndicator: true,
      showPane: true,
      shading: true,
      closeOnOutsideClick: false,
      onShown: () => {
         setTimeout(() => {
            loadPanel.hide();
         }, 30000);
      },
   }).dxLoadPanel('instance');

   // hub start connection
   assignmentHubConnection.start().then(() => {
      assignmentHubConnection.invoke('GetDropDownOptions').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   function invoke(id) {
      switch (id) {
         case 1:
            // start loading
            loadPanel.show();
            assignmentHubConnection.invoke('GetNoFlightsPerMonth').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 5:
            // start loading
            loadPanel.show();
            assignmentHubConnection.invoke('GetAllMeasuredTempAtJFK').catch((err) => {
               return console.error(err.toString());
            });
            break;
         default:
         // code block
      }
   }

   // hub events
   assignmentHubConnection.on('SendDropDownOptions', (data) => {
      var message = JSON.parse(data);
      if (message.StatusCode !== 0) {
         swal({
            title: 'Error',
            text: `Data could not be retrieved.\n Please refresh the page.`,
            icon: 'error',
            button: 'OK',
         });
         throw (message.Message);
      }
      //console.log(JSON.parse(message.Data));
      //--------------------------------------------
      var datasource = JSON.parse(message.Data);

      // dropdown
      $("#dropDownOptions").dxDropDownBox({
         //value: selectedValue,
         valueExpr: "ID",
         displayExpr: "Name",
         dataSource: new DevExpress.data.ArrayStore({
            data: datasource,
            key: "ID"
         }),
         contentTemplate: function (e) {
            const $dataGrid = $("<div>").dxDataGrid({
               dataSource: e.component.option("dataSource"),
               columns: ["Name"],
               sorting: {
                  mode: "none"
               },
               selection: { mode: "single" },
               //selectedRowKeys: [selectedValue],
               onSelectionChanged: function (selectedItems) {
                  const keys = selectedItems.selectedRowKeys,
                     hasSelection = keys.length;
                  invoke(keys[0]);
                  e.component.option("value", hasSelection ? keys[0] : null);
                  e.component.close();
               }
            });
            return $dataGrid;
         }
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendNoFlightsPerMonth', (data) => {
      var message = JSON.parse(data);
      if (message.StatusCode !== 0) {
         swal({
            title: 'Error',
            text: `Data could not be retrieved.\n Please refresh the page.`,
            icon: 'error',
            button: 'OK',
         });
         throw (message.Message);
      }
      //console.log(JSON.parse(message.Data));
      //--------------------------------------------
      var dataSource = JSON.parse(message.Data);
      $("#chartData").dxChart({
         dataSource: dataSource,
         title: "Total number of flights per month",
         export: {
            enabled: true,
            fileName: "flightsPerMon",
            printingEnabled: false
         },
         tooltip: {
            enabled: true,
            customizeTooltip: (arg) => {
               console.log(arg);
               return {
                  text: arg.valueText
               };
            }
         },
         series: {
            argumentField: "MonthString",
            valueField: "Count",
            name: "Flights",
            type: "bar"
         }
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendAllMeasuredTempAtJFK', (data) => {
      var message = JSON.parse(data);
      if (message.StatusCode !== 0) {
         swal({
            title: 'Error',
            text: `Data could not be retrieved.\n Please refresh the page.`,
            icon: 'error',
            button: 'OK',
         });
         throw (message.Message);
      }
      console.log(JSON.parse(message.Data));
      //--------------------------------------------
      var dataSource = JSON.parse(message.Data);
      $("#chartData").dxChart({
         dataSource: dataSource,
         title: "The temperature (in Celsius) at JFK",
         // waaaaay too much data
         //export: {
         //   enabled: true,
         //   fileName: "flightsPerMon",
         //   printingEnabled: false
         //},
         zoomAndPan: {
            valueAxis: "both",
            argumentAxis: "both",
            dragToZoom: true,
            allowMouseWheel: true,
            panKey: "shift"
         },
         tooltip: {
            enabled: true,
            customizeTooltip: (arg) => {
               return {
                  text: `Measured On: ${new Date(arg.argument).toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric' })} \n Value: ${arg.value}`
               };
            }
         },
         commonSeriesSettings: {
            type: "scatter"
         },
         series: [{
            argumentField: "TimeHour",
            valueField: "TempCels"
         }],
         argumentAxis: {
            argumentType: "datetime",  
            label: {
               type: 'monthAndYear',
               //format: "yyyy-MM-dd HH:mm"
            }
         }
      });
      loadPanel.hide();
   });




}

//tooltip: {
//   enabled: true,
//      customizeTooltip: function (arg) {
//         return {
//            text: getText(arg, arg.valueText)
//         };
//      }
//},
//"export": {
//   enabled: true
//},
//title: {
//   text: "Average Speed by Racer",
//      font: {
//      size: 28
//   }
//},
//legend: {
//   visible: true,
//      verticalAlignment: "bottom",
//         horizontalAlignment: "center",
//            customizeText: function(arg) {
//               return getText(arg.item, arg.text);
//            }
//}


      //console.log(datasource);
   //   $('#gridData').dxDataGrid({
   //      showColumnLines: false,
   //      showRowLines: true,
   //      showColumnLines: true,
   //      rowAlternationEnabled: true,
   //      paging: {
   //         pageSize: 15
   //      },
   //      pager: {
   //         showPageSizeSelector: true,
   //         allowedPageSizes: [5, 10, 15, 20],
   //         showInfo: true
   //      },
   //      dataSource: datasource,
   //      columns: [
   //         {
   //            dataField: 'Carrier',
   //            caption: 'Carrier',
   //            width: 70,
   //            alignment: 'right'
   //         },
   //         {
   //            dataField: 'Name',
   //            caption: 'Name',
   //            width: 300
   //         }
   //      ],
   //      showBorders: true
   //   });

   //   loadPanel.hide();