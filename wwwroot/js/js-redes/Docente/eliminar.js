var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-docente").click(function() { // ajax para eliminar un docente
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el id_docente "id" del docente
    $('#eliminar_Docente').modal('show'); // abre ventana modal
    $('#valor_id_docente').val(iden);   //manda valor_id_docente "id" a ventana modal
}); 

function eliminar_docente(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_docente "id" del docente
    $('#eliminar_Docente').modal('show'); // abre ventana modal
    $('#valor_id_docente').val(ide);   //manda valor_id_docente "id" a ventana modal
}

$("#confirmar_eliminar_docente").click(function() {
        
    $.ajax({
               type: 'POST',
               url: 'eliminar/docente', // ruta eliminar docente
               data: $('#delete_docente').serialize(), // manda el form donde se encuentra la modal docente
               dataType: "JSON",
               success: function(data){ 
                   if(data==1)
                   {
                       
                       $("#exito").modal("show"); //abre modal de exito
                       $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                                   $("#exito").modal("hide"); // cierra modal
                                   } );
                   } 
                   else 
                   {
                    var error="Error imposible eliminar Docente"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }                  
              
               }
        });// 

});