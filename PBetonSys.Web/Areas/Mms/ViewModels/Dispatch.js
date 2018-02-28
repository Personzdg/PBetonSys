/**
* 模块名：MMS
* 程序名: Dispatch.js
**/

var viewModel = function (data) {
    var self = this;
    this.dataSource = data.dataSource;
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Dispatch/GetDispatchList',
        pagination: true,
        pageSize: 10
    };
    this.CurrModel = 
    {
        CNo: ko.observable(),
        CPBNo: ko.observable(),
        CTaskNo: ko.observable(),
        CFirstCarTime: ko.observable('2017-12-16 01:01:00'),
        CProjectName: ko.observable(),
        LastCarTime: ko.observable('2017-12-16 01:01:00'),
        CClientName: ko.observable(),
        Cinterva: ko.observable(),
        CPosition: ko.observable(),
        Cwjj: ko.observable(),
        Cysfl: ko.observable(),
        Cydfl: ko.observable(),
        Cqd: ko.observable(),
        Ctld: ko.observable(),
        Cyscs: ko.observable(),
        ChouseId: ko.observable(),
        CCarId: ko.observable(),
        CDriveID: ko.observable(),
        CPumpId:ko.observable()
    };
    this.currRow = ko.observable({});
    this.gridEdit = new com.editGridViewModel(this.grid);
    //this.grid.onDblClickRow = this.gridEdit.begin;
    //this.grid.onClickRow = this.gridEdit.ended;
    this.grid.onClickRow = this.editClick;
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
        self.CurrModel.CNo(row.合同编号);
        self.CurrModel.CPBNo(row.配比编号);
        self.CurrModel.CTaskNo(row.任务单编号);
        self.CurrModel.CFirstCarTime(row.第一车时间);  //
        self.CurrModel.CProjectName(row.ProjectName);
        self.CurrModel.LastCarTime(row.最后一车时间);
        self.CurrModel.CClientName(row.ClientName);
        self.CurrModel.Cinterva(!row.interva ? "0" : row.interva);
        self.CurrModel.CPosition(row.结构部位);
        self.CurrModel.Cwjj(row.wjj);
        self.CurrModel.Cysfl(row.累计方量);
        self.CurrModel.Cydfl(row.预定方量);
        self.CurrModel.Cqd(row.强度等级);
        self.CurrModel.Ctld(row.坍落度);
        self.CurrModel.Cyscs(row.累计车次);
        self.CurrModel.ChouseId(row.House_id);
        //self.gridEdit.begin()
    };
    this.grid.onClickRow = this.editClick;
    this.tabClick = function ()
    {
        //setTimeout(function ()
        //{
        //    $('#tt').tabs({
        //        border: false,
        //        onSelect: function (title, index) {
        //            console.log(title + ' is selected');
        //        }
        //    });
        //}, 2000);
    };
    //this.grid.onDblClickRow = this.editClick;
};
