var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-oficio").click(function() { // ajax para eliminar un oficio
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el valor_id_oficio "id" del oficio
    $('#eliminar_Oficio').modal('show'); // abre ventana modal
    $('#valor_id_oficio').val(iden);   //manda valor_id_oficio "id" a ventana modal
}); 

function eliminar_oficio(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_oficio "id" de la oficio
    $('#eliminar_Oficio').modal('show'); // abre ventana modal
    $('#valor_id_oficio').val(ide);   //manda valor_id_oficio "id" a ventana modal
}

    $("#confirmar_eliminar_oficio").click(function() {
        
         $.ajax({
                    type: 'POST',
                    url: 'eliminar/oficio', // ruta eliminar oficio
                    data: $('#delete_oficio').serialize(), // manda el form donde se encuentra la modal oficio
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
                    var error="Error al eliminar el Oficio, se encuentra en uso"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }    
                }
             });// 
});