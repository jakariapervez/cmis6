<!-- rangeSelector: {selected: 1}, -->

<script type="text/javascript">
   
    Highcharts.stockChart('<?php echo $plot_container; ?>', {

        rangeSelector: {selected: 1},
        credits: {enabled: false},
        yAxis: {opposite:false},
        xAxis: {gridLineWidth: 1},
        
        series: [{
          type: '<?php echo $chart_type; ?>',
          marker: {enabled:true,symbol: 'circle',radius:4,lineWidth:2,lineColor: '#666666'},
        
          name: '<?php //echo $param_row['parameter_name']; ?>',
          data: [<?php echo $plot_data; ?>],
          tooltip: {
            valueDecimals: 2
          }
        }]
    });



</script>

