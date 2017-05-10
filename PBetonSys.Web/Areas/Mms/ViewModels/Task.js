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
        this.opentaskdialog();
        //com.ajax({
        //    type: 'GET',
        //    url: '/api/Mms/Clinet/getnewcode',
        //    success: function (d) {
        //        var row = { Task_id: d };
        //        self.gridEdit.addnew(row);
        //    }
        //});
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


    this.opentaskdialog = function () {
           com.dialog({
            title: "编辑任务",
            width: 500,
            height: 300,
            html: "#task-template",
            viewModel: function (win) {
                var self = this;
                this.confirmClick = function () {
                    //self.grid.treegrid('getData');
                    window.location.reload();
                    win.dialog('close');
                };
                this.cancelClick = function () {
                    window.location.reload();
                    win.dialog('close');
                };

            }
        });
    };


    var taskdialog = function (row) {
        com.dialog({
            title: "编辑任务",
            width: 800,
            height: 600,
            html: "#task-template",
            viewModel: function (win) {
                var self = this;
                //this.grid2 = {
                //    height: 460,
                //    width: 774,
                //    idField: 'MenuCode',
                //    treeField: 'MenuName',
                //    frozenColumns: [[
                //        { field: 'MenuName', width: 150, title: '菜单' },
                //        {
                //            field: 'btn__checkall',
                //            width: 50,
                //            align: 'center',
                //            title: '<span class="icon icon-chk_unchecked">全选</span>',
                //            formatter: function (v, r) {
                //                for (var i in r) {
                //                    if (i.indexOf("btn_") > -1 && r[i] > -1) {
                //                        return '<img MenuCode="' + r.MenuCode + '" ButtonCode="_checkall" src="/Content/images/' + (v ? "checkmark.gif" : "checknomark.gif") + '"/>';
                //                    }
                //                }
                //            }
                //        }
                //    ]],
                //    columns: ko.observableArray(),
                //    loadFilter: function (d) {
                //        return utils.toTreeData(d, 'MenuCode', 'ParentCode', "children");
                //    }
                //};



                //this.confirmClick = function () {
                //    //self.grid.treegrid('getData');
                //};
                //this.cancelClick = function () {
                //    win.dialog('close');
                //};

            }
        });
    };
};
