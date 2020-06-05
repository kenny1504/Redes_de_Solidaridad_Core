$(document).ready(function () { //Construye el menu segun el usuario logueado 

    $.ajax({  // ajax para para recuperar datos de Usuarios Docentes
        type: "GET",
        url: "Grados/Grados",
        success: function (data) {
            data.forEach(element => {
                var datos = "<tr id=" + element.id + ">" + "<td>" + element.grado + "</td>"
                    + "<td style='padding-top:0.1%; padding-bottom:0.1%;'>" + "<button class='btn btn-primary' data-id=" + element.id + " onclick='mostrar_Materias_grados(this);' ><i id='Ver-Asignaturas'>ver</button>"
                    + "<button class='btn btn-success' onclick='editar_Grado(this);'  data-id=" + element.id + " data-Nombre=" + element.grado + "><i class=' fa fa-fw fa-pencil'></i></button>"
                    + "<button class='btn btn-info' data-id=" + element.id + " onclick='eliminar_grado(this);'><i class='fa fa-fw fa-trash '></i></button>"
                    + "</td>" + "</tr>"; // variable guarda el valor 
                $('#grados tbody ').append(datos); // agrega nuevo registro a tabla
            })
        }
    })
})