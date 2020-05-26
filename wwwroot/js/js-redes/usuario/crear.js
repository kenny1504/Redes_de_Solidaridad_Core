
function ingresar_usuario()
{   
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


function Registro() { //ajax para Registrar una Institucion
    $.ajax({
        url: "Usuarios/Registro", // URL del controlador
        type: "POST", //tipo de metodo 
        data: $('#registrarse').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
        success: function (data) {
            var usuario = data; // varible que recive lo que retorna el controlador
            if (usuario == 1) { // si la variable es 1 entonces el usuario existe
                $("#exito").modal("show"); //abre modal de exito
            }
            else // si el usuario no existe entonces muestra el mensaje CREDENCIALES NO VALIDAS
            {
                var error = "Favor revise los datos ingresador, Datos ya existen"
                $('#mensaje').text(error);   //manda el error a la modal
                $("#Mensaje-error").modal("show"); //abre modal de error
            }

        },
        error: function (jqXHR, textStatus, errorThrown) // si el ajax presenta errores entonces muestra en el alert
        {
            alert('Error Conexion a base de datos / Error 5000'); // si el ajax contiene errores se muestran aqui
        }
    });
}