var ide; var dat;

function eliminar_Usuario_Institucion(button) {
    dat = $(button).parents("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    ide = $(button).attr("data-id"); // captura el valor_id_grupo "id" del grupo
    $('#eliminar_Institucion').modal('show'); // abre ventana modal
}


$("#confirmar_Institucion").click(function () {

    $.ajax({
        type: 'POST',
        url: 'Usuarios/Eliminar_Institucion', // ruta eliminar
        data:{ id: ide }, // manda id  al controlador, 
        dataType: "JSON",
        success: function (data) {
            if (data == 1) {
                dat.remove(); //remueve la fila eliminado 
                $("#exito").modal("show"); //abre modal de exito
                $("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
                    $("#exito").modal("hide"); // cierra modal
                });
            }
            else {
                var error = "Error al eliminar, la Institucion ya posee otros Datos"
                $('#mensaje').text(error);   //manda el error a la modal
                $("#Mensaje-error").modal("show"); //abre modal de error
                $("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
                    $("#Mensaje-error").modal("hide"); // cierra modal error
                });
            }
        }
    });// 
});