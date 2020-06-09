
var labes=[];
var datas=[];

$(document).ready(function () {
    var TotalMatriculas;

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Dashboard/TotalDocente",
        success: function (data) {
                $('#Total_Docentes').text(data);
        }
    })
    $.ajax({  // ajax para para recuperar datos de instituciones registradas
        type: "GET",
        url: "Dashboard/TotalInstituciones",
        success: function (data) {
            $('#Total_Institucion').text(data);
        }
    })
    $.ajax({  // ajax para para recuperar datos de instituciones Estudiantes
        type: "GET",
        url: "Dashboard/TotalEstudiantes", 
        success: function (data) {
            $('#Total_Estudiantes').text(data);
        }
    })
    $.ajax({  // ajax para para recuperar datos de instituciones Estudiantes
        type: "GET",
        url: "Dashboard/TotalMatriculas",
        success: function (data) {
            TotalMatriculas = data;
            $('#Total_Matriculas').text(data);
        }
    })
    var i = 0; var style;
    $.ajax({  // ajax para para recuperar datos de instituciones Estudiantes
        type: "GET",
        url: "Dashboard/Total_Estudiantes",
        success: function (data) {
            data.forEach(element => {
                i++;
                if (i == 1) {
                    style = (element.cantidad * 100)/TotalMatriculas;
                    $('#institucion1').text(element.nombre);
                    var cifra = document.getElementById('institucion_Valor1');
                    cifra.innerHTML = '<b>' + element.cantidad + '</b>/' + TotalMatriculas;
                    var intro = document.getElementById('institucion_style1');
                    intro.style.width = style.toString()+'%';
                }
                if (i == 2) {
                    style = (element.cantidad * 100)/TotalMatriculas;
                    $('#institucion2').text(element.nombre);
                    var cifra = document.getElementById('institucion_Valor2');
                    cifra.innerHTML = '<b>' + element.cantidad + '</b>/' + TotalMatriculas;
                    var intro = document.getElementById('institucion_style2');
                    intro.style.width = style.toString() + '%';
                }
                if (i == 3) {
                    style = (element.cantidad * 100)/TotalMatriculas;
                    $('#institucion3').text(element.nombre);
                    var cifra = document.getElementById('institucion_Valor3');
                    cifra.innerHTML = '<b>' + element.cantidad + '</b>/' + TotalMatriculas;
                    var intro = document.getElementById('institucion_style3');
                    intro.style.width = style.toString() + '%';
                }
                if (i == 4) {
                    style = (element.cantidad * 100)/TotalMatriculas;
                    $('#institucion4').text(element.nombre);
                    var cifra = document.getElementById('institucion_Valor4');
                    cifra.innerHTML = '<b>' + element.cantidad + '</b>/' + TotalMatriculas;
                    var intro = document.getElementById('institucion_style4');
                    intro.style.width = style.toString() + '%';;
                }
                
            })
        }
    })


    var salesChartCanvas = $('#GradosChart').get(0).getContext('2d');
    // This will get the first returned node in the jQuery collection.
    var salesChart = new Chart(salesChartCanvas); 
    var salesChartData;


    var salesChartOptions = {
        // Boolean - If we should show the scale at all
        showScale: true,
        // Boolean - Whether grid lines are shown across the chart
        scaleShowGridLines: false,
        // String - Colour of the grid lines
        scaleGridLineColor: 'rgba(0,0,0,.05)',
        // Number - Width of the grid lines
        scaleGridLineWidth: 1,
        // Boolean - Whether to show horizontal lines (except X axis)
        scaleShowHorizontalLines: true,
        // Boolean - Whether to show vertical lines (except Y axis)
        scaleShowVerticalLines: true,
        // Boolean - Whether the line is curved between points
        bezierCurve: true,
        // Number - Tension of the bezier curve between points
        bezierCurveTension: 0.3,
        // Boolean - Whether to show a dot for each point
        pointDot: false,
        // Number - Radius of each point dot in pixels
        pointDotRadius: 4,
        // Number - Pixel width of point dot stroke
        pointDotStrokeWidth: 1,
        // Number - amount extra to add to the radius to cater for hit detection outside the drawn point
        pointHitDetectionRadius: 20,
        // Boolean - Whether to show a stroke for datasets
        datasetStroke: true,
        // Number - Pixel width of dataset stroke
        datasetStrokeWidth: 2,
        // Boolean - Whether to fill the dataset with a color
        datasetFill: true,
        // String - A legend template
        legendTemplate: '<ul class=\'<%=name.toLowerCase()%>-legend\'><% for (var i=0; i<datasets.length; i++){%><li><span style=\'background-color:<%=datasets[i].lineColor%>\'></span><%=datasets[i].label%></li><%}%></ul>',
        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: true,
        // Boolean - whether to make the chart responsive to window resizing
        responsive: true
    };



    $.ajax({  // ajax para para Llenar grafico
        type: "GET",
        url: "Dashboard/Total_Grados",
        success: function (data) {
            data.forEach(element => {
                labes.push("Grado "+element.grado.toString());
                datas.push(element.cantidad);
            })

           salesChartData = {
                labels: labes,
                datasets: [
                    {
                        fillColor: 'rgb(53, 120, 11)',
                        strokeColor: 'rgb(53, 120, 11)',
                        pointColor: 'rgb(53, 120, 11)',
                        pointStrokeColor: '#c1c7d1',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgb(53, 120, 11)',
                        data: datas
                    }
                ]
            };

            // Create the line chart
            salesChart.Bar(salesChartData, salesChartOptions);

        }
    })

    var PfemeninoE, PmasculinoE, femeninoE, masculinoE, PfemeninoT, PmasculinoT, femeninoT, MasculinoT;

    $.ajax({  // ajax para para recuperar datos de instituciones Estudiantes
        type: "GET",
        url: "Dashboard/Estudiantes_Sexo",
        success: function (data) {
            var total;
            data.forEach(element => {
                if (element.sexo == 'F')
                    femeninoE = element.cantidad; 
                else
                    masculinoE = element.cantidad
            })
            total = femeninoE + masculinoE
            PfemeninoE = (femeninoE * 100) / total
            PmasculinoE = (masculinoE * 100) / total

            $('#PmasculinoE').text(PmasculinoE.toFixed(2) + "%");
            $('#masculinoE').text(masculinoE);
            $('#PfemeninoE').text(PfemeninoE.toFixed(2) + "%");
            $('#fememeninoE').text(femeninoE);
        }
    })

    $.ajax({  // ajax para para recuperar datos de instituciones Estudiantes
        type: "GET",
        url: "Dashboard/Tutores_Sexo",
        success: function (data) {
            var total;
            data.forEach(element => {
                if (element.sexo == 'F')
                    femeninoT = element.cantidad
                else
                    MasculinoT = element.cantidad
            })
            total = femeninoT + MasculinoT
            PfemeninoT = (femeninoT * 100) / total
            PmasculinoT = (MasculinoT * 100) / total

            $('#PmasculinoT').text(PmasculinoT.toFixed(2) + "%");
            $('#masculinoT').text(MasculinoT);
            $('#PfemeninoT').text(PfemeninoT.toFixed(2) + "%");
            $('#fememeninoT').text(femeninoT);

        }
    })


       
});