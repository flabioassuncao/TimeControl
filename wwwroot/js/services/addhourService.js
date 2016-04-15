angular.module("timeControl").provider("functionsForHours", function () {
	
	this.$get = function () {
		return {
			addHoras: function (hrA, hrB, resetHour) {
				var temp = 0;
                var new_h = 0;
                var new_m = 0;
                var new_s = 0;
        
                var hour1 = hrA.substr(0, 2) * 1;
                var hour2 = hrB.substr(0, 2) * 1;
                var minute1 = hrA.substr(3, 2) * 1;
                var minute2 = hrB.substr(3, 2) * 1;
                var second1 = hrA.substr(6, 2) * 1;
                var second2 = hrB.substr(6, 2) * 1;
                
                temp = second1 + second2;
                while(temp > 59) {
                    new_m++;
                    temp = temp - 60;
                }
                new_s = temp.toString().length == 2? temp : ("0" + temp);
            
                temp = minute1 + minute2 + new_m;
                while(temp > 59) {
                        new_h++;
                        temp = temp - 60;
                }
                new_m = temp.toString().length == 2 ? temp : ("0" + temp);
        
                temp = hour1 + hour2 + new_h;
                while(temp > 23 && resetHour) {
                        temp = temp - 24;
                }
                new_h = temp.toString().length == 2 ? temp : ("0" + temp);
        
                return new_h + ':' + new_m + ':' + new_s;
			},
            
            
            transformingSeconds: function (seconds){
                
                function twoHouses(numeral){
                    if (numeral <= 9){
                        numeral = "0" + numeral;
                    }
                    return numeral;
                }

                var hour = twoHouses(Math.round(seconds/3600));
                var minute = twoHouses(Math.floor((seconds%3600)/60));
                var second = twoHouses((seconds%3600)%60);
                        
                var formatted = hour+":"+minute+":"+second;
                        
                return formatted;
            },
            
            formatTime: function(time){
                
                time = time.replace("H ", ":");
                time = time.replace("M ", ":");
                time = time.replace("S", "");
                
                return time;
            },
            
            turningForSeconds: function(startDate, endDate){
                
                var dt1 = moment(startDate, "YYYY/MM/DD hh:mm:ss");
                var dt2 = moment(endDate, "YYYY/MM/DD hh:mm:ss");
                return dt2.diff(dt1, 'seconds');
            },
            
            convertToFormatView: function(x){
                var h = x.hours().toString().length == 2? x.hours() : ("0" + x.hours());
                var m = x.minutes().toString().length == 2? x.minutes() : ("0" + x.minutes());
                var s = x.seconds().toString().length == 2? x.seconds() : ("0" + x.seconds());
                return h + "H " + m + "M " + s + "S";
            }

		};
	};
});

