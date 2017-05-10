/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function () {
    var self = this;
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Task/GetTaskList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'Task_id', '_id');
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
            url: '/api/Mms/Clinet/getnewcode',
            success: function (d) {
                var row = { Task_id: d };
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
    this.saveClick = function () {
        self.gridEdit.ended();
        var post = {};
        post.list = self.gridEdit.getChanges(['_id', 'Task_id', 'Name', 'CheckDateTime', 'State', 'Remark', 'ClerkID', 'LinkName', 'LinkPhon', 'WXCode', 'Password']);
        if (self.gridEdit.isChangedAndValid()) {
            com.ajax({
                url: '/api/mms/Clinet/edit',
                data: ko.toJSON(post),
                success: function (d) {
                    com.message('success', '保存成功！');
                    self.gridEdit.accept();
                }
            });
        }
    };
};
