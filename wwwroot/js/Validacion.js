function iniciar() {
    setDiaMin();
    //home.addEventListener("input", controlar, false);
    document.home.addEventListener("input", controlar, false)
}
function setDiaMin() {
    let today = new Date();
    let dd = today.getDate();
    let mm = today.getMonth() + 1; //Enero es  0!!!!
    let yyyy = today.getFullYear();

    if (dd < 10) { // Normalizo DIA: Agrego un 0 adelante a los dias menores a 10
        dd = '0' + dd;
    }

    if (mm < 10) { // Normalizo MES: Agrego un 0 adelante a los meses menores a 10
        mm = '0' + mm;
    }

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("t_fecha").setAttribute("min", today); //EL ATRIBUTO MIN ESTA AUTOMATIZADO
}
function controlar(e) {
    var elemento = e.target;
    if (elemento.validity.valid) {
        elemento.style.background = 'white';
    } else {
        elemento.style.background = "#F1BDB1";
    }
}
function ValidarIngreso() {

    {
        const correo = document.querySelector("#correo").value
        if (correo == "") {
            alert("Por favor ingresa tu mail");
            return false;
        }
        else {
            const contrasenia = document.querySelector("#contrasenia").value
            if (contrasenia == "") {
                alert("Por favor ingresa tu contraseña");
                return false;
            }
            else {

                alert("Ingreso Valido");

                }

        
   
}
window.addEventListener("load", iniciar, false);