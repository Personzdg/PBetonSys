$(function () {
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
                $('#list').datagrid('resize', { width: w, height: h - 38 });
            }
        });

        //设置grid列
        var cols = [[
                { title: '任务编号', field: 'Task_id', sortable: true, align: 'left', width: 50 },
                { title: '工程名称', field: 'ProjectName', sortable: true, align: 'left', width: 120 },
                { title: '施工单位', field: 'Name', sortable: true, align: 'left', width: 80 },
                { title: '结构部位', field: 'Place', sortable: true, align: 'left', width: 80 },
                { title: '强度等级', field: 'Strong', sortable: true, align: 'left', width: 50 },
                { title: '预定方量', field: 'Amount', sortable: true, align: 'left', width: 50 },
                { title: '泵送方式', field: 'Pump_vehicle', sortable: true, align: 'left', width: 50 },
                { title: '供应时间', field: 'Provide_DateTime', sortable: true, align: 'left', width: 50 }

        ]];

        //定义返回值
        var selected = new Object();
        var grid = $('#list');

        var defaults = {
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
        var opt = $.extend({}, defaults, {
            height: 310,
            pagination: true,
            url: '/api/mms/task/GetNewTakList',
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

        grid.datagrid(opt);

        var search = function () {
            var queryParams = $.extend({}, param, {
                Task_id: $('#id').val(),
                ProjectName: $('#text').val(),
                Provide_DateTime: $('#text').val()

            });
            grid.datagrid('reload', queryParams);
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