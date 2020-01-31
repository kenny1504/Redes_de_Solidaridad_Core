$('#datepickerOferta').datepicker({ //Fecha Matricula
  format: 'yyyy',
  viewMode: "years",
  minViewMode: "years",
  autoclose: true,
}).datepicker("setDate", new Date());

$('#datepickerOfertaEditar').datepicker({ //Fecha Matricula
  format: 'yyyy',
  viewMode: "years",
  minViewMode: "years",
  autoclose: true,
})


$(function() //funcion para buscar dentro del combobox
{
  $('#Docente').select2({width:"80%"}) // agrega el select2 a combobox docentes para buscar 
});

$("#cargar,#cargar2").click(function() { //ajax para cargar datos en el combobox Grados
    $.ajax({
        type: 'POST',
        url: 'cargargrados/oferta', // llamada a ruta para cargar combobox con datos de tabla grados
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#Grado').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de grados
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Grado+'</option>';

              $('#Grado').append(datos);//ingresa valores al combobox
              $('#Grado').val(''); // limpiar el grado
          });
          
      }
    });//Fin ajax combobox Grado

    $.ajax({
        type: 'POST',
        url: 'cargarsecciones/seccion', // llamada a ruta para cargar combobox con datos de tabla secciones
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#Seccion').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de secciones
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Codigo+'</option>';

              $('#Seccion').append(datos);//ingresa valores al combobox
              $('#Seccion').val(''); // limpiar la seccion
          });
          
      }
    });//Fin ajax combobox Seccion

    $.ajax({
        type: 'POST',
        url: 'cargargrupos/grupo', // llamada a ruta para cargar combobox con datos de tabla grupos
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
          $('#Grupo').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de grupos
              //variable para asignarle los valores al combobox
            var datos='<option  value="'+element.id+'">'+element.Grupo+'</option>';

              $('#Grupo').append(datos);//ingresa valores al combobox
              $('#Grupo').val(''); // limpiar el grupo
          });
          
      }
    });//Fin ajax combobox Grupo

    $.ajax({
        type: 'POST',
        url: '/docente/cargardoc/'+ $('#datepickerOferta').val(), // llamada a ruta para cargar combobox con datos de tabla docentes
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          $('#Docente').empty();//limpia el combobox
          var datos="";
          for(var i=0; i<data.length;i++){
            datos+='<option  value="'+data[i].id+'">'+data[i].Nombre+'</option>';
          }
          $('#Docente').append(datos);//ingresa valores al combobox
          $('#Docente').val(''); // limpiar el docente
      }
    });//Fin ajax combobox Docentes
});

function nueva_oferta() { // ajax para guardar en la tabla oferta
  $("#ingresar_oferta").on('submit', function(evt){
    evt.preventDefault();  
  });


  if($('#Descripcion-Oferta').val()!="" && $('#datepickerOferta').val()!="" && $('#Grado').val()!=null && $('#Grupo').val()!=null && $('#Seccion').val()!=null  && $('#Docente').val()!=null) // si el input contiene valores entra 
  {
  $.ajax({
    type: 'POST', 
    url: 'guardar/oferta', // llamada a ruta para guardar la nueva oferta
    data: $('#ingresar_oferta').serialize(), // manda el form donde se encuentra la modal dataType: "JSON", // tipo de respuesta del controlador
    dataType: "JSON", // tipo de respuesta del controlador
    success: function(data){ 
      if ((data==null)) { // si el ajax contiene errores agrega un label indicando el error 
        $('.error').removeClass('hidden');
        $("#Nombre-error").addClass('hidden');
        $('.error').text("Error: "+ data.Nombre); 
      } else { // si no contiene errores agrega el dato a la tabla ofertas
        $('.error').addClass('hidden'); //elimina el mensaje de error
        for(var i=0; i<data.length;i++){
          var datos= "<tr id=" + data[i].idOferta + ">"+"<td>"+data[i].Descripcion+"</td>"+"<td>"+data[i].FechaOferta+"</td>"+"<td>"+data[i].Nombre+"</td>"+"<td>"+data[i].Grado+"</td>"+"<td>"+data[i].Grupo+"</td>"+"<td>"+data[i].Codigo+"</td>"
      + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>"+"<button class='btn btn-success' onclick='editar_Oferta(this);' data-id="+ data[i].idOferta +" data-Nombre="+data[i].Descripcion+"  ><i class=' fa fa-fw fa-pencil'></i></button>"        
      + "<button class='btn btn-info' data-id="+ data[i].idOferta +" onclick='eliminar_oferta(this);'><i class='fa fa-fw fa-trash '></i></button>"                                   
      +"</td>"+"</tr>"; }// variable guarda el valor 
     $('#ofertas').append(datos); // agrega nuevo registro a tabla
    
     $("#exito").modal("show"); //abre modal de exito
     $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
              $("#exito").modal("hide"); // cierra modal
              } );
     }      
  }   
});
$('#Descripcion-Oferta').val(''); // limpiar el input Descripcion Oferta
$('#Grado').val(''); // limpiar el grado
$('#Grupo').val(''); // limpiar el grupo
$('#Seccion').val(''); // limpiar la seccion
$('#Docente').val(''); // limpiar el docente
  }
  else { // si el input esta vacio
    $("#Nombre-error").removeClass('hidden'); //muestra el campo Validacion (validacion-cliente)
    $('.error').addClass('hidden'); // oculta error del servidor(validacion-servidor)
  }
}