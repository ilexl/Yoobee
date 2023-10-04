const usernameInput = document.querySelector("#username");
const passwordInput = document.querySelector("#password");
const rePasswordInput = document.querySelector("#repassword");

const SYMBOLSPLIT = '☒';

function TrySignUp(){
  let testUserName = usernameInput.value;
  let testPassword = passwordInput.value;
  let testRePassword = rePasswordInput.value;

  if(testUserName == "DELETEALL"){
    // Error the user
    alert("All cookies are now deleted (:");
    deleteCookies();
  }

  if(testUserName == ""){
    // Error the user
    alert("Please enter a user name (:");
  }
  else{
    // Continue Check
    if(CheckUserNameMatch(testUserName)){
      // true == match
      alert("User name already taken :(");
    }
    else{
      // User name doesnt exist yet
      if(testPassword != testRePassword || testPassword == ""){
        alert("Passwords are invalid :(");
      }
      else{
        // User name doesn't exist, is real and passwords match
        CreateUser(testUserName, testPassword);
        login(testUserName, testPassword);
      }
    }      
  }
}

function CheckUserNameMatch(str){
  let cookie = getCookie("usernames");
  if(cookie == null){
    return false;
  }
  let userNames = cookie.split(SYMBOLSPLIT);

  for(let userName in userNames){
    if(userNames[userName] == str){
      return true;
    }
  }
  return false;
}

function CreateUser(userName, Password){
  let usernames = getCookie("usernames");
  let passwords = getCookie("passwords");

  if(usernames == null || passwords == null || usernames == "" || passwords == ""){
    setCookie("usernames", userName);
    setCookie("passwords", Password);
  }
  else{
    if(usernames.length == 0 || passwords.length==0){
      setCookie("usernames", userName);
      setCookie("passwords", Password);
    }
    else{
      setCookie("usernames", getCookie("usernames") + SYMBOLSPLIT + userName);
      setCookie("passwords", getCookie("passwords") + SYMBOLSPLIT + Password);
    }
  }
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

function deleteCookies(){
  document.cookie.split(';').forEach(function(c) {
    document.cookie = c.trim().split('=')[0] + '=;' + 'expires=Thu, 01 Jan 1970 00:00:00 UTC;';
  });
}

function login(username, password){
  if(getCookie("loggedIn") == "true"){
    alert("Already logged in (:");
    window.open("./Ordering.html","_self");
  }
  else{

    
    let usernames = getCookie("usernames");
    let passwords = getCookie("passwords");

    if(usernames == null || passwords == null){
      alert("Logged in Failed!", "\nCheck login details are correct...");
      
    }
    else{
      let ua = usernames.split(SYMBOLSPLIT);
      let pa = passwords.split(SYMBOLSPLIT);
      
      let userFound = false;
      let passwordCorrect = false;
      for(let user in ua){
        if(ua[user] == username){
          userFound = true;
          if(pa[user] == password){
            passwordCorrect = true;
          }
          else{

          }
        }
      }

      if(userFound && passwordCorrect){
        alert("Logged in Successfully!", "\nWelcome back", username);
        setCookie("loggedIn", "true");
        setCookie("currentLogin", username + SYMBOLSPLIT + password);
        window.open("./Ordering.html","_self");
      }
      else{
        alert("Logged in Failed!", "\nCheck login details are correct...");
      }
      

    }
  }
}

function TrySignIn(){
  let testUserName = usernameInput.value;
  let testPassword = passwordInput.value;

  login(testUserName, testPassword);
}


console.log(getCookie("loggedIn"));
console.log(getCookie("currentLogin"));

console.log(getCookie("usernames"));
console.log(getCookie("passwords"));

// if(getCookie("loggedIn")){
//   setCookie("loggedIn", "");
// }
// if(getCookie("currentLogin")){
//   setCookie("currentLogin", "");
// }

// if(getCookie("usernames")){
//   setCookie("usernames", "");
// }
// if(getCookie("passwords")){
//   setCookie("passwords", "");
// }