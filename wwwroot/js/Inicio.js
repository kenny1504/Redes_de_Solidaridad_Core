$(document).ready(function () {
	var text = $('#rol_u').text()

	if (text == "1 ") {
		$('#rol_u').text("Super Usuario")//Pone tipo de usuario en sesion

		document.getElementById("menu_u").innerHTML = //inserta item Usuarios
			"<li class='header' > Menu Navegacion</li >"+
            "<li id='menu_usuarios' class='treeview'>"+//Menu Usuarios
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Usuarios</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
            "<li class='treeview ' id='menu_Administra'>"+//Menu Administra
                "<a href='#'>"+
                    "<i class='fa fa-cogs'></i> <span>Administra</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                        "<small class='label pull-right bg-red'></small>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+//Menu dentro de Aministras "Menu Grados"
                    "<li class='treeview'>"+
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
                                "<li><a href=' method=' GET'><i class='fa fa-circle-o text-yellow'></i> Mostrar</a></li>"+
                   " </li>"+
                "</ul>"
               
	}
    if (text == "2 ") {
        $('#rol_u').text("Docente")//Pone tipo de usuario en sesion
    }
	if (text == "3 ") {
		$('#rol_u').text("Institucion")//Pone tipo de usuario en sesion

		document.getElementById("menu_u").innerHTML = //inserta item Usuarios
			"<li class='header' > Menu Navegacion</li >"+
            "<li id='menu_usuarios' class='treeview'>"+//Menu Usuarios
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Usuarios</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
			"</li >" +
			 "<li id='menu_Docentes' class='treeview'>"+//Menu Docentes
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Docentes</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
             "<li id='menu_Estudiantes' class='treeview'>"+//Menu Estudiantes
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Estudiantes</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
                "</ul>"+
            "</li >" +
             "<li id='menu_Ofertas' class='treeview'>"+//Menu Ofertas
                "<a href='#'>"+
                   "<i class='fa fa-user'></i> <span>Ofertas</span>"+
                    "<span class='pull-right-container'>"+
                        "<i class='fa fa-angle-left pull-right'></i>"+
                    "</span>"+
                "</a>"+
                "<ul class='treeview-menu'>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Agregar</a></li>"+
                    "<li><a href='#' ><i class='fa fa-circle-o text-aqua'></i> Mostrar</a></li>"+
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