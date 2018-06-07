

var viewModel = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 50 },
        queryParams: ko.observable(),
        url: '/api/Mms/Confect/GetAjustList',
        pagination: false
    };
    this.grid.queryParams({ Confect_ID: data.form.Confect_ID });

    this.grid.onClickRow = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', self.resx.noneSelect);
        self.griddetail.datagrid('reload', { Confect_Detail_ID: row.Confect_Detail_ID });
    };
    this.grid.onDblClickRow = this.grid.onClickRow;

    this.griddetail ={
        iconCls: 'icon icon-list',
        nowrap: true,           //折行
        rownumbers: false,       //行号
        striped: true,          //隔行变色
        singleSelect: false,     //单选
        remoteSort: false,       //后台排序
        pagination: false,      //翻页
        fit: false,
        //size: { w: 8, h:15 },
        queryParams: {
            Confect_Detail_ID: '-10000'
        },
        method: "GET",
        contentType: "application/json",
        url: '/api/Mms/Confect/GetConfectDetaiList?rad=' + Math.random(),
        onLoadSuccess: function () {
            var row = self.grid.datagrid('getSelected');
            if (row) {
                self.griddetail.datagrid('hideColumn', 'Hous1Name');
                self.griddetail.datagrid('hideColumn', 'Hous2Name');
                self.griddetail.datagrid('hideColumn', 'Hous3Name');
                self.griddetail.datagrid('hideColumn', 'Hous4Name');
                if (row.Hous_ID == '01') {
                    self.griddetail.datagrid('showColumn', 'Hous1Name');
                } else if (row.Hous_ID == '02') {
                    self.griddetail.datagrid('showColumn', 'Hous2Name');
                } else if (row.Hous_ID == '03') {
                    self.griddetail.datagrid('showColumn', 'Hous3Name');
                } else if (row.Hous_ID == '04') {
                    self.griddetail.datagrid('showColumn', 'Hous4Name');
                }
            }
        }
    };
    this.gridEdit = new com.editGridViewModel(this.griddetail);
    this.griddetail.onDblClickRow = this.gridEdit.begin;
    this.griddetail.onClickRow = this.gridEdit.ended;
};
