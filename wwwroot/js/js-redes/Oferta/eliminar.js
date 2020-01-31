var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-oferta").click(function() { // ajax para eliminar una oferta
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el id_oferta "id" de la oferta
    $('#eliminar_Oferta').modal('show'); // abre ventana modal
    $('#valor_id_oferta').val(iden);   //manda valor_id_oferta "id" a ventana modal
}); 

function eliminar_oferta(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_oferta "id" de la oferta
    $('#eliminar_Oferta').modal('show'); // abre ventana modal
    $('#valor_id_oferta').val(ide);   //manda valor_id_oferta "id" a ventana modal
}

$("#confirmar_eliminar_oferta").click(function() {
        
    $.ajax({
               type: 'POST',
               url: 'eliminar/oferta', // ruta eliminar oferta
               data: $('#delete_oferta').serialize(), // manda el form donde se encuentra la modal oferta
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
                    var error="Error imposible eliminar Oferta, Actualmente se encuentra en Matricula"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }                  
              
               }
        });// 

});