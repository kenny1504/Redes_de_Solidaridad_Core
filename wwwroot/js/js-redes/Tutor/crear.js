$(function() //funcion para buscar dentro del combobox
{
  $('#oficiot').select2({width:"80%"}) // agrega el select2 a combobox tutores para buscar
});
$('#datepickertutor').datepicker({ //sirve para mostrar Datepicker
    format: 'yyyy-mm-dd',
    autoclose: true
  })
  $('#datepickerTutorEditar').datepicker({ //mostrar Datepicker editar
    format: 'yyyy-mm-dd',
    autoclose: true
  })
function ingresar_tutor()
{   
    $('#crear_tutor').modal('show'); // abre ventana modal
    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'cargar/oficio', // llamada a ruta para cargar combobox oficio
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#oficiot').empty();
        //ciclo para recorrer el arreglo de oficios
          data.forEach(element => {
              //variable para asignarle los valores al combobox
             var datos='<option  value="'+element.id+'">'+element.Nombre+'</option>'; 
  
              $('#oficiot').append(datos); //ingresa valores al combobox
          });
          
      }   
    });//Fin ajax combobox oficios
}//fin del metodo Ingresar_tutor

function guardar_Tutor()
{ 
      $("#ingresar_tutor").on('submit', function(evt){
        evt.preventDefault();  
      });
      //Verifica que le formulario no este vacio
      if($('#Cedulat').val().length==16 && $('#Nombre-tutor').val()!="" && $('#apellido1-tutor').val()!="" && $('#telefonot').val()!="" && $('#sexot').val()!=null && $('#oficiot').val()!=null 
      && $('#correot').val()!=null&& $('#direcciont').val()!="" && $('#datepickertutor').val()!="")
      {
          if($("#telefonot").val().length==8 && ValidarCedulaTutor($('#Cedulat').val())==true) //verifica que el input contenga 8 valores 
          {

            $.ajax({ // ajax para guardar docente
              type: 'POST',
              url: 'tutor/agregar', // llamada a ruta para guardar un nuevo tutor
              data: $('#ingresar_tutor').serialize(), // manda el form donde se encuentra la modal ingresar_tutor
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
               if(data==0) // muestra mensaje de error
               {
                  var error="El tutor ya existe, favor verificar Cedula"
                  $('#mensaje').text(error);   //manda el error a la modal
                  $("#Mensaje-error").modal("show"); //abre modal de error
                  $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                      $("#Mensaje-error").modal("hide"); // cierra modal error
                      } );

               }
               else
               {
                var cedula=$('#Cedulat').val();
                var nombre=$('#Nombre-tutor').val()+" "+$('#apellido1-tutor').val()+" "+$('#apellido2-tutor').val();
                var sexo=$('#sexot').val();
                var correo=$('#correot').val();
                var oficio=$('#oficiot').find('option:selected').text();
                var telefono=$('#telefonot').val();;
                var id=data.id;

              var datos=  "<tr>"+                            
                "<td>"+cedula+"</td>"+
                "<td>"+nombre+"</td>"+
                "<td>"+sexo+"</td> "+
                "<td>"+correo+"</td>"+
                "<td>"+oficio+"</td>"+
                "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id="+id+" >"+
                            "<button class='btn btn-primary'  onclick='ver_tutor(this);'  data-id="+id+" id='Ver-tutor'>ver</button>"+
                            "<button class='btn btn-success' onclick=''><i class='fa fa-fw fa-pencil'></i></button>"+
                            "<button class='btn btn-info' data-id="+id+" onclick='eliminar_tutor(this);' ><i class='fa fa-fw fa-trash '></i></button>"+
                           " <i class='fa fa-angle-double-right pull-right' onclick='mostrarT(this);'  data-id="+id+"></i>"+                             
                "</td>"+
                "</td>"+
                "<td id="+id+"a>"+telefono+ "<i class='fa fa-angle-double-right pull-right' onclick='mostrarT(this);' data-id="+id+"></i> </td>"            
              "</tr>"  //Datos a ingresar en la tabla tutor

              var datos_combo='<option  value="'+id+'">'+nombre+'</option>'; //datos a ingresar en el combobox tutores

              $('#tutor').append(datos); // agrega nuevo registro a tabla
              $('#tutores').append(datos_combo); //ingresa valores al combobox tutores

                $("#exito").modal("show"); //abre modal de exito
                               
                $("#crear_tutor").modal("hide"); // cierra modal
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                          $("#exito").modal("hide"); // cierra modal
                          } );

               }
               
            }   
          });//Fin ajax Guardar tutor
          
          }
          else{
            return ValidarCedulaTutor($('#Cedulat').val());
          }
                 
      }
}