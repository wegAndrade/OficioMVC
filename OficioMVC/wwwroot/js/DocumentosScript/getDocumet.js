
function GetToken() {
    let Token = document.getElementById('RequestVerificationToken').value;

    return Token;
}
function getId(){
    return document.getElementById('input-id').value;
}
function getAno(){
    return document.getElementById('input-ano').value;
}
function getNumeracao(){
    return document.getElementById('input-numeracao').value;
}
function getAssunto(){
 return document.getElementById('input-assunto').value;
}
function getObservacoes(){
    return document.getElementById('input-obs').value;
}
function getTipo(){
    return document.getElementById('input-tipo').value;
}
function getDataEnvio(){
    return document.getElementById('input-dataEnvio').value;
}
function getUsuarioId(){
    return document.getElementById('input-usuarioId').value;
}
function getArquivo(){
    return document.getElementById('input-arquivo').files[0];
}