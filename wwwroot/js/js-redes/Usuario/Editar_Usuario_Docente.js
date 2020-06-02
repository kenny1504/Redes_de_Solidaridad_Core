$(function () //funcion para buscar dentro del combobox
{
    $('#Instituciones2').select2({ width: "100%" })// agrega el select2 a combobox parentesco para buscar 
});


var dat; var usuario; var inst;

function editar_Usuario_D(button) {
    dat = $(button).parents("tr"); //captura toda la fila donde se efectuo el click (Editar)
    var ide = $(button).attr("data-id"); // captura el valor_id_grupo "id" 
    limpiar();
    $('#editar_Docente').modal('show'); // abre ventana modal

    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'Usuarios/Instituciones', // llamada a ruta para cargar combobox con datos de tabla tutores
        dataType: "JSON", // tipo de respuesta del controlador
        success: function (data) {
            $('#Instituciones').empty();
            //ciclo para recorrer el arreglo 
            var datos = "<option value='' disabled selected>Instituciones</option>";
            data.forEach(element => {
                //variable para asignarle los valores al combobox
                datos += '<option  value="' + element.id + '">' + element.nombre + '</option>';
            });
            $('#Instituciones2').append(datos); //ingresa valores al combobox
        }
    });//Fin ajax combobox 




    $.ajax({ // ajax para cargar datos 
        type: 'POST',
        url: 'Usuarios/Docente', // ruta 
        data: { id: ide }, // manda id  al controlador, 
        dataType: "JSON",
        success: function (data) {
            //ciclo para recorrer el arreglo 
            data.forEach(element => {

                $('#Id_UsuarioD').val(element.idUsuario);
                $('#CedulaU2').val(element.cedula);
                $('#UsuarioU2').val(element.usuario);
                $('#ContrasenaU2').val(element.contrasena);
                $('#Instituciones2').select2("val", element.institucion);
                inst = $('#Instituciones2').val();
            });

        }
    });//Fin ajax 
}//fin del metodo


function limpiar() { // limpia campos

    $('#Id_UsuarioD').val("");
    $('#CedulaU2').val("");
    $('#UsuarioU2').val("");
    $('#ContrasenaU2').val("");
    $('#Instituciones2').val(null);
};


function editar_Usuario_Docente() {

    if ($('#CedulaU2').val() != "" && $('#UsuarioU2').val() != "" && $('#ContrasenaU2').val() != "" && $('#Instituciones2').val() != null) {

        $('#Instituciones2').select2("val", inst);
        usuario = $('#Id_UsuarioD').val();

        $.ajax({
            type: 'POST',
            url: 'Usuarios/Editar_Usuario_Docente', // ruta editar grado
            data: $('#editar_Docentes').serialize(), // manda el form donde se encuentra la modal
            dataType: "JSON", // tipo de respuesta del controlador
            success: function (data) {
                if (data != -1) {
                    if (data != 0) {//si retorna 0 es porque el numero de cedula no pertene a un docente de la institucion
                        data.forEach(element => {   //recorre arreglo
                            var datos = '<tr>'
                                + '<td>' + element.user + '</td>'
                                + '<td>' + element.pass + '</td>'
                                + '<td>' + element.nombbre + '</td>'
                                + '<td>' + element.inst + '</td>'
                                + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + usuario + '" >'
                                + '<a class="btn btn-primary" onclick="ver_estudiante(this);" data-id="' + usuario + '" id="Ver-estudiante">ver</a>'
                                + '<a class="btn btn-success" data-id="' + usuario + '" onclick="editar_Usuario_D(this)" ><i class=" fa fa-fw fa-pencil"></i></a>'
                                + '<a class="btn btn-info" data-id="' + usuario + '" onclick="eliminar_Usuario_Docente(this)"><i class="fa fa-fw fa-trash "></i></a>'
                                + '</tr>';// variable guarda los nuevos valores

                            dat.replaceWith(datos); //reemplaza por los nuevos datos
                        })
                        $("#exito").modal("show"); //abre modal de exito
                        $("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
                            $("#exito").modal("hide"); // cierra modal

                        });
                        limpiar();
                        $('#editar_Docente').modal("hide"); // cierra modal
                    }
                    else { // error el usuario que ingreso ya existe en esta institucion

                        var error = "Error,La cedula que ingreso no pertence a un docente de la institucion seleccionada"
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
            }
        });
    }
}
