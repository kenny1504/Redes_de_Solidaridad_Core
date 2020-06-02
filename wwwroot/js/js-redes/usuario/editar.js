var dat; var usuario;

function editar_Institucion(button) {
     dat = $(button).parents("tr"); //captura toda la fila donde se efectuo el click (Editar)
    var ide = $(button).attr("data-id"); // captura el valor_id_grupo "id" 
    limpiar();
    $('#Editar_Institucion').modal('show'); // abre ventana modal

      $.ajax({ // ajax para cargar datos 
          type: 'POST',
          url: 'Usuarios/Institucion', // ruta 
          data: { id: ide }, // manda id  al controlador, 
          dataType: "JSON",
          success: function(data){ 
        //ciclo para recorrer el arreglo 
            data.forEach(element => {
    
                $('#Id_Institucion').val(element.idUsuario); 
                $('#Institucion2').val(element.nombre);
                $('#Direcccion2').val(element.direccion);
                $('#Usuario2').val(element.usuario);
                $('#Contrasena2').val(element.contraseña);
          });
          
      }   
    });//Fin ajax 
}//fin del metodo


function limpiar(){ // limpia campos

    $('#Id_Institucion').val("");
    $('#Institucion2').val("");
    $('#Direcccion2').val("");
    $('#Usuario2').val("");
    $('#Contrasena2').val("");
};


function editar_Usuario_Docente() {

    if ($('#Institucion2').val() != "" && $('#Direcccion2').val() != "" && $('#Usuario2').val() != "" && $('#Contrasena2').val() != "") {

        usuario = $('#Id_Institucion').val();

    $.ajax({
        type: 'POST',
        url: 'Usuarios/Editar_Institucion', // ruta editar grado
        data: $('#Editar_Instituciones').serialize(), // manda el form donde se encuentra la modal
        dataType: "JSON", // tipo de respuesta del controlador
        success: function (data) {
            if (data == 1) {
                var datos = '<tr>'
                    + '<td>' + $('#Usuario2').val() + '</td>'
                    + '<td>' + $('#Contrasena2').val() + '</td>'
                    + '<td>' + $('#Institucion2').val() + '</td>'
                    + '<td>' + $('#Direcccion2').val() + '</td>'
                    + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + usuario + '" >'
                    + '<a class="btn btn-success " data-id="' + usuario + '"  onclick="editar_Institucion(this);" ><i class=" fa fa-fw fa-pencil"></i></a>'
                    + '<a class="btn btn-info" data-id="' + usuario + '"  onclick="eliminar_Usuario_Institucion(this);"><i class="fa fa-fw fa-trash "></i></a>'
                    + '</tr>';// variable guarda los nuevos valores

                dat.replaceWith(datos); //reemplaza por los nuevos datos
                $("#exito").modal("show"); //abre modal de exito
                $("#exito").fadeTo(2000, 500).slideUp(450, function () {   // cierra la modal despues del tiempo determinado  
                    $("#exito").modal("hide"); // cierra modal

                });
                limpiar();
                $('#Editar_Institucion').modal("hide"); // cierra modal
            }
            else {
                var error = "Error al Actualizar, datos YA EXISTEN"
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
