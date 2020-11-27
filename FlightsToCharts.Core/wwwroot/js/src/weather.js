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

   // start loading
   loadPanel.show();

   // hub start connection
   weatherHubConnection.start().then(() => {
      // invoke data right after connection was established
      weatherHubConnection.invoke('GetAllWeather').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   weatherHubConnection.on('SendAllWeather', (data) => {
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
               dataField: 'FAA',
               caption: 'FAA',
               width: 75,
               alignment: 'right'
            },
            {
               dataField: 'Name',
               caption: 'Name',
               width: 350
            },
            {
               caption: 'Location',
               encodeHtml: false,
               alignment: 'center',
               width: 100,
               calculateCellValue: (rowData) => {
                  return rowData.Lat + ',' + rowData.Lon;
               },
               cellTemplate: (element, info) => {
                  element.append('<a href="https://www.google.com/maps/search/?api=1&zoom=10&query=' + info.text + '"> <i class="fas fa-map-marked-alt"></i></a>').css("color", "blue");
               }
            },
            {
               dataField: 'Alt',
               caption: 'Altitude(ft)',
               width: 150
            },
            {
               dataField: 'Tz',
               caption: 'Timezone(GMT)',
               width: 150
            },
            {
               dataField: 'Dst',
               caption: 'Daylight savings time zone',
               width: 200,
               alignment: 'center'
            },
            {
               dataField: 'Tzone',
               caption: 'IANA time zone',
               width: 250
            },
            {
               dataField: 'X',
               caption: ' '
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