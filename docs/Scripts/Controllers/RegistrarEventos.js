$(document).ready(function () {
    $("#datetimepicker6").datetimepicker({
        language: 'ES',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1
    });
    $("#datetimepicker7").datetimepicker({
        language: 'ES',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1
    });
    $('#btnGuardar').on("click", function () {
        if (ValidarRegristro()) {
            GuardarEvento();
        } else {
            console.log("no valido");
        }
    });
    $("#btnOT").on("click", function () {
        GenerarOT();
    });
    $('#selectZonas').on("change", function () {
        if ($('#selectZonas').val() !== -1) {
            $('#selectZonas').css("border-color", "green");
        } else {
            $('#selectZonas').css("border-color", "red");
        }
    })
    $('#selectFalla').on("change", function () {
        if ($('#selectFalla').val() !== -1) {
            $('#selectFalla').css("border-color", "green");
        } else {
            $('#selectFalla').css("border-color", "red");
        }
    })
    $('#selectEstado').on("change", function () {
        if ($('#selectEstado').val() !== -1) {
            $('#selectEstado').css("border-color", "green");
        } else {
            $('#selectEstado').css("border-color", "red");
        }
    })
    $('#selectMantenimiento').on("change", function () {
        if ($('#selectMantenimiento').val() !== -1) {
            $('#selectMantenimiento').css("border-color", "green");
        } else {
            $('#selectMantenimiento').css("border-color", "red");
        }
    })
    $('#textareaActividadAsignada').on("change", function () {
        if ($('#textareaActividadAsignada').val() === "") {
            $('#textareaActividadAsignada').css("border-color", "red");
        }
        else {
            $('#textareaActividadAsignada').css("border-color", "green");
        }
    })
    $('#textareaActividadRealizada').on("change", function () {
        if ($('#textareaActividadRealizada').val() === "") {
            $('#textareaActividadRealizada').css("border-color", "red");
        } else {
            $('#textareaActividadRealizada').css("border-color", "green");
        }

    })
    $('#datetimepicker7').on("change", function () {
        CalcularTiempoEmpleado();
        if ($('#txtTiempoEmpleado').val() === "") {
            $('#txtTiempoEmpleado').css("border-color", "red");
        } else {
            $('#txtTiempoEmpleado').css("border-color", "green");
        }
    })


});
function GuardarEvento() {
    var parametros = $("#formEvento").serializeArray();
    $.ajax({
        url: "/Procesos/RegistroEvento",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        async: false,
        success: function (datos) {
            if (datos === 'GuardadoOK') {
                document.getElementById("msg_notificacion_tiempo").innerHTML = "Registro Guardado Correctamente.";
                toastr.success($("#notificacion_tiempo").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 300px !important;height:90px !important;text-align:center;background-color:#0088CC;position:fixed;bottom:20%;right:0%;');
                $("#btnOT").attr("disabled", false);
            }
            else {
                if (datos === 'error') {
                    //document.getElementById("msgTiempo").innerHTML = "No hay información para mostrar";
                    document.getElementById("msg_notificacion_tiempo").innerHTML = "Ocurrió un error al guardar. No es posible en estos momentos.";
                    toastr.error($("#notificacion_tiempo").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 400px !important;height:120px !important;text-align:center;background-color:red;position:fixed;bottom:20%;right:0%;');
                }
                else {
                    window.location('~/Login/index');
                }
            }
        },
        error: function () {
            document.getElementById("msg_notificacion_tiempo").innerHTML = "Ocurrió un error al calcular el tiempo empleado.";
            toastr.error($("#notificacion_tiempo").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 200px !important;height:120px !important;text-align:center;background-color:red;position:fixed;bottom:20%;right:0%;');
        }
    });


}
function GenerarOT() {

    $.ajax({
        url: "/Procesos/GenerarPdfOT",
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        async: false,
        success: function (datos) {
            console.log("listo");
        },
        error: function () {
            console.log("error");
        }
    });
}
function ValidarRegristro() {
    var formularioValido = true;
    if ($('#selectZonas').val() == -1) {
        formularioValido = false;
        $('#selectZonas').css("border-color", "red");
        $('#selectZonas').focus();
    }
    if ($('#selectEstado').val() == -1) {
        formularioValido = false;
        $('#selectEstado').css("border-color", "red");
        $('#selectEstado').focus();
    }
    if ($('#selectMantenimiento').val() == -1) {
        formularioValido = false;
        $('#selectMantenimiento').css("border-color", "red");
        $('#selectMantenimiento').focus();
    }
    if ($('#selectFalla').val() == -1) {
        formularioValido = false;
        $('#selectFalla').css("border-color", "red");
        $('#selectFalla').focus();
    }
    if ($('#selectFalla').val() == -1) {
        formularioValido = false;
        $('#selectFalla').css("border-color", "red");
        $('#selectFalla').focus();
    }
    if ($('#txtTiempoEmpleado').val() == "") {
        formularioValido = false;
        $('#txtTiempoEmpleado').css("border-color", "red");
        $('#txtTiempoEmpleado').focus();
    }
    //if ($('#datetimepicker6').data('date') == " ") {
    //    formularioValido = false;
    //    $('#datetimepicker6').css("border-color", "red");
    //    $('#datetimepicker6').focus();
    //}
    if ($('#textareaActividadAsignada').val() == "") {
        formularioValido = false;
        $('#textareaActividadAsignada').css("border-color", "red");
        $('#textareaActividadAsignada').focus();
    }
    if ($('#textareaActividadRealizada').val() == "") {
        formularioValido = false;
        $('#textareaActividadRealizada').css("border-color", "red");
        $('#textareaActividadRealizada').focus();
    }

    return formularioValido;
}
function CalcularTiempoEmpleado() {
    var parametros = {
        fechaInicio: $('#datetimepicker6').data('date'),
        fechaFin: $('#datetimepicker7').data('date')
    }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Procesos/CalcularTiempoEmpleado",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        async: false,
        success: function (datos) {
            if (datos !== 'sin_datos') {
                $('#txtTiempoEmpleado').val(datos);
            }
            else {
                //document.getElementById("msgTiempo").innerHTML = "No hay información para mostrar";
                document.getElementById("msg_notificacion_tiempo").innerHTML = "El evento no puede culminar antes de empezar. <br/> Rango de fechas invalidas.";
                toastr.error($("#notificacion_tiempo").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 400px !important;height:120px !important;text-align:center;background-color:red;position:fixed;bottom:20%;right:0%;');
            }
        },
        error: function () {
            document.getElementById("msg_notificacion_tiempo").innerHTML = "Ocurrió un error al calcular el tiempo empleado.";
            toastr.error($("#notificacion_tiempo").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 200px !important;height:120px !important;text-align:center;background-color:red;position:fixed;bottom:20%;right:0%;');
        }

    });
}

