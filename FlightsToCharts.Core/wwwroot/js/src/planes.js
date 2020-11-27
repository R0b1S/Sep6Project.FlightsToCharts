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
   planesHubConnection.start().then(() => {
      // invoke data right after connection was established
      planesHubConnection.invoke('GetAllPlanes').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   planesHubConnection.on('SendAllPlanes', (data) => {
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
               dataField: 'Tailnum',
               caption: 'Tail number',
               width: 120,
               //alignment: 'right'
            },
            {
               dataField: 'Year',
               caption: 'Manuf. Year',
               //width: 350
            },
            //{
            //   caption: 'Location',
            //   encodeHtml: false,
            //   alignment: 'center',
            //   width: 100,
            //   calculateCellValue: (rowData) => {
            //      return rowData.Lat + ',' + rowData.Lon;
            //   },
            //   cellTemplate: (element, info) => {
            //      element.append('<a href="https://www.google.com/maps/search/?api=1&zoom=10&query=' + info.text + '"> <i class="fas fa-map-marked-alt"></i></a>').css("color", "blue");
            //   }
            //},
            {
               dataField: 'Type',
               caption: 'Type',
               //width: 150
            },
            {
               dataField: 'Manufacturer',
               caption: 'Manufacturer',
               //width: 150
            },
            {
               dataField: 'Model',
               caption: 'Model',
               //width: 200,
               //alignment: 'center'
            },
            {
               dataField: 'Engines',
               caption: 'Engines',
               //width: 250
            },
            {
               dataField: 'Seats',
               caption: 'Seats',
               //width: 250
            },
            {
               dataField: 'Speed',
               caption: 'Speed',
               //width: 250
            },
            {
               dataField: 'Engine',
               caption: 'Engine',
               //width: 250
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