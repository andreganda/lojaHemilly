function SalvarCompra() {


    let dataDaVenda = $("#DataCompra").val();
    let clienteId = $("#ClienteId").val();
    let numeroParcelas = 0;
    let total = totalCompra;
    let entrada = $("#ValorEntrada").val();

    // Obtém os dados do formulário
    var formData = {
        DataDaVenda: dataDaVenda,
        ClienteId: clienteId,
        NumeroParcelas: numeroParcelas,
        Total: total,
        Entrada: entrada
    };

    debugger;
    
    $.ajax({
        type: "POST",
        url: "/Vendas/SalvarVenda",
        dataType: "json",
        async: false,
        data: JSON.stringify(formData), // Converte os dados para JSON
        contentType: "application/x-www-form-urlencoded",
        success: function(msg) {

            
            
        },
        error: function(response) {
            alert(response);
        }
    });


    debugger;

};