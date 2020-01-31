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
function ingresar_matricula(button)
{   
        $('#Seccion_M').val("");
        $('#Docente_M').val("");
        $('#Grado_M').val("");
        $('#Grupo_M').val("");
        $('#asignaturas_grado_M').empty();
        dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Matricular)
        $('#crear_matricula').modal('show'); // abre ventana modal

        
        var ide=$(button).attr("data-id");//obtiene el id del estudiante a matricular
        $('#idestudiante_M').val(ide);   //manda valor "id" a ventana modal
        cargar_oferta(); //llamado al metodo para que carga a la primera
      
      $.ajax({
        type: 'POST',
        url: 'estudiante/cargar_editar/'+ide, // llamada a la consulta
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){
            $('#CodigoE_M').val(data[0].CodigoEstudiante);
            $('#NombreE_M').val(data[0].Nombre+' '+data[0].Apellido1+' '+data[0].Apellido2);
            
          }
       });//Fin cargar datos del estudiante a matricular

    $.ajax({
        type: 'POST',
        url: 'cargarturnos/turno', // llamada a ruta para cargar combobox con datos de tabla turno
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){        
          $('#Turno').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de turno
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Nombre+'</option>';

              $('#Turno').append(datos);//ingresa valores al combobox
              $('#Turno').val(''); // limpiar los turno
          });          
      }
    });//Fin ajax combobox Turno

    $.ajax({
      type: 'POST',
      url: 'cargarsituacion_matricula/matricula', // llamada a ruta para cargar combobox con datos de tabla situacion matricula
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){        
        $('#Situacion_Matricula').empty();//limpia el combobox
        data.forEach(element => { //ciclo para recorrer el arreglo de turno
            //variable para asignarle los valores al combobox
          var datos='<option  value="'+element.id+'">'+element.Descripcion+'</option>';

            $('#Situacion_Matricula').append(datos);//ingresa valores al combobox
            $('#Situacion_Matricula').val(''); // limpiar la situacion matricula
        });          
    }
  });//Fin ajax combobox situacion matricula

}//fin del metodo

function cargar_oferta() //Metodo para cargar las ofertas segun el año seleccionado
{   
     //Limpia campos del detalle de oferta
       $('#asignaturas_grado_M').empty(); //limpia la tabla
        $('#Seccion_M').val("");
        $('#Docente_M').val("");
        $('#Grado_M').val("");
        $('#Grupo_M').val("");
    $.ajax({
      type: 'POST',
      url: 'oferta/cargar_ofertas/'+$('#datepickerMatricula').val(),//ruta para cargar la descripcion de las ofertas
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
          $('#Oferta_M').empty();//limpia el combobox
        for(var a=0; a<data.length;a++){          
            var datos='<option  value="'+data[a].idOferta+'">'+data[a].Descripcion+'</option>';
            $('#Oferta_M').append(datos);//ingresa valores al combobox de ofertas
            $('#Oferta_M').val(''); // limpiar las ofertas
        }         
    }
   });//Fin ajax 
}

$("#Oferta_M").change(function () { //ajax para ver detalles de oferta
  if($('#Oferta_M').val()!=null) // si se ha seleccionado una oferta
    {
        $.ajax({
          type: 'POST',
          url: 'oferta/cargar/'+$('#Oferta_M').val(), //llamada a la ruta para mostrar detalles
          dataType: "JSON", // tipo de respuesta del controlador
          success: function(data){ //carga el detalle de la oferta

            for(var a=0; a<data.length;a++){          
            $('#Seccion_M').val(data[0].Nombre_Seccion);
            $('#Docente_M').val(data[0].Nombre_Docente);
            $('#Grado_M').val(data[0].Nombre_Grado);
            $('#Grupo_M').val(data[0].Nombre_Grupo);
            }     
        }
        });//fin de ajax
        //***************** AJAX PARA CARGAR ASIGNATURAS EN TABLA  ************* */
        $('#asignaturas_grado_M').empty(); //limpia la tabla
        $.ajax({
            type: 'POST',
            url: 'matricula/cargarmaterias_grado_M/'+$('#Oferta_M').val(), // llamada a ruta para cargar asignaturas en las tablas
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
                    for(var a=0; a<data.length;a++){                     
                           var datos=  "<tr value=" + data[a].id + ">"+"<td>"+data[a].Nombre+"</td>"
                            + "<td>"+ "<button type='button' class='btn btn-danger' data-id="+ data[a].id +" onclick='remover_Materia_Grado(this)'><i class='fa fa-fw fa-trash '></i></button>"                                   
                            +"</td>"+"</tr>"+"<td><input type='hidden' name='MateriasM[]' value='"+data[a].id+"'></td>"; 
                        $('#asignaturas_grado_M').append(datos); // agrega nuevo registro a tabla
                    }      
                }      
        });//Fin ajax cargar materias en tabla

    }
}); //fin de funcion

function remover_Materia_Grado(button)
{
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
$("#datepickerMatricula").change(function () { //esta funcion sirve para que las ofertas cambien segun cambie el año
  cargar_oferta(); //llamado a funcion
}); //fin de funcion


function nueva_matricula() { // ajax para guardar en la tabla matricula

    $("#ingresar_Matricula").on('submit', function(evt){
      evt.preventDefault();  
    });
  
    if($('#Oferta_M').val()!=null && $('#Turno').val()!=null && $('#Situacion_Matricula').val()!=null ) // si el input contiene valores entra 
    {
    $.ajax({
      type: 'POST', 
      url: 'matricula/guardar', // llamada a ruta para guardar la nueva matricula
      data: $('#ingresar_Matricula').serialize(), // manda el form donde se encuentra la modal dataType: "JSON", // tipo de respuesta del controlador
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
        if (data==0) { 
          $('#crear_matricula').modal('hide'); // cierra ventana modal
          $('#ver_matricula_confirmar').modal('show'); // cierra ventana modal
        } 
        else {
          $('#crear_matricula').modal('hide'); // cierra ventana modal
          $("#exito").modal("show"); //abre modal de exito          
          $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
            $("#exito").modal("hide"); // cierra modal
          } );
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

  
