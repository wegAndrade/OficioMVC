function EditDocument() {
    let url = '/Documentos/Edit';
    
      var  doc =
      {
      document:
        {
            Id: getId(),
            Numeracao: getNumeracao(),
            Ano: getAno(),
            Assunto:getAssunto(),
            Observacoes: getObservacoes(),
            Tipo: getTipo(),
            DataEnvio: getDataEnvio(),
            UsuarioId: getUsuarioId()
        } 
      };
    console.log(this.documento);
    
    //var data = new FormData();
    //data.append('file', getArquivo());

    //console.log(`${url}/${getId()}`);
   

    //console.log(data);

    axios.post(`${url}/${doc.documento.Id}`, this.documento)
        .then(function (response) {

            let modal = document.getElementById('myModalSucess');

            console.log(response.data.Usuario.user_nicename);

            let RespostaElement = document.getElementById('Resposta');
            let RespostaText = document.createTextNode(`Documento enviado númeração: ${response.data.Numeracao}/${response.data.Ano}`);
            RespostaElement.appendChild(RespostaText);
            let UsuarioElement = document.getElementById('Usuario');
            let UsuarioText = document.createTextNode(`Usuario de envio: ${response.data.Usuario.user_nicename}`);
            UsuarioElement.appendChild(UsuarioText);
            $(modal).modal('show');



        }).catch(function (error) {
            console.log(error);
            let modal = document.getElementById('myModalError');
            let resposta = error.data;
            $("#message").append(resposta);
            $(modal).modal('show');
        });

}




