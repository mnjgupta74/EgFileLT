<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebCaptcha.ascx.cs" Inherits="UserControls_WebCaptcha" %>



    <style type="text/css">
        form.registration
        {
            width: 100%;
            margin: 10px auto;
            padding: 10px;
            font-family: "Trebuchet MS";
        }
        form.registration fieldset
        {
            background-color: #707070;
            border: none;
            padding: 10px;
            -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -moz-border-radius: 15px;
            -webkit-border-radius: 15px;
            padding: 6px;
            margin: 0px 30px 0px 0px;
        }
        form.registration legend
        {
            text-align: center;
            color: #fff;
            font-size: 14px;
            padding: 0px 4px 15px 4px;
            margin-left: 20px;
            font-weight: bold;
        }
        form.registration label
        {
            font-size: 18px;
            width: 200px;
            float: left;
            text-align: right;
            clear: left;
            margin: 4px 4px 0px 0px;
            padding: 0px;
            color: #FFF;
            text-shadow: 0 1px 1px rgba(0,0,0,0.8);
        }
        form.registration input
        {
            font-family: "Trebuchet MS";
            font-size: 18px;
            float: left;
            width: 300px;
            border: 1px solid #cccccc;
            margin: 2px 0px 4px 2px;
            color: #00abdf;
            height: 26px;
            padding: 3px;
            -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }
        form.registration input:focus, form.registration select:focus
        {
            background-color: #E0E6FF;
        }
        form.registration select
        {
            font-family: "Trebuchet MS";
            font-size: 20px;
            float: left;
            border: 1px solid #cccccc;
            margin: 2px 0px 2px 2px;
            color: #00abdf;
            height: 32px;
            -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }
        .button, .button:visited
        {
            float: right;
            background: #2daebf url(images/overlay.png) repeat-x;
            font-weight: bold;
            display: inline-block;
            padding: 5px 0px 6px;
            color: #fff;
            text-decoration: none;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            text-shadow: 0 -1px 1px rgba(0,0,0,0.25);
            border-bottom: 1px solid rgba(0,0,0,0.25);
            cursor: pointer;
            margin-top: 95px;
            margin-right: 15px;
        }
        .button:hover
        {
            background-color: #007d9a;
        }
        #sortable
        {
            list-style-type: none;
            margin: 5px 16px 0px 16px;
            padding: 0;
        }
        #sortable li
        {
            margin: 3px 3px 3px 65px;
            padding: 5px 0px 3px 0px;
            float: left;
            width: 30px;
            height: 20px;
            font-size: 25px;
            text-align: center;
            line-height: 20px;
            cursor: pointer;
           
           
            -moz-box-shadow: 0 1px 1px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,0.5);
            text-shadow: 0 -1px 1px rgba(0,0,0,0.25);
            background: #2daebf url(images/overlay.png) repeat-x scroll 50% 50%;
            color: #fff;
            font-weight: normal;
            vertical-align:middle;

        }
        .captcha_wrap
        {
            border: 1px solid gray;
            
            
            -moz-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.5);
            float: left;
            height: 60px;
            overflow: auto;
            width: 100%;
            margin-top:5px;
            overflow: hidden;
            background-color: #fff;
        }
        .captcha
        {
            
            font-size: 15px;
            color: #BBBBBB;
            text-align: center;
            border-bottom: 1px solid #CCC;
            background-color: #fff;
        }
    </style>
    <script type="text/javascript">
        (
            function ($) {
                $.fn.shuffle = function () {
                    return this.each(function () {
                        var items = $(this).children();

                        return (items.length)
                            ? $(this).html($.shuffle(items, $(this)))
                        : this;
                    });
                }

                $.fn.validate = function () {
                    var res = false;
                    this.each(function () {
                        var arr = $(this).children();
                        res = ((arr[0].innerHTML == "A") &&
                            (arr[1].innerHTML == "B") &&
                            (arr[2].innerHTML == "C") &&
                            (arr[3].innerHTML == "D"));
                    });
                    return res;
                }

                $.shuffle = function (arr, obj) {
                    for (
                    var j, x, i = arr.length; i;
                    j = parseInt(Math.random() * i),
                    x = arr[--i], arr[i] = arr[j], arr[j] = x
                );
                    if (arr[0].innerHTML == "1") obj.html($.shuffle(arr, obj))
                    else return arr;
                }

            })(jQuery);

            $(function() {
                $("#sortable").sortable();
                $("#sortable").disableSelection();
                $('ul').shuffle();
//                $("#btnSub").click(function() {
                $("#btnSub").mousedown(function() {
                    if ($('ul').validate()) {
                        var num = generaterandomnumber();
                        
                        //                        $("#imgHide").attr('src', "~/Image/captcha.ashx?arg=" + num);
                        $("#imgHide").attr('src', "../../Image/captcha.ashx?arg=" + num);
                        $("#inpHide").val(num);

                    }
                    else {
                        $("#inpHide").val("0");
                        $('ul').shuffle();
                    }
                });
            });
        function generaterandomnumber() {
            var subject = Math.floor(Math.random() * 999999) + 1;
            return subject;
        }
    </script>
    <div class="captcha_wrap">
            <div class="captcha" style="color:Teal;">
               <%-- Drag to order--%>
               Drag And Arrange In Alphabetical Order-A B C D
            </div>
            <ul id="sortable">
                <li class="captchaItem">A</li>
                <li class="captchaItem">B</li>
                <li class="captchaItem">C</li>
                <li class="captchaItem">D</li>
                <%--<li class="captchaItem">E</li>
                <li class="captchaItem">F</li>--%>
            </ul>
        </div>