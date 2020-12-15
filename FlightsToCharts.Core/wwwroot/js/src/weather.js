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
      sharedHubConnection.invoke('GetAllWeather').catch((err) => {
         return console.error(err.toString());
      });
   }).catch((err) => {
      return console.error(err.toString());
   });

   // hub events
   sharedHubConnection.on('SendAllWeather', (data) => {
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
               dataField: 'Origin',
               caption: 'Origin',
               width: 85
            },
            {
               dataField: 'TimeHour',
               caption: 'Date of record',
               dataType: 'datetime',
               format: 'dd.MM.yyyy HH:mm'
            },
            {
               dataField: 'TempCels',
               caption: 'Temperature(C)',
               //width: 350
            },
            {
               dataField: 'DewCels',
               caption: 'Dew Point(C)',
               //width: 350
            },
            {
               dataField: 'Humid',
               caption: 'Humidity(%)',
               //width: 350
            },
            {
               dataField: 'WindDir',
               encondeHtml: false,
               caption: 'Wind Direction(<span>&#176;</span>)',
               //width: 350
            },
            {
               dataField: 'WindSpeed',
               caption: 'Wind speed(mph)',
               //width: 350
            },
            {
               dataField: 'WindGust',
               caption: 'Gust speed(mph)',
               //width: 350
            },
            {
               dataField: 'Precip',
               caption: 'Precipitation(in)',
               //width: 350
            },
            {
               dataField: 'Pressure',
               caption: 'Pressure(millibars)',
               //width: 350
            },
            {
               dataField: 'Visib',
               caption: 'Visibility(miles)',
               //width: 350
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