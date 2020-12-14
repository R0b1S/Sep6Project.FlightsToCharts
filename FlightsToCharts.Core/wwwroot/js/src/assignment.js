window.onload = () => {
   // utils
   var barType;

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
            loadPanel.show();
            assignmentHubConnection.invoke('GetNoFlightsPerMonth').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 2:
            barType = "bar";
            loadPanel.show();
            assignmentHubConnection.invoke('GetNoFlightsPerMonthOrigin').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 3:
            barType = "stackedBar";
            loadPanel.show();
            assignmentHubConnection.invoke('GetNoFlightsPerMonthOrigin').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 4:
            barType = "fullStackedBar";
            loadPanel.show();
            assignmentHubConnection.invoke('GetNoFlightsPerMonthOrigin').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 5:
            loadPanel.show();
            assignmentHubConnection.invoke('GetTopTenDest').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 6:
            loadPanel.show();
            assignmentHubConnection.invoke('GetTopTenDestOrigin').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 7:
            loadPanel.show();
            assignmentHubConnection.invoke('GetMeanAirtime').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 8:
            loadPanel.show();
            assignmentHubConnection.invoke('GetNumberOfWeatherObservations').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 9:
            loadPanel.show();
            assignmentHubConnection.invoke('GetMeanWeatherByOrigin').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 10:
            loadPanel.show();
            assignmentHubConnection.invoke('GetAllMeasuredTempAtJFK').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 11:
            loadPanel.show();
            assignmentHubConnection.invoke('GetDailyMeanTempAtJFK').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 12:
            loadPanel.show();
            assignmentHubConnection.invoke('GetDailyMeanTempAtOrigins').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 13:
            loadPanel.show();
            assignmentHubConnection.invoke('GetMeanArrDepDelay').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 14:
            loadPanel.show();
            assignmentHubConnection.invoke('GetPlanesPerManuf').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 15:
            loadPanel.show();
            assignmentHubConnection.invoke('GetFlightsPerManuf').catch((err) => {
               return console.error(err.toString());
            });
            break;
         case 16:
            loadPanel.show();
            assignmentHubConnection.invoke('GetAirbusModels').catch((err) => {
               return console.error(err.toString());
            });
            break;
         default:
            return;
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
      $("#dropDownOptions").dxSelectBox({
         //value: selectedValue,
         valueExpr: "ID",
         displayExpr: "Name",
         dataSource: new DevExpress.data.ArrayStore({
            data: datasource,
            key: "ID"
         }),
         onValueChanged: function (e) {
            console.log(e);
            //const previousValue = e.previousValue;
            //const newValue = e.value;
            invoke(e.value);
            // Event handling commands go here
         }
         //contentTemplate: function (e) {
         //   const $dataGrid = $("<div>").dxDataGrid({
         //      dataSource: e.component.option("dataSource"),
         //      columns: ["Name"],
         //      sorting: {
         //         mode: "none"
         //      },
         //      selection: { mode: "single" },
         //      //selectedRowKeys: [selectedValue],
         //      onSelectionChanged: function (selectedItems) {
         //         const keys = selectedItems.selectedRowKeys,
         //            hasSelection = keys.length;
         //         invoke(keys[0]);
         //         e.component.option("value", hasSelection ? keys[0] : null);
         //         e.component.close();
         //      }
         //   });
         //   return $dataGrid;
         //}
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
               //console.log(arg);
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

   assignmentHubConnection.on('SendNoFlightsPerMonthOrigin', (data) => {
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
         commonSeriesSettings: {
            argumentField: "MonthName",
            type: barType,
            hoverMode: "allArgumentPoints",
            selectionMode: "allArgumentPoints",
            label: {
               visible: true,
               format: {
                  type: "fixedPoint",
                  precision: 0
               }
            }
         },
         series: [
            { valueField: "EWR", name: "EWR" },
            { valueField: "JFK", name: "JFK" },
            { valueField: "LGA", name: "LGA" }
         ],
         title: "Total number of flights per month from the three origins in one plot (Frequency)",
         legend: {
            verticalAlignment: "bottom",
            horizontalAlignment: "center"
         },
         export: {
            enabled: true,
            fileName: "flightsPerMonOrigin",
            printingEnabled: false
         },
         tooltip: {
            enabled: true,
            customizeTooltip: (arg) => {
               //console.log(arg);
               return {
                  text: arg.valueText
               };
            }
         }
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendTopTenDest', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Destination',
               caption: 'Destination',
               width: 120
            },
            {
               dataField: 'NumberOfFlights',
               caption: 'Flights',
               width: 150
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendTopTenDestOrigin', (data) => {
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
         commonSeriesSettings: {
            argumentField: "MonthName",
            type: "bar",
            hoverMode: "allArgumentPoints",
            selectionMode: "allArgumentPoints",
            label: {
               visible: true,
               format: {
                  type: "fixedPoint",
                  precision: 0
               }
            }
         },
         series: [
            { valueField: "EWR", name: "EWR" },
            { valueField: "JFK", name: "JFK" },
            { valueField: "LGA", name: "LGA" }
         ],
         title: "The Top-10 Origin-Destination number of flights",
         legend: {
            verticalAlignment: "bottom",
            horizontalAlignment: "center"
         },
         export: {
            enabled: true,
            fileName: "flightsPerMonOrigin",
            printingEnabled: false
         },
         tooltip: {
            enabled: true,
            customizeTooltip: (arg) => {
               //console.log(arg);
               return {
                  text: arg.valueText
               };
            }
         }
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendMeanAirtime', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Origin',
               caption: 'Origin',
               width: 120
            },
            {
               dataField: 'AverageAirTime',
               caption: 'Mean Airtime',
               width: 150
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendNumberOfWeatherObservations', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Origin',
               caption: 'Origin',
               width: 120
            },
            {
               dataField: 'WeatherObservations',
               caption: 'Number of Observations',
               width: 250
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendMeanWeatherByOrigin', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Origin',
               caption: 'Origin',
               width: 120
            },
            {
               dataField: 'Temperature',
               caption: 'Temperature (Celsius)',
               width: 200
            },
            {
               dataField: 'Dewp',
               caption: 'Dewp (Celsius)',
               width: 200
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
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

   assignmentHubConnection.on('SendDailyMeanTempAtJFK', (data) => {
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
         title: "The daily mean temperature (in Celsius) at JFK",
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
            valueField: "Temperature",
            name: "JFK"
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

   assignmentHubConnection.on('SendDailyMeanTempAtOrigins', (data) => {
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
         title: "The daily mean temperature (in Celsius)",
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
                  text: `Measured On: ${new Date(arg.argument).toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' })} \n Value: ${arg.value}`
               };
            }
         },
         commonSeriesSettings: {
            type: "scatter"
         },
         series: [{
            argumentField: "Day",
            valueField: "JFK",
            name: "JFK"
         }, {
               argumentField: "Day",
               valueField: "EWR",
               name: "EWR"
            }, {
               argumentField: "Day",
               valueField: "LGA",
               name: "LGA"
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

   assignmentHubConnection.on('SendMeanArrDepDelay', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Origin',
               caption: 'Origin',
               width: 120
            },
            {
               dataField: 'DepartureDelay',
               caption: 'Mean Departure Delay',
               width: 200
            },
            {
               dataField: 'ArrivalDelay',
               caption: 'Mean Arrival Delay',
               width: 200
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendPlanesPerManuf', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Manufacturer',
               caption: 'Manufacturer',
               width: 250
            },
            {
               dataField: 'NumberOfPlanes',
               caption: 'Number Of Planes',
               width: 200
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendFlightsPerManuf', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Manufacturer',
               caption: 'Manufacturer',
               width: 250
            },
            {
               dataField: 'NumberOfFlightsResponsible',
               caption: 'Number Of Flights',
               width: 200
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });

   assignmentHubConnection.on('SendAirbusModels', (data) => {
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
      $('#chartData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         dataSource: dataSource,
         columns: [
            {
               dataField: 'Model',
               caption: 'Model',
               width: 250
            },
            {
               dataField: 'NumberOfPlanes',
               caption: 'Number Of Planes',
               width: 200
            },
            {
               dataField: '',
               caption: ''
            }
         ],
         showBorders: true
      });
      loadPanel.hide();
   });
}
