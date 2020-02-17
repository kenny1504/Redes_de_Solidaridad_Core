  var results = new Array(); //arreglo para almacenar datos
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
                        pageSize: 10, //indica la cantidad a mostrar por cada Pagina
                        callback: function (data, pagination) { 
                            
                        var html = "<table class='table table-bordered table-striped display'><thead><tr><th>Codigo estudiante</th><th>Nombre completo</th><th>Sexo</th><th>Direccion</th><th>Tutor</th><th>Tutor Telefono</th></tr></thead><tbody>";
                            $.each(data, function (index, item){
                                
                                html += '<tr>';
                                html +=
                                    '<td>' + item.codigo + '</td>'
                                    + '<td>' + item.nombre + '</td>'
                                    + '<td>' + item.sexo + '</td>'
                                    + '<td>' + item.direccion + '</td>'
                                    + '<td>' + item.tutor
                                    + '<td style="padding-top:0.1%; padding-bottom:0.1%;"class= "hidden" id="'+item.idEstudiante+'" >'
                                    + '<button class="btn btn-primary" onclick="ver_estudiante(this);" data-id="'+item.idEstudiante+'" id="Ver-estudiante">ver</button>'
                                    + '<button class="btn btn-success " data-id="'+item.idEstudiante+'" data-idper="'+item.idPersona+'" onclick="editar_estudiante(this);" ><i class=" fa fa-fw fa-pencil"></i></button>'
                                    + '<button class="btn btn-info" data-id="'+item.idEstudiante+'" onclick="eliminar_estudiante(this);"><i class="fa fa-fw fa-trash "></i></button>'
                                    + '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="' +item.idEstudiante+'"></i>'
                                    + '</td>'
                                    + '<td id="'+item.idEstudiante+'a" >' +item.telefono_tutor+ '<i class="fa fa-angle-double-right pull-right" onclick="mostrar(this);" data-id="'+item.idEstudiante+'"></i> </td>';
                              html += '</tr>';
                            })
                            html += "</tbody></table>";
                            $('#estudiantes').html(html); //insertamos datos paginados en tabla
                        }
                    })
                }

             
                               
                        


            })
        });

