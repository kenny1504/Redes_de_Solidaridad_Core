$(function() //funcion para buscar dentro del combobox
{
  $('#edit_tutores').select2({width:"80%"}) // agrega el select2 a combobox tutores para buscar 
  $('#edit_parent').select2({width:"100%"})// agrega el select2 a combobox parentesco para buscar 
});

$('#datepicker4').datepicker({ //sirve para mostrar Datepicker
  format: 'yyyy-mm-dd',
  autoclose: true
})
var id=0; var idper=0;  var dat=0; var cod=0;
function editar_estudiante(button)
{   
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Editar)
    $('#edit_estudiante').modal('show'); // abre ventana modal
    id=$(button).attr("data-id"); // captura el id_materia "id" del estudiante
    idper=$(button).attr("data-idper"); // captura el id_materia "id" de persona (estudiante)

    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'tutor/tutores', // llamada a ruta para cargar combobox con datos de tabla tutores
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#tutores').empty(); var datos=0;
        //ciclo para recorrer el arreglo de tutores
          data.forEach(element => {
              //variable para asignarle los valores al combobox
              datos+='<option  value="'+element.id+'">'+element.Nombre+'</option>';
          });
        $('#edit_tutores').append(datos); //ingresa valores al combobox
      }   
    });//Fin ajax combobox tutores

    $.ajax({ // ajax para cargar datos en el combobox
      type: 'POST',
      url: 'parentesco/parentescos', // llamada a ruta para cargar combobox con datos de tabla parentesco
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
        $('#parent').empty(); var datos2=0;
      //ciclo para recorrer el arreglo de parentesco
        data.forEach(element => {
            //variable para asignarle los valores al combobox
           datos2+='<option  value="'+element.id+'">'+element.Parentesco+'</option>'; 
        });
        
        $('#edit_parent').append(datos2); //ingresa valores al combobox
    }   
  });//Fin ajax combobox parentesco

  
  $.ajax({
    type: 'POST',
    url: '/estudiante/cargar_editar/'+id, // llamada a la consulta
    dataType: "JSON", // tipo de respuesta del controlador
    success: function(data){
          cod=data[0].CodigoEstudiante;
        $('#edit_Codigo').val(data[0].CodigoEstudiante);
        $('#edit_NombreE').val(data[0].Nombre);
        $('#edit_Apellido1').val(data[0].Apellido1);
        $('#edit_Apellido2').val(data[0].Apellido2);
        $('#edit_Sexo').val(data[0].Sexo);
        $('#edit_telefono').val(data[0].Telefono);
        $('#edit_tutores').select2("val",data[0].tutorid);
        $('#edit_direccion_estudiante').val(data[0].Direccion);
        $('#edit_parent').select2("val",data[0].parentescoid);
        $('#datepicker4').val(data[0].FechaNacimiento);
              
      }
   });//Fin cargar datos estudiante

}//fin del metodo Ingresar_usuario

function guardar_editar_estudiante()
{ 
 
      var codigo1=$('#edit_Codigo').val();
     if(cod==$('#edit_Codigo').val()) //si no se detectan cambios en el input
     {
      $('#edit_Codigo').val("0"); //agregar un valor nulo
     }
    //este codigo cancela el evento submit en el form donde esta la modal ingresar_Estudiante
      $("#editar_Estudiante").on('submit', function(evt){
        evt.preventDefault();  
      });


      //Verifica que le formulario no este vacio
      if($('#edit_Codigo').val()!="" && $('#edit_NombreE').val()!="" && $('#edit_Apellido1').val()!="" && $('#edit_Sexo').val()!=null 
       && $('#edit_tutores').val()!=null && $('#edit_parent').val()!=null && $('#edit_direccion_estudiante').val()!="" && $('#datepicker4').val()!="")
      {
        
          if(($("#edit_Codigo").val().length==1 || $("#edit_Codigo").val().length==8) && ($("#edit_telefono").val().length==8  || $("#edit_telefono").val().length==0 )) //verifica que el input contenga 8 valores 
          {

            $.ajax({ // ajax para guardar estudiante
              type: 'POST',
              url: '/estudiante/actualizar/'+id+'/'+idper, // llamada a ruta para guardar actualizar nuesvo estudiante
              data: $('#editar_Estudiante').serialize(), // manda el form donde se encuentra la modal ingresar_Estudiante
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
               if(data==0) // muestra mensaje de error
               {
                  var error="Imposible actualizar el estudiante, Verifique El Numero de Estudiante"
                  $('#mensaje').text(error);   //manda el error a la modal
                  $("#Mensaje-error").modal("show"); //abre modal de error
                  $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                      $("#Mensaje-error").modal("hide"); // cierra modal error
                      } );

               }
               else
               {
                 ///// Capturamos datos a insertar en la tabla estudiantes
                  var codigo=codigo1;
                  var nombre=$('#edit_NombreE').val()+" "+$('#edit_Apellido1').val()+" "+$('#edit_Apellido2').val();
                  var sexo=$('#edit_Sexo').val();
                  var dirreccion=$('#edit_direccion_estudiante').val();
                  var tutor=$('#edit_tutores').find('option:selected').text();
                  var telefono_tutor=data.Telefono;
                  var id=data.id;

                var datos=  "<tr>"+                            
                  "<td>"+codigo+"</td>"+
                  "<td>"+nombre+"</td>"+
                  "<td>"+sexo+"</td> "+
                  "<td>"+dirreccion+"</td>"+
                  "<td>"+tutor+"</td>"+
                  "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id="+id+" >"+
                              "<button class='btn btn-primary'  onclick='ver_estudiante(this);'  data-id="+id+" id='Ver-estudiante'>ver</button>"+
                              "<button class='btn btn-success' data-id="+id+" data-idper="+idper+"  onclick='editar_estudiante(this);'><i class='fa fa-fw fa-pencil'></i></button>"+
                              "<button class='btn btn-info' data-id="+id+" onclick='eliminar_estudiante(this);' ><i class='fa fa-fw fa-trash '></i></button>"+
                             " <i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);'  data-id="+id+"></i>"+                             
                  "</td>"+
                  "</td>"+
                  "<td id="+id+"a>"+telefono_tutor+ "<i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);' data-id="+id+"></i> </td>"            
                "</tr>"  //Datos a ingresar en la tabla estudiantes

                dat.replaceWith(datos); // Reemplaza nuevo registro a tabla
                  
                $("#exito").modal("show"); //abre modal de exito
                $("#edit_estudiante").modal("hide"); // cierra modal
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                          $("#exito").modal("hide"); // cierra modal
                          } );

               }
              
            }   
          });//Fin ajax Guardar estudiante
          }
                 
      }
}




