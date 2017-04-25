/**
* 模块名：mms viewModel
* 程序名: mms.viewModel.search.js
* Copyright(c) 2013-2015 [.xm@gmail.com ] 
**/
var mms = mms || {};
mms.viewModel = mms.viewModel || {};

mms.viewModel.search = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.urls = data.urls;
    //delete this.form.__ko_mapping__;
    this.grid = {
        size: { w: 4, h: 94 },
        url: "/api/Mms/Contract/GetContractList",
        queryParams: ko.observable(),
        pagination: true,
        //customLoad: true,
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'PrintCont_ID', '_id');
            return d;
        }
    };
    this.grid.queryParams(data.form);

    this.searchClick = function () {
        var param = ko.toJS(this.form);
        this.grid.queryParams(param);
    };

    this.clearClick = function () {
        $.each(self.form, function () { this(''); });
        this.searchClick();
    };

    this.refreshClick = function () {
        window.location.reload();
    };

    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', self.resx.noneSelect);
        com.openTab('编辑合同', self.urls + "/" + row.SysCont_ID);
    };

    this.grid.onDblClickRow = this.editClick;

    this.addClick = function () {
        com.ajax({
            type: 'GET',
            url: "/api/mms/Contract/GetNewCode",
            success: function (d) {
                com.openTab('新增合同',self.urls +"/"+d);
            }
        });
    };

    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', self.resx.noneSelect);
        com.message('confirm', self.resx.deleteConfirm, function (b) {
            if (b) {
                com.ajax({
                    type: 'DELETE', url: self.urls.remove + row[self.idField], success: function () {
                        com.message('success', self.resx.deleteSuccess);
                        self.searchClick();
                    }
                });
            }
        });
    };
};


 