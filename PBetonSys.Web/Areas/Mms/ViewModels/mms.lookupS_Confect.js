﻿$(function () {
    using(['layout', 'datagrid', 'tree'], function () {
        //获取window信息
        var iframe = getThisIframe();
        var thiswin = parent.$(iframe).parent();
        var options = thiswin.window("options");
        var param = options.paramater;

        //初始化layout
        var box = $("#layoutbox"), right = $('#right').layout();
        box.width($(window).width()).height($(window).height()).layout();
        $(window).resize(function () { box.width($(window).width()).height($(window).height()).layout('resize'); });

        //调整layout时自动调整grid
        var panels = $('#right').data('layout').panels;
        panels.north.panel({
            onResize: function (w, h) {
                $('#list111').datagrid('resize', { width: w, height: h - 38 });
            }
        });

        //设置grid列
        var cols = [[
                { title: '标配编号', field: 'inside_id', sortable: true, align: 'left', width: 80 },
                { title: '强度', field: 'Strong', sortable: true, align: 'left', width: 80 },
                { title: '泵送', field: 'Pump', sortable: true, align: 'left', width: 80 },
                { title: '坍落度', field: 'Fall', sortable: true, align: 'left', width: 80 }
                
        ]];

        //定义返回值
        var selected = new Object();
        var grid1 = $('#list111');

        var defaults1 = {
            iconCls: 'icon icon-list',
            nowrap: true,           //折行
            rownumbers: true,       //行号
            striped: true,          //隔行变色
            singleSelect: true,     //单选
            remoteSort: true,       //后台排序
            pagination: false,      //翻页
            pageSize: com.getSetting("gridrows", 20),
            contentType: "application/json",
            method: "GET"
        };

        //设置明细表格的属性
        var opt1 = $.extend({}, defaults1, {
            height: 310,
            pagination: true,
            url: '/api/mms/S_Confect/GetS_ConfectList',
            queryParams: param,
            pageSize: 10,
            columns: cols,
            onClickRow: function (index, row) {
                selected = row;
            },
            onDblClickRow: function (index, row) {
                selected = row;
                $('#btnConfirm').click();
            }
        });
        //grid1.datagrid(opt1);

        grid1.datagrid({
            iconCls: 'icon icon-list',
            nowrap: true,           //折行
            rownumbers: true,       //行号
            striped: true,          //隔行变色
            singleSelect: true,     //单选
            remoteSort: true,       //后台排序
            pagination: false,      //翻页
            pageSize: com.getSetting("gridrows", 20),
            method: "GET",
            contentType: "application/json",
            height: 310,
            pagination: true,
            pageSize: 10,
            url: '/api/mms/S_Confect/GetS_ConfectList',
            columns: cols,
            onClickRow: function (index, row) {
                selected = row;
            },
            onDblClickRow: function (index, row) {
                selected = row;
                $('#btnConfirm').click();
            }
        });

        var search = function () {
            var queryParams = $.extend({}, param, {
                Cont_ID: $('#id').val(),
                ProjectName: $('#text').val()
            });
            grid1.datagrid('reload', queryParams);
        };

        var paramStr = "";
        for (var key in param)
            paramStr += (paramStr ? "&" : "?") + key + "=" + param[key];

        $('#btnSearch').click(search);
        $('#btnClear').click(function () { $('#master').find("input").val(""); search(); });

        $('#btnConfirm').click(function () {
            options.onSelect(selected);
            destroyIframe(iframe);
            thiswin.window('destroy');
        });

        $('#btnCancel').click(function () {
            destroyIframe(iframe);
            thiswin.window('destroy');
        });
    });
});

function getThisIframe() {
    if (!parent) return null;
    var iframes = parent.document.getElementsByTagName('iframe');
    if (iframes.length == 0) return null;
    for (var i = 0; i < iframes.length; ++i) {
        var iframe = iframes[i];
        if (iframe.contentWindow === self) {
            return iframe;
        }
    }
    return null;
}

function destroyIframe(iframeEl) {
    if (!iframeEl) return;
    iframeEl.parentNode.removeChild(iframeEl);
    iframeEl = null;
};