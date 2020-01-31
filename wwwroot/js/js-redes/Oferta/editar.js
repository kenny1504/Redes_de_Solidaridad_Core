var dat; var docente //variable global que guarda el dato "tr" (Fila a editar)

$(function() //funcion para buscar dentro del combobox
{
  $('#Docente1').select2({width:"80%"}) // agrega el select2 a combobox docentes para buscar 
});

function editar_Oferta(button)
{

  
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Editar)
    $('#editar_Oferta').modal('show'); // abre ventana modal
    var ide=$(button).attr("data-id");//obtiene el id de la oferta
    
        $.ajax({
            type: 'POST',
            url: 'cargargrados/oferta', // llamada a ruta para cargar combobox con datos de tabla grados
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
            
              $('#Grado1').empty();//limpia el combobox
              data.forEach(element => { //ciclo para recorrer el arreglo de grados
                  //variable para asignarle los valores al combobox
                var datos='<option  value="'+element.id+'">'+element.Grado+'</option>';
    
                  $('#Grado1').append(datos);//ingresa valores al combobox
              });
              
          }
        });//Fin ajax combobox Grado
    
        $.ajax({
            type: 'POST',
            url: 'cargarsecciones/seccion', // llamada a ruta para cargar combobox con datos de tabla secciones
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
            
              $('#Seccion1').empty();//limpia el combobox
              data.forEach(element => { //ciclo para recorrer el arreglo de secciones
                  //variable para asignarle los valores al combobox
                var datos='<option  value="'+element.id+'">'+element.Codigo+'</option>';
    
                  $('#Seccion1').append(datos);//ingresa valores al combobox
              });
              
          }
        });//Fin ajax combobox Seccion
    
        $.ajax({
            type: 'POST',
            url: 'cargargrupos/grupo', // llamada a ruta para cargar combobox con datos de tabla grupos
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
            
              $('#Grupo1').empty();//limpia el combobox
              data.forEach(element => { //ciclo para recorrer el arreglo de grupos
                  //variable para asignarle los valores al combobox
                var datos='<option  value="'+element.id+'">'+element.Grupo+'</option>';
    
                  $('#Grupo1').append(datos);//ingresa valores al combobox
              });
              
          }
        });//Fin ajax combobox Grupo
    
        $.ajax({
            type: 'POST',
            url: '/docente/cargar_deco_oferta/'+$('#datepickerOfertaEditar').val()+'/'+docente, // llamada a ruta para cargar combobox con datos de tabla docentesn  
            dataType: "JSON", // tipo de respuesta del controlador
            success: function(data){ 
              $('#Docente1').empty();//limpia el combobox
              var datos="";
              for(var x=0; x<data.length;x++){
                datos+='<option  value="'+data[x].id+'">'+data[x].Nombre+'</option>';
              }
              $('#Docente1').append(datos);//ingresa valores al combobox
          }
        });//Fin ajax combobox Docentes
    
    
    $.ajax({
        type: 'POST',
        url: 'oferta/cargar/'+ide,//ruta para cargar datos de la oferta seleccionada a editar
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
          for(var a=0; a<data.length;a++){
              var id=data[a].idOferta;
              var Descrip=data[a].Descripcion;
               docente=data[a].Docente;
              var fecha=data[a].FechaOferta;
              var seccion=data[a].Seccion;
              var grupo=data[a].Grupo;
              var grado=data[a].Grado;
          }
          $('#Descripcion_Oferta1').val(Descrip);
          $('#datepickerOfertaEditar').val(fecha);
          $('#idoferta').val(id);
          $('#Docente1').select2("val",docente);
          $('#Grupo1').val(grupo);
          $('#Grado1').val(grado);
          $('#Seccion1').val(seccion); 
      }
    });//Fin ajax 
    
}

$("#editar_confirmar_Oferta").click(function() {
    $.ajax({
                
        type: 'POST',
        url: 'actualizar/oferta', // ruta editar oferta
        data: $('#editar-oferta').serialize(), // manda el form donde se encuentra la modal oferta
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){
        if ((data==null)) // si el ajax contiene errores agrega un label indicando el error 
        { 
            $('.error').removeClass('hidden');
            $('.error').text("Error: "+ data.Nombre); 
        } 
        else 
        {
            $('.error').addClass('hidden'); //elimina el mensaje de error
            for(var i=0; i<data.length;i++)
            {
                var datos= "<tr id=" + data[i].idOferta + ">"+"<td>"+data[i].de+"</td>"+"<td>"+data[i].FechaOferta+"</td>"+"<td>"+data[i].Nombre+"</td>"+"<td>"+data[i].Grado+"</td>"+"<td>"+data[i].Grupo+"</td>"+"<td>"+data[i].Codigo+"</td>"
                + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>"+"<button class='btn btn-success' onclick='editar_Oferta(this);' data-id="+ data[i].idOferta +" data-Nombre="+data[i].de+"  ><i class=' fa fa-fw fa-pencil'></i></button>"        
                + "<button class='btn btn-info' data-id="+ data[i].idOferta +" onclick='eliminar_oferta(this);'><i class='fa fa-fw fa-trash '></i></button>"                                   
                +"</td>"+"</tr>"; }// variable guarda el valor 

                dat.replaceWith(datos); //reemplaza por los nuevos datos
                $("#exito").modal("show"); //abre modal de exito
                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                    $("#exito").modal("hide"); // cierra modal                                           
                    } );
                $("#editar_Oferta").modal("hide"); // cierra modal
                                 }
        }
        });
    });