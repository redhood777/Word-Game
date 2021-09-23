mergeInto(LibraryManager.library, {
createCookie: function(name, value, days) {

	var date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
	
	var curCookie = name + "=" + value + 
    ", expires=" + date.toGMTString();
	console.log("Cookie Created");
	document.cookie = curCookie;
	console.log(curCookie);
	
},
getCookie: function(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = document.cookie.length;
            }
			console.log(document.cookie.substring(c_start, c_end));
            return unescape(document.cookie.substring(c_start, c_end));
			
        }
    }
    return "";
	console.log("Cookie Fetched");
	window.alert("getCookie working");
},

});

