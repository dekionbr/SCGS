$(function () {

    $("#equipe").click(function (e) {
        var $funcionario = $("#funcionario"),
            q = { Id: $(this).val() }

        if($(this).val() != ""){
            AjaxFiltroConsulta("/Relatorios/FiltraFuncionario/", q, $funcionario, "Selecione Equipe");
        }
    });

});