
function ingresar_usuario()
{   

    limpiar();
    $('#crear_usuario').modal('show'); // abre ventana modal

    $.ajax({ // ajax para cargar datos en el combobox
        type: 'POST',
        url: 'roles', // llamada a ruta para cargar combobox con datos de tabla materia
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#roles').empty();
        //ciclo para recorrer el arreglo de roles
          data.forEach(element => {
              //variable para asignarle los valores al combobox
             var datos='<option  value="'+element.Id_FuncionAcceso+'">'+element.Descripcion+'</option>'; 
  
              $('#roles').append(datos); //ingresa valores al combobox
          });
          
      }   
    });//Fin ajax combobox roles
}//fin del metodo Ingresar_usuario


function ingresar_Institucion () {//ajax para Registrar una Institucion

    if ($('#Institucion').val() != "" && $('#Direcccion').val() != "" && $('#Usuario').val() != "" && $('#Contrasena').val() != "") {
 
        var tabla = $.trim($("#Usuarios").html());

        $.ajax({
            url: "Usuarios/Registro", // URL del controlador
            type: "POST", //tipo de metodo 
            data: $('#ingresar_Instituciones').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
            success: function (data) {
                var usuario = data; // varible que recive lo que retorna el controlador
                if (usuario != -1) { // si la variable es 1 entonces el usuario existe

                    if (tabla != "") //si la tabla no esta vacia, inserta Datos
                    {
                        var html = '<tr>'
                            + '<td>' + $('#Usuario').val() + '</td>'
                            + '<td>' + $('#Contrasena').val() + '</td>'
                            + '<td>' + $('#Institucion').val() + '</td>'
                            + '<td>' + $('#Direcccion').val() + '</td>'
                            + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + usuario + '" >'
                            + '<a class="btn btn-success " data-id="' + usuario + '"  onclick="editar_Institucion(this);" ><i class=" fa fa-fw fa-pencil"></i></a>'
                            + '<a class="btn btn-info" data-id="' + usuario + '"  onclick="eliminar_Usuario_Institucion(this);"><i class="fa fa-fw fa-trash "></i></a>'
                            + '</tr>';
                        $('#Usuarios').append(html); //insertamos datos en tabla
                    }

                    $("#exito").modal("show"); //abre modal de exito
                    $("#ingresar_Institucion").modal("hide"); // cierra modal error
                    $("#exito").fadeTo(2900, 500).slideUp(450, function () {// cierra la modal despues del tiempo determinado  
                        $("#exito").modal("hide");
                    }); // cierra modal 
                }
                else // si el usuario no existe entonces muestra el mensaje CREDENCIALES NO VALIDAS
                {
                    var error = "Favor revise los datos ingresador, Datos ya existen"
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

    $('#Usuario').val("");
    $('#Contrasena').val("");
    $('#Institucion').val("");
    $('#Direcccion').val(""); 
}
