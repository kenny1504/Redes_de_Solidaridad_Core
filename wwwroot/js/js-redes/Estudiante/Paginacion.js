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
                                html += '<td>' + item.idEstudiante + '</td><td>' + item.codigo + '</td><td>' + item.nombre + '</td>';
                        html += '</tr>';
                            })
                            html += "</tbody></table>";
                            $('#estudiantes').html(html); //insertamos datos paginados en tabla
                        }
                    })
                }
            })
        });

