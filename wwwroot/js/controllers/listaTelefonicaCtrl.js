angular.module("listaTelefonica").controller("listaTelefonicaCtrl", function ($scope, contatosAPI, operadorasAPI, serialGenerator, $timeout) {
	// console.log(serialGenerator.generate());
	$scope.app = "Time Control";
	$scope.contatos = [];
	$scope.operadoras = [];
    $scope.counter = 0;
    $scope.stopped = true;
    $scope.buttonText='Go';
    var vm = this;
    vm.Total = 0;
    $scope.onTimeout = function(){
        if(!$scope.stopped){
            $scope.counter++;
            mytimeout = $timeout($scope.onTimeout,1000);
        }
    }

	var carregarContatos = function () {
		contatosAPI.getContatos().success(function (data) {
			$scope.contatos = data;
		}).error(function (data, status) {
			$scope.message = "Aconteceu um problema: " + status;
		});
        $scope.buttonGo = {
                'background-color': 'forestgreen',
                'color': 'white',
                'font-weight': '900',
            };
	};

	var carregarOperadoras = function () {
		operadorasAPI.getOperadoras().success(function (data) {
			$scope.operadoras = data;
		});
	};

	$scope.adicionarContato = function (contato) {
		contato.activityId = serialGenerator.generate();
		// contato.data = new Date();
        // contato.Name = contato.Name.Nome;
        // contato.Time = document.getElementById('tempo').innerText;
        
         
          var tempo = document.getElementById('tempo').innerText;
          tempo = tempo.replace("H ", ":");
          tempo = tempo.replace("M ", ":");
          tempo = tempo.replace("S", "");
          contato.Time = tempo
        //   return  contato.Time= tempo;
        
        // console.log(contato.Time);
		contatosAPI.saveContato(contato).success(function (data) {
			delete $scope.contato;
            document.getElementById('tempo').innerText  = '00H 00M 00S';
			//$scope.contatoForm.$setPristine();
			carregarContatos();
		});
	};
    
    $scope.AddExecutor = function (exe){
        operadorasAPI.postExecutor(exe).success(function (data) {
            delete $scope.exe;
			carregarOperadoras();
		});
    };
    
	$scope.apagarContatos = function (contatos) {
		$scope.contatos = contatos.filter(function (contato) {
			if (!contato.selecionado){ 
                return contato;
            }
            }
            );
	};
    
    $scope.deletarContato = function (contato) {
        console.log(contato.responsibleId);
        contatosAPI.deleteContato(contato).success(function (data) {
			delete $scope.contato;
			$scope.contatoForm.$setPristine();
			carregarContatos();
		});
    }
    
	$scope.isContatoSelecionado = function (contatos) {
		return contatos.some(function (contato) {
			return contato.selecionado;
		});
	};
	$scope.ordenarPor = function (campo) {
		$scope.criterioDeOrdenacao = campo;
		$scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
	};
    
    var mytimeout = $timeout($scope.onTimeout,1000);
    $scope.takeAction = function(){
        if(!$scope.stopped){
            $timeout.cancel(mytimeout);
            $scope.buttonText='Go';
            $scope.buttonGo = {
                'background-color': 'forestgreen',
                'color': 'white',
                'font-weight': '900',
            };
        }
        else
        {
            mytimeout = $timeout($scope.onTimeout,1000);
            $scope.buttonText='Stop';
            $scope.buttonGo = {
                'background-color': 'red',
                'color': 'white',
                'font-weight': '900',
            };
            
        }
            $scope.stopped=!$scope.stopped;
    }
    
    $scope.timeTotal = function(){
        var total = "00:00:00";
        for(var i = 0; i < $scope.contatos.length; i++){
            var hora = $scope.contatos[i];
            total = somaHora(total, hora.Time, false);
        }
        return total;
    }

	carregarContatos();
	// carregarOperadoras();
});

angular.module("listaTelefonica").filter('formatTimer', function() {
  return function(input)
    {
        function z(n) {return (n<10? '0' : '') + n;}
        var seconds = input % 60;
        var minutes = Math.floor(input / 60);
        var hours = Math.floor(minutes / 60);
        return (z(hours) +'H '+z(minutes)+'M '+z(seconds)+'S');
    };
    
    
});

function somaHora(hrA, hrB, zerarHora) {
        // if(hrA.length != 5 || hrB.length != 5) return "00:00";
       
        var temp = 0;
        var nova_h = 0;
        var novo_m = 0;
        var novo_s = 0;
 
        var hora1 = hrA.substr(0, 2) * 1;
        var hora2 = hrB.substr(0, 2) * 1;
        var minu1 = hrA.substr(3, 2) * 1;
        var minu2 = hrB.substr(3, 2) * 1;
        var segu1 = hrA.substr(6, 2) * 1;
        var segu2 = hrB.substr(6, 2) * 1;
        
        temp = segu1 + segu2;
        while(temp > 59) {
            novo_m++;
            temp = temp - 60;
        }
        novo_s = temp.toString().length == 2? temp : ("0" + temp);
       
        temp = minu1 + minu2 + novo_m;
        while(temp > 59) {
                nova_h++;
                temp = temp - 60;
        }
        novo_m = temp.toString().length == 2 ? temp : ("0" + temp);
 
        temp = hora1 + hora2 + nova_h;
        while(temp > 23 && zerarHora) {
                temp = temp - 24;
        }
        nova_h = temp.toString().length == 2 ? temp : ("0" + temp);
 
        return nova_h + ':' + novo_m + ':' + novo_s;
}