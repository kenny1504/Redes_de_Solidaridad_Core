function ver_docente(button) {


    $("#perfil_docente").modal("show"); //abre modal ver docente
    var ide = $(button).attr("data-id");

    $.ajax({
        type: 'POST',
        url: 'Docente/Datos_DocenteU', // llamada a la consulta
        data: { id: ide }, // manda parametro
        dataType: "JSON", // tipo de respuesta del controlador
        success: function (data) {

            data.forEach(element => { 
                    //obtiene datos del docente
                var cedula = element.cedula;
                var nombre = element.nombre;
                var apellido1 = element.apellido1;
                var apellido2 = element.apellido2;
                var sexo = element.sexo;
                var direccion = element.direccion;
                var correo = element.correo;
                var telefono = element.telefono;
                var fecha = element.fecha;
                var estado = element.estado;
                if (element.estado == 1) {
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
            })
        }
    });

};//fin metodo