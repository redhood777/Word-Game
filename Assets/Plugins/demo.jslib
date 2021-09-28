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
var cookieArr = document.cookie.split(";");
for(var i = 0; i < cookieArr.length; i++) {
    var cookiePair = cookieArr[i].split("=");
    if(name == cookiePair[0].trim()) {
	window.alert(name); 
   var bufferSize = lengthBytesUTF8(decodeURIComponent(cookiePair[1])) + 1;
   var buffer = _malloc(bufferSize);
   stringToUTF8(decodeURIComponent(cookiePair[1]), buffer, bufferSize);
	return buffer;
    }
}
// Return null if not found
return null;
},
});