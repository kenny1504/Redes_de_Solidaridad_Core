
function ingresar_Usuario_D() {

    limpiar();
    $('#ingresar_Docente2').modal('show'); // abre ventana modal

    var intitucion = $("#id_u").text();

    $('#Instituciones').val(intitucion); //ingresa id de la intitucion loqueada

}

function ingresar_usuario_docente() { //ajax para Registrar una Institucion

    if ($('#CedulaU').val() != "" && $('#UsuarioU').val() != "" && $('#ContrasenaU').val() != "" && $('#Instituciones').val() != null) {

        var tabla = $.trim($("#Usuarios").html());

        $.ajax({
            url: "Usuarios/Registro_Usuario_Docente", // URL del controlador
            type: "POST", //tipo de metodo 
            data: $('#ingresar_Docentes2').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
            success: function (data) {
                var usuario = data; // varible que recive lo que retorna el controlador
                if (usuario != -1) { // si la variable es 1 entonces el usuario existe

                    if (usuario != 0) {//si retorna 0 es porque el numero de cedula no pertene a un docente de la institucion

                        if (tabla != "") //si la tabla no esta vacia, inserta Datos
                        {
                            data.forEach(element => {   //recorre arreglo
                                var html = '<tbody><tr>'
                                    + '<td>' + element.user + '</td>'
                                    + '<td>' + element.pass + '</td>'
                                    + '<td>' + element.nombbre + '</td>'
                                    + '<td>' + element.inst + '</td>'
                                    + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + element.id + '" >'
                                    + '<a class="btn btn-primary" onclick="ver_estudiante(this);" data-id="' + element.id + '" id="Ver-estudiante">ver</a>'
                                    + '<a class="btn btn-success" data-id="' + element.id + '" onclick="editar_Usuario_D(this)" ><i class=" fa fa-fw fa-pencil"></i></a>'
                                    + '<a class="btn btn-info" data-id="' + element.id + '" onclick="eliminar_Usuario_Docente(this)"><i class="fa fa-fw fa-trash "></i></a>'
                                    + '</tr>'
                                    + '</tbody>';
                                $('#Usuarios').append(html); //insertamos datos en tabla
                            })
                        }

                        $("#exito").modal("show"); //abre modal de exito
                        $("#ingresar_Docente2").modal("hide"); // cierra modal error
                        $("#exito").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
                            $("#exito").modal("hide");
                        }); // cierra modal 
                    }
                    else { // error el usuario que ingreso ya existe en esta institucion

                        var error = "Error,La cedula que ingreso no pertence a un docente de esta institucion "
                        $('#mensaje').text(error);   //manda el error a la modal
                        $("#Mensaje-error").modal("show"); //abre modal de error
                        $("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
                            $("#Mensaje-error").modal("hide"); // cierra modal error
                        });
                    }

                }
                else // si el usuario no existe 
                {
                    var error = "Error, Favor Verifique los datos, el nombre Usuario que ingreso Ya existe"
                    $('#mensaje').text(error);   //manda el error a la modal
                    $("#Mensaje-error").modal("show"); //abre modal de error
                    $("#Mensaje-error").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
                        $("#Mensaje-error").modal("hide"); // cierra modal error
                    });
                }

            },
            error: function (jqXHR, textStatus, errorThrown) // si el ajax presenta errores entonces muestra en el alert
            {
                alert('Error Conexion a base de datos / Error 5000'); // si el ajax contiene errores se muestran aqui
            }
        });
    }
}
function limpiar() {

    $('#CedulaU').val("");
    $('#UsuarioU').val("");
    $('#ContrasenaU').val("");
    $('#Instituciones').val("");
}
