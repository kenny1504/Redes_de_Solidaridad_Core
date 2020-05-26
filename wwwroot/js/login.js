let prism = document.querySelector(".rec-prism");

function showSignup(){
  prism.style.transform = "translateZ(-100px) rotateY( -90deg)";
}
function showLogin(){
  prism.style.transform = "translateZ(-100px)";
}
function showForgotPassword(){
  prism.style.transform = "translateZ(-100px) rotateY( -180deg)";
}

function showSubscribe(){
  prism.style.transform = "translateZ(-100px) rotateX( -90deg)";
}

function showContactUs(){
  prism.style.transform = "translateZ(-100px) rotateY( 90deg)";
}

function showThankYou(){
    url = "Autenticar/Autenticacion"; //URL

  $.ajax({
      url : url, // URL del controlador
      type: "POST", //tipo de metodo 
      data: $('#login').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
      success: function(data)
      {
        var usuario=data; // varible que recive lo que retorna el controlador
          if(usuario==1){ // si la variable es 1 entonces el usuario existe
              prism.style.transform = "translateZ(-100px) rotateX( 90deg)"; // muetsra el mensaje de BIENVENIDO
             window.setTimeout("inicio()",2500);  //llama a la vista Inicio
          }
      else // si el usuario no existe entonces muestra el mensaje CREDENCIALES NO VALIDAS
      {
        showSignup();// si el controlador retorna 0 envia mensaje de ERROR
      }

      },
      error: function (jqXHR, textStatus, errorThrown) // si el ajax presenta errores entonces muestra en el alert
      {
          alert('Error Conexion a base de datos / Error 5000'); // si el ajax contiene errores se muestran aqui
      }
  });
}

  function inicio(){ //llamado a la vista Inicio
    window.location.href='inicio';
  }


function Registro() { //ajax para Registrar una Institucion
    $.ajax({
        url: "Usuarios/Registro", // URL del controlador
        type: "POST", //tipo de metodo 
        data: $('#registrarse').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
        success: function (data) {
            var usuario = data; // varible que recive lo que retorna el controlador
            if (usuario == 1) { // si la variable es 1 entonces el usuario existe
                alert('Guardado con exito');
                window.setTimeout("showLogin()", 1000);  //llama a la vista login
            }
            else // si el usuario no existe entonces muestra el mensaje CREDENCIALES NO VALIDAS
            {
                alert('Favor revise los datos ingresador, Datos ya existen');// si el controlador retorna -1 envia mensaje de ERROR
            }

        },
        error: function (jqXHR, textStatus, errorThrown) // si el ajax presenta errores entonces muestra en el alert
        {
            alert('Error Conexion a base de datos / Error 5000'); // si el ajax contiene errores se muestran aqui
        }
    });
  }
