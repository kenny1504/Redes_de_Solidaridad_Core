var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
$(".eliminar-turno").click(function() { // ajax para eliminar un turno
    dat = $(this).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var iden=$(this).attr("data-id"); // captura el valor_id_turno "id" del turno
    $('#eliminar_Turno').modal('show'); // abre ventana modal
    $('#valor_id_turno').val(iden);   //manda valor_id_turno "id" a ventana modal
}); 

function eliminar_turno(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    var ide=$(button).attr("data-id"); // captura el valor_id_turno "id" del turno
    $('#eliminar_Turno').modal('show'); // abre ventana modal
    $('#valor_id_turno').val(ide);   //manda valor_id_turno "id" a ventana modal

}

    $("#confirmar_eliminar_turno").click(function() {
        
         $.ajax({
                    type: 'POST',
                    url: 'eliminar/turno', // ruta eliminar turno
                    data: $('#delete_turno').serialize(), // manda el form donde se encuentra la modal turno
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
                    var error="Error al eliminar el Turno, debido a que se ha asignada en una Matricula"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                        } );
                   }    
                }
             });// 
});