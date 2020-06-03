$(document).ready(function () {
        var contenido_fila;
        var coincidencias;
        var exp;
//Este metodo es el evento keyup del campo de búsqueda
    $("#buscarU").keyup(function () { //captura los valores del imput segun escriba
    if ($(this).val().length >= 1) //verifica si no esta vacio
        filtrar($(this).val()); //invoca al metodo filtrar
    else
        mostrarfilas(); //invoca al metodo para mostrar filas
});

function filtrar(cadena) { //busca coincidencias 
    $("#Usuarios tbody tr").each(function () { //recorre todas las filas de las tablas
        //$(this).removeClass('muestra'); //remueve la clase mostrar
        contenido_fila = $(this).find('td:eq(0)').text(); //verifica si el contenido de la celdas en la columna 0
        exp = RegExp(cadena, 'gi'); //expresion regular
        coincidencias = contenido_fila.match(exp); // establece el valor obtenido en la expresion regular
        if (coincidencias == null) //Si No encuentro coincidencias de búsqueda
            $(this).addClass('oculta');
    })
};
//Este metodo se invoca cuando el campo de búsqueda está vacío, o quiero mostrar
//todos los campos
function mostrarfilas() {
    $("#Usuarios tbody tr").each(function () {
        $(this).removeClass('oculta');
        //$(this).addClass('muestra');
    })
};
 }) //Aquí termina el document.ready



