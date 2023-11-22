//function DateCheck(date1,date2)
//{
//    alert(date1.value);
//    alert(date2.value);
//}

  /*head ckeck validation */
  
        function headcheck(head,fromDate,ToDate) {

            var ddL1 = head;
            var fdate = fromDate;
            var tdate =ToDate;

            var txtzero = head.value;
            var txtzero1 = fromDate.value;
            var txtzero2 = ToDate.value;

            if (txtzero1.length != 10) {
                alert('Please Enter Correct From Date ');
                return false;
            }

            if (txtzero2.length != 10) {
                alert('Please Enter Correct To Date ');
                return false;
            }

            if (txtzero.length != 4 && txtzero.length != 13) {
                alert('Please Enter Major Head / Complete Budget Head');
                return false;
            }
        }
    
 /* number only validation*/
        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }

function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;

            if (evt.keyCode == 46) return (parts.length == 1);

            if (parts[0].length >= 14) return false;

            if (parts.length == 3 && parts[1].length >= 3) return false;
        }
        function DisplayTable() {
            document.getElementById("trRevenue").style.display = "none";
        }

function dateValidation(FromDate , ToDate) {
            var dtObj = FromDate;
            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt < 2009) {
                alert('Year cannot be less than 2009')
                dtObj.value = ""
                return false
            }
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)

            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
           
            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = (parseInt(dtStr.substring(3, 5), 10)-1 );
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1); // from Date
            
            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = (parseInt(s.substring(3, 5), 10)-1);
            var yr2 = parseInt(s.substring(6, 10), 10);
            var date2 = new Date(yr2, mon2, dt2); // Current Date
            
            //compairing from date with current date
            if (date2 < date1) 
            {
                alert("Fromdate cannot be greater than current date");
                dtObj.value = ""
                return false
            }
            //end
            
            //compairing from date with To date
            var toDate = ToDate;
            var toDateStr = toDate.value;
            
            if(toDateStr != null && toDateStr != "")
            {
                var toDateday =  parseInt(toDateStr.substring(0, 2), 10);
                var toDateMonth = (parseInt(toDateStr.substring(3, 5), 10)-1);
                var toDateYear = parseInt(toDateStr.substring(6, 10), 10);
                var date3 = new Date(toDateYear, toDateMonth, toDateday); // To Date
                
                if(date3 < date1)
                {
                    alert("Fromdate cannot be greater than to Todate");
                    dtObj.value = ""
                    return false
                }
            }
            //end
        }


function dateValidation1(ToDate ,FromDate) {
            var dtObj = ToDate;
            var dtStr = dtObj.value
            var dtTemp = dtStr

            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }

            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }
            //check for parts of date
            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt < 2009) {
                alert('year cannot be less than 2009')
                dtObj.value = ""
                return false
            }
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }

            //alert("Day :" + DayDt + " Mon:" + MonDt + " Year:" + YearDt)
            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }
            var DateEntered = new Date()
            DateEntered.setFullYear(YearDt, parseInt(MonDt - 1), DayDt)


            if (DateEntered.getMonth() != (parseInt(MonDt - 1))) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }


            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 =( parseInt(dtStr.substring(3, 5), 10)-1);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1); // To Date
            
           
            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = (parseInt(s.substring(3, 5), 10)-1);
            var yr2 = parseInt(s.substring(6, 10), 10); // Current Date
            
            // Compairing To Date with Current Date
            var date2 = new Date(yr2, mon2, dt2);
            if (date2 < date1) {
                alert("Todate cannot be greater than current date");
                dtObj.value = ""
                return false
            }
            //End
            
            //compairing from date with To date
            var fromDate =FromDate;
            var fromDateStr = fromDate.value;
            
            if(fromDateStr != null && fromDateStr != "")
            {
                var fromDateday =  parseInt(fromDateStr.substring(0, 2), 10);
                var fromDateMonth = (parseInt(fromDateStr.substring(3, 5), 10)-1);
                var fromDateYear = parseInt(fromDateStr.substring(6, 10), 10);
                var date3 = new Date(fromDateYear, fromDateMonth,fromDateday ); // From Date
                
                if(date3 > date1)
                {
                    alert("ToDate cannot be lesser than  FromDate");
                    dtObj.value = ""
                    return false
                }
            }
            //end 
        }



