//Metodo para mostrar u ocultar botones en index Estudiantes
var n=0; var fila=$("#estudiantes").closest('table').children('tbody').children('tr:first');/* captura el primer tr de la tabla*/   var id1; var id2; var i=0; var a=0; var fila1;
function mostrar(button){ // funcion para mostrar y o ocultar botones
  
  fila.attr("style"," ");
  fila=$(button).parents("tr"); //captura fila para ponerle color
  if(fila.index()==a)
  {
    fila.attr("style","background-color:#F9E79F;"); // le pones color a la fila seleccionada
    var iden=$(button).attr("data-id"); // captura el id_grado "id" del grado
    id1="#"+iden;//captura id de la celda de botones
    id2="#"+iden+"a";// captura id del contenido a ocultar o mostrar
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
    id1="#"+iden;//captura id de la celda de botones
    id2="#"+iden+"a";// captura id del contenido a ocultar o mostrar
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


  //datos estudiante
  var codigo;
  var nombre;
  var apellido1;
  var apellido2;
  var fecha;
  var tutor;
  var parentesco;
  var telefono;
  var dirreccion;
  var sexo;
  //datos tutor
  var  cedula;
  var nombret;
  var apellido1t;
  var apellido2t;
  var fechat;
  var correot;
  var telefonot;
  var dirrecciont;
  var sexot;
  var oficiot;

function ver_estudiante(button){ 

   $("#perfil_estudiante").modal("show"); //abre modal ver usuario
   var id=$(button).attr("data-id");

   $.ajax({
    type: 'POST',
    url: 'Estudiante/Detalles', // llamada a la consulta
    data: { id: id },
    dataType: "JSON", // tipo de respuesta del controlador
    success: function(data){
    
        //obtiene datos del estudiante
        data.forEach(dat => {
            codigo = dat.codigo;
            nombre = dat.nombre;
            apellido1 = dat.apellido1;
            apellido2 = dat.apellido2;
            fecha = dat.fecha;
            tutor = dat.tutor; // nombre completo del tutor
            parentesco = dat.parentesco;
            telefono = dat.telefono;
            dirreccion = dat.direccion;
            sexo = dat.sexo;
        });
        //obtiene datos del tutor
        cedula=data.Cedula;
        nombret=data.Nombret;
        apellido1t=data.apellido1t;
        apellido2t=data.apellido2t;
        fechat=data.fechat;
        correot=data.correot;
        telefonot=data.telefonot;
        dirrecciont=data.dirrecciont;
        sexot=data.sexot;
        oficiot=data.oficiot;

            //asignar valores obtenidos en el ajax aventana modal
            $('#N1').text(nombre);
            $('#p1').text(apellido1);
            $('#p2').text(apellido2);
            $('#s').text(sexo);
            $('#fn').text(fecha);
            $('#t').text(tutor);
            $('#p').text(parentesco);
            $('#te').text(telefono);
            $('#direcc').text(dirreccion);
            $('#co').text(codigo);
              
            fila.attr("style"," ");//remueve el color a fila
            $(id2).removeClass('hidden'); //muestra celda
            $(id1).addClass('hidden'); // oculta botones
      }
   });//Fin cargar datos estudiante y tutor
   
  };//fin metodo


  $(".ver-tutor").click(function() { 
    
    $("#perfil_tutor").modal("show"); //abre modal ver usuario

    if(sexot!='M')//si el tutor es mujer muestra imagen femenina
    {
      $('#mujer').removeClass('hidden'); //muestra imagen
      $('#varon').addClass('hidden'); // oculta imagen
    }
    else
    {
      $('#varon').removeClass('hidden'); //muestra imagen
      $('#mujer').addClass('hidden'); // oculta oculta imagen 
    }
    
    $('#N1t').text(nombret);
    $('#p1t').text(apellido1t);
    $('#p2t').text(apellido2t);
    $('#st').text(sexot);
    $('#fnt').text(fechat);
    $('#ot').text(oficiot);
    $('#ct').text(correot);
    $('#tet').text(telefonot);
    $('#dirt').text(dirrecciont);
    $('#cet').text(cedula);

  }); 




  