var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-grado").click(function() { // ajax para eliminar un grado
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el id_grado "id" del grado
    $('#eliminar_Grado').modal('show'); // abre ventana modal
    $('#valor_id_grado').val(iden);   //manda valor_id_grado "id" a ventana modal
}); 

function eliminar_grado(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_grado "id" de la grado
    $('#eliminar_Grado').modal('show'); // abre ventana modal
    $('#valor_id_grado').val(ide);   //manda valor_id_grado "id" a ventana modal
}

$("#confirmar_eliminar_grado").click(function() {
        
    $.ajax({
               type: 'POST',
               url: 'eliminar/grado', // ruta eliminar materia
               data: $('#delete_grado').serialize(), // manda el form donde se encuentra la modal grado
               dataType: "JSON",
               success: function(data){ 
                   if(data==1)
                   {
                       dat.remove(); //remueve la fila eliminado 
                       $("#exito").modal("show"); //abre modal de exito
                       $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                                   $("#exito").modal("hide"); // cierra modal
                                   } );
                   }
                   else if(data==2)
                   {
                    var error="Error imposible eliminar Grado, debido a que se han asignado en una Oferta"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }  
                   else if(data==3)
                   {
                    var error="Error imposible eliminar Grado, debido a que se han asignado Materias"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }  
                   else 
                   {
                    var error="Error imposible eliminar Grado, debido a que se han asignado Materias y en una Oferta"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }                  
              
               }
        });// 

});