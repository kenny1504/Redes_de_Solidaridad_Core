$('#datepickerMatricula_Editar').datepicker({ //Fecha Matricula
    format: 'yyyy',
    viewMode: "years",
    minViewMode: "years",
    autoclose: true,
  }).datepicker("setDate", new Date());

  $('#datepickerFechaMatricula_Editar').datepicker({ //sirve para mostrar fehca actual
    format: 'yyyy-mm-dd',
    autoclose: true
  }).datepicker("setDate", new Date());

var dat; //variable global que guarda el dato "tr" (Fila a editar)
function confirmar_editar_matricula()
{
    $('#ver_matricula_confirmar').modal('hide'); // abre ventana modal
    

    var idestudi=$('#idestudiante_M').val();
    var codigo=$('#CodigoE_M').val();
    var nombre=$('#NombreE_M').val();
    var anio_oferta=$('#datepickerMatricula').val();

    $('#editar_matricula').modal('show'); // abre ventana modal

    $('#idestudiante_M_Editar').val(idestudi);
    $('#CodigoE_M_Editar').val(codigo);
    $('#NombreE_M_Editar').val(nombre);
    $('#datepickerMatricula_Editar').val(anio_oferta);
    cargar_oferta_editar();
    $.ajax({
        type: 'POST',
        url: 'cargarturnos/turno', // llamada a ruta para cargar combobox con datos de tabla turno
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){        
          $('#Turno_Editar').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de turno
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Nombre+'</option>';

              $('#Turno_Editar').append(datos);//ingresa valores al combobox
              
          });          
      }
    });//Fin ajax combobox Turno

    $.ajax({
        type: 'POST',
        url: 'cargarsituacion_matricula/matricula', // llamada a ruta para cargar combobox con datos de tabla situacion matricula
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){        
          $('#Situacion_Matricula_Editar').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de turno
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Descripcion+'</option>';
  
              $('#Situacion_Matricula_Editar').append(datos);//ingresa valores al combobox
              
          });          
      }
    });//Fin ajax combobox situacion matricula


    $.ajax({
      type: 'POST',
      url: 'matricula/recuperar_Matricula/'+idestudi+'/'+anio_oferta,//ruta para cargar datos de la oferta seleccionada a editar
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
        for(var a=0; a<data.length;a++){
            var idOferta=data[a].Ofertaid;
            var idTurno=data[a].Turnoid;
            var idSituacion=data[a].SituacionMatriculaid;
            var grado=data[a].Grado;
            var grupo=data[a].Grupo;
            var seccion=data[a].Seccion;
            var docente=data[a].NombreD;
        }
        $('#idoferta_Editar').val(data[0].id);
        $('#Oferta_M_Editar').val(idOferta);
        $('#Turno_Editar').val(idTurno);
        $('#Situacion_Matricula_Editar').val(idSituacion);
        $('#Docente_M_Editar').val(docente);
        $('#Grado_M_Editar').val(grado);
        $('#Grupo_M_Editar').val(grupo);
        $('#Seccion_M_Editar').val(seccion);
        if($('#Oferta_M_Editar').val()!=null) // si se ha seleccionado una oferta
      {
          $.ajax({
              type: 'POST',
              url: 'matricula/cargarmaterias_grado_M/'+$('#Oferta_M_Editar').val(), // llamada a ruta para cargar asignaturas en las tablas
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
                      for(var a=0; a<data.length;a++){                     
                             var datos=  "<tr value=" + data[a].id + ">"+"<td>"+data[a].Nombre+"</td>"
                              + "<td>"+ "<button type='button' class='btn btn-danger' data-id="+ data[a].id +" onclick='remover_Materia_Grado_Editar(this)'><i class='fa fa-fw fa-trash '></i></button>"                                   
                              +"</td>"+"</tr>"+"<td><input type='hidden' name='MateriasM_Editar[]' value='"+data[a].id+"'></td>"; 
                          $('#asignaturas_grado_M_Editar').append(datos); // agrega nuevo registro a tabla
                      }      
                  }      
          });//Fin ajax cargar materias en tabla
        }
    }
  });//Fin ajax 
  
}

