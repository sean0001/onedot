/*!
 * jQuery blockUI plugin
 * Version 2.66.0-2013.10.09
 * Requires jQuery v1.7 or later
 *
 * Examples at: http://malsup.com/jquery/block/
 * Copyright (c) 2007-2013 M. Alsup
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
 *
 * Thanks to Amir-Hossein Sobhi for some excellent contributions!
 */

; (function () {
    /*jshint eqeqeq:false curly:false latedef:false */
    "use strict";

    function setup($) {
        $.fn._fadeIn = $.fn.fadeIn;

        var noOp = $.noop || function () { };

        // this bit is to ensure we don't call setExpression when we shouldn't (with extra muscle to handle
        // confusing userAgent strings on Vista)
        var msie = /MSIE/.test(navigator.userAgent);
        var ie6 = /MSIE 6.0/.test(navigator.userAgent) && ! /MSIE 8.0/.test(navigator.userAgent);
        var mode = document.documentMode || 0;
        var setExpr = $.isFunction(document.createElement('div').style.setExpression);

        // global $ methods for blocking/unblocking the entire page
        $.blockUI = function (opts) { install(window, opts); };
        $.unblockUI = function (opts) { remove(window, opts); };

        // convenience method for quick growl-like notifications  (http://www.google.com/search?q=growl)
        $.growlUI = function (title, message, growcss, timeout, onClose) {
         
           
            var $m = $('<div id="promptGrowUI" class="' + growcss + '"  style=""></div>');
            if (title) $m.append('<span class="closebtn"></span><h2>' + title + '</h2>');
            if (message) {
                //console.info(message.length,message);
                if (message[0].length > 24) {
                    $m.append('<p style="font-size:1em;">' + message + '</p>');
                } else {
                    $m.append('<h1>' + message + '</h1>');
                }
             
            }
            
            if (timeout === undefined) timeout = 5000;

            // Added by konapun: Set timeout to 30 seconds if this growl is moused over, like normal toast notifications
            var callBlock = function (opts) {
                opts = opts || {};

                $.blockUI({
                    message: $m,
                    fadeIn: typeof opts.fadeIn !== 'undefined' ? opts.fadeIn : 300,
                    fadeOut: typeof opts.fadeOut !== 'undefined' ? opts.fadeOut : 1200,
                    timeout: typeof opts.timeout !== 'undefined' ? opts.timeout : timeout,
                    centerY: false,
                    showOverlay: false,
                    onUnblock: onClose,
                    css: $.blockUI.defaults.growlCSS
                });
            };

            callBlock();
            var nonmousedOpacity = $m.css('opacity');
            $m.mouseover(function () {
                callBlock({
                    fadeIn: 0,
                    timeout: 30000
                });

                var displayBlock = $('.blockMsg');
                displayBlock.stop(); // cancel fadeout if it has started
                displayBlock.fadeTo(300, 1); // make it easier to read the message by removing transparency
            }).mouseout(function () {
                $('.blockMsg').fadeOut(1000);
            });
            // End konapun additions
        };

        // plugin method for blocking element content
        $.fn.block = function (opts) {
            if (this[0] === window) {
                $.blockUI(opts);
                return this;
            }
            var fullOpts = $.extend({}, $.blockUI.defaults, opts || {});
            this.each(function () {
                var $el = $(this);
                if (fullOpts.ignoreIfBlocked && $el.data('blockUI.isBlocked'))
                    return;
                $el.unblock({ fadeOut: 0 });
            });

            return this.each(function () {
                if ($.css(this, 'position') == 'static') {
                    this.style.position = 'relative';
                    $(this).data('blockUI.static', true);
                }
                this.style.zoom = 1; // force 'hasLayout' in ie
                install(this, opts);
            });
        };

        // plugin method for unblocking element content
        $.fn.unblock = function (opts) {
            if (this[0] === window) {
                $.unblockUI(opts);
                return this;
            }
            return this.each(function () {
                remove(this, opts);
            });
        };

        $.blockUI.version = 2.66; // 2nd generation blocking at no extra cost!

        // override these in your code to change the default behavior and style
        $.blockUI.defaults = {
            // message displayed when blocking (use null for no message)
            message: '<h1>Please wait...</h1>',

            title: null,		// title string; only used when theme == true
            draggable: true,	// only used when theme == true (requires jquery-ui.js to be loaded)

            theme: false, // set to true to use with jQuery UI themes

            // styles for the message when blocking; if you wish to disable
            // these and use an external stylesheet then do this in your code:
            // $.blockUI.defaults.css = {};
            css: {
                padding: 0,
                margin: 0,
                width: '30%',
                top: '40%',
                left: '35%',
                textAlign: 'center',
                color: '#000',
                border: '3px solid #aaa',
                backgroundColor: '#fff',
                cursor: 'wait'
            },

            // minimal style set used when themes are used
            themedCSS: {
                width: '30%',
                top: '40%',
                left: '35%'
            },

            // styles for the overlay
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.6,
                cursor: 'wait'
            },

            // style to replace wait cursor before unblocking to correct issue
            // of lingering wait cursor
            cursorReset: 'default',

            // styles applied when using $.growlUI
            growlCSS: {
                width: 'auto',
                top: '2px',
                left: '',
                right: '4px',
                border: 'none',
                padding: '5px',
                opacity: 0.5,
                cursor: 'default',
                color: '#fff',
                backgroundColor: '#000',
                '-webkit-border-radius': '4px',
                '-moz-border-radius': '4px',
                'border-radius': '4px'
            },

            // IE issues: 'about:blank' fails on HTTPS and javascript:false is s-l-o-w
            // (hat tip to Jorge H. N. de Vasconcelos)
            /*jshint scripturl:true */
            iframeSrc: /^https/i.test(window.location.href || '') ? 'javascript:false' : 'about:blank',

            // force usage of iframe in non-IE browsers (handy for blocking applets)
            forceIframe: false,

            // z-index for the blocking overlay
            baseZ: 100000,

            // set these to true to have the message automatically centered
            centerX: true, // <-- only effects element blocking (page block controlled via css above)
            centerY: true,

            // allow body element to be stetched in ie6; this makes blocking look better
            // on "short" pages.  disable if you wish to prevent changes to the body height
            allowBodyStretch: true,

            // enable if you want key and mouse events to be disabled for content that is blocked
            bindEvents: true,

            // be default blockUI will supress tab navigation from leaving blocking content
            // (if bindEvents is true)
            constrainTabKey: true,

            // fadeIn time in millis; set to 0 to disable fadeIn on block
            fadeIn: 200,

            // fadeOut time in millis; set to 0 to disable fadeOut on unblock
            fadeOut: 400,

            // time in millis to wait before auto-unblocking; set to 0 to disable auto-unblock
            timeout: 0,


            growcss:"okcss",

            // disable if you don't want to show the overlay
            showOverlay: true,

            // if true, focus will be placed in the first available input field when
            // page blocking
            focusInput: true,

            // elements that can receive focus
            focusableElements: ':input:enabled:visible',

            // suppresses the use of overlay styles on FF/Linux (due to performance issues with opacity)
            // no longer needed in 2012
            // applyPlatformOpacityRules: true,

            // callback method invoked when fadeIn has completed and blocking message is visible
            onBlock: null,

            // callback method invoked when unblocking has completed; the callback is
            // passed the element that has been unblocked (which is the window object for page
            // blocks) and the options that were passed to the unblock call:
            //	onUnblock(element, options)
            onUnblock: null,

            // callback method invoked when the overlay area is clicked.
            // setting this will turn the cursor to a pointer, otherwise cursor defined in overlayCss will be used.
            onOverlayClick: null,

            // don't ask; if you really must know: http://groups.google.com/group/jquery-en/browse_thread/thread/36640a8730503595/2f6a79a77a78e493#2f6a79a77a78e493
            quirksmodeOffsetHack: 4,

            // class name of the message block
            blockMsgClass: 'blockMsg',

            // if it is already blocked, then ignore it (don't unblock and reblock)
            ignoreIfBlocked: false
        };

        // private data and functions follow...

        var pageBlock = null;
        var pageBlockEls = [];

        function install(el, opts) {
            var css, themedCSS;
            var full = (el == window);
            var msg = (opts && opts.message !== undefined ? opts.message : undefined);
            opts = $.extend({}, $.blockUI.defaults, opts || {});

            if (opts.ignoreIfBlocked && $(el).data('blockUI.isBlocked'))
                return;

            opts.overlayCSS = $.extend({}, $.blockUI.defaults.overlayCSS, opts.overlayCSS || {});
            css = $.extend({}, $.blockUI.defaults.css, opts.css || {});
            if (opts.onOverlayClick)
                opts.overlayCSS.cursor = 'pointer';

            themedCSS = $.extend({}, $.blockUI.defaults.themedCSS, opts.themedCSS || {});
            msg = msg === undefined ? opts.message : msg;

            // remove the current block (if there is one)
            if (full && pageBlock)
                remove(window, { fadeOut: 0 });

            // if an existing element is being used as the blocking content then we capture
            // its current place in the DOM (and current display style) so we can restore
            // it when we unblock
            if (msg && typeof msg != 'string' && (msg.parentNode || msg.jquery)) {
                var node = msg.jquery ? msg[0] : msg;
                var data = {};
                $(el).data('blockUI.history', data);
                data.el = node;
                data.parent = node.parentNode;
                data.display = node.style.display;
                data.position = node.style.position;
                if (data.parent)
                    data.parent.removeChild(node);
            }

            $(el).data('blockUI.onUnblock', opts.onUnblock);
            var z = opts.baseZ;

            // blockUI uses 3 layers for blocking, for simplicity they are all used on every platform;
            // layer1 is the iframe layer which is used to supress bleed through of underlying content
            // layer2 is the overlay layer which has opacity and a wait cursor (by default)
            // layer3 is the message content that is displayed while blocking
            var lyr1, lyr2, lyr3, s;
            if (msie || opts.forceIframe)
                lyr1 = $('<iframe class="blockUI" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0" src="' + opts.iframeSrc + '"></iframe>');
            else
                lyr1 = $('<div class="blockUI" style="display:none"></div>');

            if (opts.theme)
                lyr2 = $('<div class="blockUI blockOverlay ui-widget-overlay" style="z-index:' + (z++) + ';display:none"></div>');
            else
                lyr2 = $('<div class="blockUI blockOverlay" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;width:100%;height:100%;top:0;left:0"></div>');

            if (opts.theme && full) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage ui-dialog ui-widget ui-corner-all" style="z-index:' + (z + 10) + ';display:none;position:fixed">';
                if (opts.title) {
                    s += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>';
                }
                s += '<div class="ui-widget-content ui-dialog-content"></div>';
                s += '</div>';
            }
            else if (opts.theme) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement ui-dialog ui-widget ui-corner-all" style="z-index:' + (z + 10) + ';display:none;position:absolute">';
                if (opts.title) {
                    s += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>';
                }
                s += '<div class="ui-widget-content ui-dialog-content"></div>';
                s += '</div>';
            }
            else if (full) {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage" style="z-index:' + (z + 10) + ';display:none;position:fixed"></div>';
            }
            else {
                s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement" style="z-index:' + (z + 10) + ';display:none;position:absolute"></div>';
            }
            lyr3 = $(s);

            // if we have a message, style it
            if (msg) {
                if (opts.theme) {
                    lyr3.css(themedCSS);
                    lyr3.addClass('ui-widget-content');
                }
                else
                    lyr3.css(css);
            }

            // style the overlay
            if (!opts.theme /*&& (!opts.applyPlatformOpacityRules)*/)
                lyr2.css(opts.overlayCSS);
            lyr2.css('position', full ? 'fixed' : 'absolute');

            // make iframe layer transparent in IE
            if (msie || opts.forceIframe)
                lyr1.css('opacity', 0.0);

            //$([lyr1[0],lyr2[0],lyr3[0]]).appendTo(full ? 'body' : el);
            var layers = [lyr1, lyr2, lyr3], $par = full ? $('body') : $(el);
            $.each(layers, function () {
                this.appendTo($par);
            });

            if (opts.theme && opts.draggable && $.fn.draggable) {
                lyr3.draggable({
                    handle: '.ui-dialog-titlebar',
                    cancel: 'li'
                });
            }

            // ie7 must use absolute positioning in quirks mode and to account for activex issues (when scrolling)
            var expr = setExpr && (!$.support.boxModel || $('object,embed', full ? null : el).length > 0);
            if (ie6 || expr) {
                // give body 100% height
                if (full && opts.allowBodyStretch && $.support.boxModel)
                    $('html,body').css('height', '100%');

                // fix ie6 issue when blocked element has a border width
                if ((ie6 || !$.support.boxModel) && !full) {
                    var t = sz(el, 'borderTopWidth'), l = sz(el, 'borderLeftWidth');
                    var fixT = t ? '(0 - ' + t + ')' : 0;
                    var fixL = l ? '(0 - ' + l + ')' : 0;
                }

                // simulate fixed position
                $.each(layers, function (i, o) {
                    var s = o[0].style;
                    s.position = 'absolute';
                    if (i < 2) {
                        if (full)
                            s.setExpression('height', 'Math.max(document.body.scrollHeight, document.body.offsetHeight) - (jQuery.support.boxModel?0:' + opts.quirksmodeOffsetHack + ') + "px"');
                        else
                            s.setExpression('height', 'this.parentNode.offsetHeight + "px"');
                        if (full)
                            s.setExpression('width', 'jQuery.support.boxModel && document.documentElement.clientWidth || document.body.clientWidth + "px"');
                        else
                            s.setExpression('width', 'this.parentNode.offsetWidth + "px"');
                        if (fixL) s.setExpression('left', fixL);
                        if (fixT) s.setExpression('top', fixT);
                    }
                    else if (opts.centerY) {
                        if (full) s.setExpression('top', '(document.documentElement.clientHeight || document.body.clientHeight) / 2 - (this.offsetHeight / 2) + (blah = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + "px"');
                        s.marginTop = 0;
                    }
                    else if (!opts.centerY && full) {
                        var top = (opts.css && opts.css.top) ? parseInt(opts.css.top, 10) : 0;
                        var expression = '((document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + ' + top + ') + "px"';
                        s.setExpression('top', expression);
                    }
                });
            }

            // show the message
            if (msg) {
                if (opts.theme)
                    lyr3.find('.ui-widget-content').append(msg);
                else
                    lyr3.append(msg);
                if (msg.jquery || msg.nodeType)
                    $(msg).show();
            }

            if ((msie || opts.forceIframe) && opts.showOverlay)
                lyr1.show(); // opacity is zero
            if (opts.fadeIn) {
                var cb = opts.onBlock ? opts.onBlock : noOp;
                var cb1 = (opts.showOverlay && !msg) ? cb : noOp;
                var cb2 = msg ? cb : noOp;
                if (opts.showOverlay)
                    lyr2._fadeIn(opts.fadeIn, cb1);
                if (msg)
                    lyr3._fadeIn(opts.fadeIn, cb2);
            }
            else {
                if (opts.showOverlay)
                    lyr2.show();
                if (msg)
                    lyr3.show();
                if (opts.onBlock)
                    opts.onBlock();
            }

            // bind key and mouse events
            bind(1, el, opts);

            if (full) {
                pageBlock = lyr3[0];
                pageBlockEls = $(opts.focusableElements, pageBlock);
                if (opts.focusInput)
                    setTimeout(focus, 20);
            }
            else
                center(lyr3[0], opts.centerX, opts.centerY);

            if (opts.timeout) {
                // auto-unblock
                var to = setTimeout(function () {
                    if (full)
                        $.unblockUI(opts);
                    else
                        $(el).unblock(opts);
                }, opts.timeout);
                $(el).data('blockUI.timeout', to);
            }
        }

        // remove the block
        function remove(el, opts) {
            var count;
            var full = (el == window);
            var $el = $(el);
            var data = $el.data('blockUI.history');
            var to = $el.data('blockUI.timeout');
            if (to) {
                clearTimeout(to);
                $el.removeData('blockUI.timeout');
            }
            opts = $.extend({}, $.blockUI.defaults, opts || {});
            bind(0, el, opts); // unbind events

            if (opts.onUnblock === null) {
                opts.onUnblock = $el.data('blockUI.onUnblock');
                $el.removeData('blockUI.onUnblock');
            }

            var els;
            if (full) // crazy selector to handle odd field errors in ie6/7
                els = $('body').children().filter('.blockUI').add('body > .blockUI');
            else
                els = $el.find('>.blockUI');

            // fix cursor issue
            if (opts.cursorReset) {
                if (els.length > 1)
                    els[1].style.cursor = opts.cursorReset;
                if (els.length > 2)
                    els[2].style.cursor = opts.cursorReset;
            }

            if (full)
                pageBlock = pageBlockEls = null;

            if (opts.fadeOut) {
                count = els.length;
                els.stop().fadeOut(opts.fadeOut, function () {
                    if (--count === 0)
                        reset(els, data, opts, el);
                });
            }
            else
                reset(els, data, opts, el);
        }

        // move blocking element back into the DOM where it started
        function reset(els, data, opts, el) {
            var $el = $(el);
            if ($el.data('blockUI.isBlocked'))
                return;

            els.each(function (i, o) {
                // remove via DOM calls so we don't lose event handlers
                if (this.parentNode)
                    this.parentNode.removeChild(this);
            });

            if (data && data.el) {
                data.el.style.display = data.display;
                data.el.style.position = data.position;
                if (data.parent)
                    data.parent.appendChild(data.el);
                $el.removeData('blockUI.history');
            }

            if ($el.data('blockUI.static')) {
                $el.css('position', 'static'); // #22
            }

            if (typeof opts.onUnblock == 'function')
                opts.onUnblock(el, opts);

            // fix issue in Safari 6 where block artifacts remain until reflow
            var body = $(document.body), w = body.width(), cssW = body[0].style.width;
            body.width(w - 1).width(w);
            body[0].style.width = cssW;
        }

        // bind/unbind the handler
        function bind(b, el, opts) {
            var full = el == window, $el = $(el);

            // don't bother unbinding if there is nothing to unbind
            if (!b && (full && !pageBlock || !full && !$el.data('blockUI.isBlocked')))
                return;

            $el.data('blockUI.isBlocked', b);

            // don't bind events when overlay is not in use or if bindEvents is false
            if (!full || !opts.bindEvents || (b && !opts.showOverlay))
                return;

            // bind anchors and inputs for mouse and key events
            var events = 'mousedown mouseup keydown keypress keyup touchstart touchend touchmove';
            if (b)
                $(document).bind(events, opts, handler);
            else
                $(document).unbind(events, handler);

            // former impl...
            //		var $e = $('a,:input');
            //		b ? $e.bind(events, opts, handler) : $e.unbind(events, handler);
        }

        // event handler to suppress keyboard/mouse events when blocking
        function handler(e) {
            // allow tab navigation (conditionally)
            if (e.type === 'keydown' && e.keyCode && e.keyCode == 9) {
                if (pageBlock && e.data.constrainTabKey) {
                    var els = pageBlockEls;
                    var fwd = !e.shiftKey && e.target === els[els.length - 1];
                    var back = e.shiftKey && e.target === els[0];
                    if (fwd || back) {
                        setTimeout(function () { focus(back); }, 10);
                        return false;
                    }
                }
            }
            var opts = e.data;
            var target = $(e.target);
            if (target.hasClass('blockOverlay') && opts.onOverlayClick)
                opts.onOverlayClick(e);

            // allow events within the message content
            if (target.parents('div.' + opts.blockMsgClass).length > 0)
                return true;

            // allow events for content that is not being blocked
            return target.parents().children().filter('div.blockUI').length === 0;
        }

        function focus(back) {
            if (!pageBlockEls)
                return;
            var e = pageBlockEls[back === true ? pageBlockEls.length - 1 : 0];
            if (e)
                e.focus();
        }

        function center(el, x, y) {
            var p = el.parentNode, s = el.style;
            var l = ((p.offsetWidth - el.offsetWidth) / 2) - sz(p, 'borderLeftWidth');
            var t = ((p.offsetHeight - el.offsetHeight) / 2) - sz(p, 'borderTopWidth');
            if (x) s.left = l > 0 ? (l + 'px') : '0';
            if (y) s.top = t > 0 ? (t + 'px') : '0';
        }

        function sz(el, p) {
            return parseInt($.css(el, p), 10) || 0;
        }

    }


    /*global define:true */
    if (typeof define === 'function' && define.amd && define.amd.jQuery) {
        define(['jquery'], setup);
    } else {
        setup(jQuery);
    }

})();


























