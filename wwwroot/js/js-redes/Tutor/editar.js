var dat; //variable global que guarda el dato "tr" (Fila a editar)
function editar_Tutor(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Editar)
    $('#editar_Tutor').modal('show'); // abre ventana modal
    var ide=$(button).attr("data-id");//obtiene el id del tutor

    
    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'cargar/oficio', // llamada a ruta para cargar combobox oficio
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#Oficio_Editar1').empty();
        //ciclo para recorrer el arreglo de oficios
          data.forEach(element => {
              //variable para asignarle los valores al combobox
             var datos='<option  value="'+element.id+'">'+element.Nombre+'</option>'; 
  
              $('#Oficio_Editar1').append(datos); //ingresa valores al combobox
          });
          
      }   
    });//Fin ajax combobox oficios 
    
    
    $.ajax({
        type: 'POST',
        url: 'tutor/cargar/'+ide,
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          for(var a=0; a<data.length;a++){
              var id=data[a].idTutor;
              var id2=data[a].idPersona;
              var cedula=data[a].Cedula;
              var nombre=data[a].Nombre;
              var apellido1=data[a].Apellido1;
              var apellido2=data[a].Apellido2;
              var sexo=data[a].Sexo;
              var direccion=data[a].Direccion;
              var correo=data[a].Correo;
              var telefono=data[a].Telefono;
              var fecha=data[a].FechaNacimiento;
              var idoficio=data[a].idOficio;
          }
          $('#idtutor_editar').val(id);
          $('#idpersona_tutor ').val(id2);
          $('#Cedula_Tutor_Editar').val(cedula);
          $('#Nombre_Tutor_Editar').val(nombre);
          $('#Apellido1_Tutor_Editar').val(apellido1);
          $('#Apellido2_Tutor_Editar').val(apellido2);
          $('#Sexo_Tutor_Editar').val(sexo);
          $('#Telefono_Tutor_Editar').val(telefono);
          $('#Oficio_Editar1').val(idoficio);
          $('#Correo_Tutor_Editar').val(correo); 
          $('#Direccion_Tutor_Editar').val(direccion); 
          $('#datepickerTutorEditar').val(fecha); 

           
      }
    });//Fin ajax 
    
}
function editar_confirmar_Tutor() {

    $("#editar_tutor").on('submit', function(evt){
        evt.preventDefault();  
      });
    //Verifica que le formulario no este vacio
    if($('#Cedula_Tutor_Editar').val().length==16 && $('#Nombre_Tutor_Editar').val()!="" && $('#Apellido1_Tutor_Editar').val()!="" && $('#Telefono_Tutor_Editar').val()!="" && $('#Sexo_Tutor_Editar').val()!=null && $('#Oficio_Editar1').val()!=null 
    && $('#Correo_Tutor_Editar').val()!=null && $('#Direccion_Tutor_Editar').val()!="" && $('#datepickerTutorEditar').val()!="")
  {
      if($("#Telefono_Tutor_Editar").val().length==8 && ValidarCedulaTutorEditar($('#Cedula_Tutor_Editar').val())==true) //verifica que el input contenga 8 valores 
      {
    $.ajax({
                
        type: 'POST',
        url: 'actualizar/tutor', // ruta editar oferta
        data: $('#editar_tutor').serialize(), // manda el form donde se encuentra la modal oferta
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){
        if ((data==1)) // si se actualiza todo correcto
        { 
            //capturamos datos
            var id=$('#idtutor_editar').val();
            var cedula=$('#Cedula_Tutor_Editar').val();
            var nombre=$('#Nombre_Tutor_Editar').val()+" "+$('#Apellido1_Tutor_Editar').val()+" "+$('#Apellido2_Tutor_Editar').val();            
            var sexo=$('#Sexo_Tutor_Editar').val();
            var telefono=$('#Telefono_Tutor_Editar').val();
            var correo=$('#Correo_Tutor_Editar').val();
            var oficio=$('#Oficio_Editar1').find('option:selected').text();
            
            var datos=  "<tr>"+                            
                  "<td>"+cedula+"</td>"+
                  "<td>"+nombre+"</td>"+
                  "<td>"+sexo+"</td> "+
                  "<td>"+correo+"</td>"+
                  "<td>"+oficio+"</td> "+
                  "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id="+id+" >"+
                              "<button class='btn btn-primary'  onclick='ver_tutor(this);' data-id="+id+" id='Ver-tutor'>ver</button>"+
                              "<button class='btn btn-success' onclick='editar_Tutor(this);' data-id="+id+"><i class='fa fa-fw fa-pencil'></i></button>"+
                              "<button class='btn btn-info' onclick='eliminar_tutor(this);' data-id="+id+"><i class='fa fa-fw fa-trash '></i></button>"+
                             " <i class='fa fa-angle-double-right pull-right' onclick='mostrarT(this);'  data-id="+id+"></i>"+                             
                  "</td>"+
                  "</td>"+
                  "<td id="+id+"a>"+telefono+ "<i class='fa fa-angle-double-right pull-right' onclick='mostrarT(this);' data-id="+id+"></i> </td>"            
                "</tr>"  //Datos a ingresar en la tabla docentes

           
                dat.replaceWith(datos); //reemplaza por los nuevos datos
                $("#exito").modal("show"); //abre modal de exito
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                    $("#exito").modal("hide"); // cierra modal                                           
                    } );
                $("#editar_Tutor").modal("hide"); // cierra modal
        }
        else 
            {
                    var error="Error Al Actualizar, verifique datos ingresado"
                    $('#mensaje').text(error);   //manda el error a la modal
                   $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                       $("#Mensaje-error").modal("hide"); // cierra modal error
                       } );
            }     
        }
        });
    }
    else{
        return ValidarCedulaTutorEditar($('#Cedula_Tutor_Editar').val());
      }
}
    }