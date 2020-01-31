function mostrar_Materias_grados(button)
{
    $("#asignaturas_grado").removeClass('text-red'); //remueve clase que asigna el color rojo al texto
    $("#asignaturas_grado").addClass('text-black');//agrega clase que asigna el color negro al texto
    var ide=$(button).attr("data-id");//obtiene el id del grado
    $('#mostrar_Materia-grados').modal('show'); // abre ventana modal
    $('#grado').text(name);   //manda el grado a la modal
    //***************** AJAX PARA CARGAR ASIGNATURAS EN TABLA  ************* */
    $('#asignaturas_grado').empty(); //limpia la tabla
    $.ajax({
        type: 'POST',
        url: '/asignatura/cargarmaterias_grado/'+ide, // llamada a ruta para cargar asignaturas en las tablas
        data: $('#mostrar_Materia-grado').serialize(), // manda el form donde se encuentra la modal 
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
              if(data!=false)
              {
                data.forEach(element => {
                    var datos=  "<tr id=" + element.Id + ">"+"<td>"+element.Nombre+"</td>"
                        + "<td>"+ "<button type='button' class='btn btn-danger' data-id="+ element.Id +" onclick='eliminar_Materia_Grado(this);'><i class='fa fa-fw fa-trash '></i></button>"                                   
                        +"</td>"+"</tr>"; // variable guarda el valor del registro de materias
                    $('#asignaturas_grado').append(datos); // agrega nuevo registro a tabla
                });

              }
              else
              {
                $("#asignaturas_grado").removeClass('text-black'); //remueve clase que asigna el color negro al texto
                $("#asignaturas_grado").addClass('text-red');//agrega clase que asigna el color rojo al texto
                var dato= "<th colspan='2'>No Se Encontraron Asignaturas En Este Grado</th>";
                $('#asignaturas_grado').append(dato); // agrega nuevo registro a tabla
              }             
            }      
    });//Fin ajax cargar materias en tabla
}

function eliminar_Materia_Grado(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var id=$(button).attr("data-id");//obtiene el id del materia
    $.ajax({
        type: 'POST',
        url: 'detalleAsignatura/eliminar/'+id, // llamada a ruta para cargar asignaturas en las tablas
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
           if(data==1)
           {
            dat.remove(); //remueve la fila eliminado 
            $("#exito").modal("show"); //abre modal de exito
            $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                        $("#exito").modal("hide"); // cierra modal
                        });
           }
          }      
    });//Fin ajax cargar materias en tabla
}
  