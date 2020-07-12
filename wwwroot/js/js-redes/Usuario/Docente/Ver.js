$(document).ready(function () { //Construye el menu segun el usuario logueado 
    var ide = $("#id_u").text()

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Usuarios/Usarios_Docentes2",
        data: { id: ide },
        success: function (data) {
            data.forEach(element => {
                var html = '<tr>'
                    + '<td>' + element.usuario + '</td>'
                    + '<td>' + element.nombre + '</td>'
                    + '<td>' + element.institucion + '</td>'
                    + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + element.idUsuario + '" >'
                    + '<a class="btn btn-primary" onclick="ver_docente(this);" data-id="' + element.idUsuario + '" id="Ver-estudiante">ver</a>'
                    + '<a class="btn btn-success" data-id="' + element.idUsuario + '" onclick="editar_Usuario_D(this)" ><i class=" fa fa-fw fa-pencil"></i></a>'
                    + '<a class="btn btn-info" data-id="' + element.idUsuario + '" onclick="eliminar_Usuario_Docente(this)"><i class="fa fa-fw fa-trash "></i></a>'
                    + '</tr>';
                $('#Usuarios tbody ').append(html); //insertamos datos en tabla
            })
        }
    })
})