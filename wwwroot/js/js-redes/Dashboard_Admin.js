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



       
});