var dat; //variable global que guarda el dato "tr" (Fila a editar)
function editar_Grado(button)
{
    dat = $(button).closest("tr"); //captura toda la fila donde se efectuo el click (Editar)
    //var name =$(button).parents("tr").find("td").text(); //obtiene nombre del grado (nuevo)
    var ide=$(button).attr("data-id");//obtiene el id del grado
    var name=$(button).attr("data-Nombre"); //anterior capturar nombre
    $('#editar_Grado').modal('show'); // abre ventana modal
    $('.error').addClass('hidden'); // oculta error del servidor(validacion-servidor)
    $('#idgrado').val(ide);   //manda valor "id" a ventana modal Nombre
    $('#Nombre-Grado').val(name);
}

function Ingresar_grado(e) { // Metodo para guardar(editar) datos los datos al presionar ENTER 
    var tecla = (document.all) ? e.keyCode : e.which;
    if (tecla==13) // si es 13 entonces presiono ENTER
    {
        $("#editar_confirmar_Grado").click(); // llama al evento click "editar_confirmar_Grado"
    }
  }
    $("#editar_confirmar_Grado").click(function() {
        $.ajax({
                
                    type: 'POST',
                    url: 'actualizar/grado', // ruta editar grado
                    data: $('#editar-grado').serialize(), // manda el form donde se encuentra la modal grado
                    dataType: "JSON", // tipo de respuesta del controlador
                    success: function(data){
                        if ((data.msg!=true)) { // si el ajax contiene errores agrega un label indicando el error 
                                $('.error').removeClass('hidden');
                                $('.error').text("Error: "+ data.Grado); 
                          } else {
                                var datos=  "<tr id=" + data.id + ">"+"<td>"+data.Grado+"</td>"
                                + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-primary' data-id="+ data.id +" onclick='mostrar_Materias_grados(this);' ><i id='Ver-Asignaturas'>ver</button>"  
                                +"<button class='btn btn-success'  onclick='editar_Grado(this);' data-id="+ data.id +" data-Nombre="+data.Grado+"><i class=' fa fa-fw fa-pencil'></i></button>"
                                + "<button class='btn btn-info ' onclick='eliminar_grado(this);' data-id="+ data.id +"><i class='fa fa-fw fa-trash '></i></button>"                                   
                                +"</td>"+"</tr>";// variable guarda los nuevos valores

                                dat.replaceWith(datos); //reemplaza por los nuevos datos
                                $("#exito").modal("show"); //abre modal de exito
                                $("#exito").fadeTo(2000,500).slideUp(450,function(){   // cierra la modal despues del tiempo determinado  
                                            $("#exito").modal("hide"); // cierra modal
                                            
                                            } );
                                 $("#editar_Grado").modal("hide"); // cierra modal
                                 }
                    }
             });
    });