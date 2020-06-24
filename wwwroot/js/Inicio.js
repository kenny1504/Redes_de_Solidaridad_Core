$(document).ready(function () { //Construye el menu segun el usuario logueado 
	var text = $('#rol_u').text()

	if (text == "1 ") {
		$('#rol_u').text("Administrador de sistema")//Pone tipo de usuario en sesion

		document.getElementById("menu_u").innerHTML = //inserta item 
			"<li class='header' > Menu Navegacion</li >"+
            "<li id='menu_usuarios' class='treeview'>"+//Menu Usuarios
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Usuarios</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='Usuarios' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
                "</li >" +
                     "<li id='menu_Docentes' class='treeview'>"+//Menu Docentes
                        "<a href='#'>"+
                           "<i class='fa fa-male'></i> <span>Docentes</span>"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                        "</a>"+
                        "<ul class='treeview-menu'>"+
                            "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                            "<li><a href='Docentes' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                        "</ul>"+
                        "</li >" +
                            "<li id='menu_Estudiantes' class='treeview'>"+//Menu Estudiantes
                                "<a href='#'>"+
                                   "<i class='fa   fa-user-plus'></i> <span>Estudiantes</span>"+
                                    "<span class='pull-right-container'>"+
                                        "<i class='fa fa-angle-left pull-right'></i>"+
                                    "</span>"+
                                "</a>"+
                                "<ul class='treeview-menu'>"+
                                    "<li><a href='Estudiantes' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                                "</ul>"+
                            "</li >"+
            "<li class='treeview ' id='menu_Administra'>"+//Menu Administra
                "<a href='#'>"+
                    "<i class='fa fa-cogs'></i> <span>Administra</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                        "<small class='label pull-right bg-red'></small>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+//Menu dentro de Aministras "Menu Grados"
                    "<li class='treeview'>"+//Menu Grados
                        "<a href='#'>"+
                            "<i class='fa fa-circle-o text-aqua'></i> Grados"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                       " </a>"+
                        "<ul class='treeview-menu'>"+
                            "<li>"+
                                "<a href='#' data-toggle='modal' data-target='#modal_Grado' id='m'>"+
                                    "<i class='fa fa-circle-o text-yellow'></i> Agregar"+
                                "</a>"+
                            "</li>"+
                            "<li class='treeview'>"+
                                "<li><a href='Grados'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+
                        "</li></ul>" +
                      "<li class='treeview'>" +//Menu Grupos
                        "<a href='#'>"+
                            "<i class='fa fa-circle-o text-aqua'></i> Grupos"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                       " </a>"+
                        "<ul class='treeview-menu'>"+
                            "<li>"+
                                "<a href='#' data-toggle='modal' data-target='' id='m'>"+
                                    "<i class='fa fa-circle-o text-yellow'></i> Agregar"+
                                "</a>"+
                            "</li>"+
                            "<li class='treeview'>"+
                                "<li><a href='Grupos'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+
                        "</li></ul>"+          
                      "<li class='treeview'>" +//Menu Oficios
                        "<a href='#'>"+
                            "<i class='fa fa-circle-o text-aqua'></i> Oficios"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                       " </a>"+
                        "<ul class='treeview-menu'>"+
                            "<li>"+
                                "<a href='#' data-toggle='modal' data-target='' id='m'>"+
                                    "<i class='fa fa-circle-o text-yellow'></i> Agregar"+
                                "</a>"+
                            "</li>"+
                            "<li class='treeview'>"+
                                "<li><a href='Oficios'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+
                        "</li></ul>"+
                      "<li class='treeview'>" +//Menu Parentescos
                        "<a href='#'>"+
                            "<i class='fa fa-circle-o text-aqua'></i> Parentescos"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                       " </a>"+
                        "<ul class='treeview-menu'>"+
                            "<li>"+
                                "<a href='#' data-toggle='modal' data-target='' id='m'>"+
                                    "<i class='fa fa-circle-o text-yellow'></i> Agregar"+
                                "</a>"+
                            "</li>"+
                            "<li class='treeview'>"+
                                "<li><a href='Parentescos_Vista'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+
                        "</li></ul>" +                   
                      "<li class='treeview'>" +//Menu Turnos
                        "<a href='#'>"+
                            "<i class='fa fa-circle-o text-aqua'></i> Turnos"+
                            "<span class='pull-right-container'>"+
                                "<i class='fa fa-angle-left pull-right'></i>"+
                            "</span>"+
                       " </a>"+
                        "<ul class='treeview-menu'>"+
                            "<li>"+
                                "<a href='#' data-toggle='modal' data-target='' id='m'>"+
                                    "<i class='fa fa-circle-o text-yellow'></i> Agregar"+
                                "</a>"+
                            "</li>"+
                            "<li class='treeview'>"+
                                "<li><a href='Turnos'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+

                        "</li></ul>"+
                    "</ul >" //Fin Menu Administra
               
	}
    if (text == "2 ") {
        $('#rol_u').text("Docente")//Pone tipo de usuario en sesion
        document.getElementById("menu_u").innerHTML = //inserta item Usuarios
            "<li class='header' > Menu Navegacion</li >"+
            "<li id='menu_Notas' class='treeview'>"+//Menu Notas
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Notas</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='' ><i class='fa fa-circle-o text-aqua'></i>Agregar</a></li>"+
                "</ul>"+
			"</li >" +
			 "<li id='menu_Asistencia' class='treeview'>"+//Menu Asistencia
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Asistencia</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='Asistencia' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
            "<li id='menu_Estudiantes' class='treeview'>"+//Menu Estudiantes
                "<a href='#'>"+
                   "<i class='fa   fa-user-plus'></i> <span>Estudiantes</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='Estudiantes' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" 
    }
	if (text == "3 ") {
		$('#rol_u').text("Institucion")//Pone tipo de usuario en sesion

		document.getElementById("menu_u").innerHTML = //inserta item 
			"<li class='header' > Menu Navegacion</li >"+
            "<li id='menu_usuarios' class='treeview'>"+//Menu Usuarios
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Usuarios</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='Usuarios' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
			"</li >" +
			 "<li id='menu_Docentes' class='treeview'>"+//Menu Docentes
                "<a href='#'>"+
                   "<i class='fa fa-male'></i> <span>Docentes</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='Docentes' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
             "<li id='menu_Estudiantes' class='treeview'>"+//Menu Estudiantes
                "<a href='#'>"+
                   "<i class='fa   fa-user-plus'></i> <span>Estudiantes</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='Estudiantes' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
             "<li id='menu_Ofertas' class='treeview'>"+//Menu Ofertas
                "<a href='#'>"+
                   "<i class='fa fa-address-book'></i> <span>Ofertas</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='Ofertas' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >"+
             "<li id='menu_usuarios' class='treeview'>"+//Menu Asignaturas
                    "<a href='#'>"+
                       "<i class='fa fa-user'></i> <span>Asignaturas</span>"+
                        "<span class='pull-right-container'>"+
                            "<i class='fa fa-angle-left pull-right'></i>"+
                        "</span>"+
                    "</a>"+
                    "<ul class='treeview-menu'>"+
                        "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                        "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                    "</ul>"+
                "</li >"
	}
	
})