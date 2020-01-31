$("#m,#m2").click(function(){ // agrega la clase hidden para ocultar label error
    $('.error').addClass('hidden');
    $("#Nombre_grupo-error").addClass('hidden');
  
  });
  
  $("#grupo_guardar").click(function() { //ajax para ingresar grupo
  
    if($('input[name=Nombre_grupo]').val()!="") // si el input contiene valores entra 
    {
    $.ajax({
      type: 'POST',
      url: 'agregar/grupo', //llamada a la ruta ingresar grupo
      data: $('#ingresar_grupo').serialize(), // manda el form donde se encuentra la modal grupo
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ //agregar el nuevo ingreso a la tabla
                if ((data.msg!=true)) { // si el ajax contiene errores agrega un label indicando el error 
                  $('.error').removeClass('hidden');
                  $("#Nombre_grupo-error").addClass('hidden');
                  $('.error').text("Error: El "+ data.Grupo); 
                } else {
                var datos=  "<tr id=" + data.id + ">"+"<td>"+data.Grupo+"</td>"
                + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>"+"<button class='btn btn-success' data-id="+ data.id +"  onclick='editar_Grupo(this);' ><i class=' fa fa-fw fa-pencil'></i></button>"
                + "<button class='btn btn-info' data-id="+ data.id +" onclick='eliminar_grupo(this);'><i class='fa fa-fw fa-trash '></i></button>"                                   
                +"</td>"+"</tr>"; // variable guarda el valor 
              $('#grupos').append(datos); // agrega nuevo registro a tabla
                
              $("#exito").modal("show"); //abre modal de exito
              $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                        $("#exito").modal("hide"); // cierra modal
                        } );
              }
       } });
    $('#Nombre_grupo').val(''); // limpiar el input Nombre
    }
    else { // si el input esta vacio
      $("#Nombre-error").removeClass('hidden'); //muestra el campo Validacion (validacion-cliente)
      $('.error').addClass('hidden'); // oculta error del servidor(validacion-servidor)
    }
  }); //fin del ajax