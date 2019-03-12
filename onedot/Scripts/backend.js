;var oneDotCultures = {
    "needSelectRow": ["\u8bf7\u5148\u5728\u8868\u683c\u4e2d\u5148\u9009\u62e9\u4e00\u884c\u8bb0\u5f55\uff01", "Please First to select one row in table."],
    "confirmSubmit": ["确定要保存当前数据?", ""],
    "operationSuccess": ["Success : 操作成功！", ""],
    "operationFailure": ["error : 操作失败！", ""],
}


//请先在表格中先选择一行记录！
//\u8bf7\u5148\u5728\u8868\u683c\u4e2d\u5148\u9009\u62e9\u4e00\u884c\u8bb0\u5f55

oneDotCultures.get = function (code) {
    var s;
    kendo.culture("zh-CN");

    if (kendo.culture().name == "zh-CN")
        s = oneDotCultures[code][0];
    else
        s = oneDotCultures[code][1];
    return s;
};




//function isEmptyNull(obj) {

//    if (obj) return true;
//    if (obj.length < 1) return true;

//    return isEmptyObject(obj);
//}





//function isEmptyObject(obj) {

//    for (var name in obj) {
//        return false;
//    }
//    return true;
//}



function resizeGrid(grid, offset) {
    /// 有两种方式使用:
    /// 1 给kendo grid 加上css 类 one-grid-resize,js 将在页面加载后自动调整grid 尺寸。
    /// 2 在grid 的databounding中添加resizeGrid('gridId',offset),在数据加载完成后执行。

       var _offset = offset || 0;

       var _grid = grid || $("#center-pane-left").find(".one-grid-resize").attr("id");

        var tbar = $("#" + _grid).closest(".k-tabstrip").find("ul.k-reset").outerHeight() || 0;
        //jQuery.noConflict();innerHeight
    
        var paneHeight = $("#center-pane").innerHeight();

        //alert(paneHeight);

        var GridBarHeight = $("#" + _grid + " > .k-grid-toolbar").outerHeight() || 0;

        var GridHeadHeight = $("#" + _grid + " .k-grid-header").outerHeight() || 0;

        var GridPagerHeight = $("#" + _grid + " .k-grid-pager").outerHeight() || 0;

        var GridFootAggregateHeight = $("#" + _grid + " .k-footer-template").outerHeight() || 0;
        $("#" + _grid).height(paneHeight - _offset);
        $("#" + _grid + " > .k-grid-content").height(paneHeight - GridPagerHeight - GridBarHeight - GridHeadHeight - GridFootAggregateHeight - _offset - tbar);
        
};



!(function ($) {

    var OneDot = function () {

        if (window.addEventListener) {
            window.addEventListener('load', oneInit)
        } else {
            window.attachEvent('onload', oneInit)
        }

        function IniGridEvent() {
            var grid, row;
            $(document).delegate('.one-grid-edit', 'click',
                function (e) {

                    e.preventDefault();
                    grid = $("#" + $(e.target).closest(".k-grid").attr("id")).data("kendoGrid");
                    row = grid.select();
                    if (row.length == 0) {
                        alert(oneDotCultures.get('needSelectRow'))
                        return ;
                    }
                    grid.editRow(row);
                    alterGridPopWinPosition();
                });




            $(document).delegate('.one-grid-add', 'click',
                function (e) {
                    e.preventDefault();
                    grid = $("#" + $(e.target).closest(".k-grid").attr("id")).data("kendoGrid");
                    grid.addRow();
                    alterGridPopWinPosition();
                });




            $(document).delegate('.one-grid-delete', 'click',
                function (e) {
                    e.preventDefault();
                    grid = $("#" + $(e.target).closest(".k-grid").attr("id")).data("kendoGrid");
                    row = grid.select();
                    if (!row) {
                        alert(oneDotCultures['needSelectRow'])
                        return false;
                    }
                    grid.removeRow(row);
                    alterGridPopWinPosition();
                });




            $(document).delegate('.one-gridrow-delete', 'click',
                function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    grid = $("#" + $(e.target).closest(".k-grid").attr("id")).data("kendoGrid");
                    row = grid.select();
                    if (!row) {
                        alert(oneDotCultures['needSelectRow'])
                        return false;
                    }
                    grid.removeRow(row);
                });



            $(document).delegate('.one-gridrow-edit', 'click',
                function (e) {
                    e.preventDefault();
                    grid = $("#" + $(e.target).closest(".k-grid").attr("id")).data("kendoGrid");
                    row = grid.select();
                    if (!row) {
                        alert(oneDotCultures['needSelectRow'])
                        return;
                    }
                    grid.editRow(row);
                    alterGridPopWinPosition();
                });




            $(document).delegate('.one-form-button', 'click',
                function (e) {
                    var form = $(e.target).closest("form"),
                        parent = form.parent(),
                        _data = form.serializeForm(),
                        _url = form.attr("action"),
                        _valid = form.kendoValidator().data("kendoValidator");

                    var validatable = form.kendoValidator().data("kendoValidator");

                    if (!validatable.validate())
                        return false;

                    //var mm = validatable.validate();
                    //if (mm) {
                    //    //表单验证通过
                    //} else {
                    //    //表单验证未通过
                    //    return false;
                    //}

                    $.majax({
                        url: _url,
                        data : _data,
                        dataType: 'html',
                        type: "post",
                        success: function (data) {
                            if ($.isEmptyObject(data) || data =="{}") {
                                toastr.success(onedot.gw("operationSuccess"));
                            }
                            else {
                                parent.html(data);
                            }
                        }
                    })
                    
                    
                });






            $(document).delegate('.one-grid-editRoles', 'click',
                function (e) {
                    e.preventDefault();
                    $("#testWindow").data("kendoWindow").open();

                });
        }


        function alterGridPopWinPosition() {

            //to set popup windows position
            $("#oneGridPopwinWrap").closest(".k-window").css({ top: 160 });

            //bind onclick event to  a button, avoid duplication when submiting.
            $(".k-edit-buttons a").attr("onclick", "$(this).hide()");
        }

        function oneInit() {
            IniGridEvent();
            setTimeout("resizeGrid()");
        }
    };


    OneDot.prototype = {
        gw: function (code) {
            return oneDotCultures[code];
        },

        //alterGridPopWinPosition: function () {
        //    $("#oneGridPopwinWrap").closest(".k-window").css({ top: 160 });
        //}
    };


    window.onedot = new OneDot();

}(jQuery));



