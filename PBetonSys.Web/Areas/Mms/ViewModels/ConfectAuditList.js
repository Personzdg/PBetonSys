

var viewModel = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);

    this.grid = {
        size: { w: 4, h: 50 },
        queryParams: ko.observable(),
        url: '/api/Mms/Confect/GetAuditConfectList',
        pagination: true,
        pageSize: 20
    };

    this.grid.queryParams(data.form);

    this.searchClick = function () {
        var param = ko.toJS(this.form);
        self.grid.queryParams(param);
    };

    this.clearClick = function () {
        window.location.reload();
    };

    this.refreshClick = function () {
        window.location.reload();
    };
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', self.resx.noneSelect);
        com.openTab('配比详情', "/mms/Confect/AuditDetail?id=" + row.Confect_ID);
    };

    this.grid.onDblClickRow = this.editClick;
    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
