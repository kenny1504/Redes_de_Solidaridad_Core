$(document).ready(function () { //Construye el menu segun el usuario logueado 

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Oficio/Oficios",
        success: function (data) {
            data.forEach(element => {
                var datos = "<tr id=" + element.id + ">" + "<td>" + element.nombre + "</td>"
                    + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-success' data-id=" + element.id + "  onclick='editar_Oficio(this);' ><i class=' fa fa-fw fa-pencil'></i></button>"
                    + "<button class='btn btn-info' data-id=" + element.id + " onclick='eliminar_oficio(this);'><i class='fa fa-fw fa-trash '></i></button>"
                    + "</td>" + "</tr>"; // variable guarda el valor 
                $('#oficios tbody').append(datos); // agrega nuevo registro a tabla
            })
        }
    })
})