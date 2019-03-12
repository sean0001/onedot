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
    setInterval(function () {
        $("#current_datetime_dynamic").text(kendo.toString(new Date(), "yyyy-MM-dd HH:mm:ss dddd","zh-CH"));
    }, 1000)

})



function addToken(data) {
    data.__RequestVerificationToken = $($("input[name=__RequestVerificationToken]")[0]).val();
    return data;
}



//function resizeGrid(grid, offset) {
//    var _offset = offset || 0;
//    //jQuery.noConflict();innerHeight
//    var paneHeight = $("#center-pane").innerHeight();
//    //alert(paneHeight);
//    var GridBarHeight = $("#" + grid + " > .k-grid-toolbar").outerHeight() || 0;
//    var GridHeadHeight = $("#" + grid + " .k-grid-header").outerHeight() || 0;
//    var GridFootHeight = $("#" + grid + " .k-grid-pager").outerHeight() || 0;
//    var GridFootAggregateHeight = $("#" + grid + " .k-footer-template").outerHeight() || 0;
//    $("#" + grid).height(paneHeight - _offset);
//    $("#" + grid + " > .k-grid-content").height(paneHeight - GridFootHeight - GridBarHeight - GridHeadHeight - GridFootAggregateHeight - _offset);
//    //  $.noConflict();
//}



;(function ($) {
    $.extend($.fn, {
        InnerVerticalCenter: function () {
            debugger;
            var parentHeight = $(this).parent().outerHeight(true);
            var top = (parentHeight - $(this).height()) / 2;
            $(this).css("top", top);
        }
    });
})(jQuery)


Array.prototype.contains = function (elem) {
    for (var i in this) {
        if (this[i] == elem) return true;
    }
    return false;
}


Array.prototype.distinct = function () {
    var derivedArray = [];
    for (var i = 0; i < this.length; i += 1) {
        if (!derivedArray.contains(this[i])) {
            derivedArray.push(this[i])
        }
    }
    return derivedArray;
};


Array.prototype.removeByValue = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            this.splice(i, 1);
            break;
        }
    }
}


String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
};

String.prototype.trim = function () {
    // 用正则表达式将前后空格  
    // 用空字符串替代。  
    return this.replace(/(^\s*)|(\s*$)/g, "");
}


function ifContainSpecialSign(fn) {
    if (fn.indexOf(",") >= 0) return true;
    if (fn.indexOf("/") >= 0) return true;
    return false;
}


function processingBar(p) {

    if (typeof p == "boolean") {
        if (p)
            $(".upload-process").show();
        else
            $(".upload-process").hide();
        return false;
    }

    var persent = 5 * p,
        $p = $(".upload-process > div");
    $p.css("width", persent);
    $p.children("span").text(p + "%");
}

function kendoRequestEnd(e) {
    if (e.type == "read") return;

    if (e.response && e.response.Errors == null) {
        $.growlUI('提示信息', '操作请求成功！', 'okcss');
    } else {
        for (var error in e.response.Errors) {
            var k = error;
            var c = e.response.Errors[error].errors;
            $.growlUI(k, c, 'errorcss');
            this.cancelChanges();
        }
    }
}



