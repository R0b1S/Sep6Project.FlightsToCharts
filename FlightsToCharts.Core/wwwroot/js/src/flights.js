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
         }, 60000);
      },
   }).dxLoadPanel('instance');

   // start loading
   loadPanel.show();

   // hub start connection
   flightsHubConnection.start().then(() => {
      // invoke data right after connection was established
      flightsHubConnection.invoke('GetAllFlights').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   flightsHubConnection.on('SendAllFlights', (data) => {
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
      //console.log(datasource);
      $('#gridData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
         width: '80%',
         filterRow: {
            visible: true,
            applyFilter: "auto"
         },
         paging: {
            pageSize: 15
         },
         pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 15, 20],
            showInfo: true
         },
         dataSource: datasource,
         columns: [
            {
               dataField: 'DateOfFlight',
               caption: 'Date of flights',
               //width: 75,
               dataType: 'date',
               format: 'dd.MM.yyyy',
               alignment: 'center'
            },
            {
               dataField: 'DepSchedDelay',
               caption: 'Departure (delay[m])',
               alignment: 'left',
               calculateCellValue: (rowData) => {
                  return (rowData.DepSchedDelay).toString().split(' ')[0] === "00:00" ? "-" : rowData.DepSchedDelay;
               }
               //width: 350
            },
            {
               dataField: 'ArrSchedDelay',
               caption: 'Arrival (delay[m])',
               alignment: 'left',
               calculateCellValue: (rowData) => {
                  return (rowData.ArrSchedDelay).toString().split(' ')[0] === "00:00" ? "-" : rowData.ArrSchedDelay;
               }
               //width: 350
            },
            {
               dataField: 'AirTime',
               caption: 'AirTime',
               alignment: 'left',
               calculateCellValue: (rowData) => {
                  return (rowData.AirTime).toString() === "00:00" ? "-" : rowData.AirTime;
               }
               //width: 350
            },
            {
               dataField: 'Distance',
               caption: 'Distance (miles)',
               //width: 350
            },
            {
               dataField: 'Origin',
               caption: 'Origin',
               alignment: 'center'
            },
            {
               dataField: 'Dest',
               caption: 'Destination',
               alignment: 'center'
            },
            {
               dataField: 'Carrier',
               caption: 'Airline',
               alignment: 'center',
            },
            {
               dataField: 'FlightNo',
               caption: 'FlightNumber'
            },
            {
               dataField: 'Tailnum',
               caption: 'Plane Number'
            }
         ],
         showBorders: true
      });

      loadPanel.hide();
   });


   //cellTemplate: (element, info) => {
   //   element.append("<a href=/" + info.text + ">" + info.text + "</a>").css("color", "blue");
   //}
   //
}