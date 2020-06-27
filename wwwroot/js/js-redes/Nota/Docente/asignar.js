

$(document).ready(function () { //ajax para cargar combo box en vista Notas 

	$.ajax({
		type: 'POST',
		url: 'Nota/Detallenota', // llamada a ruta para cargar combobox  Detalle de Nota
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {

			$('#Detalle_nota').empty();//limpia el combobox
			var datos = "<option value='' disabled selected>Semestre</option>";
			data.forEach(element => { //ciclo para recorrer el arreglo de grados
				//variable para asignarle los valores al combobox
				datos += '<option  value="' + element.id + '">' + element.descripcion + '</option>';
			});
			$('#Detalle_nota').append(datos);//ingresa valores al combobox

		}
	});//Fin ajax combobox Detalle de Nota

	$.ajax({
		type: 'POST',
		url: 'Nota/MateriasDocente', // llamada a ruta para cargar asignaturas en las tablas
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {

			$('#materias_nota').empty();//limpia el combobox
			if (data != false) {
				var datos = "<option value='' disabled selected>Materia</option>";
				data.forEach(element => {
					datos += '<option  value="' + element.id + '">' + element.nombre + '</option>';
				});
				$('#materias_nota').append(datos);//ingresa valores al combobox
			}
			else {
				var datos = "<option value='' disabled selected>Materia</option>";
				datos += "<option  value=''>'Sin datos'</option>";
				$('#materias_nota').append(datos);//ingresa valores al combobox
			}
		}
	});//Fin ajax cargar materias en tabla

});


function Mostar_Notas() {

	$('#estudiantes_Notas').empty();//limpia tabla de Notas
	var dat = "<thead> " + "<tr> " + "<th>Codigo estudiante</th>" +
		"<th>Nombre completo</th>" + "<th>Sexo</th>" +
		"<th>Grado</th>" + "<th>Grupo</th>" + "<th>Materia</th>" +
		"<th>Nota</th>" + "</tr>" + "</thead> ";
	$('#estudiantes_Notas').append(dat);

	var cedula = $("#id_u").text() //Cedula del Docente
	var id_detalle = $('#Detalle_nota').val();
	var id_materia = $('#materias_nota').val();

	if ( $('#Detalle_nota').val() != null && $('#materias_nota').val() != null) {

		$.ajax({ //ajax Mostra Notas
			type: 'POST',
			url: 'Nota/Mostrar_Notas_Docente', // llamada a ruta para cargar combobox con datos de tabla grupos
			data: { cedula: cedula, id_detalle_Nota: id_detalle, idMateria: id_materia},
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {

				if (data == -1) {
					var error = "No se a encontrado ningun registro"
					$('#mensaje').text(error);   //manda el error a la modal
					$("#Mensaje-error").modal("show"); //abre modal de error
					$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
						$("#Mensaje-error").modal("hide"); // cierra modal error
					});

				}
				else {
					var datos;
					data.forEach(element => {
						datos += "<tbody><tr>" +
							"<td>" + element.codigoEstudiante + "</td>" +
							"<td>" + element.nombre + "</td>" +
							"<td>" + element.sexo + "</td> " +
							"<td>" + element.grado + "</td>" +
							"<td>" + element.grupo + "</td>" +
							"<td>" + element.asignatura + "</td>" +
							"<td><input min='0'  max='100' type='number' style='width: 50%;' name='Nota[]' value='" + element.nota + "'></td>" +
							"<td class='hidden'><input type='text' name='Id[]' value='" + element.id + "'></td></tr></tbody >";
					});
					$('#estudiantes_Notas').append(datos);//ingresa valores al combobox

				}


			}
		});//Fin ajax Mostra Notas

	}
	else {
		var error = "Favor seleccione todos los  datos de los Desplegables"
		$('#mensaje').text(error);   //manda el error a la modal
		$("#Mensaje-error").modal("show"); //abre modal de error
		$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
			$("#Mensaje-error").modal("hide"); // cierra modal error
		});

	}


}

function Guardar_Notas() {
	//Arreglos
	var Nota = [];
	var idNota = [];
	var i = 0;
	var Incorecto;

	//recorremos la tabla para capturar Notas y Id de notas 
	$('#estudiantes_Notas tbody tr').each(function () {

		var nota = $(this).find('input[type="number"]').val();
		if (nota > 100 || nota <0) {//si un valor ingresado es incorrecto
			Incorecto = true;
		}
		Nota[i] = nota;//ingresa nota al arreglo
		var id = $(this).find('input[type="text"]').val();
		idNota[i] = id;//ingresa idnota al arreglo
		i++;
	});

	if (Incorecto != true) {
		$.ajax({ //ajax Mostra Notas
			type: 'POST',
			url: 'Nota/AgregarNota', // llamada a ruta para cargar combobox con datos de tabla grupos
			data: { Nota: Nota, IdNota: idNota },
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				if (data == true) {
					$('#crear_matricula').modal('hide'); // cierra ventana modal
					$("#exito").modal("show"); //abre modal de exito          
					$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
						$("#exito").modal("hide"); // cierra modal
					});
				}
				else {
					var error = "Error uno o varios registro no se guardaron"
					$('#mensaje').text(error);   //manda el error a la modal
					$("#Mensaje-error").modal("show"); //abre modal de error
					$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
						$("#Mensaje-error").modal("hide"); // cierra modal error
					});
				}

			}
		});//Fin ajax Mostra Notas
	}
	else {
		var error = "Una o varias de las  notas ingresadas es incorrecta,el valor debe ser inferior o igual a 100 "
		$('#mensaje').text(error);   //manda el error a la modal
		$("#Mensaje-error").modal("show"); //abre modal de error
		$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
			$("#Mensaje-error").modal("hide"); // cierra modal error
		});
	}


}



