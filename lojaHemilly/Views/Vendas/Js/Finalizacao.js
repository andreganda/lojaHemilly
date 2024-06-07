function FinalizarCompra() {
    $("#modal_finalizacao").modal('show');
};

function FormaPagamento() {

    let tipo = $("#TipoFormaPagamento").val();

    if (tipo == 1) {
        $("#divNumeroParcelas").show();
        $("#divEntrada").show();
        $("#divTableParcelas").show();
        CalcularPrecoParcela();
    }else{
        $("#divNumeroParcelas").hide();
        $("#divEntrada").hide();
        $("#divTableParcelas").hide();
        LimparDadosModalPagamento();
    }
};

function CalcularPrecoParcela(){
    let nParcela = parseInt($("#NumeroParcelas").val());
    let valorParcela = (totalCompra/nParcela);
    let data = new Date(GetData());

    $("#tbodyParcelas").empty();

    for (let index = 1; index <= nParcela; index++) {
        
        let dataParcela = new Date(GetData());
        dataParcela.setMonth(dataParcela.getMonth() + index);

        let linha = `
        <tr>
            <th scope="row" class="vertical-align-middle">${index}</th>
            <td class="vertical-align-middle"> ${dataParcela.toLocaleDateString()} </td>
            <td class="vertical-align-middle">${valorParcela.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</td>
        </tr>`;

        $("#tbodyParcelas").append(linha)
    }
};

function GetData(){
    return $("#DataCompra").val();
};

function LimparDadosModalPagamento(){
    $("#divTableParcelas").empty();
    $("#tbodyParcelas").empty();
    $("#divNumeroParcelas").hide();
    $("#divEntrada").hide();
    $("#NumeroParcelas").val(1)
    $("#TipoFormaPagamento").val(2)
    $("#ValorEntrada").val('0,00')
};

function RefazerValorTotal(){

    totalCompra = totalCompra2;
    let valor = $("#ValorEntrada").val().replace(".","");
    valor = valor.replace(",",".");
    valor = parseFloat(valor);

    totalCompra = totalCompra - valor;

    CalcularPrecoParcela();

};