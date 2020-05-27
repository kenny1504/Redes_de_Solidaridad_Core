function ver_usuario(button) { //metodo para mostrar datos de usuario en ventana modal
    var contraseña = $(button).attr("data-password");//obtiene el id de la materia
    var NombreUsuario = $(button).attr("data-user");//obtiene el id de la materia
    var cedula = $(button).attr("data-cedula");//obtiene el id de la materia
    var nombre_u = $(button).attr("data-Nombre");//obtiene el id de la materia
    var rol_user = $(button).attr("data-rol");//obtiene el id de la materia
    var vencimiento_user = $(button).attr("data-vencimiento");//obtiene el id de la materia

    $("#perfil_usuario").modal("show"); //abre modal ver usuario
    //envia datos del usuario a ventana modal
    $('#nombre_u').text(nombre_u);
    $('#rol_user').text(rol_user);
    $('#nombre_user').text(NombreUsuario);
    $('#Contraseña_user').text(contraseña);
    $('#cedula_user').text(cedula);
    $('#vencimiento_user').text(vencimiento_user);
};

function Mostrar_Usuarios() {

    var TipoUsuario = $('#tipo').val();//capturamos el valor de combobox

    if (TipoUsuario == 2) {//Si selecciona Docente

        $("#Usuarios").empty();//Limpia Datos de la tabla Usuarios
        var html2 = "<thead><tr><th>Usuario</th><th>Contraseña</th><th>Nombre completo</th><th>Institucion</th><th></th></tr></thead>";
        $('#Usuarios').append(html2);

        $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
            type: "GET",
            url: "Usuarios/Usarios_Docentes",
            success: function (data) {
                data.forEach(element => {            
                  var  html = '<tbody><tr>'
                        + '<td>' + element.usuario + '</td>'
                        + '<td>' + element.contraseña + '</td>'
                        + '<td>' + element.nombre + '</td>'
                        + '<td>' + element.institucion + '</td>'
                        + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + element.idUsuario + '" >'
                        + '<button class="btn btn-primary" onclick="ver_estudiante(this);" data-id="' + element.idUsuario + '" id="Ver-estudiante">ver</button>'
                        + '<button class="btn btn-success " data-id="' + element.idUsuario + '" data-idper="' + element.idUsuario + '" onclick="editar_estudiante(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
                        + '<button class="btn btn-info" data-id="' + element.idUsuario + '" onclick="eliminar_estudiante(this);"><i class="fa fa-fw fa-trash "></i></button>'                     
                        + '</tr>';
                    +"</tbody>";
                    $('#Usuarios').append(html); //insertamos datos en tabla
                })
            }
        })
    }
    if (TipoUsuario == 3) {//Si selecciona Instituciones

        $("#Usuarios").empty();//Limpia Datos de la tabla Usuarios
        var html2 = "<thead><tr><th>Usuario</th><th>Contraseña</th><th>Nombre Institucion</th><th>Direccion</th><th></th></tr></thead>";
        $('#Usuarios').append(html2);

        $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
            type: "GET",
            url: "Usuarios/Usarios_Instituciones",
            success: function (data) {
                data.forEach(element => {
                    var html = '<tbody><tr>'
                        + '<td>' + element.usuario + '</td>'
                        + '<td>' + element.contraseña + '</td>'
                        + '<td>' + element.nombre + '</td>'
                        + '<td>' + element.direccion + '</td>'
                        + '<td style="padding-top:0.1%; padding-bottom:0.1%; id="' + element.idUsuario + '" >'
                        + '<a class="btn btn-success " data-id="' + element.idUsuario + '"  onclick="editar_Institucion(this);" ><i class=" fa fa-fw fa-pencil"></i></a>'
                        + '<a class="btn btn-info" data-id="' + element.idUsuario + '" onclick="eliminar_Usuario_Institucion(this);"><i class="fa fa-fw fa-trash "></i></a>'
                        + '</tr>';
                    +"</tbody>";
                    $('#Usuarios').append(html); //insertamos datos en tabla
                })
            }
        })

    }
}