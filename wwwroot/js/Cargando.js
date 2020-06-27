$(document).ready(function () {
    var screen = $('#Cargando'); //obtiene modal Cargando
    configureLoadingScreen(screen); //llamada a metodo usando AJAX
})

function configureLoadingScreen(screen) {  // metodo para mostrar Loader
    $(document)
        .ajaxStart(function () { //muestra imagen
            screen.modal("show");
        })
        .ajaxStop(function () { //oculta imagen
            screen.modal("hide");
        });
}