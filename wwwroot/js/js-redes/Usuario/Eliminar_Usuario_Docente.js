var ide; var dat;

function eliminar_Usuario_Docente(button) {
    dat = $(button).parents("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    ide = $(button).attr("data-id"); // captura el valor_id_grupo "id" del grupo
    $('#eliminar_docente').modal('show'); // abre ventana modal
}


$("#confirmar_Docente").click(function () {

    $.ajax({
        type: 'POST',
        url: 'Usuarios/Eliminar_Usuario_Docente', // ruta eliminar
        data: { id: ide }, // manda id  al controlador, 
        dataType: "JSON",
        success: function (data) {
                dat.remove(); //remueve la fila eliminado 
                $("#exito").modal("show"); //abre modal de exito
                $("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
                    $("#exito").modal("hide"); // cierra modal
                });
        }
    });// 
});