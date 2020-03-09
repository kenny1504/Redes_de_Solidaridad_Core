  //Metodo para mostrar u ocultar botones en index Tutores
var n=0; var fila=$("#tutor").closest('table').children('tbody').children('tr:first');/* captura el primer tr de la tabla*/   
var id1; var id2; var i=0; var a=0; var fila1;
function mostrarT(button){ // funcion para mostrar y o ocultar botones
  
  fila.attr("style"," ");
  fila=$(button).parents("tr"); //captura fila para ponerle color
  if(fila.index()==a)
  {
    fila.attr("style","background-color:#F9E79F;"); // le pones color a la fila seleccionada
    var iden=$(button).attr("data-id"); // captura el id_grado "id" del grado
    id1="#"+iden+"T";//captura id de la celda de botones
    id2="#"+iden+"T2";// captura id del contenido a ocultar o mostrar
        if(n==0){
          
          $(id1).removeClass('hidden');
          $(id2).addClass('hidden');
          n=1; // oculta error del servidor(validacion-servidor)
        }
        else{
            fila.attr("style"," ");//remueve el color a fila
            $(id2).removeClass('hidden '); //muestra celda
            $(id1).addClass('hidden '); // oculta botones
            n=0; a=0; 
        }
  }
  else
  {
    $(id2).removeClass('hidden '); //muestra celda
    $(id1).addClass('hidden '); // oculta botones
    n=0; a=0;
    a=fila.index();
    fila.attr("style","background-color:#F9E79F;"); // le pones color a la fila seleccionada
    var iden=$(button).attr("data-id"); // captura el id_grado "id" del grado
    id1="#"+iden+"T";//captura id de la celda de botones
    id2="#"+iden+"T2";// captura id del contenido a ocultar o mostrar
        if(n==0){
          
          $(id1).removeClass('hidden');
          $(id2).addClass('hidden');
          n=1; // oculta error del servidor(validacion-servidor)
        }
        else{
            fila.attr("style"," ");//remueve el color a fila
            $(id2).removeClass('hidden '); //muestra celda
            $(id1).addClass('hidden '); // oculta botones
            n=0; a=0;
        }

  }
   
  }; 
  function ver_tutor(button){ 

    $("#perfil_tutor").modal("show"); //abre modal ver usuario
      var id = $(button).attr("data-id");
      var oficio; var correo; var direccion; var cedula; var nombre; var apellido1; var apellido2; var sexo; var telefono; var fecha;
    $.ajax({
     type: 'POST',
     url: 'Tutores/Ver', // llamada a la consulta
     data: { id: id },
     dataType: "JSON", // tipo de respuesta del controlador
     success: function(data){
     
     //obtiene datos del docente
     data.forEach(dat => {
         cedula = dat.cedula;
          nombre =dat.nombre;
          apellido1 =dat.apellido1;
          apellido2 =dat.apellido2;
          sexo =dat.sexo;
          direccion =dat.direccion; 
          correo =dat.correo;
          telefono =dat.telefono;
          fecha =dat.fecha;
          oficio =dat.oficio;
     });

      if(sexo!='M')//si el tutor es mujer muestra imagen femenina
    {
      $('#mujer').removeClass('hidden'); //muestra imagen
      $('#varon').addClass('hidden'); // oculta imagen
    }
    else
    {
      $('#varon').removeClass('hidden'); //muestra imagen
      $('#mujer').addClass('hidden'); // oculta oculta imagen 
    }

    //asignar valores obtenidos en el ajax aventana modal
    $('#N1t').text(nombre);
    $('#p1t').text(apellido1);
    $('#p2t').text(apellido2);
    $('#st').text(sexo);
    $('#fnt').text(fecha);
    $('#ot').text(oficio);
    $('#ct').text(correo);
    $('#tet').text(telefono);
    $('#dirt').text(direccion);
    $('#cet').text(cedula);
   
      
    fila.attr("style"," ");//remueve el color a fila
    $(id2).removeClass('hidden'); //muestra celda
    $(id1).addClass('hidden'); // oculta botones
   }
    });
    
   };//fin metodo