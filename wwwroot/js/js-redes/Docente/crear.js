
$('#datepickerDocente').datepicker({ //mostrar Datepicker
	format: 'yyyy-mm-dd',
	autoclose: true
})
$('#datepickerDocenteEditar').datepicker({ //mostrar Datepicker editar
	format: 'yyyy-mm-dd',
	autoclose: true
})
function ingresar_docente() {
	$('#crear_docente').modal('show'); // abre ventana modal
}//fin del metodo Ingresar_usuario



function nuevo_docente() {

	//este codigo cancela el evento submit en el form donde esta la modal ingresar_Docente
	$("#ingresar_Docente").on('submit', function (evt) {
		evt.preventDefault();
	});

	var cedula = $('#Cedula_Docente').val();
	var nombre = $('#Nombre_Docente').val();
	var apellido1 = $('#Apellido1_Docente').val();
	var apellido2 = $('#Apellido2_Docente').val();
	if (apellido2 == "")
		apellido2 = "-";
	var correo = $('#Correo_Docente').val();
	var sexo = $('#Sexo_Docente').val();
	var estado = $('#Estado').val();
	var telefono = $('#Telefono_Docente').val();
	var direccion = $('#Direccion_Docente').val()
	var fechaNacimiento = $('#datepickerDocente').val();
	var idinstitucion = $("#id_u").text(); //Id de la institucion 

	//Verifica que le formulario no este vacio
	if (cedula != "" && nombre != "" && apellido1 != "" && correo != "" && sexo != null && estado != null && telefono != null && direccion != "" && fechaNacimiento != "") {


		$.ajax({ // ajax para guardar docente
			type: 'POST',
			url: 'Docente/Agregar', // llamada a ruta para guardar un nuevo docente
			data: { Cedula: cedula, Nombre: nombre, Apellido1: apellido1, Apellido2: apellido2, Sexo: sexo, Direccion: direccion, Correo: correo, Telefono: telefono, FechaNacimiento: fechaNacimiento, IdInstitucion: idinstitucion, Estado: estado }, // manda el form donde se encuentra la modal ingresar_Docente
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				if (data == 0) // muestra mensaje de error
				{
					var error = "El docente ya existe, favor reingresar Cedula"
					$('#mensaje').text(error);   //manda el error a la modal
					$("#Mensaje-error").modal("show"); //abre modal de error
					$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
						$("#Mensaje-error").modal("hide"); // cierra modal error
					});

				}
				else {
					///// Capturamos datos a insertar en la tabla docente
					nombre = nombre + " " + apellido1 + " " + apellido2;
					var id = data;
					var estado = 0;
					if ($('#Estado').val() == 1) {
						estado = "Activo";
					}
					else {
						estado = "Inactivo";
					}

					var datos = "<tr>" +
						"<td>" + cedula + "</td>" +
						"<td>" + nombre + "</td>" +
						"<td>" + sexo + "</td> " +
						"<td>" + correo + "</td> " +
						"<td>" + telefono + "</td>" +
						"<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id=" + id + " >" +
						"<button class='btn btn-primary'  onclick='ver_docente(this);'  data-id=" + id + " id='Ver-docente'>ver</button>" +
						"<button class='btn btn-success' onclick='editar_Docente(this);' data-id=" + id + "><i class='fa fa-fw fa-pencil'></i></button>" +
						"<button class='btn btn-info' onclick='eliminar_oferta(this);' data-id=" + id + "><i class='fa fa-fw fa-trash '></i></button>" +
						" <i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);'  data-id=" + id + "></i>" +
						"</td>" +
						"</td>" +
						"<td id=" + id + "a>" + estado + "<i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);' data-id=" + id + "></i> </td>"
					"</tr>"  //Datos a ingresar en la tabla docente

					$('#Docentes').append(datos); // agrega nuevo registro a tabla

					$("#exito").modal("show"); //abre modal de exito
					//limpia campos
					limpiar();
					$("#crear_docente").modal("hide"); // cierra modal
					$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
						$("#exito").modal("hide"); // cierra modal
					});

				}

			}
		});//Fin ajax Guardar docente
	}

}


function limpiar() {
	$('#Cedula_Docente').val("");
	$('#Nombre_Docente').val("");
	$('#Apellido1_Docente').val("");
	$('#Apellido2_Docente').val("");
	$('#Correo_Docente').val("");
	$('#Sexo_Docente').val(null);
	$('#Estado').val(null);
	$('#Telefono_Docente').val("");
	$('#Direccion_Docente').val("")
	$('#datepickerDocente').val(null);
}
