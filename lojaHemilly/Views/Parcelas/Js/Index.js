var diasVencidoGLOBAL = 0;
var valorParcelaGLOBAL = 0;

jQuery(document).ready(function () {
    SetDataHoje();
});

function AbrirModalParcela(numeroParcela, diasVencido, valorParcela, idParcela) {

    var valorCorrigido = CalcularValorCorrigido(diasVencido, valorParcela);
    $("#ValorParcela").val(valorParcela);
    $("#DiasDeAtraso").val(diasVencido);
    $("#IdParcela").val(idParcela);
    $("#modal_parcela").modal('show');

};

function FormatarMoeda(i) {
    var v = i.value.replace(/\D/g, '');
    v = (v / 100).toFixed(2) + '';
    v = v.replace(".", ",");
    v = v.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
    i.value = v;
};

function SetDataHoje() {

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = dd + '/' + mm + '/' + yyyy;

    let today2 = `${yyyy}-${mm}-${dd}`;

    $("#DataPagamento").val(today2);
};

function CalcularValorCorrigido(diasVencido, valorParcela) {

    let juros = $("#ValorJuros").val().replace(".", "");
    juros = juros.replace(",", ".");
    juros = parseFloat(juros);

    valorParcela = valorParcela.replace(".", "");
    valorParcela = valorParcela.replace(",", ".");
    valorParcela = parseFloat(valorParcela).toFixed(2);

    let valorJuros = valorParcela * (juros / 100);
    valorJuros = valorJuros * diasVencido;

    let valorCorrigido = (valorJuros + parseFloat(valorParcela)).toFixed(2);

    $("#ValorCorrigido").val(valorCorrigido);

};

function CalcularValorCorrigidoChange() {
    let juros = $("#ValorJuros").val().replace(".", "");
    juros = juros.replace(",", ".");
    juros = parseFloat(juros);

    let valorParcela = $("#ValorParcela").val().replace(".", "");
    valorParcela = valorParcela.replace(".", "");
    valorParcela = valorParcela.replace(",", ".");
    valorParcela = parseFloat(valorParcela).toFixed(2);

    let diasVencido = $("#DiasDeAtraso").val();

    let valorJuros = valorParcela * (juros / 100);
    valorJuros = valorJuros * diasVencido;

    let valorCorrigido = (valorJuros + parseFloat(valorParcela)).toFixed(2);

    $("#ValorCorrigido").val(valorCorrigido);
};

function Receber() {

    let valorPagamento = $("#ValorPagamento").val();
    let dataPagamento = $("#DataPagamento").val();
    let idParcela = $("#IdParcela").val();

    let juros = $("#ValorJuros").val().replace(".", "");
    juros = juros.replace(",", ".");
    juros = parseFloat(juros);

    // Obtém os dados do formulário
    var formData = {
        Valor: valorPagamento,
        IdParcela: idParcela,
        DataPagamento: dataPagamento,
        Juros: juros
    };

    $.ajax({
        type: "POST",
        url: "/Parcelas/EfetuarPagamento",
        dataType: "json",
        async: false,
        data: formData,
        success: function (msg) {


            if (msg.status == 1) {
                Swal.fire({
                    title: "Eba",
                    text: msg.descricao,
                    icon: "success",
                    confirmButtonText: "OK!",
                    allowOutsideClick: false,  // Evita que o alerta seja fechado ao clicar fora dele
                    allowEscapeKey: false,     // Evita que o alerta seja fechado ao pressionar a tecla "Escape"
                    allowEnterKey: false       // Evita que o alerta seja fechado ao pressionar a tecla "Enter"
                }).then(function (result) {
                    if (result.value) {

                        location.reload();
                    }
                });

            }

        },
        error: function (response) {
            alert(response);
        }
    });
};

function AbrirModalDetalhesParcela(idParcela) {
    $("#modal_detalhes_parcela").modal('show');

    let param = {};
    param.idParcela = idParcela;

    $.ajax({
        type: "GET",
        url: "/Parcelas/DetalharHistorico",
        // dataType: "json",
        // async: false,
        data: param,
        // contentType: "application/json; charset=utf-8",
        success: function (msg) {

            $("#tbodyDetalhesPagamento").html(msg);

        },
        error: function (response) {
            alert(response);
        }
    });
};