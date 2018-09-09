/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function (data) {
    var self = this;
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Contract/GetContractList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'PrintCont_ID', '_id');
            return d;
        }
    };
    this.gridEdit = new com.editGridViewModel(this.grid);
    this.grid.onDblClickRow = this.gridEdit.begin;
    this.grid.onClickRow = this.gridEdit.ended;
    this.grid.OnAfterCreateEditor = function (editors, row) {
        if (row._isnew == undefined)
            com.readOnlyHandler('input')(editors.Clinet_id.target, true);
    };

    this.refreshClick = function () {
        window.location.reload();
    };
    this.addClick = function () {

        com.ajax({
            type: 'GET',
            url: '/api/Mms/Contract/getnewcode',
            success: function (d) {
                var row = { PrintCont_ID: d };
                self.gridEdit.addnew(row);
            }
        });
        //self.gridEdit.addnew({});
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        // if (!row.IsUserEditable) return com.message('warning', '此参数不能被删除！');
        self.gridEdit.deleterow();
    }
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个参数！');
        self.gridEdit.begin()
    };
    this.grid.onDblClickRow = this.editClick;
    //this.saveClick = function () {
    //    self.gridEdit.ended();
    //    var post = {};
    //    post.list = self.gridEdit.getChanges(['_id', 'SysCont_ID', 'Cont_ID', 'ProjectName', 'ProjectAddr', 'CheckAdder', 'Interva', 'Clinet_id', 'CheckDateTime', 'SalseName', 'BossName'
    //        , 'SalseRecevice', 'LinkPhon', 'MobilePhon', 'Strong', 'Amount', 'Price', 'FinshFlag', 'FinShDateTime', 'paymentType', 'term', 'GatheringRatio', 'Remark', 'ContType', 'ResizeRatio'
    //    ]);
    //    if (self.gridEdit.isChangedAndValid()) {
    //        com.ajax({
    //            url: '/api/mms/Contract/edit',
    //            data: ko.toJSON(post),
    //            success: function (d) {
    //                com.message('success', '保存成功！');
    //                self.gridEdit.accept();
    //            }
    //        });
    //    }
    //};
    this.saveClick= function (type, vm, win) {
        var post = new Object();
        post.list = new Object();
        post.list[type] = [];
        var data = ko.toJS(vm.form);
        if (data.Amount == "")
            data.Amount = 0;
        delete data[ClientName];
        post.list[type].push(data);
        com.ajax({
            type: 'POST',
            url: '/api/Mms/Contract/edit',
            data: JSON.stringify(post),
            success: function (d) {
                data = d;
                com.message('success', '保存成功！');
                win.dialog('close');
                self.searchClick();
            }
        });
    };

};