<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebCaptchal.ascx.cs" Inherits="UserControls_WebCaptchal" %>

<%@ OutputCache Duration=20 VaryByParam="None" %>
<script type="text/javascript" src="../js/jquery-3.6.0.min.js"></script>

<script type="text/javascript" charset="utf-8">

    function generaterandomnumber() {
        var subject = Math.floor(Math.random() * 999999) + 1;
        return subject;
    }
    //          jQuery.noConflict();
    $(function() { imageload(); });
    function imageload() {
        
        var pic = randomNumber();
        var flag = 0;
        var defaults =
             {
                 captchaDir: "",
                 items: Array("Book", "Scissors", "Pencil", "Music", "Heart", "Clock")
             };
        var options = $.extend(defaults, options);
        var rand = options.items[pic];
        //            $("span").text(rand);

        //            var used = Array();
        //            var image = "Select:<span>" + rand + "</span><img id='boxA' src=\"" + options.captchaDir + "/imgs/circle.png\"/></br>";
        //            for (var i = 0; i < 5; i++) {

        //                image += "<img id='draggable_" + i + "' name=" + options.items[i] + " src=\"" + options.captchaDir + "/imgs/item-" + options.items[i] + ".png\"/>";
        //                used[i] = options.items[i];
        //            };


        var image = ShowImage(rand, options);
        $("#form1").find("#main").html(image);

        var imgName = $("#draggable_" + pic + "").attr('name');

        $("#draggable_0").draggable({ containment: 'parent', revert: 'invalid', cursor: 'hand' });
        $("#draggable_1").draggable({ containment: 'parent', revert: 'invalid', cursor: 'hand' });
        $("#draggable_2").draggable({ containment: 'parent', revert: 'invalid', cursor: 'hand' });
        $("#draggable_3").draggable({ containment: 'parent', revert: 'invalid', cursor: 'hand' });
        $("#draggable_4").draggable({ containment: 'parent', revert: 'invalid', cursor: 'hand' });

        //                   $("#imgServerControl").draggable();
        //            $("#boxA").droppable({ drop: function() { $(this).find("#draggable_0").attr('name'); } });
        //                      $("#boxA").droppable({ drop: function(event, ui) { ui.draggable.draggable("disable", 1); } });
        $("#boxA").droppable({
            drop: function(event, ui) {
                if (rand == ui.draggable.draggable().attr('name')) {
                    $("#draggable_" + pic).draggable("disable");
                    var num = generaterandomnumber();
                    $("#imgHide").attr('src', "Image/captcha.ashx?arg=" + num);
                    $("#inpHide").val(num);
                }
                else {
                    $("#inpHide").val("0");
                    imageload();
                    //                        location.reload();
                }

            },
            tolerance: 'touch'
        });
    }



    //        });

    function randomNumber() {
        var chars = "01234";
        chars += ".";
        var size = 1;
        var i = 1;
        var ret = "";
        //		while ( i <= size ) {
        $max = chars.length - 1;
        $num = Math.floor(Math.random() * $max);
        $temp = chars.substr($num, 1);
        ret = $temp;
        //			i++;
        //		}
        return ret;
    }
    function ShowImage(rand, options) {
        var used = Array();
        var image = "<FONT COLOR=#BBBBDD ><span STYLE='font-family: Arial;font-size: 16px; color:Black ;text-align:left;vertical-align:top;width:100px;'>";
        image += "Select:</span><span STYLE='font-family: Arial;font-size: 16px; color:Teal;text-align:left;vertical-align:top;width:100px;'>";
        image += "" + rand + "</span></FONT><img id='boxA' width='50' height='50' src=\"Image/circle1.gif\"/></br>";
        for (var i = 0; i < 5; i++) {
            var a = check(used);
            image += "<img id='draggable_" + a + "' name=" + options.items[a] + " src=\"Image/item-" + options.items[a] + ".gif\"/>";
            used[i] = a;

        };
        return image;
    }
    function check(arr) {
        var no = randomNumber();
        var aray = Array();
        aray = arr;
        var value;
        var flag = 0;
        if (aray.length != 0) {
            for (var a = 0; a < 5; a++) {
                if (aray[a] == no) {
                    flag = 1;
                    break;
                }

            }
            if (flag == 0) {
                value = no;
            }
            else {
                value = check(aray);
            }
        }
        else {
            value = no;
        }
        return value;
    }
        
</script>
    
     <div id="main">
     <%--<img id="aa" runat="server" src="~/images/circle1.gif"--%>
     </div>
   