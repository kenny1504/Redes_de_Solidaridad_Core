var cedula = $("#id_u").text() //Cedula del Docente
$(document).ready(function () {

    $.ajax({  // ajax para para recuperar datos de estudiantes
        type: "GET",
        url: "Asistencia/Datos",
        data: { cedula: cedula },
        beforeSend: function () {
            $('#Cargando2').modal('show');
        },
        success: function (data) {
            var html = "<table class='table table-bordered table-striped display'><thead><tr><th>Codigo estudiante</th><th>Nombre completo</th><th>Sexo</th><th>Direccion</th><th>Tutor</th><th>Tutor Telefono</th></tr></thead><tbody>";
            data.forEach(element => {

                html = '<tr>'
                    + '<td>' + element.codigo + '</td>'
                    + '<td>' + element.nombre + '</td>'
                    + '<td>' + element.sexo + '</td>'
                    + '<td>' + element.direccion + '</td>'
                    + '<td>' + element.tutor
                    + '<td style="padding-top:0.1%; padding-bottom:0.1%;" >'
                    + '<a class="btn btn-success" onclick="ver_Asistencia(this)" data-id="' + element.idEstudiante + '"data-grado="' + element.idGrado + '" data-grupo="' + element.idGrupo +'" >ver</a>'
                    + '</td>'
                    + '</tr>';
                +"</tbody></table>";
                $('#estudiantes').append(html); //insertamos datos en tabla
            })
            $('#Cargando2').modal('hide');
        }
    })
});