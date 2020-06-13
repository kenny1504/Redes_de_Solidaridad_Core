//Metodo para mostrar u ocultar botones en index Estudiantes
var n = 0; var fila = $("#docentes").closest('table').children('tbody').children('tr:first');/* captura el primer tr de la tabla*/   var id1; var id2; var i = 0; var a = 0; var fila1;
function ver_completo(button) { // funcion para mostrar y o ocultar botones

    fila.attr("style", " ");
    fila = $(button).parents("tr"); //captura fila para ponerle color
    if (fila.index() == a) {
        fila.attr("style", "background-color:#F9E79F;"); // le pones color a la fila seleccionada
        var iden = $(button).attr("data-id"); // captura el id_grado "id" del grado
        id1 = "#" + iden;//captura id de la celda de botones
        id2 = "#" + iden + "a";// captura id del contenido a ocultar o mostrar
        if (n == 0) {

            $(id1).removeClass('hidden');
            $(id2).addClass('hidden');
            n = 1; // oculta error del servidor(validacion-servidor)
        }
        else {
            fila.attr("style", " ");//remueve el color a fila
            $(id2).removeClass('hidden '); //muestra celda
            $(id1).addClass('hidden '); // oculta botones
            n = 0; a = 0;
        }
    }
    else {
        $(id2).removeClass('hidden '); //muestra celda
        $(id1).addClass('hidden '); // oculta botones
        n = 0; a = 0;
        a = fila.index();
        fila.attr("style", "background-color:#F9E79F;"); // le pones color a la fila seleccionada
        var iden = $(button).attr("data-id"); // captura el id_grado "id" del grado
        id1 = "#" + iden;//captura id de la celda de botones
        id2 = "#" + iden + "a";// captura id del contenido a ocultar o mostrar
        if (n == 0) {

            $(id1).removeClass('hidden');
            $(id2).addClass('hidden');
            n = 1; // oculta error del servidor(validacion-servidor)
        }
        else {
            fila.attr("style", " ");//remueve el color a fila
            $(id2).removeClass('hidden '); //muestra celda
            $(id1).addClass('hidden '); // oculta botones
            n = 0; a = 0;
        }

    }

};


function ver_docente(button) {

    $("#perfil_docente").modal("show"); //abre modal ver docente
    var id = $(button).attr("data-id");

    $.ajax({
        type: 'POST',
        url: 'docente/cargar/' + id, // llamada a la consulta
        dataType: "JSON", // tipo de respuesta del controlador
        success: function (data) {

            //obtiene datos del docente
            var cedula = data[0].Cedula;
            var nombre = data[0].Nombre;
            var apellido1 = data[0].Apellido1;
            var apellido2 = data[0].Apellido2;
            var sexo = data[0].Sexo;
            var direccion = data[0].Direccion;
            var correo = data[0].Correo;
            var telefono = data[0].Telefono;
            var fecha = data[0].FechaNacimiento;
            var estado = data[0].Estado;
            if (data[0].Estado == 1) {
                $('#estad').text("Activo");
            }
            else {
                $('#estad').text("Inactivo");
            }
            //asignar valores obtenidos en el ajax aventana modal
            $('#cedul').text(cedula);
            $('#nombr').text(nombre);
            $('#apellid1').text(apellido1);
            $('#apellid2').text(apellido2);
            $('#sex').text(sexo);
            $('#fechanaci').text(fecha);
            $('#telefon').text(telefono);

            $('#corre').text(correo);
            $('#direccio').text(direccion);


            fila.attr("style", " ");//remueve el color a fila
            $(id2).removeClass('hidden'); //muestra celda
            $(id1).addClass('hidden'); // oculta botones
        }
    });

};//fin metodo


$(document).ready(function () { //Carga los docentes en la tabla Docentes
    var ide = $("#id_u").text()//Capturamos el id de la Institucion logueada

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Docente/MostrarADMIN",
        data: { id: ide },
        success: function (data) {
            data.forEach(element => {
                if (element.estado == 1)
                    var estado = "Activo"
                else
                    var estado = "Inactivo"
                var datos = "<tr>" +
                    "<td>" + element.cedula + "</td>" +
                    "<td>" + element.nombre + "</td>" +
                    "<td>" + element.sexo + "</td> " +
                    "<td>" + element.correo + "</td> " +
                    "<td>" + element.telefono + "</td>" +
                    "<td style='padding-top:0.1%; padding-bottom:0.1%;' class='hidden' id=" + element.id + " >" +
                    "<button class='btn btn-primary'  onclick='ver_docente(this);'  data-id=" + element.id + " id='Ver-docente'>ver</button>" +
                    "<button class='btn btn-success' onclick='editar_Docente(this);' data-id=" + element.id + "><i class='fa fa-fw fa-pencil'></i></button>" +
                    "<button class='btn btn-info' onclick='eliminar_oferta(this);' data-id=" + element.id + "><i class='fa fa-fw fa-trash '></i></button>" +
                    " <i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);'  data-id=" + element.id + "></i>" +
                    "</td>" +
                    "</td>" +
                    "<td id=" + element.id + "a>" + estado + "<i class='fa fa-angle-double-right pull-right' onclick='ver_completo(this);' data-id=" + element.id + "></i> </td>"
                "</tr>"
                $('#Docentes tbody ').append(datos); //insertamos datos en tabla
            })
        }
    })
})


