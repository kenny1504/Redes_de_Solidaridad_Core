// script que llama a otro al script general y verifica las reglas de validacion

$(document).ready(function () {
    Redes.validacionGeneral('ingresar_grado');
    Redes.validacionGeneral('Editar_grado');
});