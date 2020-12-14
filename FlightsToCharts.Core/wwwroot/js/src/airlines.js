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
   airlinesHubConnection.start().then(() => {
      // invoke data right after connection was established
      airlinesHubConnection.invoke('GetAllAirlines').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   airlinesHubConnection.on('SendAllAirlines', (data) => {
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
      console.log(datasource);
      $('#gridData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         showColumnLines: true,
         rowAlternationEnabled: true,
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
               dataField: 'Carrier',
               caption: 'Carrier',
               width: 70,
               alignment: 'right'
            },
            {
               dataField: 'Name',
               caption: 'Name',
               width: 300
            }
         ],
         showBorders: true
      });

      loadPanel.hide();
   });


}