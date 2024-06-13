jQuery(document).ready(function () {

    var idCliente = getUrlVars()["idCliente"];
    $("#idCliente").select2();

    if(idCliente!="" && idCliente != undefined){
        $("#idCliente").val(idCliente).trigger('change');
    }


});


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

    $("#startDate").val(today2);
    $("#endDate").val(today2);
};

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars()
{
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}