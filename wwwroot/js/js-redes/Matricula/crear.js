$('#datepickerMatricula').datepicker({ //Fecha Matricula
	format: 'yyyy',
	viewMode: "years",
	minViewMode: "years",
	autoclose: true,
}).datepicker("setDate", new Date());

$('#datepickerFechaMatricula').datepicker({ //sirve para mostrar fehca actual
	format: 'yyyy-mm-dd',
	autoclose: true
}).datepicker("setDate", new Date());

var dat;
function ingresar_matricula(button) {
	$('#Seccion_M').val("");
	$('#Docente_M').val("");
	$('#Grado_M').val("");
	$('#Grupo_M').val(""); 
	$('#asignaturas_grado_M').empty();
	dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Matricular)
	$('#crear_matricula').modal('show'); // abre ventana modal

	var fecha = new Date();//fecha actual
	$('#Fecha').text(fecha.toLocaleString()); //Establece la fecha actual en la MOdal 


	var ide = $(button).attr("data-id");//obtiene el id del estudiante a matricular
	var codigo = $(button).attr("data-codigo");//obtiene el id del estudiante a matricular
	var nombre = $(button).attr("data-nombre");//obtiene el id del estudiante a matricular

	//Asigna Valores de nombre y codigo a modal matricular
	$('#CodigoE_M').val(codigo);
	$('#NombreE_M').val(nombre);

	$('#idestudiante_M').val(ide);   //manda valor "id" a ventana modal
	cargar_oferta(); //llamado al metodo para que carga a la primera



	$.ajax({
		type: 'POST',
		url: 'Turno/Turnos', // llamada a ruta para cargar combobox con datos de tabla turno
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {
			$('#Turno').empty();//limpia el combobox
			data.forEach(element => { //ciclo para recorrer el arreglo de turno
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.nombre + '</option>';

				$('#Turno').append(datos);//ingresa valores al combobox
				$('#Turno').val(''); // limpiar los turno
			});
		}
	});//Fin ajax combobox Turno

	$.ajax({
		type: 'POST',
		url: 'Oferta/situacionMatricula', // llamada a ruta para cargar combobox con datos de tabla situacion matricula
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {
			$('#Situacion_Matricula').empty();//limpia el combobox
			data.forEach(element => { //ciclo para recorrer el arreglo de turno
				//variable para asignarle los valores al combobox
				var datos = '<option  value="' + element.id + '">' + element.descripcion + '</option>';

				$('#Situacion_Matricula').append(datos);//ingresa valores al combobox
				$('#Situacion_Matricula').val(''); // limpiar la situacion matricula
			});
		}
	});//Fin ajax combobox situacion matricula

}//fin del metodo

var ide = $("#id_u").text()//Capturamos el id de la Institucion logueada
function cargar_oferta() //Metodo para cargar las ofertas segun el año seleccionado
{
	//Limpia campos del detalle de oferta
	$('#asignaturas_grado_M').empty(); //limpia la tabla
	$('#Seccion_M').val("");
	$('#Docente_M').val("");
	$('#Grado_M').val("");
	$('#Grupo_M').val("");
	var año = $('#datepickerMatricula').val()

	$.ajax({
		type: 'POST',
		url: 'Oferta/Ofertas',//ruta para cargar la descripcion de las ofertas
		data: { id: ide },
		dataType: "JSON", // tipo de respuesta del controlador
		success: function (data) {
			$('#Oferta_M').empty();//limpia el combobox
			for (var a = 0; a < data.length; a++) {
				var datos = '<option  value="' + data[a].id + '">' + data[a].descripcion + '</option>';
				$('#Oferta_M').append(datos);//ingresa valores al combobox de ofertas
				$('#Oferta_M').val(''); // limpiar las ofertas
			}
		}
	});//Fin ajax 
}

$("#Oferta_M").change(function () { //ajax para ver detalles de oferta
	if ($('#Oferta_M').val() != null) // si se ha seleccionado una oferta
	{
		var ofertaid = $('#Oferta_M').val();
		$.ajax({
			type: 'POST',
			url: 'Oferta/DetallesOfertas', //llamada a la ruta para mostrar detalles
			data: { id: ofertaid },
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) { //carga el detalle de la oferta

				
					$('#Seccion_M').val(data.nombre_Seccion);
					$('#Docente_M').val(data.nombre_Docente);
					$('#Grado_M').val(data.nombre_Grado);
					$('#Grupo_M').val(data.nombre_Grupo);
				
			}
		});//fin de ajax
		//***************** AJAX PARA CARGAR ASIGNATURAS EN TABLA  ************* */
		$('#asignaturas_grado_M').empty(); //limpia la tabla

		$.ajax({
			type: 'POST',
			url: 'Oferta/Materiasoferta' , // llamada a ruta para cargar asignaturas en las tablas
			data: { idoferta: ofertaid, idinstitucion: ide},
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				for (var a = 0; a < data.length; a++) {
					var datos = "<tr value=" + data[a].id + ">" + "<td>" + data[a].nombre + "</td>"
						+ "<td>" + "<button type='button' class='btn btn-danger' data-id=" + data[a].id + " onclick='remover_Materia_Grado(this)'><i class='fa fa-fw fa-trash '></i></button>"
						+ "</td>" + "</tr>" + "<td><input type='hidden' name='MateriasM[]' value='" + data[a].id + "'></td>";
					$('#asignaturas_grado_M').append(datos); // agrega nuevo registro a tabla
				}
			}
		});//Fin ajax cargar materias en tabla

	}
}); //fin de funcion

function remover_Materia_Grado(button) {
	dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
	dat.remove(); //remueve la fila eliminado 
}
//function mandarid_Materia()
//{
//var ty=$('#asignaturas_grado_M').val();
//for(var a=0;a<ty;a++)
//{
//  var datos= "</td>"+"</tr>"+"<td><input type='hidden' name='MateriasMe[]' value='"+ty[a].id+"'></td>"; 
//   }
//}

function nueva_matricula() { // ajax para guardar en la tabla matricula

	$("#ingresar_Matricula").on('submit', function (evt) {
		evt.preventDefault();
	});

	if ($('#Oferta_M').val() != null && $('#Turno').val() != null && $('#Situacion_Matricula').val() != null) // si el input contiene valores entra 
	{
		$.ajax({
			type: 'POST',
			url: 'matricula/guardar', // llamada a ruta para guardar la nueva matricula
			data: $('#ingresar_Matricula').serialize(), // manda el form donde se encuentra la modal dataType: "JSON", // tipo de respuesta del controlador
			dataType: "JSON", // tipo de respuesta del controlador
			success: function (data) {
				if (data == 0) {
					$('#crear_matricula').modal('hide'); // cierra ventana modal
					$('#ver_matricula_confirmar').modal('show'); // cierra ventana modal
				}
				else {
					$('#crear_matricula').modal('hide'); // cierra ventana modal
					$("#exito").modal("show"); //abre modal de exito          
					$("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
						$("#exito").modal("hide"); // cierra modal
					});
				}
			}
		});
		//limpiar cajas de textos
		$('#Seccion_M').val("");
		$('#Docente_M').val("");
		$('#Grado_M').val("");
		$('#Grupo_M').val("");
		$('#Oferta_M').val('');
		$('#asignaturas_grado_M').val('');
		$('#Turno').val('');
		$('#Situacion_Matricula').val('');
	}
}


