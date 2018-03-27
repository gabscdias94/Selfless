$("#Salvar").click(function () {
    var dadosSerializados = $('#formNovo').serialize();
    $.ajax({
        type: "POST",
        url: "/Home/Novo",
        data: dadosSerializados,
        success: function () {
            Mensagem("success", "Cadastrado com Sucesso!");
        },
        error: function () {
            Mensagem("danger", "Erro ao cadastrar!");
        }
    });
});



$(function () {
    var displayMessage = function (message, msgType) {
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
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr[msgType](message);
    };

    if ($('#success').val()) {
        displayMessage($('#success').val(), 'success');
    }
    if ($('#info').val()) {
        displayMessage($('#info').val(), 'info');
    }
    if ($('#warning').val()) {
        displayMessage($('#warning').val(), 'warning');
    }
    if ($('#error').val()) {
        displayMessage($('#error').val(), 'error');
    }
});





function LimparFormulario() {
    //Limpar formulario
    //pega todos os itens do form e limpa os campos
    $('#formNovo').each(function () {
        this.reset();
    });
}

//Recebe a classe css para o tipo, seja sucesso ou erro.
// passamos como parâmetro SUCCESS(para sucesso) ou DANGER(para erro),
//conforme o bootstrap
function Mensagem(stringCss, mensagem) {
    //caso exista uma mensagem, ele remove
    $("#mensagem").remove();

    //serve para limitar um tempo para aparecer a nova mensagem
    setTimeout(function () {
        //serve este passo pode não ser a melhor abortagem,
        //fazemos assim, para que tenha uma familharidade maior com o JQuery.
        $('#formNovo').append("<div class='alert alert-" + stringCss + "' id=mensagem role=alert>" + mensagem + "</div>");
    }, 10);
}