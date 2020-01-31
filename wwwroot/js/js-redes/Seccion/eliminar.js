var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-seccion").click(function() { // ajax para eliminar una seccion
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el valor_id_seccion "id" de la seccion
    $('#eliminar_Seccion').modal('show'); // abre ventana modal
    $('#valor_id_seccion').val(iden);   //manda valor_id_seccion "id" a ventana modal
}); 

function eliminar_seccion(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_seccion "id" de la seccion
    $('#eliminar_Seccion').modal('show'); // abre ventana modal
    $('#valor_id_seccion').val(ide);   //manda valor_id_seccion "id" a ventana modal
}
    $("#confirmar_eliminar_seccion").click(function() {
        
         $.ajax({
                    type: 'POST',
                    url: 'eliminar/seccion', // ruta eliminar seccion
                    data: $('#delete_seccion').serialize(), // manda el form donde se encuentra la modal seccion
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
                    var error="Error al eliminar la Seccion, debido a que se ha asignada a una oferta"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }    
                }
             });// 
    });
