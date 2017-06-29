$("#myTopnav").load("MenuPlugin.html", function () {
    if (sessionStorage.getItem("UserName")) {
        document.getElementById("idRegister").textContent = "Hi " + sessionStorage.getItem("UserName");
        document.getElementById("idRegister").herf = "#";
        document.getElementById("idLogin").textContent = "Log Off";
        document.getElementById("idLogin").onclick = LogOff;
        document.getElementById("idLogin").herf = "HomePage.html"
    }
})

function LogOff() {
    sessionStorage.removeItem("UserName");
}