angular.module("timeControl").filter('formatLink', function() {
  return function(targ)
    {
        if(targ){
            this.ArrayTexto = targ.split(" ");
            this.tmpChar = "";
            for(var k=0; k<this.ArrayTexto.length; k++) {
                this.idxUrl1 = this.ArrayTexto[k].slice(0,7);
                this.idxUrl2 = this.ArrayTexto[k].slice(0,4);
                this.idxUrl3 = this.ArrayTexto[k].slice(0,8);
                if(this.idxUrl1 != "http://" && this.idxUrl2 != "www." && this.idxUrl3 != "https://"){
                    this.tmpChar += this.ArrayTexto[k] + " ";
                }else{
                    this.Link = "";
                    if (this.idxUrl2 == "www.") this.Link = "http://";
                    this.Link += this.ArrayTexto[k];
                    if (this.Link.charAt(this.Link.length-1) == "."){
                        this.Link = this.Link.substring(0,this.Link.length-1);
                    }
                    this.tmpChar += "<a href=\"" + this.Link + "\" target=\"" + targ + "\">" + this.ArrayTexto[k] + "</a> ";
                }
            }
            return this.tmpChar;
        }
    };
});