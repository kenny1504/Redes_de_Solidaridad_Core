var dat; //variable global que guarda el dato "tr" (Fila a eliminar)
var ide; // captura el id_materia "id" de la materia
function eliminar_estudiante(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Eliminar)
    ide=$(button).attr("data-id"); // captura el id_materia "id" del estudiante
    $('#eliminar_Estudiante').modal('show'); // abre ventana modal

}


$("#confirmar_Estudiante").click(function() {
        
    $.ajax({
               type: 'POST',
               url: '/estudiante/eliminar/'+ide, // ruta eliminar materia
               dataType: "JSON", // tipo de respuesta del controlador
               success: function(data){ 

                    if(data==1)
                    {
                        dat.remove(); //remueve la fila eliminado 
                        $("#exito").modal("show"); //abre modal de exito
                        $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                                    $("#exito").modal("hide"); // cierra modal
                                    } );
                    }
                    if(data==-1)
                    {
                        var error="Error imposible eliminar asignatura, debido a que esta asignada a un grado academico"
                        $('#mensaje').text(error);   //manda el error a la modal
                        $("#Mensaje-error").modal("show"); //abre modal de error
                        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
                            $("#Mensaje-error").modal("hide"); // cierra modal error
                            } );
                    }                  
              
               }
        });// 
});