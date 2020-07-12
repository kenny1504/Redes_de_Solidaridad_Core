$(function () //funcion para buscar dentro del combobox
{
	$('#oficiot').select2({ width: "80%" }) // agrega el select2 a combobox tutores para buscar
});
$('#datepickertutor').datepicker({ //sirve para mostrar Datepicker
	format: 'yyyy-mm-dd',
	autoclose: true
})
$('#datepickerTutorEditar').datepicker({ //mostrar Datepicker editar
	format: 'yyyy-mm-dd',
	autoclose: true
})
function ingresar_tutor() {
	$('#crear_tutor').modal('show'); // abre ventana modal
	$.ajax({ // ajax para cargar datos en el combobox
		type: 'POST',
		url: 'Oficio/Oficios', // llamada a ruta para cargar combobox oficio
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {
			$('#oficiot').empty();
			//ciclo para recorrer el arreglo de oficios
			data.forEach(element => {
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.nombre + '</option>';

				$('#oficiot').append(datos); //ingresa valores al combobox
			});

		}
	});//Fin ajax combobox oficios
}//fin del metodo Ingresar_tutor

function guardar_Tutor() {
	$("#ingresar_tutor").on('submit', function (evt) {
		evt.preventDefault();
	});
	var cedula = $('#Cedulat').val();
	var nombre = $('#Nombre-tutor').val();
	var apellido1 = $('#apellido1-tutor').val();
	var apellido2 = $('#apellido2-tutor').val();
	if (apellido2 == "")
		apellido2 = "-";
	var telefono = $('#telefonot').val();
	var sexo = $('#sexot').val();
	var oficio = $('#oficiot').val();
	var correo = $('#correot').val();
	var direccion = $('#direcciont').val();
	var fechaNacimiento = $('#datepickertutor').val();
	var idinstitucion = $("#id_u").text() //Id de la institucion 
	//Verifica que le formulario no este vacio
	if (cedula != "" && nombre != "" && apellido1 != "" && telefono != "" && sexo != null && oficio != null
		&& correo != null && direccion != "" && fechaNacimiento != "") {

		$.ajax({ // ajax para guardar docente
			type: 'POST',
			url: 'Tutores/AgregarTutor', // llamada a ruta para guardar un nuevo tutor
			data: { Cedula: cedula, Nombre: nombre, Apellido1: apellido1, Apellido2: apellido2, Sexo: sexo, Direccion: direccion, Correo: correo, Telefono: telefono, FechaNacimiento: fechaNacimiento, IdInstitucion: idinstitucion, OficiosId: oficio }, // manda el form donde se encuentra la modal ingresar_tutor
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				if (data == 0) // muestra mensaje de error
				{
					var error = "El tutor ya existe, favor verificar Cedula"
					$('#mensaje').text(error);   //manda el error a la modal
					$("#Mensaje-error").modal("show"); //abre modal de error
					$("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
						$("#Mensaje-error").modal("hide"); // cierra modal error
					});

				}
				else {
					 nombre = $('#Nombre-tutor').val() + " " + $('#apellido1-tutor').val() + " " + $('#apellido2-tutor').val();
					var Oficio2 = $('select[name="oficiot"] option:selected').text();
					var id = data;

					var datos = "<tr>" +
						"<td>" + cedula + "</td>" +
						"<td>" + nombre + "</td>" +
						"<td>" + sexo + "</td> " +
						"<td>" + correo + "</td>" +
						"<td>" + Oficio2 + "</td>"
						+ '<td style="padding-top:0.1%; padding-bottom:0.1%;"class= "hidden" id="' + id + 'T" >'
						+ '<button class="btn btn-primary" onclick="ver_tutor(this);" data-id="' + id + '" id="Ver-estudiante">ver</button>'
						+ '<button class="btn btn-success " data-id="' + id + '" onclick="editar_Tutor(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
						+ '<button class="btn btn-info" data-id="' + id + '" onclick="eliminar_tutor(this);"><i class="fa fa-fw fa-trash "></i></button>'
						+ '<i class="fa fa-angle-double-right pull-right" onclick="mostrarT(this);;" data-id="' + id + '"></i>'
						+ '</td>'
						+ '<td id="' + id + 'T2" >' + telefono + '<i class="fa fa-angle-double-right pull-right" onclick="mostrarT(this);" data-id="' + id + '"></i> </td>'
						+ '</tr>' //Datos a ingresar en la tabla tutor

					var datos_combo = '<option  value="' + id + '">' + nombre + '</option>'; //datos a ingresar en el combobox tutores

					$('#tutor').append(datos); // agrega nuevo registro a tabla
					$('#tutores').append(datos_combo); //ingresa valores al combobox tutores

					$("#exito").modal("show"); //abre modal de exito

					$("#crear_tutor").modal("hide"); // cierra modal
					$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
						$("#exito").modal("hide"); // cierra modal
					});

				}

			}
		});//Fin ajax Guardar tutor

	}
}