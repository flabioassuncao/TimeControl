angular.module("timeControl").filter('betweenDate', function($filter) {
   return function(collection, column, startDate, endDate) {
        var new_collection = [];
    if (angular.isDefined(startDate) && angular.isDefined(endDate)) {
       if (startDate != '' && endDate != '') {
          if (angular.isDefined(startDate)) {
             startDate = Date.parse($filter('date')(startDate, 'yyyy-MM-dd HH:mm:ss'));
          }
          if (angular.isDefined(endDate)) {
             endDate = Date.parse($filter('date')(endDate, 'yyyy-MM-dd HH:mm:ss'));
          }
          if (!isNaN(startDate) && !isNaN(endDate)) {
              angular.forEach(collection, function (value, index) {
              var obj = value[column];
              var currentDate = Date.parse($filter('date')(obj, 'yyyy-MM-dd HH:mm:ss'));
                 if ((currentDate >= startDate &&  endDate >= currentDate)) {
                    new_collection.push(value);
                 }
              });
          } else {
            new_collection = collection;
          }
      } else {
        new_collection = collection;
      }
      collection = new_collection;
    }
  return collection;
  };
  
}); 