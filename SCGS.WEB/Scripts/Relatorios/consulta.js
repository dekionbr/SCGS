var BarChart = null;

$(function () {
    $('.date').datepicker({
        format: 'dd/mm/yyyy'
    });


    $('#submit').click(function (e) {
        e.preventDefault();

        var dtini = $("#dtini").val();
        var dtfim = $("#dtfim").val();
        var corte = $("#corte").val() == "" ? 0 : $("#corte").val();
        var funcionario = $("#funcionario").val();

        $("#rel_consultas").empty().append("<canvas id=\"GraficoBarra\"style=\"width:100%;\"></canvas>");
        var ctx = document.getElementById("GraficoBarra").getContext("2d")


        $.ajax({
            url: "/Relatorios/VisualizaConsultas/",
            type: "post",
            data: { "corte": corte, "dtini": dtini, "dtfim": dtfim, "funcionario": funcionario },
            dataType: "json",

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error: ' + textStatus + " msg: " + errorThrown);
            },

            success: function (data, textStatus, XMLHttpRequest) {
                if (data.datasets.length > 0) {
                    grafico(data, ctx);
                } else {
                    $("#rel_consultas").empty().append("<p>Nenhum resultado para a busca</p>");
                }
            }
        });
    });
});



function AjaxFiltroConsulta(url, q, Element, placeholder) {
    $.ajax({
        url: url,
        type: "post",
        data: q,
        dataType: "json",

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error: ' + textStatus + " msg: " + errorThrown);
        },

        success: function (data, textStatus, XMLHttpRequest) {
            Element.empty().append("<option value=\"\">" + placeholder + "</option>");

            for (var i = 0; i < data.length; i++) {
                Element.append("<option value=\"" + data[i].Id + "\">" + data[i].Nome + "</option>");
            }
        }
    });
}


function grafico(data, ctx) {
    var options = {
        responsive: true,
        tooltipTemplate: "<%if (datasetLabel){%><%=datasetLabel%> : <%}%><%= value %>",
        multiTooltipTemplate: "<%if (datasetLabel){%><%=datasetLabel%> : <%}%><%= value %>",
    };

    //var data = {
    //    labels: label,
    //    datasets: [
    //        {
    //            label: "Dados primários",
    //            fillColor: "rgba(151,187,205,0.2)",
    //            strokeColor: "rgba(151,187,205,1)",
    //            pointColor: "rgba(151,187,205,1)",
    //            pointStrokeColor: "#fff",
    //            pointHighlightFill: "#fff",
    //            pointHighlightStroke: "rgba(151,187,205,1)",
    //            data: value
    //        }
    //    ]
    //};


    if (data.datasets.length == 1)
        BarChart = new Chart(ctx).Bar(data, options);
    else
        BarChart = new Chart(ctx).Line(data, options);

}