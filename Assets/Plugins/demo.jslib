mergeInto(LibraryManager.library, {
setCookie: function(a, b, expiredays) {
var c_name = Pointer_stringify(a);
var value = Pointer_stringify(b);
var exdate = new Date();
exdate.setDate(exdate.getDate() + expiredays);
document.cookie = c_name + "=" + value + ";path=/" + ((expiredays ==null) ? "" : ";expires=" + exdate.toGMTString());
},
getCookie :function(a) {
var name = Pointer_stringify(a);
// Split cookie string and get all individual name=value pairs in an array
var cookieArr = document.cookie.split(";");
// Loop through the array elements
for(var i = 0; i < cookieArr.length; i++) {
    var cookiePair = cookieArr[i].split("=");
    /* Removing whitespace at the beginning of the cookie name
    and compare it with the given string */
    if(name == cookiePair[0].trim()) {
        // Decode the cookie value and return
        return decodeURIComponent(cookiePair[1]);
		window.alert(cookiePair[1].toString());
       return cookiePair[1].toString();
    }
}
// Return null if not found
return null;
},
getCookie1 :function(a) {
var name = Pointer_stringify(a);
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
},
getCookie2 : function()
{
return document.cookie;
},
});