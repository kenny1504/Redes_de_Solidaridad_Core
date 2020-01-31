
$('#datepickerDocente').datepicker({ //mostrar Datepicker
  format: 'yyyy-mm-dd',
  autoclose: true
})
$('#datepickerDocenteEditar').datepicker({ //mostrar Datepicker editar
  format: 'yyyy-mm-dd',
  autoclose: true
})
function ingresar_docente()
{   
    $('#crear_docente').modal('show'); // abre ventana modal
}//fin del metodo Ingresar_usuario



function nuevo_docente()
{ 

    //este codigo cancela el evento submit en el form donde esta la modal ingresar_Docente
      $("#ingresar_Docente").on('submit', function(evt){
        evt.preventDefault();  
      });
      //Verifica que le formulario no este vacio
      if($('#Cedula_Docente').val().length==16 && $('#Nombre_Docente').val()!="" && $('#Apellido1_Docente').val()!="" && $('#Telefono_Docente').val()!="" && $('#Sexo_Docente').val()!=null && $('#Estado').val()!=null 
        && $('#Correo_Docente').val()!=null && $('#Direccion_Docente').val()!="" && $('#datepickerDocente').val()!="")
      {
          if($("#Telefono_Docente").val().length==8 && ValidarCedulaDocente($('#Cedula_Docente').val())==true) //verifica que el input contenga 8 valores 
          {

            $.ajax({ // ajax para guardar docente
              type: 'POST',
              url: 'docente/agregar', // llamada a ruta para guardar un nuevo docente
              data: $('#ingresar_Docente').serialize(), // manda el form donde se encuentra la modal ingresar_Docente
              dataType: "JSON", // tipo de respuesta del controlador
              success: function(data){ 
               if(data==0) // muestra mensaje de error
               {
                  var error="El docente ya existe, favor reingresar Cedula"
                  $('#mensaje').text(error);   //manda el error a la modal
                  $("#Mensaje-error").modal("show"); //abre modal de error
                  $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                      $("#Mensaje-error").modal("hide"); // cierra modal error
                      } );

               }
               else
               {
                 ///// Capturamos datos a insertar en la tabla docente
                 var cedula=$('#Cedula_Docente').val();
                 var nombre=$('#Nombre_Docente').val()+" "+$('#Apellido1_Docente').val()+" "+$('#Apellido2_Docente').val();
                 var sexo=$('#Sexo_Docente').val();
                 var correo=$('#Correo_Docente').val();
                 var telefono=$('#Telefono_Docente').val();
                 var id=data.id;
                 var estado=0;
                 if($('#Estado').val()==1)
                 {
                 estado="Activo";
                 }
                 else
                 {
                     estado="Inactivo";
                 }

                var datos=  "<tr>"+                            
                  "<td>"+cedula+"</td>"+
                  "<td>"+nombre+"</td>"+
                  "<td>"+estado+"</td> "+
                  "<td>"+sexo+"</td> "+
                  "<td>"+correo+"</td>"+
                  "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id="+id+" >"+
                              "<button class='btn btn-primary'  onclick='ver_docente(this);'  data-id="+id+" id='Ver-docente'>ver</button>"+
                              "<button class='btn btn-success' onclick='editar_Docente(this);' data-id="+id+"><i class='fa fa-fw fa-pencil'></i></button>"+
                              "<button class='btn btn-info' onclick='eliminar_oferta(this);' data-id="+id+"><i class='fa fa-fw fa-trash '></i></button>"+
                             " <i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);'  data-id="+id+"></i>"+                             
                  "</td>"+
                  "</td>"+
                  "<td id="+id+"a>"+telefono+ "<i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);' data-id="+id+"></i> </td>"            
                "</tr>"  //Datos a ingresar en la tabla docente

                $('#docentes').append(datos); // agrega nuevo registro a tabla
                  
                $("#exito").modal("show"); //abre modal de exito
                    //limpia campos
                    $('#Cedula_Docente').val("");
                    $('#Nombre_Docente').val("");
                    $('#Apellido1_Docente').val("");
                    $('#Apellido2_Docente').val("");
                    $('#Sexo_Docente').val(null);
                    $('#Correo_Docente').val("");
                    $('#Telefono_Docente').val(null);
                $("#crear_docente").modal("hide"); // cierra modal
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                          $("#exito").modal("hide"); // cierra modal
                          } );

               }
              
            }   
          });//Fin ajax Guardar docente
          }
          else{
            return ValidarCedulaDocente($('#Cedula_Docente').val());
          }
                 
      }
}