/*
    Autor: Julio Cesar 
*/

(function ($, undefined) {
    "use strict";
    /*global document, window, jQuery, console */



    $.fn.ajaxSCGSquery = function () {
        $.ajax({
            url: "/Relatorios/FiltraFuncionario/",
            type: "post",
            data: q,
            dataType: "json",

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error: ' + textStatus + " msg: " + errorThrown);
            },

            success: function (data, textStatus, XMLHttpRequest) {
                $funcionario.empty.append("<option value=\"0\">Selecione Equipe</option>");

                for (var i = 0; i < data.length; i++) {
                    $funcionario.append("<option value=\"" + data[i].Id + "\">" + data[i].Nome + "</option>");
                }
            }
        });
    }

    $.fn.ajaxSCGSquery.defaults = {
        url: "/",
        data: {},
        Element: null,
        placeholder: "Selecione",
        success: function (data, textStatus, XMLHttpRequest) {
            Element.empty.append("<option value=\"0\">Selecione Equipe</option>");

            for (var i = 0; i < data.length; i++) {
                $funcionario.append("<option value=\"" + data[i].Id + "\">" + data[i].Nome + "</option>");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error: ' + textStatus + " msg: " + errorThrown);
        }
    }
}(jQuery));