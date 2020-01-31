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
    var id=$(button).attr("data-id");
 
    $.ajax({
     type: 'POST',
     url: 'tutor/cargar/'+id, // llamada a la consulta
     dataType: "JSON", // tipo de respuesta del controlador
     success: function(data){
     
     //obtiene datos del docente
      var cedula=data[0].Cedula;
      var nombre=data[0].Nombre;
      var apellido1=data[0].Apellido1;
      var apellido2=data[0].Apellido2;
      var sexo=data[0].Sexo;
      var direccion=data[0].Direccion; 
      var correo=data[0].Correo;
      var telefono=data[0].Telefono;
      var fecha=data[0].FechaNacimiento;
      var oficio=data[0].Oficio;
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