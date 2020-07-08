
$(function () //funcion para buscar dentro del combobox
{
	$('#Docente').select2({ width: "80%" }) // agrega el select2 a combobox docentes para buscar 
});

function CargarModal() { //ajax para cargar datos en el combobox Grados
	$.ajax({
		type: 'POST',
		url: 'Grados/Grados', // llamada a ruta para cargar combobox con datos de tabla grados
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {

			$('#Grado').empty();//limpia el combobox
			data.forEach(element => { //ciclo para recorrer el arreglo de grados
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.grado + '</option>';

				$('#Grado').append(datos);//ingresa valores al combobox
				$('#Grado').val(''); // limpiar el grado
			});

		}
	});//Fin ajax combobox Grado

	$.ajax({
		type: 'POST',
		url: 'Secciones/Secciones', // llamada a ruta para cargar combobox con datos de tabla secciones
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {

			$('#Seccion').empty();//limpia el combobox
			data.forEach(element => { //ciclo para recorrer el arreglo de secciones
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.codigo + '</option>';

				$('#Seccion').append(datos);//ingresa valores al combobox
				$('#Seccion').val(''); // limpiar la seccion
			});

		}
	});//Fin ajax combobox Seccion

	$.ajax({
		type: 'POST',
		url: 'Grupo/Grupos', // llamada a ruta para cargar combobox con datos de tabla grupos
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {

			$('#Grupo').empty();//limpia el combobox
			data.forEach(element => { //ciclo para recorrer el arreglo de grupos
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.grupo + '</option>';

				$('#Grupo').append(datos);//ingresa valores al combobox
				$('#Grupo').val(''); // limpiar el grupo
			});

		}
	});//Fin ajax combobox Grupo

	var id = $("#id_u").text() //Id de la institucion 
	$('#Docente').empty();
	$.ajax({
		type: 'POST',
		url: 'Docente/MostrarDocentes', // llamada a ruta para cargar combobox con datos de tabla docentes
		data: { id: id },
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {
			$('#Docente').empty();//limpia el combobox
			var datos = "";
			for (var i = 0; i < data.length; i++) {
				datos += '<option  value="' + data[i].id + '">' + data[i].nombre + '</option>';
			}

			$('#Docente').val(''); // limpiar el docente
			$('#Docente').append(datos);//ingresa valores al combobox
		}
	});//Fin ajax combobox Docentes
}

function nueva_oferta() { // ajax para guardar en la tabla oferta
	$("#ingresar_oferta").on('submit', function (evt) {
		evt.preventDefault();
	});

	var Descripcion = $('#Descripcion-Oferta').val();
	var FechaOferta = new Date;
	var SeccionesId = $('#Seccion').val();
	var GradoAcademicoId = $('#Grado').val();
	var GruposId = $('#Grupo').val();
	var DocentesId = $('#Docente').val();
	var Idinstitucion = $("#id_u").text();



	if (Descripcion != "" && GradoAcademicoId != null && GruposId != null && SeccionesId != null && DocentesId != null) // si el input contiene valores entra 
	{
		$.ajax({
			type: 'POST',
			url: 'Oferta/Agregar', // llamada a ruta para guardar la nueva oferta
			data: { Descripcion: Descripcion, FechaOferta: FechaOferta.toISOString(), SeccionesId: SeccionesId, GradoAcademicoId: GradoAcademicoId, GruposId: GruposId, DocentesId: DocentesId, Idinstitucion: Idinstitucion}, // manda el form donde se encuentra la modal dataType: "JSON", // tipo de respuesta del controlador
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				if (data == 0) { // si el ajax contiene errores agrega un label indicando el error 

					var error = "Favor revise los datos ingresador, Datos ya existen (Grado y Grupo)";
					$('#mensaje').text(error);   //manda el error a la modal
					$("#Mensaje-error").modal("show"); //abre modal de error
					$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
						$("#Mensaje-error").modal("hide"); // cierra modal error
					});
				} else { // si no contiene errores agrega el dato a la tabla ofertas

					var datos = "<tr id=" + data[0].idoferta + ">" +
						"<td>" + data[0].descripcion + "</td>" +
						"<td>" + data[0].fecha + "</td>" +
						"<td>" + data[0].docente + "</td>" +
						"<td>" + data[0].grado + "</td>" +
						"<td>" + data[0].grupo + "</td>" +
						"<td>" + data[0].seccion + "</td>"
						+ "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-success' onclick='editar_Oferta(this);' data-id=" + data[0].idOferta + " data-Nombre=" + data[0].Descripcion + "  ><i class=' fa fa-fw fa-pencil'></i></button>"
						+ "<button class='btn btn-info' data-id=" + data[0].idOferta + " onclick='eliminar_oferta(this);'><i class='fa fa-fw fa-trash '></i></button>"
							+ "</td>" + "</tr>";

						$('#ofertas tbody ').append(datos); //insertamos datos en tabla

					$("#exito").modal("show"); //abre modal de exito
					$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
						$("#exito").modal("hide"); // cierra modal
					});
				}
			}
		});
		$('#Descripcion-Oferta').val(''); // limpiar el input Descripcion Oferta
		$('#Grado').val(''); // limpiar el grado
		$('#Grupo').val(''); // limpiar el grupo
		$('#Seccion').val(''); // limpiar la seccion
		$('#Docente').val(''); // limpiar el docente
	}
}