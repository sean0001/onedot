

/*!
 * jQuery Cookie Plugin v1.4.1
 * https://github.com/carhartl/jquery-cookie
 *
 * Copyright 2013 Klaus Hartl
 * Released under the MIT license
 */
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        // CommonJS
        factory(require('jquery'));
    } else {
        // Browser globals
        factory(jQuery);
    }
}

(function ($) {

    var pluses = /\+/g;

    function encode(s) {
        return config.raw ? s : encodeURIComponent(s);
    }

    function decode(s) {
        return config.raw ? s : decodeURIComponent(s);
    }

    function stringifyCookieValue(value) {
        return encode(config.json ? JSON.stringify(value) : String(value));
    }

    function parseCookieValue(s) {
        if (s.indexOf('"') === 0) {
            // This is a quoted cookie as according to RFC2068, unescape...
            s = s.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, '\\');
        }

        try {
            // Replace server-side written pluses with spaces.
            // If we can't decode the cookie, ignore it, it's unusable.
            // If we can't parse the cookie, ignore it, it's unusable.
            s = decodeURIComponent(s.replace(pluses, ' '));
            return config.json ? JSON.parse(s) : s;
        } catch (e) { }
    }

    function read(s, converter) {
        var value = config.raw ? s : parseCookieValue(s);
        return $.isFunction(converter) ? converter(value) : value;
    }

    var config = $.cookie = function (key, value, options) {

        // Write

        if (value !== undefined && !$.isFunction(value)) {
            options = $.extend({}, config.defaults, options);

            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setTime(+t + days * 864e+5);
            }

            return (document.cookie = [
				encode(key), '=', stringifyCookieValue(value),
				options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
				options.path ? '; path=' + options.path : '',
				options.domain ? '; domain=' + options.domain : '',
				options.secure ? '; secure' : ''
            ].join(''));
        }

        // Read

        var result = key ? undefined : {};

        // To prevent the for loop in the first place assign an empty array
        // in case there are no cookies at all. Also prevents odd result when
        // calling $.cookie().
        var cookies = document.cookie ? document.cookie.split('; ') : [];

        for (var i = 0, l = cookies.length; i < l; i++) {
            var parts = cookies[i].split('=');
            var name = decode(parts.shift());
            var cookie = parts.join('=');

            if (key && key === name) {
                // If second argument (value) is a function it's a converter...
                result = read(cookie, value);
                break;
            }

            // Prevent storing a cookie that we couldn't decode.
            if (!key && (cookie = read(cookie)) !== undefined) {
                result[name] = cookie;
            }
        }

        return result;
    };

    config.defaults = {};

    $.removeCookie = function (key, options) {
        if ($.cookie(key) === undefined) {
            return false;
        }

        // Must not alter options, thus extending a fresh object...
        $.cookie(key, '', $.extend({}, options, { expires: -1 }));
        return !$.cookie(key);
    };

}));






$(function () {
    $(".usercenter-menu-list a").removeClass("list-group-item-default");
    var m = $(".usercenter-menu-list").find("a[data-id='" + $.cookie("ucid") + "']");
    m.addClass("list-group-item-default");
    iniform();


    $("#KinSlideshow").KinSlideshow({ titleBar: { titleBar_height: 20 }, btn: { btn_borderHoverColor: "#fff", btn_bgHoverColor: "#fff" } });
    $("#btnlistID").find("li").html("").css({ "height": "6px", "width": "6px" });
    $("#KinSlideshow").css({ "height": "200px" });


})


function iniform() {

    var d = $("#ids").val(),
        t = $("#cause").val(),
        c = $("#dataClass").val();
    if (!d)
        return;

    var dd = d.split(',');

    $(".checkbox").find("input[type='checkbox']").each(function (index,elem) {
        for (var i = 0; i < dd.length; i++) {

            if (parseInt(dd[i]) == parseInt($(elem).val())) {
                $(elem).prop("checked", true);
            }
        }
    })

    $("textarea[data-class='" + c + "']").val(t);
    $("textarea[data-class='" + c + "']").closest(".submit-area").children("h4").show();

}


function setCookieState($this) {
    $.cookie("ucid", $($this).attr("data-id"), {path:'/'})
 }


function checkDownload($this) {

    var m = parseInt($("#islogin").val());
    if (m == 0) {
        alert("只有注册用户才能申请下载资料！");
        return false;
    }

  
    var a = $("#isAgreement").val();
    if (a == '0') {
        $('#agreement').modal('show');
        return false;
    }
    

    var s = checkArray($this);
    var t = $($this).closest("div").children("textarea").val();
    var c = $($this).closest("div").children("textarea").attr("data-class");
    if (!t) {
        alert("请说明下载输入的原因及用途！")
        return false;
    }





    $("#cause").val(t);
    $("#dataClass").val(c)






    if (s) {
        $("#ids").val(s);
        $("#idsForm").submit();
    } else {

        alert("请先选择要下载的数据！");
    }
    
}



function checkArray($this) {
    //var bb = $($this).closest(".show-border");
    var bb = $($this).closest(".data-area");
    
    var checked = $.map($(bb).find(":checked"), function (checkbox, index) {
        return parseInt($(checkbox).val());
    });
    return checked.toString();
}



function SwitchBlock(d,$t) {

    if (d == 1) {
        $(".s-disclaimer").hide();
        $(".s-summary").show();
    } else {
        $(".s-disclaimer").show();
        $(".s-summary").hide();
    }

    
}





function checkUserState($this,event) {
    var b = $("#userAgreement").is(':checked');
    if (!b) {
        alert("必须先勾选左侧的同意条款！");
        return false;
    }
    var form = $("#agreeForm");
    var formContent = form.serialize();
    var url = form[0].action;
    $.post(url, formContent)
        .done(function (data) {
            if (data.agreement == "1")
                $("#isAgreement").val(1);
                $('#agreement').modal('hide');
        });

}


//$(function () {
//    $('#agreement').on('hidden.bs.modal', function (e) {
//        debugger;
//        var ss = e;
//           //var form = $("#agreeForm");
//           //  var formContent = form.serialize();
//           //  var url = form[0].action;
//           //  $.post(url, formContent)
//           //      .done(function (data) {
//           //          // $(document.body).append("<div class='savedRecordMessage'>success</div>");
//           //          //alert(data.classId + "       " + data.agreement);
//           //          if (data.agreement == "1")
//           //              window.location.href = "/web/datacenter/main/index?classid=" + data.classId;
//           //         // $.growlUI('操作提示信息', '没有更多记录！', 'warncss');
//           //  });
//    })
//})

function showLeaveWord($this) {
    //$("#leaveWordid").val("");
    $('#leaveWord').modal('show')
    $("#leaveWord").off("click");

    $(".btn-default,.close").bind("click", function () {
        $('#leaveWord').modal('hide');
    })
}