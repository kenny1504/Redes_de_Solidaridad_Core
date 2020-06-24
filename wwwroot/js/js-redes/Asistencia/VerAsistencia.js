function ver_Asistencia(button) { 
	$("#Ver_Asistencia").modal("show"); //abre modal ver usuario
	$('#fechas tbody').empty();
    var id = $(button).attr("data-id"); // captura el id_Estdiante "id" del estudiante
	var grado = $(button).attr("data-grado"); // captura el id_grupo "id" del estudiante
	var grupo = $(button).attr("data-grupo"); // captura el id_grado "id" del estudiante


	$.ajax({  // ajax para para recuperar datos de estudiantes
		type: "GET",
		url: "Asistencia/Ver_Asistencia",
		data: { id: id, grupo: grupo, grado: grado },
		success: function (data) {

			$("#Ausente").text(data[0]);
			$("#Presente").text(data[1]);
		}
	})

	$.ajax({  // ajax para para recuperar datos de estudiantes
		type: "GET",
		url: "Asistencia/Fechas",
		data: { id: id, grupo: grupo, grado: grado },
		success: function (data) {
			var i = 0;
			data.forEach(element => {
				i++
				html = '<tr>'
					+ '<td>' + element.fecha + '</td>'
					+ '</tr>';
				$('#fechas tbody').append(html); //insertamos datos en tabla

			})

		}
	})


};//fin metodo