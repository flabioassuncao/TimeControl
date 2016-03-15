angular.module("listaTelefonica").factory("contatosAPI", function ($http, config) {
	var _getContatos = function () {
		return $http.get(config.baseUrl + "/Activity");
	};

	var _saveContato = function (contato) {
        //Console.log(contato.Name);
		return $http.post(config.baseUrl + "/Activity", contato);
	};
    
    var _deleteContato = function(contato){
        // console.log(config.baseUrl + "/Responsible/" + contato.responsibleId);
        return $http.delete(config.baseUrl + "/Activity/" + contato.activityId);
    }

	return {
		getContatos: _getContatos,
		saveContato: _saveContato,
        deleteContato: _deleteContato
	};
});