function kendoRequestBack(e) {

    var err, result;

    if (e.type && e.type == "read")
        return true;
    
    if (!e ||  e == "{}") {
        toastr.success(onedot.gw("operationSuccess"));
        return true;
    }

    

    info = e.Errors || (e.response && e.response.Errors);
    if (!info) {
        toastr.success(onedot.gw("operationSuccess"));
        return true;
    }


    if (info.Success) {

        for (var error in info) {
            var k = error;
            var c = info[error].errors || info[error].Errors;
            toastr.success(c);
            result = true;
        }

        return result;

    } else if (info.Error) {

        for (var error in info) {
            var k = error;
            var c = info[error].errors || info[error].Errors;
            toastr.error(c);
            result = true;
        }

        return result;
    }


    if (info) {
        for (var error in info) {
            var k = error;
            var c = info[error].errors || info[error].Errors;
            //c += e.response.Errors[error];
            if (typeof (this.cancelChanges) != 'undefined') {
                this.cancelChanges();
                e.preventDefault();
            }

            c.replace(/</g, "《");
            c.replace(/>/g, "》");
            c.replace(/"/g, "'");
            toastr.error(c);
            result = false;
        }

        return false;

    } else {
        if (e.type == "update" || e.type == "create" || e.type == "destroy") {
            toastr.success(onedot.gw("operationSuccess"));
        }
        toastr.success(onedot.gw("operationSuccess"));
        result = true;
    }

    return result;
};






/*
情况1： ajax 调用 partail 页面，如果成功了，服务器端返回空对象，客户端为{}字符串，如果失败了。。则返回 partailview 页面。
*/
$(function () {
    jQuery.extend({
        majax: function (opt) {
            var body = $(document.body);

            $.ajax({
                url: opt['url'],
                data: opt['data'],
                type: opt['type'] || 'get',
                dataType: opt['dataType'] || "html",
                async: opt['async'] || true,
                timeout: opt['timeout'] || 30000,
                contentType: opt['contentType'] || "application/x-www-form-urlencoded; charset=UTF-8",
                beforeSend: function (xhr) {
                    kendo.ui.progress(body, true);
                },
                success: function (data, elm, idx) {
                    if (opt['success']) {
                        opt['success'](data, elm, idx);
                    }
                    //if (opt['dataType'] == "json")
                    //    kendoRequestBack(data);
                },
                error: function (d, elm, idx) {
                    toastr.error(oneDotCultures['operationFailure']);
                },
                complete: function (XMLHttpRequest, textStatus) {

                    kendo.ui.progress(body, false);
                }
            });
        },

        /* transform all data of form  to object*/
        serializeForm: function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        },

        /*set or read cookie value */
        lang: function () {
            var days = arguments[1] || 365;

            if (arguments.length == 1) {
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + days);
                var culture = escape(arguments[0]) + ((days == null) ? "" : "; expires=" + exdate.toUTCString());
                document.cookie = "lang =" + culture + ";" + "path=/";
                kendo.culture(arguments[0]);
                return;
            }
            return $.cookie("lang");

        }
    })

});










$.fn.extend({
    serializeForm: function () {
        var o = {};
        var a = this.serializeArray();

        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }
});