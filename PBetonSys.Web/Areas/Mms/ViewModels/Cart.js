/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function () {
    var self = this;
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Cart/GetCartList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'CartID', '_id');
            return d;
        }
    };
    this.gridEdit = new com.editGridViewModel(this.grid);
    this.grid.onDblClickRow = this.gridEdit.begin;
    this.grid.onClickRow = this.gridEdit.ended;
    this.grid.OnAfterCreateEditor = function (editors, row) {
        //if (row._isnew == undefined)
        //    com.readOnlyHandler('input')(editors.CartID.target, true);
    };

    this.refreshClick = function () {
        window.location.reload();
    };
    this.addClick = function () {
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Cart/getnewcode',
            success: function (d) {
                var row = { CartID: d };
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
        post.list = self.gridEdit.getChanges(['_id','CartID', 'CartType', 'licenseID', 'Company', 'department', 'brand', 'Tare', 'Cart_bulk', 'UseTime', 'BuyDateTime', 'StartDateTime', 'State', 'Average_Oil_Consume', 'CardId', 'Remark']);
        if (self.gridEdit.isChangedAndValid()) {
            com.ajax({
                url: '/api/mms/Cart/edit',
                data: ko.toJSON(post),
                success: function (d) {
                    com.message('success', '保存成功！');
                    self.gridEdit.accept();
                }
            });
        }
    };
};
