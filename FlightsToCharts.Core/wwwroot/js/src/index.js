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
   sharedHubConnection.start().then(() => {
      // invoke data right after connection was established
      sharedHubConnection.invoke('GetAllBlobsMetaData').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   sharedHubConnection.on('SendAllBlobsMetadata', (data) => {
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
      $('#gridData').dxDataGrid({
         showColumnLines: false,
         showRowLines: true,
         rowAlternationEnabled: true,
         dataSource: datasource,
         columns: [
            {
               dataField: 'Table',
               caption: 'Name',
               cellTemplate: (element, info) => {
                  element.append("<a href=/" + info.text + ">" + info.text + "</a>").css("color", "blue");
               }
            },
            {
               dataField: 'UploadDate',
               caption: 'Upload Date',
               dataType: 'datetime',
               format: 'dd.MM.yyyy HH:mm'
            },
            {
               dataField: 'EditDate',
               caption: 'Edit Date',
               dataType: 'datetime',
               format: 'dd.MM.yyyy HH:mm'
            },
            {
               dataField: 'DataMinDate',
               caption: 'First dated information',
               dataType: 'date',
               format: 'dd.MM.yyyy',
               alignment: 'center'
            },
            {
               dataField: 'DataMaxDate',
               caption: 'Last dated information',
               dataType: 'date',
               format: 'dd.MM.yyyy',
               alignment: 'center'
            },
            {
               dataField: 'DataCount',
               caption: 'Data Count'
            },
            {
               dataField: 'DataProcessed',
               caption: 'Processed',
               dataType: 'boolean'
            }
         ],
         showBorders: true
      });

      loadPanel.hide();
   });


}