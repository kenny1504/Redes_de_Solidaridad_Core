function Asignar() { //ajax para cargar combobox Asignaturas y Grados

    var id = $("#id_u").text() //Id de la institucion 
      $.ajax({
          type: 'POST',
          url: 'Asignaturas/Datos', // llamada a ruta para cargar combobox con datos de tabla materia
          data: { idinstitucion: id },
          dataType: "JSON", // tipo de respuesta del controlador
          success: function (data) { 
              $('#Asignaturaid').empty();
          //ciclo para recorrer el arreglo de asignaturas
            data.forEach(element => {
                //variable para asignarle los valores al combobox
               var datos='<option style="color: blue;" value="'+element.id+'">'+element.nombre+'</option>'; 

                $('#Asignaturaid').append(datos); //ingresa valores al combobox
            });
        
        }   
      });//Fin ajax combobox Asignaturas
      
      $.ajax({
        type: 'POST',
          url: 'Grados/Grados', // llamada a ruta para cargar combobox con datos de tabla grados
        dataType: "JSON", // tipo de respuesta del controlador
        success: function(data){ 
        
            $('#Gradoid').empty();//limpia el combobox
          data.forEach(element => { //ciclo para recorrer el arreglo de grados
              //variable para asignarle los valores al combobox
            var datos='<option style="color: blue;" value="'+element.id+'">'+element.grado+'</option>';

              $('#Gradoid').append(datos);//ingresa valores al combobox
          });
          
      }
    });//Fin ajax combobox Grados
};

function AsignarMateria() { // ajax para guardar en la tabla detalleAsignatura

    var idasignatura = $('#Asignaturaid').val();
    var idgrado = $('#Gradoid').val();

    $.ajax({
        type: 'POST',
        url: 'Asignaturas/Asignar', // llamada a ruta para cargar combobox con datos de tabla materia
        data: { GradoAcademicoId: idgrado, AsignaturasId: idasignatura }, // manda el form donde se encuentra la modal dataType: "JSON", // tipo de respuesta del controlador
      dataType: "JSON", // tipo de respuesta del controlador
      success: function(data){ 
        if(data==1) // si la aun no se asigna  a ese grado manda mensaje de exito
        {
          $("#asignar_asignatura").modal("hide"); // cierra modal confirmar
          $("#exito").modal("show"); //abre modal de exito
          $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                   $("#exito").modal("hide"); // cierra modal exito
                   } );
        }
        if(data==-1) // si la materia ya existe se manda mensaje de error
        {
          var error="La Materia que intenta ingresar ya esta asignada a este grado"
          $('#mensaje').text(error);   //manda el error a la modal
          $("#Mensaje-error").modal("show"); //abre modal de error
          $("#Mensaje-error").fadeTo(2000,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                   $("#Mensaje-error").modal("hide"); // cierra modal error
                   } );
        }      
    }   
  });//Fin ajax guardar materia asignada
};
