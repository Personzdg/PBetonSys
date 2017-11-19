/**
* 模块名：MMS
* 程序名: Dispatch.js
**/

var viewModel = function () {
    var self = this;
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Dispatch/GetDispatchList',
        pagination: false
    };
    this.gridEdit = new com.editGridViewModel(this.grid);
    this.grid.onDblClickRow = this.gridEdit.begin;
    this.grid.onClickRow = this.gridEdit.ended;
    this.grid.OnAfterCreateEditor = function (editors, row) {
    };

    this.refreshClick = function () {
        window.location.reload();
    };
    this.addClick = function () {
        
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        self.gridEdit.deleterow();
    }
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个参数！');
        self.gridEdit.begin()
    };
    this.grid.onDblClickRow = this.editClick;
};
