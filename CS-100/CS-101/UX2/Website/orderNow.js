// Button Id's
const signInUpButton = document.querySelector("#SigninoutButton");

const navOrderButton = document.querySelector("#Order_Now_Button_Nav");

function OrderNowButton(){
    console.log("Attempting to login after click!!");


    let loggedIn = GetStoredData("loggedIn");
    if(loggedIn == "" || loggedIn == "false"){
        console.log("NOT logged in");
        // Take to sign up page
        window.open("./SignUp.html","_self");
    }
    else{
        console.log("ALREADY logged in");
        // Take to ordering page
        window.open("./Ordering.html","_self");
    }
}

function GetStoredData(key){
    let data = getCookie(key);
    if(data == null){
      data = "";
    }
    return data;
}

function getCookie(name) {
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
      }
  }
  
  // Return null if not found
  return null;
}

function setCookie(name, value, daysToLive = 365) {
  // Encode value in order to escape semicolons, commas, and whitespace
  var cookie = name + "=" + encodeURIComponent(value);
  
  if (typeof daysToLive === "number") {
      /* Sets the max-age attribute so that the cookie expires
      after the specified number of days */
      cookie += "; max-age=" + (daysToLive*24*60*60);
  }
      
  document.cookie = cookie;
}

console.log(getCookie("loggedIn"));
console.log(getCookie("currentLogin"));

console.log(getCookie("usernames"));
console.log(getCookie("passwords"));

let loggedInT = GetStoredData("loggedIn");
if(loggedInT == "" || loggedInT == "false" || loggedInT == null){
    console.log("NOT logged in");
    signInUpButton.textContent = "Sign In";
}
else{
    console.log("ALREADY logged in");
    signInUpButton.textContent = "Sign Out";
}

function signOutIn(){
  let loggedInTT = GetStoredData("loggedIn");
  if(loggedInTT == "" || loggedInTT == "false" || loggedInTT == null){
    window.open("./Login.html","_self");
  }
  else{
    setCookie("loggedIn", "false");
    setCookie("currentLogin", "false");
    window.open("./Login.html","_self");
  }
}