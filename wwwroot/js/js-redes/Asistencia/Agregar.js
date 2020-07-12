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


function Guardar() {

	$('#estudiantes tbody  tr').each(function () {

		var Id = $(this).attr("value");
		console.log("ID" + Id)
		var Asistencia = $(this).find('input[type="checkbox"]');

		if (Asistencia[0].checked == true)
			console.log("Asistencia " + 1)
		else
			console.log("Asistencia " + 0)

	});


}
