function SignUp() {

    var email = document.getElementById("email").value.indexOf("@");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var repassword = document.getElementById("reassword").value;

    alert(email + username + password + repassword);
    submit = "true";


    if (password!=repassword) {
        alert("Password does not match each other");
        submit = "false";
    }

    if (username.length > 5 && username.length < 14) {
        alert("The username must have more than 5 characters and less than 14");
        submitOK = "false";
      } 
    
    if (at == -1) {
        alert("Not a valid e-mail!");
        submitOK = "false";
    }
    
    if (submit == "false") {
        return false;
    }

    

}