function cargar_oferta_editar() //Metodo para cargar las ofertas segun el año seleccionado
{   
     //Limpia campos del detalle de oferta
    $.ajax({
      type: 'POST',
      url: 'oferta/cargar_ofertas/'+$('#datepickerMatricula_Editar').val(),//ruta para cargar la descripcion de las ofertas
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
          $('#Oferta_M_Editar').empty();//limpia el combobox
        for(var a=0; a<data.length;a++){          
            var datos='<option  value="'+data[a].idOferta+'">'+data[a].Descripcion+'</option>';
            $('#Oferta_M_Editar').append(datos);//ingresa valores al combobox de ofertas
        }         
    }
   });//Fin ajax 
}

$("#Oferta_M_Editar").change(function () { //ajax para ver detalles de oferta
    if($('#Oferta_M_Editar').val()!=null) // si se ha seleccionado una oferta
      {
          $.ajax({
            type: 'POST',
            url: 'oferta/cargar/'+$('#Oferta_M_Editar').val(), //llamada a la ruta para mostrar detalles
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ //carga el detalle de la oferta
  
              for(var a=0; a<data.length;a++){          
              $('#Seccion_M_Editar').val(data[0].Nombre_Seccion);
              $('#Docente_M_Editar').val(data[0].Nombre_Docente);
              $('#Grado_M_Editar').val(data[0].Nombre_Grado);
              $('#Grupo_M_Editar').val(data[0].Nombre_Grupo);
              }     
          }
          });//fin de ajax
          //***************** AJAX PARA CARGAR ASIGNATURAS EN TABLA  ************* */
          $('#asignaturas_grado_M_Editar').empty(); //limpia la tabla
          $.ajax({
              type: 'POST',
              url: 'matricula/cargarmaterias_grado_M/'+$('#Oferta_M_Editar').val(), // llamada a ruta para cargar asignaturas en las tablas
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
                      for(var a=0; a<data.length;a++){                     
                             var datos=  "<tr value=" + data[a].id + ">"+"<td>"+data[a].Nombre+"</td>"
                              + "<td>"+ "<button type='button' class='btn btn-danger' data-id="+ data[a].id +" onclick='remover_Materia_Grado_Editar(this)'><i class='fa fa-fw fa-trash '></i></button>"                                   
                              +"</td>"+"</tr>"+"<td><input type='hidden' name='MateriasM_Editar[]' value='"+data[a].id+"'></td>"; 
                          $('#asignaturas_grado_M_Editar').append(datos); // agrega nuevo registro a tabla
                      }      
                  }      
          });//Fin ajax cargar materias en tabla
  
      }
  }); //fin de funcion
  function remover_Materia_Grado_Editar(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    dat.remove(); //remueve la fila eliminado 
}
$("#datepickerMatricula_Editar").change(function () { //esta funcion sirve para que las ofertas cambien segun cambie el año
    cargar_oferta_editar(); //llamado a funcion
  }); //fin de funcion
  

function actualizar_matricula() {
  $("#editar_Matricula").on('submit', function(evt){
    evt.preventDefault();  
  });
    $.ajax({
                
        type: 'POST',
        url: 'actualizar/matricula', // ruta editar oferta
        data: $('#editar_Matricula').serialize(), // manda el form donde se encuentra la modal oferta
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){
        if ((data==1)) // si el ajax contiene errores agrega un label indicando el error 
        { 
          $("#exito").modal("show"); //abre modal de exito
          $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
              $("#exito").modal("hide"); // cierra modal                                           
              } );
          $("#editar_matricula").modal("hide"); // cierra modal 
        } 
        else 
        {
          var error="Error al Actualizar Matricula"
          $('#mensaje').text(error);   //manda el error a la modal
          $("#Mensaje-error").modal("show"); //abre modal de error
          $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
              $("#Mensaje-error").modal("hide"); // cierra modal error
              } );
                                 
        }
        }
        });
    }