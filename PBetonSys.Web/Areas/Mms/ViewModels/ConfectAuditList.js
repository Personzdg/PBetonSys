/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);

    this.grid = {
        size: { w: 4, h: 50 },
        url: '/api/Mms/Confect/GetAuditConfectList',
        pagination: true,
        pageSize: 20
    };

    //this.grid.queryParams(data.form);

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


    this.grid.onDblClickRow = function ()
    {

    };
};
