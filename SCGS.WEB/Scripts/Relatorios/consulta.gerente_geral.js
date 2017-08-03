$(function () {

    $("#unidade").change(function (e) {
        var $Equipe = $("#equipe"),
            q = { Id: $(this).val() }

        if ($(this).val() != "") {
            AjaxFiltroConsulta("/Relatorios/Equipes/", q, $Equipe, "Selecione a Equipe");
        };
    });


    $("#equipe").change(function (e) {
        var $funcionario = $("#funcionario"),
            q = { Id: $(this).val() }

        //$.ajax({
        //    url: "/Relatorios/FiltraFuncionario/",
        //    type: "post",
        //    data: q,
        //    dataType: "json",

        //    error: function (XMLHttpRequest, textStatus, errorThrown) {
        //        alert('Error: ' + textStatus + " msg: " + errorThrown);
        //    },

        //    success: function (data, textStatus, XMLHttpRequest) {
        //        $funcionario.empty.append("<option value=\"0\">Selecione Equipe</option>");

        //        for (var i = 0; i < data.length; i++) {
        //            $funcionario.append("<option value=\"" + data[i].Id + "\">" + data[i].Nome + "</option>");
        //        }
        //    }
        //});
        if ($(this).val() != "") {
            AjaxFiltroConsulta("/Relatorios/Funcionarios/", q, $funcionario, "Selecione o Profissional");
        }
    });    

});