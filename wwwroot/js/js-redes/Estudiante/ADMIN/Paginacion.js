//PAGINACION 

/* var results = new Array(); //arreglo para almacenar datos
        $(document).ready(function () {
            //Paginacion para estudiantes
            $.ajax({  // ajax para para recuperar datos
                type: "GET",
                url: "Estudiante/Datos",
                success: function (data) {
                    data.forEach(element => {
                        results.push(element);//inserta en arreglo
                    })
                        
                    
                    $('#pagination-container').pagination({ //declaracion de la paginacion
                        dataSource: results, //asignacion de datos para paginar
                        pageSize: 15, //indica la cantidad a mostrar por cada Pagina
                        callback: function (data, pagination) { 
                            
                        var html = "<table class='table table-bordered table-striped display'><thead><tr><th>Codigo estudiante</th><th>Nombre completo</th><th>Sexo</th><th>Direccion</th><th>Tutor</th><th>Tutor Telefono</th></tr></thead><tbody>";
                            $.each(data, function (index, element){
                                
                                html += '<tr>';
                                html +=
                                    '<td>' + element.codigo + '</td>'
                                    + '<td>' + element.nombre + '</td>'
                                    + '<td>' + element.sexo + '</td>'
                                    + '<td>' + element.direccion + '</td>'
                                    + '<td>' + element.tutor
                                    + '<td style="padding-top:0.1%; padding-bottom:0.1%;"class= "hidden" id="'+element.idEstudiante+'" >'
                                    + '<button class="btn btn-primary" onclick="ver_estudiante(this);" data-id="'+element.idEstudiante+'" id="Ver-estudiante">ver</button>'
                                    + '<button class="btn btn-success " data-id="'+element.idEstudiante+'" data-idper="'+element.idPersona+'" onclick="editar_estudiante(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
                                    + '<button class="btn btn-info" data-id="'+element.idEstudiante+'" onclick="eliminar_estudiante(this);"><i class="fa fa-fw fa-trash "></i></button>'
                                    + '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="' +element.idEstudiante+'"></i>'
                                    + '</td>'
                                    + '<td id="'+element.idEstudiante+'a" >' +element.telefono_tutor+ '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="'+element.idEstudiante+'"></i> </td>';
                              html += '</tr>';
                            })
                            html += "</tbody></table>";
                            $('#estudiantes').html(html); //insertamos datos paginados en tabla
                        }
                    })
                }                                               
            })
        }); */

$(document).ready(function () {

    //
    $.ajax({  // ajax para para recuperar datos de estudiantes
        type: "GET",
        url: "Estudiante/Datos",
        success: function (data) {
            var html = "<table class='table table-bordered table-striped display'><thead><tr><th>Codigo estudiante</th><th>Nombre completo</th><th>Sexo</th><th>Direccion</th><th>Tutor</th><th>Tutor Telefono</th></tr></thead><tbody>";
            data.forEach(element => {

                   html = '<tr>'
                        + '<td>' + element.codigo + '</td>'
                        + '<td>' + element.nombre + '</td>'
                        + '<td>' + element.sexo + '</td>'
                        + '<td>' + element.direccion + '</td>'
                        + '<td>' + element.tutor
                        + '<td style="padding-top:0.1%; padding-bottom:0.1%;"class= "hidden" id="' + element.idEstudiante + '" >'
                        + '<button class="btn btn-primary" onclick="ver_estudiante(this);" data-id="' + element.idEstudiante + '" id="Ver-estudiante">ver</button>'
                        + '<button class="btn btn-success " data-id="' + element.idEstudiante + '" data-idper="' + element.idPersona + '" onclick="editar_estudiante(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
                        + '<button class="btn btn-info" data-id="' + element.idEstudiante + '" onclick="eliminar_estudiante(this);"><i class="fa fa-fw fa-trash "></i></button>'
                        + '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="' + element.idEstudiante + '"></i>'
                        + '</td>'
                        + '<td id="' + element.idEstudiante + 'a" >' + element.telefono_tutor + '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="' + element.idEstudiante + '"></i> </td>'
                        +'</tr>';
                        +"</tbody></table>";
                $('#estudiantes').append(html); //insertamos datos en tabla
            })
        }
    })
        
    $.ajax({  // ajax para para recuperar datos de tutores
        type: "GET",
        url: "Tutores/Datos",
        success: function (data) {
            var html = "< thead ><tr> <th>Cedula</th><th>Nombre completo</th><th>Sexo</th><th>Correo</th><th>Oficio</th><th>Telefono</th></tr></thead >";
            data.forEach(element => {

                html = '<tr>'
                    + '<td>' + element.cedulat + '</td>'
                    + '<td>' + element.nombret + '</td>'
                    + '<td>' + element.sexot + '</td>'
                    + '<td>' + element.correot + '</td>'
                    + '<td>' + element.oficiot 
                    + '<td style="padding-top:0.1%; padding-bottom:0.1%;"class= "hidden" id="' + element.idtutor + 'T" >'
                    + '<button class="btn btn-primary" onclick="ver_tutor(this);" data-id="' + element.idtutor + '" id="Ver-estudiante">ver</button>'
                    + '<button class="btn btn-success " data-id="' + element.idtutor + '" onclick="editar_Tutor(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
                    + '<button class="btn btn-info" data-id="' + element.idtutor + '" onclick="eliminar_tutor(this);"><i class="fa fa-fw fa-trash "></i></button>'
                    + '<i class="fa fa-angle-double-right pull-right" onclick="mostrarT(this);;" data-id="' + element.idtutor + '"></i>'
                    + '</td>'
                    + '<td id="' + element.idtutor + 'T2" >' + element.telefonot + '<i class="fa fa-angle-double-right pull-right" onclick="mostrarT(this);" data-id="' + element.idtutor + '"></i> </td>'
                    + '</tr>';
                +"</tbody></table>";
                $('#tutor').append(html); //insertamos datos en tabla
            })
        }
    })



});
