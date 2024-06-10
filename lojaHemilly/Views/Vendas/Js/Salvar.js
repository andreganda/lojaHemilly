function SalvarCompra() {


    let dataDaVenda = $("#DataCompra").val();
    let clienteId = $("#ClienteId").val();
    let numeroParcelas = 0;
    let total = totalCompra2;
    let entrada = $("#ValorEntrada").val();

    // Obtém os dados do formulário
    var formData = {
        DataDaVenda: dataDaVenda,
        ClienteId: clienteId,
        NumeroParcelas: numeroParcelas,
        Total: total,
        Entrada: entrada
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


    debugger;

};