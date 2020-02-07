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
    url = "Usuarios/Autenticacion"; //URL

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
          alert('Error adding / update data'); // si el ajax contiene errores se muestran aqui
      }
  });
}


function login2(){ // Si quiere ingresar con el mismo usuario
  url = "usuario/autenticacion"; //URL

  $.ajax({
      url : url, // URL del controlador
      type: "POST", //tipo de metodo 
      data: $('#login2').serialize(), // pasamos el id del formulario para poder usar campos en el controlador
      success: function(data)
      {
        var usuario=data; // varible que recive lo que retorna el controlador
          if(usuario==1){ // si la variable es 1 entonces el usuario existe
             inicio();  //llama a la vista Inicio
          }
          else // si el usuario no existe entonces muestra el mensaje CREDENCIALES NO VALIDAS
          {
            $('#error').removeClass('hidden');
          }
      },
      error: function (jqXHR, textStatus, errorThrown) // si el ajax presenta errores entonces muestra en el alert
      {
          alert('Error adding / update data'); // si el ajax contiene errores se muestran aqui
      }
  });
}

  function inicio(){ //llamado a la vista Inicio
    window.location.href='inicio';
  }
   
  