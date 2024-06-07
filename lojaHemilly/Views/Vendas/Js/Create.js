var listaProdutos = [];
var totalCompra = 0;
var totalCompra2 = 0;

jQuery(document).ready(function () {
    $("#ClienteId").select2();
    SetDataHoje();
});

function AdicionarProduto() {

    if(ValidacaoAdicionarProduto()){

        let descricao = $("#NomeDoProduto").val();
        let quantidade = parseInt($("#Quantidade").val());
        
        let valor = $("#PrecoUnitario").val().replace(".","");
        valor = valor.replace(",",".");
        valor = parseFloat(valor);
        
        let item = {};
        item.Id = listaProdutos.length;
        item.NomeDoProduto = descricao.toUpperCase();
        item.Quantidade = quantidade;
        item.PrecoUnitario = valor;
        item.Total = quantidade * valor;

        listaProdutos.push(item);
        AdicionarProdutoNaTabela(listaProdutos);
    }
   
};

function AdicionarProdutoEnter(event){
    if (event.key === "Enter") {
        AdicionarProduto();
    }
};

function LimparCampos(){
    $("#NomeDoProduto").val("");
     $("#Quantidade").val(1);
     $("#PrecoUnitario").val('0,00');
};

function AdicionarProdutoNaTabela(array){

    $("#tbodyProdutos").empty();
    let valorTotal = 0;

    for (let index = 0; index < array.length; index++) {

        let element = array[index];
        let nome = element.NomeDoProduto;
        let quantidade = element.Quantidade;
        let preco = element.PrecoUnitario;
        let total = element.Total;

        valorTotal+=total;

        let linha = `
        <tr>
            <th scope="row" class="vertical-align-middle">1</th>
            <td class="vertical-align-middle" >${nome}</td>
            <td class="vertical-align-middle" >${preco.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</td>
            <td class="vertical-align-middle" >${quantidade}</td>
            <td class="vertical-align-middle" >${total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</td>
            <td class="vertical-align-middle" ><a href="javascript:RemoverProduto(${index})"><i class="icon-2x text-dark-50 flaticon-cancel"></i><a/></td>
        </tr>`;

        $("#tbodyProdutos").append(linha);
    }

    totalCompra = valorTotal;
    totalCompra2 = valorTotal;

   
    $("#valorTotal").text(`Total Compra: ${valorTotal.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}`);
    $("#h4TotalCompra").text(valorTotal.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));

    LimparCampos();
    $("#NomeDoProduto").focus();
};

function RemoverProduto(indice){
    listaProdutos[indice] = null;
    let listTemporaria = [];
    for (let index = 0; index < listaProdutos.length; index++) {
        let element = listaProdutos[index];
        if(element!=null){
            listTemporaria.push(element);
        }
    }
    listaProdutos = listTemporaria;
    AdicionarProdutoNaTabela(listaProdutos);
};

function FormatarMoeda(i) {
    var v = i.value.replace(/\D/g, '');
    v = (v / 100).toFixed(2) + '';
    v = v.replace(".", ",");
    v = v.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
    i.value = v;
};

function ToastrOptionsSucess(texto1, texto2) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr.success(texto1, texto2);
};

function MensagemErroSucesso(tipo, texto) {
    if (tipo == 2) {
        Swal.fire({
            position: "top-center",
            icon: "error",
            title: texto,
            showConfirmButton: false,
            timer: 4500
        });
    } else {
        Swal.fire({
            position: "top-right",
            icon: "success",
            title: texto,
            showConfirmButton: false,
            timer: 4500
        });
    }
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

    $("#DataCompra").val(today2);
};