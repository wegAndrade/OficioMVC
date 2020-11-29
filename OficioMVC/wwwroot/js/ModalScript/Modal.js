function abrirModal(modal){
  $(modal).modal('show');
}

function fecharModal(modal,history){
$(modal).modal('hide');
if(history == true){
  window.history.back();
}
}