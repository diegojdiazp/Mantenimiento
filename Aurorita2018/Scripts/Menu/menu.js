var idUsuario = document.getElementById("idUsuario").value;
var urlCuadroTareas = document.getElementById("urlCuadroTareas").value;
var urlEstadisticaMensual = document.getElementById("urlEstadisticaMensual").value;
var urlEstadisticaPendientes = document.getElementById("urlEstadisticaPendientes").value;
var urlEstadisticaCulminados = document.getElementById("urlEstadisticaCulminados").value;

$(document).ready(function () {
    //obtenerTareas();
    //ObtenerEstadisticaMensual();

    EstadisticaTiposMantenimientos();
    EstadisticaEstadoEventos();
    EstadisticaAnualUsuario();
    EstadisticaMensual();
    EstadisticaFallasRegistradas();    
});

function obtenerTareas() {
    var url = urlCuadroTareas;
    var usuario = idUsuario;
    var values = { usuario: idUsuario };

    $.post(url, values).done(function (data) {
        if (data !== "sin_datos") {
            var i = 0;
            var fila = '';
            var tabla = '<div class="col-md-12 col-lg-12 col-xl-12">'
                + '<div class="card">'
                + '<div class="card-body">'
                + '<h4 class="box-title">Mis Actividades Pendientes</h4>'
                + '</div>'
                + '<div class="card-body">'
                + '<div class="table-stats order-table ov-h">'
                + '<table class="table">'
                + '<thead>'
                + '<tr>'
                + '<th> ID</th>'
                + '<th>Fecha</th>'
                + '<th>Zona</th>'
                + '<th>Actividad</th>'
                + '<th>Estado</th>'
                + '</tr>'
                + '</thead >'
                + '<tbody>';
            $.each(data, function (key, val) {
                fila = fila + '<tr>'
                    + '<td>' + val.Indice + '</td>'
                    + '<td><span class="name">' + val.Fecha + '</span> </td>'
                    + '<td><span class="product">' + val.NombreZona + '</span></td >'
                    + '<td><span class="count">' + val.Actividad + '</span></td>';
                if (val.Estado === 'CULMINADO') {
                    fila = fila + '<td><span class="badge badge-complete">Culminado</span>'
                        + '</td>';

                } else {
                    fila = fila + '<td><span class="badge badge-pending"><a href="Procesos/RegistrarEvento">Pendiente</a></span>'
                        + '</td>';
                }
                fila = fila + ' </tr>'
                    + '<tr>';
            });
            tabla = tabla + fila + '</tbody ></table>'
                + '</div>'
                + '</div>'
                + '</div>'
                + '</div>';
            $("#misActividades").append(tabla);

            i++;
        }
        else {
            var row = '<td colspan="7" style="text-align:center;" ><h2>No Hay Datos Para Mostrar</h2></td>';


        }
    });
}
function ObtenerEstadisticaMensual() {
    var url = urlEstadisticaMensual;
    var usuario = idUsuario;
    var values = { usuario: idUsuario };
    var meses;
    var cantidades;
    var cantidadesPendientes;
    var cantidadCulminado;
    var cantidadCulminados;
    $.post(url, values).done(function (datos) {
        if (datos !== "sin_datos") {
            var i = 0;
            var mes = "";
            var cantidad = 0;

            $.each(datos, function (key, val) {
                if (i === 0) {
                    mes = val.Descripcion + ', ';
                    cantidad = val.Cantidad + ', ';
                }
                else {
                    mes += val.Descripcion + ', ';
                    cantidad += val.Cantidad + ', ';
                }
                i++;
            });
            cantidades = cantidad.split(',');
            meses = mes.split(',');

        } else {
            document.getElementById("msgEstMensual").innerHTML = "No hay información para mostrar";
        }

        url = urlEstadisticaPendientes;
        usuario = idUsuario;
        values = { usuario: idUsuario };
        $.post(url, values).done(function (datos) {
            if (datos !== "sin_datos") {
                var i = 0;
                var mesPendientes = "";

                $.each(datos, function (key, val) {
                    if (i === 0) {
                        mesPendientes = val.Descripcion + ', ';
                        cantidadPendientes = val.Cantidad + ', ';
                    }
                    else {
                        mesPendientes += val.Descripcion + ', ';
                        cantidadPendientes += val.Cantidad + ', ';
                    }
                    i++;
                });
                cantidadesPendientes = cantidadPendientes.split(',');
                mesesPendientes = mesPendientes.split(',');
                //console.log(cantidadesPendientes);
            }

            url = urlEstadisticaCulminados;
            usuario = idUsuario;
            values = { usuario: idUsuario };
            $.post(url, values).done(function (datos) {
                if (datos !== "sin_datos") {
                    $.each(datos, function (key, val) {
                        if (i === 0) {
                            cantidadCulminado = val.Cantidad + ', ';
                        }
                        else {
                            cantidadCulminado += val.Cantidad + ', ';
                        }
                    });
                    cantidadCulminados = cantidadCulminado.split(',');
                    //console.log(cantidadesPendientes);
                }
                //if ($('#traffic-chart').length) {
                var chart = new Chartist.Line('#traffic-chart', {
                    labels: meses,
                    series: [
                        cantidades,
                        cantidadesPendientes,
                        cantidadCulminados
                    ]
                }, {
                        low: 0,
                        showArea: true,
                        showLine: false,
                        showPoint: false,
                        fullWidth: true,
                        axisX: {
                            showGrid: true
                        }
                    });

                chart.on('draw', function (data) {
                    if (data.type === 'line' || data.type === 'area') {
                        data.element.animate({
                            d: {
                                begin: 4000 * data.index,
                                dur: 5000,
                                from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                                to: data.path.clone().stringify(),
                                easing: Chartist.Svg.Easing.easeOutQuint
                            }
                        });
                    }
                });
            });
        });
    });
}
function prueba() {
    // Pie chart flotPie1 
    var piedata = [
        { label: "Desktop visits", data: [[1, 32]], color: '#5c6bc0' },
        { label: "Tab visits", data: [[1, 33]], color: '#ef5350' },
        { label: "Mobile visits", data: [[1, 35]], color: '#66bb6a' }
    ];
    $.plot('#flotPie1', piedata, {
        series: {
            pie: {
                show: true,
                radius: 1,
                innerRadius: 0.65,
                label: {
                    show: true,
                    radius: 2 / 3,
                    threshold: 1
                },
                stroke: {
                    width: 0
                }
            }
        },
        grid: {
            hoverable: true,
            clickable: true
        }
    });

    // Pie chart flotPie1  End
}
function EstadisticaTiposMantenimientos() {
    var parametros = { usuario: idUsuario }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Home/TiposMantenimientosUsuario",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        success: function (datos) {
            if (datos !== 'sin_datos') {
                CargarGraficoTiposMantenimientos(datos);
            }
            else {
                document.getElementById("msgTiposMant").innerHTML = "No hay información para mostrar";
            }

        },
        error: function () {
            console.log("Error al cargar la Estadística de tipos de mantenimientos del ususario");
        }

    });
}
function EstadisticaEstadoEventos() {
    var parametros = { usuario: idUsuario }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Home/EstadoEnventosUsuario",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        success: function (datos) {
            if (datos !== 'sin_datos') {

                CargarGraficoEstadoEventos(datos);

                //document.getElementById("mensajeGuardarClienteDocumento").innerHTML = datos;
                //toastr.success($("#notificacion_guardarOK").html(), '', { timeOut: 7000, progressBar: true }).attr('style', 'width: 90px !important;height:90px !important;text-align:center;background-color:#0088CC;position:fixed;bottom:35%;right:0%;');
            } else {
                document.getElementById("msgEstados").innerHTML = "No hay información para mostrar";
            }

        },
        error: function () {
            console.log("error");
        }

    });

}
function EstadisticaAnualUsuario() {
    var parametros = { usuario: idUsuario }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Home/EstadisticaAnualUsuario",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        success: function (datos) {
            if (datos !== 'sin_datos') {
                cargaGrafioEstadisticaAnual(datos);
            }

        },
        error: function () {
            console.log("error");
        }

    });
}
function EstadisticaMensual() {
    var parametros = { usuario: idUsuario }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Home/EstadisticaMensualEventos",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        success: function (datos) {
            if (datos !== 'sin_datos') {
                CargarGraficoEventosMensuales(datos);
            }
            else {
                document.getElementById("msgEstMensual").innerHTML = "No hay información para mostrar";
            }

        },
        error: function () {
            console.log("Error al cargar la Estadística de tipos de mantenimientos del ususario");
        }

    });
}
function EstadisticaFallasRegistradas() {
    var parametros = { usuario: idUsuario }
    var CantidadMantenimientos;
    var i = 0;
    $.ajax({
        url: "/Home/FallasRegistradasUsuario",
        data: JSON.stringify(parametros),
        DataType: "JSON",
        contentType: "application/json",
        type: "POST",
        success: function (datos) {
            if (datos !== 'sin_datos') {
                CargarGraficoFallasRegistradas(datos);
            }
            else {
                document.getElementById("msgFallas").innerHTML = "No hay información para mostrar";
            }

        },
        error: function () {
            console.log("Error al cargar la Estadística de tipos de mantenimientos del ususario");
        }

    });

}
function CargarGraficoEstadoEventos(DatosGrafico) {
    var cant;
    var status;
    var i = 0;

    $.each(DatosGrafico, function (key, val) {
        if (i === 0) {
            cantidades = val.Cantidad + ', ';
            status = val.Estado + ', ';
        }
        else {
            cantidades += val.Cantidad + ', ';
            status += val.Estado + ', ';
        }
        i++;
    });
    cant = cantidades.split(',');
    status = status.split(',');
    status.pop();
    //pie chart
    var ctx = document.getElementById("pieChart2");
    ctx.height = 300;
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            datasets: [{
                data: cant,
                backgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ],
                hoverBackgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ]

            }],
            labels: status
        },
        options: {
            responsive: true
        }
    });
}
function CargarGraficoTiposMantenimientos(DatosGrafico) {
    var cantidadesMantenimiento;
    var tipoMantenimiento;
    var i = 0;
    var descripcionMantenimiento;

    $.each(DatosGrafico, function (key, val) {
        if (i === 0) {
            CantidadMantenimientos = val.Cantidad + ', ';
            descripcionMantenimiento = val.Descripcion + ', ';
        }
        else {
            CantidadMantenimientos += val.Cantidad + ', ';
            descripcionMantenimiento += val.Descripcion + ', ';
        }
        i++;
    });
    cantidadesMantenimiento = CantidadMantenimientos.split(',');
    tipoMantenimiento = descripcionMantenimiento.split(',');
    tipoMantenimiento.pop();
    //console.log(tipoMantenimiento);


    //pie chart
    var ctx = document.getElementById("pieChart");
    ctx.height = 300;
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            datasets: [{
                data: cantidadesMantenimiento,
                backgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ],
                hoverBackgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ]

            }],
            labels: tipoMantenimiento
        },
        options: {
            responsive: true
        }
    });
}
function CargarGraficoEventosMensuales(DatosGrafico) {
    var cantidadesMantenimiento;
    var MesMantenimiento;
    var i = 0;
    var descripcionMantenimiento;

    $.each(DatosGrafico, function (key, val) {
        if (i === 0) {
            CantidadMantenimientos = val.Cantidad + ', ';
            descripcionMantenimiento = val.Descripcion + ', ';
        }
        else {
            CantidadMantenimientos += val.Cantidad + ', ';
            descripcionMantenimiento += val.Descripcion + ', ';
        }
        i++;
    });
    cantidadesMantenimiento = CantidadMantenimientos.split(',');
    MesMantenimiento = descripcionMantenimiento.split(',');
    MesMantenimiento.pop();
    //console.log(tipoMantenimiento);

    var ctx = document.getElementById("singelBarChart");
    ctx.height = 150;
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: MesMantenimiento,
            datasets: [
                {
                    label: "Registro Mensual",
                    data: cantidadesMantenimiento,
                    borderColor: "rgba(0, 194, 146, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 194, 146, 0.5)"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function cargaGrafioEstadisticaAnual(DatosGraf) {
    var CantidadxAnio = [];
    var anio = [];
    $.each(DatosGraf, function (key, val) {
        CantidadxAnio.push(val.Cantidad);
        anio.push(val.Descripcion);
    });

    var ctx = document.getElementById("GraficoBarras");
    ctx.height = 150;
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: anio,
            datasets: [
                {
                    label: "Registros por años",
                    data: CantidadxAnio,
                    borderColor: "rgba(0, 194, 146, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 194, 146, 0.5)"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function CargarGraficoFallasRegistradas(DatosGrafico) {
    var cantidadesMantenimiento;
    var tipoMantenimiento;
    var i = 0;
    var descripcionMantenimiento;

    $.each(DatosGrafico, function (key, val) {
        if (i === 0) {
            CantidadMantenimientos = val.Cantidad + ', ';
            descripcionMantenimiento = val.Descripcion + ', ';
        }
        else {
            CantidadMantenimientos += val.Cantidad + ', ';
            descripcionMantenimiento += val.Descripcion + ', ';
        }
        i++;
    });
    cantidadesMantenimiento = CantidadMantenimientos.split(',');
    tipoMantenimiento = descripcionMantenimiento.split(',');
    tipoMantenimiento.pop();

    var ctx = document.getElementById("pieChart3");
    ctx.height = 300;
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            datasets: [{
                data: cantidadesMantenimiento,
                backgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ],
                hoverBackgroundColor: [
                    "rgba(0, 194, 146,0.9)",
                    "rgba(0, 194, 146,0.7)",
                    "rgba(0, 194, 146,0.5)",
                    "rgba(0,0,0,0.07)"
                ]

            }],
            labels: tipoMantenimiento
        },
        options: {
            responsive: true
        }
    });

}
