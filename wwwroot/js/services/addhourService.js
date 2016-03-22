angular.module("timeControl").provider("addHour", function () {
	

	this.$get = function () {
		return {
			addHoras: function (hrA, hrB, zerarHora) {
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

		};
	};
});

