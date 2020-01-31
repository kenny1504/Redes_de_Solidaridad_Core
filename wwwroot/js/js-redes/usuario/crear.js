
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
