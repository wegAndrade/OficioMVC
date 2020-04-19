//import Document from './Document.js';

function GetToken() {
    let Token = document.querySelector('body form input[name="RequestVerificationToken"').value;

    return Token;
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


function SendDocument() {
    let url = '/Documentos/Create';
    let doc =
    { 
    document:
        {
        Assunto: getAssunto(),
        Observacoes: getObservacoes(),
        Tipo: getTipo()

        }
    }    
    axios.post(url,doc.document , headers = { 'RequestVerificationToken': GetToken() })
        .then(function (response) {

            let modal = document.getElementById('myModalSucess');
            
            console.log(response);

            $('.result').append(`Seu documento de númeração ${response.data.id} e descrição: ${response.data.description} foi enviado as ${response.data.sendDate}`);
            console.log(modal);
            $(modal).modal('show');



        }).catch(function (error) {
            let modal = document.getElementById('myModalError');
            let resposta = error.data;
            $("#message").append(resposta);
            $(modal).modal('show');
        });

}



class Document
{
 constructor (assunto, tipo,observacoes){
     this.assunto = assunto;
     this.tipo = tipo;
     this.observacoes = observacoes;
 }
}