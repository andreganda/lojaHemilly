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

            if(msg.status == 1){
                Swal.fire({
                    title: "Eba",
                    text: "Venda incluída com sucesso!",
                    icon: "error",
                    confirmButtonText: "OK!",
                    allowOutsideClick: false,  // Evita que o alerta seja fechado ao clicar fora dele
                    allowEscapeKey: false,     // Evita que o alerta seja fechado ao pressionar a tecla "Escape"
                    allowEnterKey: false       // Evita que o alerta seja fechado ao pressionar a tecla "Enter"
                }).then(function(result) {
                    if (result.value) {
                        window.location.replace("/Vendas");
                    }
                });
            }
            
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