

function DecimalNumber(el, hdn) {

    // alert(hdn);
    // alert('hi')
    // var ex = /^[0-9]+\.?[0-9]*$/;
    //  document.getElementById('HiddenTextAmt').value = el.value;
    //document.getElementById('<%=HiddenTextAmt.ClientID%>').value = el.value;
    // alert(hdn.value);

    hdn.value  = Math.pow((parseFloat(el.value)) ,1.1)+19;

   // hdn.value = parseFloat(el.value);
    var ex = /^\d*\.?\d{0,2}$/; // for 2 digits after decimal
    if (el.value != "") {
        if (ex.test(el.value) == false) {
            alert('Incorrect Number');
            el.value = "";
        }
    }
}

