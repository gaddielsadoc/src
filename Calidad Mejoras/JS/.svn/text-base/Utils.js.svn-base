function ConfirmaBorrado() {
    return confirm('Atención:\n¿Está seguro que desea borrar el registro seleccionado?');
}

function seleccionar(object, count) {
    var prefix = object.id.substring(0, object.id.lastIndexOf('_') + 1);
    for (i = count; i > 0; i = i - 1) {
        var controlName = prefix + i;
        document.getElementById(controlName).checked = object.checked;
    }
}
