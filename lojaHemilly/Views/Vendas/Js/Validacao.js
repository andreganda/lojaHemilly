function ValidacaoAdicionarProduto() {

    let descricao = $("#NomeDoProduto").val();
    let quantidade = $("#Quantidade").val();
    let valor = $("#PrecoUnitario").val();
    

    if (descricao.trim().length == 0) {
        MensagemErroSucesso(2, "Insira uma descrição ao produto");
        return false;
    }

    if (descricao.trim().length <= 5) {
        MensagemErroSucesso(2, "Insira uma descrição com mais de 5 (CINCO) letras ao produto");
        return false;
    }

    if (quantidade <= 0) {
        MensagemErroSucesso(2, "Insira uma quantidade válida, ACIMA DE ZERO");
        return false;
    }

    return true;

};