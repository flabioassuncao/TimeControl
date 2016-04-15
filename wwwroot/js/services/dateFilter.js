angular.module("timeControl").filter('formatDate', function() {
  return function(input)
    {
        return input.substring(0, 10);
    };
});