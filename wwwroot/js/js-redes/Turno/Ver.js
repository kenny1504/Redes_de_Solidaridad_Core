$(document).ready(function () { //Construye el menu segun el usuario logueado 

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Turno/Turnos",
        success: function (data) {
            data.forEach(element => {
                var datos = "<tr id=" + element.id + ">" + "<td>" + element.nombre + "</td>"
                    + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-success' data-id=" + data.id + "  onclick='editar_Turno(this);' ><i class=' fa fa-fw fa-pencil'></i></button>"
                    + "<button class='btn btn-info' data-id=" + element.id + " onclick='eliminar_turno(this);'><i class='fa fa-fw fa-trash '></i></button>"
                    + "</td>" + "</tr>"; // variable guarda el valor 
                $('#turnos tbody').append(datos); // agrega nuevo registro a tabla
            })
        }
    })
})