//! moment.js
//! version : 2.4.0
//! authors : Tim Wood, Iskren Chernev, Moment.js contributors
//! license : MIT
//! momentjs.com
(function (a) {
    function b(a, b) {
        return function (c) {
            return i(a.call(this, c), b)
        }
    }

    function c(a, b) {
        return function (c) {
            return this.lang().ordinal(a.call(this, c), b)
        }
    }

    function d() { }

    function e(a) {
        u(a), g(this, a)
    }

    function f(a) {
        var b = o(a),
            c = b.year || 0,
            d = b.month || 0,
            e = b.week || 0,
            f = b.day || 0,
            g = b.hour || 0,
            h = b.minute || 0,
            i = b.second || 0,
            j = b.millisecond || 0;
        this._input = a, this._milliseconds = +j + 1e3 * i + 6e4 * h + 36e5 * g, this._days = +f + 7 * e, this._months = +d + 12 * c, this._data = {}, this._bubble()
    }

    function g(a, b) {
        for (var c in b) b.hasOwnProperty(c) && (a[c] = b[c]);
        return b.hasOwnProperty("toString") && (a.toString = b.toString), b.hasOwnProperty("valueOf") && (a.valueOf = b.valueOf), a
    }

    function h(a) {
        return 0 > a ? Math.ceil(a) : Math.floor(a)
    }

    function i(a, b) {
        for (var c = a + ""; c.length < b;) c = "0" + c;
        return c
    }

    function j(a, b, c, d) {
        var e, f, g = b._milliseconds,
            h = b._days,
            i = b._months;
        g && a._d.setTime(+a._d + g * c), (h || i) && (e = a.minute(), f = a.hour()), h && a.date(a.date() + h * c), i && a.month(a.month() + i * c), g && !d && bb.updateOffset(a), (h || i) && (a.minute(e), a.hour(f))
    }

    function k(a) {
        return "[object Array]" === Object.prototype.toString.call(a)
    }

    function l(a) {
        return "[object Date]" === Object.prototype.toString.call(a) || a instanceof Date
    }

    function m(a, b, c) {
        var d, e = Math.min(a.length, b.length),
            f = Math.abs(a.length - b.length),
            g = 0;
        for (d = 0; e > d; d++) (c && a[d] !== b[d] || !c && q(a[d]) !== q(b[d])) && g++;
        return g + f
    }

    function n(a) {
        if (a) {
            var b = a.toLowerCase().replace(/(.)s$/, "$1");
            a = Kb[a] || Lb[b] || b
        }
        return a
    }

    function o(a) {
        var b, c, d = {};
        for (c in a) a.hasOwnProperty(c) && (b = n(c), b && (d[b] = a[c]));
        return d
    }

    function p(b) {
        var c, d;
        if (0 === b.indexOf("week")) c = 7, d = "day";
        else {
            if (0 !== b.indexOf("month")) return;
            c = 12, d = "month"
        }
        bb[b] = function (e, f) {
            var g, h, i = bb.fn._lang[b],
                j = [];
            if ("number" == typeof e && (f = e, e = a), h = function (a) {
                var b = bb().utc().set(d, a);
                return i.call(bb.fn._lang, b, e || "")
            }, null != f) return h(f);
            for (g = 0; c > g; g++) j.push(h(g));
            return j
        }
    }

    function q(a) {
        var b = +a,
            c = 0;
        return 0 !== b && isFinite(b) && (c = b >= 0 ? Math.floor(b) : Math.ceil(b)), c
    }

    function r(a, b) {
        return new Date(Date.UTC(a, b + 1, 0)).getUTCDate()
    }

    function s(a) {
        return t(a) ? 366 : 365
    }

    function t(a) {
        return 0 === a % 4 && 0 !== a % 100 || 0 === a % 400
    }

    function u(a) {
        var b;
        a._a && -2 === a._pf.overflow && (b = a._a[gb] < 0 || a._a[gb] > 11 ? gb : a._a[hb] < 1 || a._a[hb] > r(a._a[fb], a._a[gb]) ? hb : a._a[ib] < 0 || a._a[ib] > 23 ? ib : a._a[jb] < 0 || a._a[jb] > 59 ? jb : a._a[kb] < 0 || a._a[kb] > 59 ? kb : a._a[lb] < 0 || a._a[lb] > 999 ? lb : -1, a._pf._overflowDayOfYear && (fb > b || b > hb) && (b = hb), a._pf.overflow = b)
    }

    function v(a) {
        a._pf = {
            empty: !1,
            unusedTokens: [],
            unusedInput: [],
            overflow: -2,
            charsLeftOver: 0,
            nullInput: !1,
            invalidMonth: null,
            invalidFormat: !1,
            userInvalidated: !1,
            iso: !1
        }
    }

    function w(a) {
        return null == a._isValid && (a._isValid = !isNaN(a._d.getTime()) && a._pf.overflow < 0 && !a._pf.empty && !a._pf.invalidMonth && !a._pf.nullInput && !a._pf.invalidFormat && !a._pf.userInvalidated, a._strict && (a._isValid = a._isValid && 0 === a._pf.charsLeftOver && 0 === a._pf.unusedTokens.length)), a._isValid
    }

    function x(a) {
        return a ? a.toLowerCase().replace("_", "-") : a
    }

    function y(a, b) {
        return b.abbr = a, mb[a] || (mb[a] = new d), mb[a].set(b), mb[a]
    }

    function z(a) {
        delete mb[a]
    }

    function A(a) {
        var b, c, d, e, f = 0,
            g = function (a) {
                if (!mb[a] && nb) try {
                    require("./lang/" + a)
                } catch (b) { }
                return mb[a]
            };
        if (!a) return bb.fn._lang;
        if (!k(a)) {
            if (c = g(a)) return c;
            a = [a]
        }
        for (; f < a.length;) {
            for (e = x(a[f]).split("-"), b = e.length, d = x(a[f + 1]), d = d ? d.split("-") : null; b > 0;) {
                if (c = g(e.slice(0, b).join("-"))) return c;
                if (d && d.length >= b && m(e, d, !0) >= b - 1) break;
                b--
            }
            f++
        }
        return bb.fn._lang
    }

    function B(a) {
        return a.match(/\[[\s\S]/) ? a.replace(/^\[|\]$/g, "") : a.replace(/\\/g, "")
    }

    function C(a) {
        var b, c, d = a.match(rb);
        for (b = 0, c = d.length; c > b; b++) d[b] = Pb[d[b]] ? Pb[d[b]] : B(d[b]);
        return function (e) {
            var f = "";
            for (b = 0; c > b; b++) f += d[b] instanceof Function ? d[b].call(e, a) : d[b];
            return f
        }
    }

    function D(a, b) {
        return a.isValid() ? (b = E(b, a.lang()), Mb[b] || (Mb[b] = C(b)), Mb[b](a)) : a.lang().invalidDate()
    }

    function E(a, b) {
        function c(a) {
            return b.longDateFormat(a) || a
        }
        var d = 5;
        for (sb.lastIndex = 0; d >= 0 && sb.test(a) ;) a = a.replace(sb, c), sb.lastIndex = 0, d -= 1;
        return a
    }

    function F(a, b) {
        var c;
        switch (a) {
            case "DDDD":
                return vb;
            case "YYYY":
            case "GGGG":
            case "gggg":
                return wb;
            case "YYYYY":
            case "GGGGG":
            case "ggggg":
                return xb;
            case "S":
            case "SS":
            case "SSS":
            case "DDD":
                return ub;
            case "MMM":
            case "MMMM":
            case "dd":
            case "ddd":
            case "dddd":
                return zb;
            case "a":
            case "A":
                return A(b._l)._meridiemParse;
            case "X":
                return Cb;
            case "Z":
            case "ZZ":
                return Ab;
            case "T":
                return Bb;
            case "SSSS":
                return yb;
            case "MM":
            case "DD":
            case "YY":
            case "GG":
            case "gg":
            case "HH":
            case "hh":
            case "mm":
            case "ss":
            case "M":
            case "D":
            case "d":
            case "H":
            case "h":
            case "m":
            case "s":
            case "w":
            case "ww":
            case "W":
            case "WW":
            case "e":
            case "E":
                return tb;
            default:
                return c = new RegExp(N(M(a.replace("\\", "")), "i"))
        }
    }

    function G(a) {
        var b = (Ab.exec(a) || [])[0],
            c = (b + "").match(Hb) || ["-", 0, 0],
            d = +(60 * c[1]) + q(c[2]);
        return "+" === c[0] ? -d : d
    }

    function H(a, b, c) {
        var d, e = c._a;
        switch (a) {
            case "M":
            case "MM":
                null != b && (e[gb] = q(b) - 1);
                break;
            case "MMM":
            case "MMMM":
                d = A(c._l).monthsParse(b), null != d ? e[gb] = d : c._pf.invalidMonth = b;
                break;
            case "D":
            case "DD":
                null != b && (e[hb] = q(b));
                break;
            case "DDD":
            case "DDDD":
                null != b && (c._dayOfYear = q(b));
                break;
            case "YY":
                e[fb] = q(b) + (q(b) > 68 ? 1900 : 2e3);
                break;
            case "YYYY":
            case "YYYYY":
                e[fb] = q(b);
                break;
            case "a":
            case "A":
                c._isPm = A(c._l).isPM(b);
                break;
            case "H":
            case "HH":
            case "h":
            case "hh":
                e[ib] = q(b);
                break;
            case "m":
            case "mm":
                e[jb] = q(b);
                break;
            case "s":
            case "ss":
                e[kb] = q(b);
                break;
            case "S":
            case "SS":
            case "SSS":
            case "SSSS":
                e[lb] = q(1e3 * ("0." + b));
                break;
            case "X":
                c._d = new Date(1e3 * parseFloat(b));
                break;
            case "Z":
            case "ZZ":
                c._useUTC = !0, c._tzm = G(b);
                break;
            case "w":
            case "ww":
            case "W":
            case "WW":
            case "d":
            case "dd":
            case "ddd":
            case "dddd":
            case "e":
            case "E":
                a = a.substr(0, 1);
            case "gg":
            case "gggg":
            case "GG":
            case "GGGG":
            case "GGGGG":
                a = a.substr(0, 2), b && (c._w = c._w || {}, c._w[a] = b)
        }
    }

    function I(a) {
        var b, c, d, e, f, g, h, i, j, k, l = [];
        if (!a._d) {
            for (d = K(a), a._w && null == a._a[hb] && null == a._a[gb] && (f = function (b) {
                return b ? b.length < 3 ? parseInt(b, 10) > 68 ? "19" + b : "20" + b : b : null == a._a[fb] ? bb().weekYear() : a._a[fb]
            }, g = a._w, null != g.GG || null != g.W || null != g.E ? h = X(f(g.GG), g.W || 1, g.E, 4, 1) : (i = A(a._l), j = null != g.d ? T(g.d, i) : null != g.e ? parseInt(g.e, 10) + i._week.dow : 0, k = parseInt(g.w, 10) || 1, null != g.d && j < i._week.dow && k++, h = X(f(g.gg), k, j, i._week.doy, i._week.dow)), a._a[fb] = h.year, a._dayOfYear = h.dayOfYear), a._dayOfYear && (e = null == a._a[fb] ? d[fb] : a._a[fb], a._dayOfYear > s(e) && (a._pf._overflowDayOfYear = !0), c = S(e, 0, a._dayOfYear), a._a[gb] = c.getUTCMonth(), a._a[hb] = c.getUTCDate()), b = 0; 3 > b && null == a._a[b]; ++b) a._a[b] = l[b] = d[b];
            for (; 7 > b; b++) a._a[b] = l[b] = null == a._a[b] ? 2 === b ? 1 : 0 : a._a[b];
            l[ib] += q((a._tzm || 0) / 60), l[jb] += q((a._tzm || 0) % 60), a._d = (a._useUTC ? S : R).apply(null, l)
        }
    }

    function J(a) {
        var b;
        a._d || (b = o(a._i), a._a = [b.year, b.month, b.day, b.hour, b.minute, b.second, b.millisecond], I(a))
    }

    function K(a) {
        var b = new Date;
        return a._useUTC ? [b.getUTCFullYear(), b.getUTCMonth(), b.getUTCDate()] : [b.getFullYear(), b.getMonth(), b.getDate()]
    }

    function L(a) {
        a._a = [], a._pf.empty = !0;
        var b, c, d, e, f, g = A(a._l),
            h = "" + a._i,
            i = h.length,
            j = 0;
        for (d = E(a._f, g).match(rb) || [], b = 0; b < d.length; b++) e = d[b], c = (F(e, a).exec(h) || [])[0], c && (f = h.substr(0, h.indexOf(c)), f.length > 0 && a._pf.unusedInput.push(f), h = h.slice(h.indexOf(c) + c.length), j += c.length), Pb[e] ? (c ? a._pf.empty = !1 : a._pf.unusedTokens.push(e), H(e, c, a)) : a._strict && !c && a._pf.unusedTokens.push(e);
        a._pf.charsLeftOver = i - j, h.length > 0 && a._pf.unusedInput.push(h), a._isPm && a._a[ib] < 12 && (a._a[ib] += 12), a._isPm === !1 && 12 === a._a[ib] && (a._a[ib] = 0), I(a), u(a)
    }

    function M(a) {
        return a.replace(/\\(\[)|\\(\])|\[([^\]\[]*)\]|\\(.)/g, function (a, b, c, d, e) {
            return b || c || d || e
        })
    }

    function N(a) {
        return a.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&")
    }

    function O(a) {
        var b, c, d, e, f;
        if (0 === a._f.length) return a._pf.invalidFormat = !0, a._d = new Date(0 / 0), void 0;
        for (e = 0; e < a._f.length; e++) f = 0, b = g({}, a), v(b), b._f = a._f[e], L(b), w(b) && (f += b._pf.charsLeftOver, f += 10 * b._pf.unusedTokens.length, b._pf.score = f, (null == d || d > f) && (d = f, c = b));
        g(a, c || b)
    }

    function P(a) {
        var b, c = a._i,
            d = Db.exec(c);
        if (d) {
            for (a._pf.iso = !0, b = 4; b > 0; b--)
                if (d[b]) {
                    a._f = Fb[b - 1] + (d[6] || " ");
                    break
                }
            for (b = 0; 4 > b; b++)
                if (Gb[b][1].exec(c)) {
                    a._f += Gb[b][0];
                    break
                }
            Ab.exec(c) && (a._f += "Z"), L(a)
        } else a._d = new Date(c)
    }

    function Q(b) {
        var c = b._i,
            d = ob.exec(c);
        c === a ? b._d = new Date : d ? b._d = new Date(+d[1]) : "string" == typeof c ? P(b) : k(c) ? (b._a = c.slice(0), I(b)) : l(c) ? b._d = new Date(+c) : "object" == typeof c ? J(b) : b._d = new Date(c)
    }

    function R(a, b, c, d, e, f, g) {
        var h = new Date(a, b, c, d, e, f, g);
        return 1970 > a && h.setFullYear(a), h
    }

    function S(a) {
        var b = new Date(Date.UTC.apply(null, arguments));
        return 1970 > a && b.setUTCFullYear(a), b
    }

    function T(a, b) {
        if ("string" == typeof a)
            if (isNaN(a)) {
                if (a = b.weekdaysParse(a), "number" != typeof a) return null
            } else a = parseInt(a, 10);
        return a
    }

    function U(a, b, c, d, e) {
        return e.relativeTime(b || 1, !!c, a, d)
    }

    function V(a, b, c) {
        var d = eb(Math.abs(a) / 1e3),
            e = eb(d / 60),
            f = eb(e / 60),
            g = eb(f / 24),
            h = eb(g / 365),
            i = 45 > d && ["s", d] || 1 === e && ["m"] || 45 > e && ["mm", e] || 1 === f && ["h"] || 22 > f && ["hh", f] || 1 === g && ["d"] || 25 >= g && ["dd", g] || 45 >= g && ["M"] || 345 > g && ["MM", eb(g / 30)] || 1 === h && ["y"] || ["yy", h];
        return i[2] = b, i[3] = a > 0, i[4] = c, U.apply({}, i)
    }

    function W(a, b, c) {
        var d, e = c - b,
            f = c - a.day();
        return f > e && (f -= 7), e - 7 > f && (f += 7), d = bb(a).add("d", f), {
            week: Math.ceil(d.dayOfYear() / 7),
            year: d.year()
        }
    }

    function X(a, b, c, d, e) {
        var f, g, h = new Date(Date.UTC(a, 0)).getUTCDay();
        return c = null != c ? c : e, f = e - h + (h > d ? 7 : 0), g = 7 * (b - 1) + (c - e) + f + 1, {
            year: g > 0 ? a : a - 1,
            dayOfYear: g > 0 ? g : s(a - 1) + g
        }
    }

    function Y(a) {
        var b = a._i,
            c = a._f;
        return "undefined" == typeof a._pf && v(a), null === b ? bb.invalid({
            nullInput: !0
        }) : ("string" == typeof b && (a._i = b = A().preparse(b)), bb.isMoment(b) ? (a = g({}, b), a._d = new Date(+b._d)) : c ? k(c) ? O(a) : L(a) : Q(a), new e(a))
    }

    function Z(a, b) {
        bb.fn[a] = bb.fn[a + "s"] = function (a) {
            var c = this._isUTC ? "UTC" : "";
            return null != a ? (this._d["set" + c + b](a), bb.updateOffset(this), this) : this._d["get" + c + b]()
        }
    }

    function $(a) {
        bb.duration.fn[a] = function () {
            return this._data[a]
        }
    }

    function _(a, b) {
        bb.duration.fn["as" + a] = function () {
            return +this / b
        }
    }

    function ab(a) {
        var b = !1,
            c = bb;
        "undefined" == typeof ender && (this.moment = a ? function () {
            return !b && console && console.warn && (b = !0, console.warn("Accessing Moment through the global scope is deprecated, and will be removed in an upcoming release.")), c.apply(null, arguments)
        } : bb)
    }
    for (var bb, cb, db = "2.4.0", eb = Math.round, fb = 0, gb = 1, hb = 2, ib = 3, jb = 4, kb = 5, lb = 6, mb = {}, nb = "undefined" != typeof module && module.exports, ob = /^\/?Date\((\-?\d+)/i, pb = /(\-)?(?:(\d*)\.)?(\d+)\:(\d+)(?:\:(\d+)\.?(\d{3})?)?/, qb = /^(-)?P(?:(?:([0-9,.]*)Y)?(?:([0-9,.]*)M)?(?:([0-9,.]*)D)?(?:T(?:([0-9,.]*)H)?(?:([0-9,.]*)M)?(?:([0-9,.]*)S)?)?|([0-9,.]*)W)$/, rb = /(\[[^\[]*\])|(\\)?(Mo|MM?M?M?|Do|DDDo|DD?D?D?|ddd?d?|do?|w[o|w]?|W[o|W]?|YYYYY|YYYY|YY|gg(ggg?)?|GG(GGG?)?|e|E|a|A|hh?|HH?|mm?|ss?|S{1,4}|X|zz?|ZZ?|.)/g, sb = /(\[[^\[]*\])|(\\)?(LT|LL?L?L?|l{1,4})/g, tb = /\d\d?/, ub = /\d{1,3}/, vb = /\d{3}/, wb = /\d{1,4}/, xb = /[+\-]?\d{1,6}/, yb = /\d+/, zb = /[0-9]*['a-z\u00A0-\u05FF\u0700-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+|[\u0600-\u06FF\/]+(\s*?[\u0600-\u06FF]+){1,2}/i, Ab = /Z|[\+\-]\d\d:?\d\d/i, Bb = /T/i, Cb = /[\+\-]?\d+(\.\d{1,3})?/, Db = /^\s*\d{4}-(?:(\d\d-\d\d)|(W\d\d$)|(W\d\d-\d)|(\d\d\d))((T| )(\d\d(:\d\d(:\d\d(\.\d+)?)?)?)?([\+\-]\d\d:?\d\d|Z)?)?$/, Eb = "YYYY-MM-DDTHH:mm:ssZ", Fb = ["YYYY-MM-DD", "GGGG-[W]WW", "GGGG-[W]WW-E", "YYYY-DDD"], Gb = [
            ["HH:mm:ss.SSSS", /(T| )\d\d:\d\d:\d\d\.\d{1,3}/],
            ["HH:mm:ss", /(T| )\d\d:\d\d:\d\d/],
            ["HH:mm", /(T| )\d\d:\d\d/],
            ["HH", /(T| )\d\d/]
    ], Hb = /([\+\-]|\d\d)/gi, Ib = "Date|Hours|Minutes|Seconds|Milliseconds".split("|"), Jb = {
        Milliseconds: 1,
        Seconds: 1e3,
        Minutes: 6e4,
        Hours: 36e5,
        Days: 864e5,
        Months: 2592e6,
        Years: 31536e6
    }, Kb = {
        ms: "millisecond",
        s: "second",
        m: "minute",
        h: "hour",
        d: "day",
        D: "date",
        w: "week",
        W: "isoWeek",
        M: "month",
        y: "year",
        DDD: "dayOfYear",
        e: "weekday",
        E: "isoWeekday",
        gg: "weekYear",
        GG: "isoWeekYear"
    }, Lb = {
        dayofyear: "dayOfYear",
        isoweekday: "isoWeekday",
        isoweek: "isoWeek",
        weekyear: "weekYear",
        isoweekyear: "isoWeekYear"
    }, Mb = {}, Nb = "DDD w W M D d".split(" "), Ob = "M D H h m s w W".split(" "), Pb = {
        M: function () {
                return this.month() + 1
    },
        MMM: function (a) {
                return this.lang().monthsShort(this, a)
    },
        MMMM: function (a) {
                return this.lang().months(this, a)
    },
        D: function () {
                return this.date()
    },
        DDD: function () {
                return this.dayOfYear()
    },
        d: function () {
                return this.day()
    },
        dd: function (a) {
                return this.lang().weekdaysMin(this, a)
    },
        ddd: function (a) {
                return this.lang().weekdaysShort(this, a)
    },
        dddd: function (a) {
                return this.lang().weekdays(this, a)
    },
        w: function () {
                return this.week()
    },
        W: function () {
                return this.isoWeek()
    },
        YY: function () {
                return i(this.year() % 100, 2)
    },
        YYYY: function () {
                return i(this.year(), 4)
    },
        YYYYY: function () {
                return i(this.year(), 5)
    },
        gg: function () {
                return i(this.weekYear() % 100, 2)
    },
        gggg: function () {
                return this.weekYear()
    },
        ggggg: function () {
                return i(this.weekYear(), 5)
    },
        GG: function () {
                return i(this.isoWeekYear() % 100, 2)
    },
        GGGG: function () {
                return this.isoWeekYear()
    },
        GGGGG: function () {
                return i(this.isoWeekYear(), 5)
    },
        e: function () {
                return this.weekday()
    },
        E: function () {
                return this.isoWeekday()
    },
        a: function () {
                return this.lang().meridiem(this.hours(), this.minutes(), !0)
    },
        A: function () {
                return this.lang().meridiem(this.hours(), this.minutes(), !1)
    },
        H: function () {
                return this.hours()
    },
        h: function () {
                return this.hours() % 12 || 12
    },
        m: function () {
                return this.minutes()
    },
        s: function () {
                return this.seconds()
    },
        S: function () {
                return q(this.milliseconds() / 100)
    },
        SS: function () {
                return i(q(this.milliseconds() / 10), 2)
    },
        SSS: function () {
                return i(this.milliseconds(), 3)
    },
        SSSS: function () {
                return i(this.milliseconds(), 3)
    },
        Z: function () {
                var a = -this.zone(),
                    b = "+";
                return 0 > a && (a = -a, b = "-"), b + i(q(a / 60), 2) + ":" + i(q(a) % 60, 2)
    },
        ZZ: function () {
                var a = -this.zone(),
                    b = "+";
                return 0 > a && (a = -a, b = "-"), b + i(q(10 * a / 6), 4)
    },
        z: function () {
                return this.zoneAbbr()
    },
        zz: function () {
                return this.zoneName()
    },
        X: function () {
                return this.unix()
    }
    }, Qb = ["months", "monthsShort", "weekdays", "weekdaysShort", "weekdaysMin"]; Nb.length;) cb = Nb.pop(), Pb[cb + "o"] = c(Pb[cb], cb);
    for (; Ob.length;) cb = Ob.pop(), Pb[cb + cb] = b(Pb[cb], 2);
    for (Pb.DDDD = b(Pb.DDD, 3), g(d.prototype, {
        set: function (a) {
            var b, c;
            for (c in a) b = a[c], "function" == typeof b ? this[c] = b : this["_" + c] = b
    },
        _months: "January_February_March_April_May_June_July_August_September_October_November_December".split("_"),
        months: function (a) {
            return this._months[a.month()]
    },
        _monthsShort: "Jan_Feb_Mar_Apr_May_Jun_Jul_Aug_Sep_Oct_Nov_Dec".split("_"),
        monthsShort: function (a) {
            return this._monthsShort[a.month()]
    },
        monthsParse: function (a) {
            var b, c, d;
            for (this._monthsParse || (this._monthsParse = []), b = 0; 12 > b; b++)
                if (this._monthsParse[b] || (c = bb.utc([2e3, b]), d = "^" + this.months(c, "") + "|^" + this.monthsShort(c, ""), this._monthsParse[b] = new RegExp(d.replace(".", ""), "i")), this._monthsParse[b].test(a)) return b
    },
        _weekdays: "Sunday_Monday_Tuesday_Wednesday_Thursday_Friday_Saturday".split("_"),
        weekdays: function (a) {
            return this._weekdays[a.day()]
    },
        _weekdaysShort: "Sun_Mon_Tue_Wed_Thu_Fri_Sat".split("_"),
        weekdaysShort: function (a) {
            return this._weekdaysShort[a.day()]
    },
        _weekdaysMin: "Su_Mo_Tu_We_Th_Fr_Sa".split("_"),
        weekdaysMin: function (a) {
            return this._weekdaysMin[a.day()]
    },
        weekdaysParse: function (a) {
            var b, c, d;
            for (this._weekdaysParse || (this._weekdaysParse = []), b = 0; 7 > b; b++)
                if (this._weekdaysParse[b] || (c = bb([2e3, 1]).day(b), d = "^" + this.weekdays(c, "") + "|^" + this.weekdaysShort(c, "") + "|^" + this.weekdaysMin(c, ""), this._weekdaysParse[b] = new RegExp(d.replace(".", ""), "i")), this._weekdaysParse[b].test(a)) return b
    },
        _longDateFormat: {
        LT: "h:mm A",
        L: "MM/DD/YYYY",
        LL: "MMMM D YYYY",
        LLL: "MMMM D YYYY LT",
        LLLL: "dddd, MMMM D YYYY LT"
    },
        longDateFormat: function (a) {
            var b = this._longDateFormat[a];
            return !b && this._longDateFormat[a.toUpperCase()] && (b = this._longDateFormat[a.toUpperCase()].replace(/MMMM|MM|DD|dddd/g, function (a) {
                return a.slice(1)
    }), this._longDateFormat[a] = b), b
    },
        isPM: function (a) {
            return "p" === (a + "").toLowerCase().charAt(0)
    },
        _meridiemParse: /[ap]\.?m?\.?/i,
        meridiem: function (a, b, c) {
            return a > 11 ? c ? "pm" : "PM" : c ? "am" : "AM"
    },
        _calendar: {
        sameDay: "[Today at] LT",
        nextDay: "[Tomorrow at] LT",
        nextWeek: "dddd [at] LT",
        lastDay: "[Yesterday at] LT",
        lastWeek: "[Last] dddd [at] LT",
        sameElse: "L"
    },
        calendar: function (a, b) {
            var c = this._calendar[a];
            return "function" == typeof c ? c.apply(b) : c
    },
        _relativeTime: {
        future: "in %s",
        past: "%s ago",
        s: "a few seconds",
        m: "a minute",
        mm: "%d minutes",
        h: "an hour",
        hh: "%d hours",
        d: "a day",
        dd: "%d days",
        M: "a month",
        MM: "%d months",
        y: "a year",
        yy: "%d years"
    },
        relativeTime: function (a, b, c, d) {
            var e = this._relativeTime[c];
            return "function" == typeof e ? e(a, b, c, d) : e.replace(/%d/i, a)
    },
        pastFuture: function (a, b) {
            var c = this._relativeTime[a > 0 ? "future" : "past"];
            return "function" == typeof c ? c(b) : c.replace(/%s/i, b)
    },
        ordinal: function (a) {
            return this._ordinal.replace("%d", a)
    },
        _ordinal: "%d",
        preparse: function (a) {
            return a
    },
        postformat: function (a) {
            return a
    },
        week: function (a) {
            return W(a, this._week.dow, this._week.doy).week
    },
        _week: {
        dow: 0,
        doy: 6
    },
        _invalidDate: "Invalid date",
        invalidDate: function () {
            return this._invalidDate
    }
    }), bb = function (b, c, d, e) {
        return "boolean" == typeof d && (e = d, d = a), Y({
        _i: b,
        _f: c,
        _l: d,
        _strict: e,
        _isUTC: !1
    })
    }, bb.utc = function (b, c, d, e) {
        var f;
        return "boolean" == typeof d && (e = d, d = a), f = Y({
        _useUTC: !0,
        _isUTC: !0,
        _l: d,
        _i: b,
        _f: c,
        _strict: e
    }).utc()
    }, bb.unix = function (a) {
        return bb(1e3 * a)
    }, bb.duration = function (a, b) {
        var c, d, e, g = bb.isDuration(a),
            h = "number" == typeof a,
            i = g ? a._input : h ? {} : a,
            j = null;
        return h ? b ? i[b] = a : i.milliseconds = a : (j = pb.exec(a)) ? (c = "-" === j[1] ? -1 : 1, i = {
        y: 0,
        d: q(j[hb]) * c,
        h: q(j[ib]) * c,
        m: q(j[jb]) * c,
        s: q(j[kb]) * c,
        ms: q(j[lb]) * c
    }) : (j = qb.exec(a)) && (c = "-" === j[1] ? -1 : 1, e = function (a) {
            var b = a && parseFloat(a.replace(",", "."));
            return (isNaN(b) ? 0 : b) * c
    }, i = {
        y: e(j[2]),
        M: e(j[3]),
        d: e(j[4]),
        h: e(j[5]),
        m: e(j[6]),
        s: e(j[7]),
        w: e(j[8])
    }), d = new f(i), g && a.hasOwnProperty("_lang") && (d._lang = a._lang), d
    }, bb.version = db, bb.defaultFormat = Eb, bb.updateOffset = function () { }, bb.lang = function (a, b) {
        var c;
        return a ? (b ? y(x(a), b) : null === b ? (z(a), a = "en") : mb[a] || A(a), c = bb.duration.fn._lang = bb.fn._lang = A(a), c._abbr) : bb.fn._lang._abbr
    }, bb.langData = function (a) {
        return a && a._lang && a._lang._abbr && (a = a._lang._abbr), A(a)
    }, bb.isMoment = function (a) {
        return a instanceof e
    }, bb.isDuration = function (a) {
        return a instanceof f
    }, cb = Qb.length - 1; cb >= 0; --cb) p(Qb[cb]);
    for (bb.normalizeUnits = function (a) {
        return n(a)
    }, bb.invalid = function (a) {
        var b = bb.utc(0 / 0);
        return null != a ? g(b._pf, a) : b._pf.userInvalidated = !0, b
    }, bb.parseZone = function (a) {
        return bb(a).parseZone()
    }, g(bb.fn = e.prototype, {
        clone: function () {
            return bb(this)
    },
        valueOf: function () {
            return +this._d + 6e4 * (this._offset || 0)
    },
        unix: function () {
            return Math.floor(+this / 1e3)
    },
        toString: function () {
            return this.clone().lang("en").format("ddd MMM DD YYYY HH:mm:ss [GMT]ZZ")
    },
        toDate: function () {
            return this._offset ? new Date(+this) : this._d
    },
        toISOString: function () {
            return D(bb(this).utc(), "YYYY-MM-DD[T]HH:mm:ss.SSS[Z]")
    },
        toArray: function () {
            var a = this;
            return [a.year(), a.month(), a.date(), a.hours(), a.minutes(), a.seconds(), a.milliseconds()]
    },
        isValid: function () {
            return w(this)
    },
        isDSTShifted: function () {
            return this._a ? this.isValid() && m(this._a, (this._isUTC ? bb.utc(this._a) : bb(this._a)).toArray()) > 0 : !1
    },
        parsingFlags: function () {
            return g({}, this._pf)
    },
        invalidAt: function () {
            return this._pf.overflow
    },
        utc: function () {
            return this.zone(0)
    },
        local: function () {
            return this.zone(0), this._isUTC = !1, this
    },
        format: function (a) {
            var b = D(this, a || bb.defaultFormat);
            return this.lang().postformat(b)
    },
        add: function (a, b) {
            var c;
            return c = "string" == typeof a ? bb.duration(+b, a) : bb.duration(a, b), j(this, c, 1), this
    },
        subtract: function (a, b) {
            var c;
            return c = "string" == typeof a ? bb.duration(+b, a) : bb.duration(a, b), j(this, c, -1), this
    },
        diff: function (a, b, c) {
            var d, e, f = this._isUTC ? bb(a).zone(this._offset || 0) : bb(a).local(),
                g = 6e4 * (this.zone() - f.zone());
            return b = n(b), "year" === b || "month" === b ? (d = 432e5 * (this.daysInMonth() + f.daysInMonth()), e = 12 * (this.year() - f.year()) + (this.month() - f.month()), e += (this - bb(this).startOf("month") - (f - bb(f).startOf("month"))) / d, e -= 6e4 * (this.zone() - bb(this).startOf("month").zone() - (f.zone() - bb(f).startOf("month").zone())) / d, "year" === b && (e /= 12)) : (d = this - f, e = "second" === b ? d / 1e3 : "minute" === b ? d / 6e4 : "hour" === b ? d / 36e5 : "day" === b ? (d - g) / 864e5 : "week" === b ? (d - g) / 6048e5 : d), c ? e : h(e)
    },
        from: function (a, b) {
            return bb.duration(this.diff(a)).lang(this.lang()._abbr).humanize(!b)
    },
        fromNow: function (a) {
            return this.from(bb(), a)
    },
        calendar: function () {
            var a = this.diff(bb().zone(this.zone()).startOf("day"), "days", !0),
                b = -6 > a ? "sameElse" : -1 > a ? "lastWeek" : 0 > a ? "lastDay" : 1 > a ? "sameDay" : 2 > a ? "nextDay" : 7 > a ? "nextWeek" : "sameElse";
            return this.format(this.lang().calendar(b, this))
    },
        isLeapYear: function () {
            return t(this.year())
    },
        isDST: function () {
            return this.zone() < this.clone().month(0).zone() || this.zone() < this.clone().month(5).zone()
    },
        day: function (a) {
            var b = this._isUTC ? this._d.getUTCDay() : this._d.getDay();
            return null != a ? (a = T(a, this.lang()), this.add({
        d: a - b
    })) : b
    },
        month: function (a) {
            var b, c = this._isUTC ? "UTC" : "";
            return null != a ? "string" == typeof a && (a = this.lang().monthsParse(a), "number" != typeof a) ? this : (b = this.date(), this.date(1), this._d["set" + c + "Month"](a), this.date(Math.min(b, this.daysInMonth())), bb.updateOffset(this), this) : this._d["get" + c + "Month"]()
    },
        startOf: function (a) {
            switch (a = n(a)) {
            case "year":
                this.month(0);
            case "month":
                this.date(1);
            case "week":
            case "isoWeek":
            case "day":
                this.hours(0);
            case "hour":
                this.minutes(0);
            case "minute":
                this.seconds(0);
            case "second":
                this.milliseconds(0)
    }
            return "week" === a ? this.weekday(0) : "isoWeek" === a && this.isoWeekday(1), this
    },
        endOf: function (a) {
            return a = n(a), this.startOf(a).add("isoWeek" === a ? "week" : a, 1).subtract("ms", 1)
    },
        isAfter: function (a, b) {
            return b = "undefined" != typeof b ? b : "millisecond", +this.clone().startOf(b) > +bb(a).startOf(b)
    },
        isBefore: function (a, b) {
            return b = "undefined" != typeof b ? b : "millisecond", +this.clone().startOf(b) < +bb(a).startOf(b)
    },
        isSame: function (a, b) {
            return b = "undefined" != typeof b ? b : "millisecond", +this.clone().startOf(b) === +bb(a).startOf(b)
    },
        min: function (a) {
            return a = bb.apply(null, arguments), this > a ? this : a
    },
        max: function (a) {
            return a = bb.apply(null, arguments), a > this ? this : a
    },
        zone: function (a) {
            var b = this._offset || 0;
            return null == a ? this._isUTC ? b : this._d.getTimezoneOffset() : ("string" == typeof a && (a = G(a)), Math.abs(a) < 16 && (a = 60 * a), this._offset = a, this._isUTC = !0, b !== a && j(this, bb.duration(b - a, "m"), 1, !0), this)
    },
        zoneAbbr: function () {
            return this._isUTC ? "UTC" : ""
    },
        zoneName: function () {
            return this._isUTC ? "Coordinated Universal Time" : ""
    },
        parseZone: function () {
            return "string" == typeof this._i && this.zone(this._i), this
    },
        hasAlignedHourOffset: function (a) {
            return a = a ? bb(a).zone() : 0, 0 === (this.zone() - a) % 60
    },
        daysInMonth: function () {
            return r(this.year(), this.month())
    },
        dayOfYear: function (a) {
            var b = eb((bb(this).startOf("day") - bb(this).startOf("year")) / 864e5) + 1;
            return null == a ? b : this.add("d", a - b)
    },
        weekYear: function (a) {
            var b = W(this, this.lang()._week.dow, this.lang()._week.doy).year;
            return null == a ? b : this.add("y", a - b)
    },
        isoWeekYear: function (a) {
            var b = W(this, 1, 4).year;
            return null == a ? b : this.add("y", a - b)
    },
        week: function (a) {
            var b = this.lang().week(this);
            return null == a ? b : this.add("d", 7 * (a - b))
    },
        isoWeek: function (a) {
            var b = W(this, 1, 4).week;
            return null == a ? b : this.add("d", 7 * (a - b))
    },
        weekday: function (a) {
            var b = (this.day() + 7 - this.lang()._week.dow) % 7;
            return null == a ? b : this.add("d", a - b)
    },
        isoWeekday: function (a) {
            return null == a ? this.day() || 7 : this.day(this.day() % 7 ? a : a - 7)
    },
        get: function (a) {
            return a = n(a), this[a]()
    },
        set: function (a, b) {
            return a = n(a), "function" == typeof this[a] && this[a](b), this
    },
        lang: function (b) {
            return b === a ? this._lang : (this._lang = A(b), this)
    }
    }), cb = 0; cb < Ib.length; cb++) Z(Ib[cb].toLowerCase().replace(/s$/, ""), Ib[cb]);
    Z("year", "FullYear"), bb.fn.days = bb.fn.day, bb.fn.months = bb.fn.month, bb.fn.weeks = bb.fn.week, bb.fn.isoWeeks = bb.fn.isoWeek, bb.fn.toJSON = bb.fn.toISOString, g(bb.duration.fn = f.prototype, {
        _bubble: function () {
            var a, b, c, d, e = this._milliseconds,
                f = this._days,
                g = this._months,
                i = this._data;
            i.milliseconds = e % 1e3, a = h(e / 1e3), i.seconds = a % 60, b = h(a / 60), i.minutes = b % 60, c = h(b / 60), i.hours = c % 24, f += h(c / 24), i.days = f % 30, g += h(f / 30), i.months = g % 12, d = h(g / 12), i.years = d
        },
        weeks: function () {
            return h(this.days() / 7)
        },
        valueOf: function () {
            return this._milliseconds + 864e5 * this._days + 2592e6 * (this._months % 12) + 31536e6 * q(this._months / 12)
        },
        humanize: function (a) {
            var b = +this,
                c = V(b, !a, this.lang());
            return a && (c = this.lang().pastFuture(b, c)), this.lang().postformat(c)
        },
        add: function (a, b) {
            var c = bb.duration(a, b);
            return this._milliseconds += c._milliseconds, this._days += c._days, this._months += c._months, this._bubble(), this
        },
        subtract: function (a, b) {
            var c = bb.duration(a, b);
            return this._milliseconds -= c._milliseconds, this._days -= c._days, this._months -= c._months, this._bubble(), this
        },
        get: function (a) {
            return a = n(a), this[a.toLowerCase() + "s"]()
        },
        as: function (a) {
            return a = n(a), this["as" + a.charAt(0).toUpperCase() + a.slice(1) + "s"]()
        },
        lang: bb.fn.lang,
        toIsoString: function () {
            var a = Math.abs(this.years()),
                b = Math.abs(this.months()),
                c = Math.abs(this.days()),
                d = Math.abs(this.hours()),
                e = Math.abs(this.minutes()),
                f = Math.abs(this.seconds() + this.milliseconds() / 1e3);
            return this.asSeconds() ? (this.asSeconds() < 0 ? "-" : "") + "P" + (a ? a + "Y" : "") + (b ? b + "M" : "") + (c ? c + "D" : "") + (d || e || f ? "T" : "") + (d ? d + "H" : "") + (e ? e + "M" : "") + (f ? f + "S" : "") : "P0D"
        }
    });
    for (cb in Jb) Jb.hasOwnProperty(cb) && (_(cb, Jb[cb]), $(cb.toLowerCase()));
    _("Weeks", 6048e5), bb.duration.fn.asMonths = function () {
        return (+this - 31536e6 * this.years()) / 2592e6 + 12 * this.years()
    }, bb.lang("en", {
        ordinal: function (a) {
            var b = a % 10,
                c = 1 === q(a % 100 / 10) ? "th" : 1 === b ? "st" : 2 === b ? "nd" : 3 === b ? "rd" : "th";
            return a + c
        }
    }), nb ? (module.exports = bb, ab(!0)) : "function" == typeof define && define.amd ? define("moment", function (b, c, d) {
        return d.config().noGlobal !== !0 && ab(d.config().noGlobal === a), bb
    }) : ab()
}).call(this);