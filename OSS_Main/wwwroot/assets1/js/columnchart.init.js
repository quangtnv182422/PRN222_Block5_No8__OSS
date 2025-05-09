var options = {
    series: [ {
        name: 'Revenue',
        data: DATA_BAR_CHART
    }],
    chart: {
        type: 'bar',
        height: 350,
        toolbar: {
            show: false,
        },
    },
    grid: {
        borderColor: '#e9ecef',
    },
    plotOptions: {
        bar: {
            horizontal: false,
            columnWidth: '40%',
            endingShape: 'rounded'
        },
    },
    dataLabels: {
        enabled: false
    },
    stroke: {
        show: true,
        width: 2,
        colors: ['transparent']
    },
    colors: [ '#53c797'],
    xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    },
    yaxis: {
        title: {
            text: 'Fruits',

            style: {
                colors: ['#8492a6'],
                fontSize: '13px',
                fontFamily: 'Inter, sans-serif',
                fontWeight: 500,
            },
        },
    },
    fill: {
        opacity: 1,
    },
    tooltip: {
        y: {
            formatter: function (val) {
                return val + " VND"
            }
        }
    }
};

var chart = new ApexCharts(document.querySelector("#dashboard"), options);
chart.render();