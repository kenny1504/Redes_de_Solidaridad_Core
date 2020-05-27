$(function() //funcion para buscar dentro del combobox
{
  $('#tutores').select2({width:"80%"}) // agrega el select2 a combobox tutores para buscar 
  $('#parent').select2({width:"100%"})// agrega el select2 a combobox parentesco para buscar 
});

$('#datepicker3').datepicker({ //sirve para mostrar Datepicker
  format: 'yyyy-mm-dd',
  autoclose: true
})

function ingresar_estudiante()
{   
    $('#crear_estudiante').modal('show'); // abre ventana modal
    //limpia campos
    $('#Codigo').val("");
    $('#NombreE').val("");
    $('#Apellido1').val("");
    $('#Apellido2').val("");
    $('#Sexo').val(null);
    $('#telefono').val("");
    $('#tutores').val(null);
    $('textarea').val ('');
    $('#parent').val(null);
    $('#datepicker3').val(null);

    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'tutor/tutores', // llamada a ruta para cargar combobox con datos de tabla tutores
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#tutores').empty();
        //ciclo para recorrer el arreglo de tutores
          var datos ="<option value='' disabled selected>Tutor</option>";
          data.forEach(element => {
              //variable para asignarle los valores al combobox
              datos+='<option  value="'+element.id+'">'+element.Nombre+'</option>';
          });
        $('#tutores').append(datos); //ingresa valores al combobox
      }   
    });//Fin ajax combobox tutores

    $.ajax({ // ajax para cargar datos en el combobox
      type: 'POST',
      url: 'parentesco/parentescos', // llamada a ruta para cargar combobox con datos de tabla parentesco
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
        $('#parent').empty();
      //ciclo para recorrer el arreglo de parentesco
        var datos2 ="<option value='' disabled selected>Parentesco</option>";
        data.forEach(element => {
            //variable para asignarle los valores al combobox
           datos2+='<option  value="'+element.id+'">'+element.Parentesco+'</option>'; 
        });
        
        $('#parent').append(datos2); //ingresa valores al combobox
    }   
  });//Fin ajax combobox parentesco

}//fin del metodo Ingresar_usuario



function nuevo_estudiante()
{ 

    //este codigo cancela el evento submit en el form donde esta la modal ingresar_Estudiante
      $("#ingresar_Estudiante").on('submit', function(evt){
        evt.preventDefault();  
      });


      //Verifica que le formulario no este vacio
      if($('#Codigo').val()!="" && $('#NombreE').val()!="" && $('#Apellido1').val()!="" && $('#Sexo').val()!=null 
       && $('#tutores').val()!=null && $('#parent').val()!=null && $('#direccion_estudiante').val()!="" && $('#datepicker3').val()!="")
      {
        ///// Capturamos datos a insertar en la tabla estudiantes
        var codigo=$('#Codigo').val();
        var nombre=$('#NombreE').val()+$('#Apellido1').val()+$('#Apellido2').val();
        var sexo=$('#Sexo').val();
        var dirreccion=$('#direccion_estudiante').val();
        var tutor=$('#tutores').text();
        

          if($("#Codigo").val().length==8 && ($("#telefono").val().length==8  || $("#telefono").val().length==0 )) //verifica que el input contenga 8 valores 
          {

            $.ajax({ // ajax para guardar estudiante
              type: 'POST',
              url: 'estudiante/agregar', // llamada a ruta para guardar un nuesvo estudiante
              data: $('#ingresar_Estudiante').serialize(), // manda el form donde se encuentra la modal ingresar_Estudiante
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
               if(data==0) // muestra mensaje de error
               {
                  var error="El estudiante ya existe, favor revisar el codigo de estudiante que ha ingresado"
                  $('#mensaje').text(error);   //manda el error a la modal
                   $("#Mensaje-error").modal("show"); //abre modal de error
                  $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                      $("#Mensaje-error").modal("hide"); // cierra modal error
                      } );

               }
               else
               {
                 ///// Capturamos datos a insertar en la tabla estudiantes
                  var codigo=$('#Codigo').val();
                  var nombre=$('#NombreE').val()+" "+$('#Apellido1').val()+" "+$('#Apellido2').val();
                  var sexo=$('#Sexo').val();
                  var dirreccion=$('#direccion_estudiante').val();
                  var tutor=$('#tutores').find('option:selected').text();
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
                              "<button class='btn btn-success'><i class='fa fa-fw fa-pencil'></i></button>"+
                              "<button class='btn btn-info' data-id="+id+" onclick='eliminar_estudiante(this);' ><i class='fa fa-fw fa-trash '></i></button>"+
                             " <i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);'  data-id="+id+"></i>"+                             
                  "</td>"+
                  "</td>"+
                  "<td id="+id+"a>"+telefono_tutor+ "<i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);' data-id="+id+"></i> </td>"            
                "</tr>"  //Datos a ingresar en la tabla estudiantes

                var  datos_="<tr>"+                            
                "<td>"+codigo+"</td>"+
                "<td>"+nombre+"</td>"+
                "<td>"+sexo+"</td> "+
                "<td>"+dirreccion+"</td>"+
                "<td>"+tutor+"</td>"+
                "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id="+id+" >"+
                            "<button class='btn btn-primary'  onclick='ver_estudiante(this);'  data-id="+id+" id='Ver-estudiante'>Matricula</button>"+
                           " <i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);'  data-id="+id+"></i>"+                             
                "</td>"+
                "</td>"+
                "<td id="+id+"a>"+telefono_tutor+ "<i class='fa fa-angle-double-right pull-right' onclick='mostrar(this);' data-id="+id+"></i> </td>"            
              "</tr>"//Datos a ingresar en la tabla estudiantes matricula



                $('#estudiantes_matricula').append(datos_); // agrega nuevo registro a tabla estudiante
                $('#estudiantes').append(datos); // agrega nuevo registro a tabla estudiante
                  
                $("#exito").modal("show"); //abre modal de exito
                $("#crear_estudiante").modal("hide"); // cierra modal
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                          $("#exito").modal("hide"); // cierra modal
                          } );

               }
              
            }   
          });//Fin ajax Guardar estudiante
          }
                 
      }
}





