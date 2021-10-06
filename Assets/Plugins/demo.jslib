mergeInto(LibraryManager.library, {
getCookie :function(a) {
var name = Pointer_stringify(a);
var cookieArr = document.cookie.split(";");
for(var i = 0; i < cookieArr.length; i++) {
    var cookiePair = cookieArr[i].split("=");
    if(name == cookiePair[0].trim()) {
	//window.alert(name); 
   var bufferSize = lengthBytesUTF8(decodeURIComponent(cookiePair[1])) + 1;
   var buffer = _malloc(bufferSize);
   stringToUTF8(decodeURIComponent(cookiePair[1]), buffer, bufferSize);
	return buffer;
    }
}
// Return null if not found
return null;
},

getOtp :function(a) {
var name = Pointer_stringify(a);
var cookieArr = document.cookie.split(";");
for(var i = 0; i < cookieArr.length; i++) {
    var cookiePair = cookieArr[i].split("=");
    if(name == cookiePair[0].trim()) {
	//window.alert(name); 
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