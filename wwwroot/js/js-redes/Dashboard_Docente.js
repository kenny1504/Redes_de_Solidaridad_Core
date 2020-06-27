var cedula = $("#id_u").text() //Cedula del Docente
$(document).ready(function () {
    
    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Dashboard/Docente",
        data: { cedula: cedula},
        success: function (data) {
            $('#institucion').text(data[0].nombre);
            var nombre = data[0].grado + " grado - " + data[0].grupo
            $('#oferta').text(nombre);
        }
    })

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Dashboard/Datosofertas",
        data: { cedula: cedula },
        success: function (data) {
        }
    })
});