$('#datepicker').datepicker({ //sirve para mostrar Datepicker
    autoclose: true
  })
function EsCedula(elTexto) {
    var es = true;
    var cadena = elTexto.replace(new RegExp('-','g'),""); // elimina guiones de la cadena
    //validar longitud
    if (elTexto.length != 16) {
        es = false;
    } else {
        //verificar si tiene el formato correcto
        var RegExPattern = /^\d{13}[A-Z]{1}$/;
        if (!cadena.match(RegExPattern)) {
            es = false;
        } else {
            //verificar si contiene una fecha válida
            var sFecha = cadena.substring(3, 9);  
            var sDia = cadena.substring(3, 5); 
            var sMes = cadena.substring(5, 7);
            var sAnio = cadena.substring(7, 9);
            var aa = parseInt(sAnio);
            var mm = parseInt(sMes);
            var dd = parseInt(sDia);
            if (aa >= 0 && aa <= 29) {
                aa += 2000;
            } else {
                aa += 1900;
            }
            var bisiesto = false;
            if (aa % 2 == 0) {
                if (aa % 4 == 0) {
                    bisiesto = true;
                }
            }
            if (mm < 1 || mm > 12) {
                es = false;
            } else {
                switch (mm) {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        if (dd < 1 || dd > 31) {
                            return false;
                        }
                        break;
                    case 2:
                        if (bisiesto) {
                            if (dd < 1 || dd > 29) {
                                es = false;
                            }
                        } else {
                            if (dd < 1 || dd > 28) {
                                es = false;
                            }
                        }
                        break;
                    default:
                        if (dd < 1 || dd > 30) {
                            es = false;
                        }
                        break;
                }
            }
        }
    }
    return es;
}

function ValidarCedula(cadena) {
    if (EsCedula(cadena)) {
        alert('Es n° cedula es correcto!');
        guardar_usuario();
    } else {
        var error="¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra al final, los caracteres del 4to al 9no deben representar una fecha válida!"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );
    }
    return false;
}

function ValidarCedulaDocente(cadena) {
    if (EsCedula(cadena)) {
return true;
    } 
    else {
        var error="¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra al final, los caracteres del 4to al 9no deben representar una fecha válida!"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );
            return false;
    }
    
}

function ValidarCedulaDocenteEditar(cadena) {
    if (EsCedula(cadena)) {
return true;
    } 
    else {
        var error="¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra al final, los caracteres del 4to al 9no deben representar una fecha válida!"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );
            return false;
    }
    
}

function ValidarCedulaTutor(cadena) {
    if (EsCedula(cadena)) {
return true;
    } 
    else {
        var error="¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra al final, los caracteres del 4to al 9no deben representar una fecha válida!"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );
            
            return false;
    }
    
}

function ValidarCedulaTutorEditar(cadena) {
    if (EsCedula(cadena)) {
return true;
    } 
    else {
        var error="¡El n° de cédula no es válido. Deben ser trece dígitos mas una letra al final, los caracteres del 4to al 9no deben representar una fecha válida!"
        $('#mensaje').text(error);   //manda el error a la modal
        $("#Mensaje-error").modal("show"); //abre modal de error
        $("#Mensaje-error").fadeTo(2900,500).slideUp(450,function(){// cierra la modal despues del tiempo determinado  
            $("#Mensaje-error").modal("hide"); // cierra modal error
            } );
            
            return false;
    }
    
}