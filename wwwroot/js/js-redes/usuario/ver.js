function ver_usuario(button) { //metodo para mostrar datos de usuario en ventana modal
    var contraseña = $(button).attr("data-password");//obtiene el id de la materia
    var NombreUsuario = $(button).attr("data-user");//obtiene el id de la materia
    var cedula = $(button).attr("data-cedula");//obtiene el id de la materia
    var nombre_u = $(button).attr("data-Nombre");//obtiene el id de la materia
    var rol_user = $(button).attr("data-rol");//obtiene el id de la materia
    var vencimiento_user = $(button).attr("data-vencimiento");//obtiene el id de la materia

    $("#perfil_usuario").modal("show"); //abre modal ver usuario
    //envia datos del usuario a ventana modal
    $('#nombre_u').text(nombre_u);
    $('#rol_user').text(rol_user);
    $('#nombre_user').text(NombreUsuario);
    $('#Contraseña_user').text(contraseña);
    $('#cedula_user').text(cedula);
    $('#vencimiento_user').text(vencimiento_user);
};