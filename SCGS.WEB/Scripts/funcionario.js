/*!
 * Bootstrap v3.3.4 (http://www.artproweb.com.br)
 * Copyright 2011-2015 ArtproWeb LTDA.
 * GNU General Public License (https://opensource.org/licenses/GPL-3.0)
 */

$(function () {

    $('body').on("click", '#btnInserir', addContato);

});



function deleteContato(id) {
    var idcontato = id;
    $.ajax({
        url: "/Cadastros/DeletarContato/",
        type: "post",
        data: { "idcontato": idcontato },
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error: ' + textStatus + "msg: " + errorThrown);
        },
        success: function (data, textStatus, XMLHttpRequest) {
            $("#tdcontato").empty();
            $("#tdcontato").append(

            "<table class='table table-bordered table-hover' id='tdcontato'>" +
           "<thead>" +
               "<tr>" +
                   "<th>Tipo Contato</th>" +
                   "<th>Contato</th>" +
                   "<th>Opções</th>" +
               "</tr>" +
           "</thead>" +
           "<tbody>"
       );
            for (var i = 0; i < data.length; i++) {
                $("#tdcontato").append(
                      "<tr>" +
                                "<td>" + data[i].TipoContato + "</td>" +
                                "<td>" + data[i].Contato + "</td>" +
                                "<td>" +
                                    "<button type='button' class='btn btn-danger btn-xs' onclick='deleteContato("+ data[i].Id + ")'title='Deletar campo'>" +
                                        "<span class='glyphicon glyphicon-minus'></span>" +
                                    "</button>" +
                                "</td>" +
                            "</tr>"
                         );
            }
            $("#tdcontato").append(
                "</tbody>" +
                        "</table>");
        }
    });
}



function addContato() {

    var idfuncionario = $("#idfuncionario").val();
    var tipocontato = $("#contato_Tipo").val();
    var contato = $("#contato_Valor").val();


    $.ajax({
        url: "/Cadastros/SalvarContato/",
        type: "post",
        data: { "idfuncionario": idfuncionario, "tipocontato":tipocontato, "contato":contato },
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error: '+textStatus+ "msg: "+errorThrown);
        },
        success: function (data, textStatus, XMLHttpRequest) {
            $("#tdcontato").empty();

                $("#tdcontato").append(

                     "<table class='table table-bordered table-hover' id='tdcontato'>"+
                    "<thead>"+
                        "<tr>"+
                            "<th>Tipo Contato</th>"+
                            "<th>Contato</th>"+
                            "<th>Opções</th>"+
                        "</tr>"+
                    "</thead>"+
                    "<tbody>"
                );
            
                     for (var i = 0; i < data.length; i++) {
                         $("#tdcontato").append(
                               "<tr>" +
                                         "<td>" + data[i].TipoContato + "</td>" +
                                         "<td>" + data[i].Contato + "</td>" +
                                         "<td>" +
                                             "<button type='button' class='btn btn-danger btn-xs' onclick='deleteContato(" + data[i].Id + ")'title='Deletar campo'>" +
                                                 "<span class='glyphicon glyphicon-minus'></span>" +
                                             "</button>" +
                                         "</td>" +
                                     "</tr>"
                                  );
                     }

            $("#tdcontato").append(

            "</tbody>"+
        "</table>"
          );

            
        }
    });

    $('#myModal').modal('hide');

}

  





$(document).ready(function () {
    //Coloque aqui o id do primeiro dropdownlist
    $('#Estado').change(function () {
        //obtém o valor selecionado
        var id = $(this).find(":selected").val();
        $.ajax({
            url: "/Cadastros/PreencherCidades/" + id,
            type: "get",
            dataType: "json",
            success: function (data) {
                $("#Cidade").empty();
                $("#Cidade").append('<option value>Selecione uma Cidade</option>');
                $.each(data, function (index, element) {
                    $("#Cidade").append('<option value="' + element.Id + '">' + element.Nome + '</option>');
                });
            }
        });
    });

});