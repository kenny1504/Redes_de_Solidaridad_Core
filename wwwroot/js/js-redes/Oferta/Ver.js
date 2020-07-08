
$(document).ready(function () { //Carga los docentes en la tabla Docentes
    var ide = $("#id_u").text()//Capturamos el id de la Institucion logueada

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "POST",
        url: "Oferta/Datos",
        data: { id: ide },
        success: function (data) {
            data.forEach(element => {
                var datos = "<tr id=" + element.idoferta + ">" +
                    "<td>" + element.descripcion + "</td>" +
                    "<td>" + element.fecha + "</td>" +
                    "<td>" + element.docente + "</td>" +
                    "<td>" + element.grado + "</td>" +
                    "<td>" + element.grupo + "</td>" +
                    "<td>" + element.seccion + "</td>"
                    + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-success' onclick='editar_Oferta(this);' data-id=" + element.idoferta + " data-Nombre=" + element.Descripcion + "  ><i class=' fa fa-fw fa-pencil'></i></button>"
                    + "<button class='btn btn-info' data-id=" + element.idoferta + " onclick='eliminar_oferta(this);'><i class='fa fa-fw fa-trash '></i></button>"
                    + "</td>" + "</tr>";

                $('#ofertas tbody ').append(datos); //insertamos datos en tabla
            })
        }
    })
})