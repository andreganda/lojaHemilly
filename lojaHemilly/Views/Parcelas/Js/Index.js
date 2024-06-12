jQuery(document).ready(function () {
    SetDataHoje();
});

function AbrirModalParcela(numeroParcela, diasVencido, valorParcela){

    var valorCorrigido = CalcularValorCorrigido(diasVencido,valorParcela);
    $("#ValorParcela").val(valorParcela);


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

function CalcularValorCorrigido(diasVencido,valorParcela){

    let juros = $("#ValorJuros").val().replace(".","");
    juros = juros.replace(",",".");
    juros = parseFloat(juros);

    valorParcela = valorParcela.replace(".","");
    valorParcela = valorParcela.replace(",",".");
    valorParcela = parseFloat(valorParcela).toFixed(2);

    let valorJuros = valorParcela*(juros/100);
    valorJuros = valorJuros*diasVencido;

    let valorCorrigido = (valorJuros + parseFloat(valorParcela)).toFixed(2);

    $("#ValorCorrigido").val(valorCorrigido);


};