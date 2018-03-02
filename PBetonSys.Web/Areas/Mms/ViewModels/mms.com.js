/// <reference path="common.js" />

/**
* 模块名：共通脚本
* 程序名: 材料模块通用方法mms.com.js
* Copyright(c) 2013-2015 [.xm@gmail.com ] 
**/

var mms = mms || {};
mms.com = {};

mms.com.getCurrentProject = function () {
    return window != parent ? parent.$.cookie('CurrentProject') : "";
};

mms.com.formatSrcBillType = function (value) {
    var dict = {
    };
    return dict[value] || value;
};

//计算总金额 用法：
//this.grid.OnAfterCreateEditor = com.psi.calcTotalMoney(self, "Num", "UnitPrice", "Money", "TotalMoney");
mms.com.bindCalcTotalMoney = function (self, fieldNum, fieldUnitPrice, fieldRowTotal, fieldAllTotal) {
    return function (editors) {
        var RowTotal = editors[fieldRowTotal].target;   //Money
        var Num = editors[fieldNum].target;             //Num
        var UnitPrice = editors[fieldUnitPrice].target; //UnitPrice

        com.readOnlyHandler('input')(editors[fieldRowTotal].target, true);
        var calc = function () {
            var rowTotalMoney = Num.numberbox('getValue') * UnitPrice.numberbox('getValue');
            RowTotal.numberbox('setValue', rowTotalMoney);
            var allMoney = rowTotalMoney - Number(editors[fieldRowTotal].oldHtml.replace(',', '') * 100) / 100;
            $.each(self.grid.datagrid('getData').rows, function () {
                var addMoney = (Number(this[fieldRowTotal] * 100) / 100) || 0;
                allMoney += addMoney
            });
            self.form[fieldAllTotal](allMoney);
        };
        Num.blur(calc);
        UnitPrice.blur(calc);
    };
};

mms.com.calcTotalMoneyWhileRemoveRow = function (self, fieldRowTotal, fieldAllTotal) {
    var allMoney = 0, fieldRowTotal = fieldRowTotal || "Money", fieldAllTotal = fieldAllTotal || "TotalMoney";
    $.each(self.grid.datagrid('getData').rows, function () {
        var addMoney = (Number(this[fieldRowTotal] * 100) / 100) || 0;
        allMoney += addMoney
    });
    self.form[fieldAllTotal](allMoney);
};

mms.com.auditDialog = function () {
    var query = parent.$;
    var winAudit = query('#w_audit_div'), args = arguments;
    if (winAudit.length == 0) {
        var html = utils.functionComment(function () { });
        var wrapper = query(html).appendTo("body");
        wrapper.find(".easyui-linkbutton").linkbutton();
        winAudit = wrapper.find(".easyui-dialog").dialog();
    }

    var viewModel = function () {
        var self = this;
        this.disabled = ko.observable(true);
        this.form = {
            status: args[0].ApproveState() == "passed" ? "reject" : "passed",
            comment: args[0].ApproveRemark()
        };
        this.confirmClick = function () {
            winAudit.dialog('close');
            if (typeof args[1] === 'function') {
                args[0].ApproveState(this.form.status);
                args[0].ApproveRemark(this.form.comment);
                args[1].call(this, ko.toJS(self.form));
            }
        };
        this.cancelClick = function () {
            winAudit.dialog('close');
        };
    }

    var node = winAudit.parent()[0];
    winAudit.dialog('open');
    ko.cleanNode(node);
    ko.applyBindings(new viewModel(), node);
};

//弹出选择合同窗口
mms.com.selectContract = function (vm, param,callback) {
    var grid = vm.grid;
    var addnew = vm.gridEdit.addnew;
    var defaultRow = vm.defaultRow;

    var orgRows = grid.datagrid('getData').rows;

    var target = parent.$('#selectContract').length ? parent.$('#selectContract') : parent.$('<div id="selectContract"></div>').appendTo('body');
    utils.clearIframe(target);

    var opt = { title: '选择合同', width: 600, height: 450, modal: true, collapsible: false, minimizable: false, maximizable: true, closable: true };
    opt.content = "<iframe id='frm_win_material' src='/mms/contract/lookupcontract' style='height:100%;width:100%;border:0;' frameborder='0'></iframe>";  //frameborder="0" for ie7
    opt.paramater = param;      //可传参数
    opt.onSelect = function (selectedData) {                //可接收选择数据
        if (selectedData) {
            callback(selectedData);
        }
    };
    target.window(opt);
};

//弹出选择施工单位窗口
mms.com.selectClient = function (param, callback) {
    var target = parent.$('#selectClient').length ? parent.$('#selectClient') : parent.$('<div id="selectClient"></div>').appendTo('body');
    utils.clearIframe(target);

    var opt = { title: '选择施工单位', width: 600, height: 450, modal: true, collapsible: false, minimizable: false, maximizable: true, closable: true };
    opt.content = "<iframe id='frm_win_material' src='/mms/Clinet/lookupclient' style='height:100%;width:100%;border:0;' frameborder='0'></iframe>";  //frameborder="0" for ie7
    opt.paramater = param;      //可传参数
    opt.onSelect = function (selectedData) {                //可接收选择数据
        if (selectedData) {
            callback(selectedData);
        }
    };
    target.window(opt);
};

//弹出选择标准配比窗口
mms.com.selectS_Confect = function (param, callback) {
    var target = parent.$('#selectS_Confect').length ? parent.$('#selectS_Confect') : parent.$('<div id="selectS_Confect"></div>').appendTo('body');
    utils.clearIframe(target);

    var opt = { title: '选择标准配比', width: 600, height: 450, modal: true, collapsible: false, minimizable: false, maximizable: true, closable: true };
    opt.content = "<iframe id='frm_win_selectS_Confect' src='/mms/S_Confect/LookupS_Confect' style='height:100%;width:100%;border:0;' frameborder='0'></iframe>";  //frameborder="0" for ie7
    opt.paramater = param;      //可传参数
    opt.onSelect = function (selectedData) {                //可接收选择数据
        if (selectedData) {
            callback(selectedData);
        }
    };
    target.window(opt);
};