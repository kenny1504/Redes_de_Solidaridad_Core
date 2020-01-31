$('#año_oferta').datepicker({ //Fecha Matricula
    format: 'yyyy',
    viewMode: "years",
    minViewMode: "years",
    autoclose: true,
  })


$(document).ready(function(){ //ajax para cargar combo box en vista Notas 
    
    $.ajax({
        type: 'POST',
        url: 'cargargrados/oferta', // llamada a ruta para cargar combobox con datos de tabla grados
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#grado_nota').empty();//limpia el combobox
          var datos ="<option value='' disabled selected>Grado</option>";
          data.forEach(element => { //ciclo para recorrer el arreglo de grados
              //variable para asignarle los valores al combobox
             datos+='<option  value="'+element.id+'">'+element.Grado+'</option>';
          });
          $('#grado_nota').append(datos);//ingresa valores al combobox
          
      }
    });//Fin ajax combobox Grado

    $.ajax({
        type: 'POST',
        url: '/cargar_detalles', // llamada a ruta para cargar combobox  Detalle de Nota
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#Detalle_nota').empty();//limpia el combobox
          var datos ="<option value='' disabled selected>Semestre</option>";
          data.forEach(element => { //ciclo para recorrer el arreglo de grados
              //variable para asignarle los valores al combobox
             datos+='<option  value="'+element.id+'">'+element.Descripcion+'</option>';
          });
          $('#Detalle_nota').append(datos);//ingresa valores al combobox
          
      }
    });//Fin ajax combobox Detalle de Nota

    
    $.ajax({
        type: 'POST',
        url: 'cargargrupos/grupo', // llamada a ruta para cargar combobox con datos de tabla grupos
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#grupo_nota').empty();//limpia el combobox
          var datos ="<option value='' disabled selected>Grupo</option>";
          data.forEach(element => { //ciclo para recorrer el arreglo de grupos
              //variable para asignarle los valores al combobox
             datos+='<option  value="'+element.id+'">'+element.Grupo+'</option>';
          });
          $('#grupo_nota').append(datos);//ingresa valores al combobox
          
      }
    });//Fin ajax combobox Grupo

});


$("#grado_nota").change(function () {
 
    var id_nota= $('#grado_nota').val();

    $.ajax({
        type: 'POST',
        url: 'asignatura/cargarmaterias_grado/'+id_nota, // llamada a ruta para cargar asignaturas en las tablas
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
             
              $('#materias_nota').empty();//limpia el combobox
              if(data!=false)
              {
                var datos ="<option value='' disabled selected>Materia</option>";
                data.forEach(element => {
                    datos+='<option  value="'+element.Id+'">'+element.Nombre+'</option>';
                });
                $('#materias_nota').append(datos);//ingresa valores al combobox
              }
              else
              {
                var datos ="<option value='' disabled selected>Materia</option>";
                    datos+="<option  value=''>'Sin datos'</option>";
                $('#materias_nota').append(datos);//ingresa valores al combobox
              }             
            }      
    });//Fin ajax cargar materias en tabla

});


function Mostar_Notas()
{
  
    $('#estudiantes_Notas').empty();//limpia tabla de Notas
    var dat="<thead> "+"<tr> "+"<th>Codigo estudiante</th>"+
      "<th>Nombre completo</th>"+"<th>Sexo</th>"+
      "<th>Grado</th>"+"<th>Grupo</th>"+"<th>Materia</th>"+
      "<th>Nota</th>"+"</tr>"+"</thead> ";
    $('#estudiantes_Notas').append(dat);

    var año=$('#año_oferta').val();
    var id_grado=$('#grado_nota').val();
    var id_grupo=$('#grupo_nota').val(); 
    var id_detalle= $('#Detalle_nota').val(); 
    var id_materia= $('#materias_nota').val(); 

    if($('#grado_nota').val()!=null && $('#grupo_nota').val()!=null &&  $('#Detalle_nota').val()!=null && $('#materias_nota').val()!=null)
    {

        $.ajax({ //ajax Mostra Notas
            type: 'POST',
            url: '/nota/cargar/'+año+'/'+id_grado+'/'+id_grupo+'/'+id_detalle+'/'+id_materia, // llamada a ruta para cargar combobox con datos de tabla grupos
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
            
             if(data==0)
             {
                var error="No se a encontrado ningun registro"
                $('#mensaje').text(error);   //manda el error a la modal
                $("#Mensaje-error").modal("show"); //abre modal de error
                $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                    $("#Mensaje-error").modal("hide"); // cierra modal error
                    } );
    
             }
             else
             { 
               var datos;
                  data.forEach(element => {
                      datos+= "<tr>"+                            
                      "<td>"+element.CodigoEstudiante+"</td>"+
                      "<td>"+element.Nombre+"</td>"+
                      "<td>"+element.Sexo+"</td> "+
                      "<td>"+element.Grado+"</td>"+
                      "<td>"+element.Grupo+"</td>"+
                      "<td>"+element.asignatura+"</td>"+
                      "<td><input minlength='2' type='number' style='width: 15%;' name='Notas[]' value='"+element.Nota+"'></td>"+
                      "<td class='hidden'><input  name='DetalleMatricula[]' value='"+element.id+"'></td></tr>";
                  });
                $('#estudiantes_Notas').append(datos);//ingresa valores al combobox

             }
  
              
          }
        });//Fin ajax Mostra Notas

    }
    else
    {
        var error="Favor seleccione todos los  datos de los Desplegables"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );

    }


}

function Guardar_Notas()
{
  $.ajax({ //ajax Mostra Notas
    type: 'POST',
    url: 'nota/Guardar_Notas', // llamada a ruta para cargar combobox con datos de tabla grupos
    data: $('#Estudiante_Notas').serialize(),
    dataType: "JSON", // tipo de respuesta del controlador
    success: function(data){ 
      if(data==true)
      {
          $('#crear_matricula').modal('hide'); // cierra ventana modal
          $("#exito").modal("show"); //abre modal de exito          
          $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
            $("#exito").modal("hide"); // cierra modal
          } );
      }
      else
      {
        var error="Error uno o varios registro no se guardaron"
                $('#mensaje').text(error);   //manda el error a la modal
                $("#Mensaje-error").modal("show"); //abre modal de error
                $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                    $("#Mensaje-error").modal("hide"); // cierra modal error
                    } );
      }
    
  }
});//Fin ajax Mostra Notas


}



