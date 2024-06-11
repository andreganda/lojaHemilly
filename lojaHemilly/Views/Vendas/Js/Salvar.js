function SalvarCompra() {

    let dataDaVenda = $("#DataCompra").val();
    let clienteId = $("#ClienteId").val();
    let numeroParcelas = $("#NumeroParcelas").val();
    let total = totalCompra2.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    let entrada = $("#ValorEntrada").val();
    let tipoPagamento = $("#TipoFormaPagamento").val();

    // Obtém os dados do formulário
    var formData = {
        DataDaVenda: dataDaVenda,
        ClienteId: clienteId,
        NumeroParcelas: numeroParcelas,
        Total: total,
        Entrada: entrada,
        TipoFormaPagamento: tipoPagamento,
        ItensVenda: GetListaProdutos()
    };
    
    $.ajax({
        type: "POST",
        url: "/Vendas/SalvarVenda",
        dataType: "json",
        async: false,
        data: formData, // Converte os dados para JSON
        //contentType: "application/x-www-form-urlencoded",
        success: function(msg) {

            debugger;
            
        },
        error: function(response) {
            alert(response);
        }
    });


};

function GetListaProdutos(){
    let lista = [];

    for (let index = 0; index < listaProdutos.length; index++) {
        const element = listaProdutos[index];
        if(element!=null){
            lista.push(element);
        }
    }
    return lista;
};