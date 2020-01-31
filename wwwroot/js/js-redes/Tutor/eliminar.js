var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-tutor").click(function() { // ajax para eliminar un tutor
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el id_docente "id" del tutor
    $('#eliminar_Tutor').modal('show'); // abre ventana modal
    $('#valor_id_tutor').val(iden);   //manda valor_id_tutor "id" a ventana modal
}); 

function eliminar_tutor(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_tutor "id" del tutor
    $('#eliminar_Tutor').modal('show'); // abre ventana modal
    $('#valor_id_tutor').val(ide);   //manda valor_id_tutor "id" a ventana modal
}

$("#confirmar_eliminar_tutor").click(function() {
        
    $.ajax({
               type: 'POST',
               url: 'eliminar/tutor', // ruta eliminar tutor
               data: $('#delete_tutor').serialize(), // manda el form donde se encuentra la modal tutor
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
                   else 
                   {
                    var error="Error imposible eliminar Tutor, a sido asignado a un Estudiante"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }                  
              
               }
        });// 

});