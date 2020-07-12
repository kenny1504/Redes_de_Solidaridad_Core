var cedula = $("#id_u").text() //Cedula del Docente
$(document).ready(function () {

	var fecha = new Date();//fecha actual
	$('#Fecha').text("Agregar Asistencia - " + (fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear())); //Establece la fecha actual

	$.ajax({  // ajax para para recuperar datos de estudiantes
		type: "GET",
		url: "Asistencia/Datos",
		data: { cedula: cedula },
		success: function (data) {
			var html = "<table class='table table-bordered table-striped display'><thead><tr><th>Codigo estudiante</th><th>Nombre completo</th><th>Sexo</th><th>Direccion</th><th>Tutor</th><th>Tutor Telefono</th></tr></thead><tbody>";
			data.forEach(element => {

				html = '<tr  value=' + element.idEstudiante + '>'
					+ '<td>' + element.codigo + '</td>'
					+ '<td>' + element.nombre + '</td>'
					+ '<td>' + element.sexo + '</td>'
					+ '<td>' + element.direccion + '</td>'
					+ '<td>' + element.tutor
					+ '<td style="padding-top:0.1%; padding-bottom:0.1%;" >'
					+ '<div  class="form-check">'
					+ '<label class="form-check-label">'
					+ '<input style="width: 20px; height: 20px;" class="form-check-input" type="checkbox"  >'
					+ "</label>"
					+ '</div>'
					+ '</td>'
					+ '</tr>';
				+"</tbody></table>";
				$('#estudiantes').append(html); //insertamos datos en tabla
			})
			$('#Cargando2').modal('hide');
		}
	})
});

var Asistencia = [];
var idMatricula = [];
function Guardar() {

	var i = 0;
	$('#estudiantes tbody  tr').each(function () {

		var Id = $(this).attr("value");
		idMatricula[i] = Id;
		var Asistencias = $(this).find('input[type="checkbox"]');

		if (Asistencias[0].checked == true)
			Asistencia[i] = 1;
		else
			Asistencia[i] = 0;
		i++;
	});


	$.ajax({  // ajax para para rGuardar asistencia
		type: "POST",
		url: "Asistencia/AgregarAsistencia",
		data: { idMatricula: idMatricula, asistencia: Asistencia },
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {


			if (data == 1) {
				$("#exito").modal("show"); //abre modal de exito          
				$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
					$("#exito").modal("hide"); // cierra modal
				});

			}
			else {
				var error = "Error, YA EXISTE  ASISTENCIA EN ESTA FECHA"
				$('#mensaje').text(error);   //manda el error a la modal
				$("#Mensaje-error").modal("show"); //abre modal de error
				$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
					$("#Mensaje-error").modal("hide"); // cierra modal error
				});
			}

			window.location.href=('Inicio');

		}
	})
}
