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
    this.opType = 'updated';
    this.selContId = ko.observable();
    this.DisplayModel =
        {
            CFirstCarTime: ko.observable('2017-12-16 01:01:00'), //??
            CProjectName: ko.observable(), //??
            LastCarTime: ko.observable('2017-12-16 01:01:00'), //??
            CClientName: ko.observable(),//??
            Cinterva: ko.observable(), //??
            CPosition: ko.observable(), //??
            Cwjj: ko.observable(), //??
            Cysfl: ko.observable(), //??
            Cydfl: ko.observable(),
            Cqd: ko.observable(),
            Ctld: ko.observable(),
            Cyscs: ko.observable(),
            CCarId: ko.observable(),
            CDriveID: ko.observable(),
            CPumpId: ko.observable()

        };
    this.CurrModel = 
    {
        Cont_ID: ko.observable(),
        Confect_ID: ko.observable(),
        Task_ID: ko.observable(),
        Hous_ID: ko.observable(),
        Transp_ID:ko.observable(),//运输单号
        CheckDateTime:ko.observable(),//开单时间
        Produce_Amount:ko.observable(),//供应量
        Amount:ko.observable(),//累计供应
        Other:ko.observable(),//砂浆
        surplus:ko.observable(),//剩转
        Vehicle_ID:ko.observable(),//车号
        Driver:ko.observable(),//驾驶员
        //车次 ??
        Trans_In_Value: ko.observable(),//转入
        Pump_No:ko.observable(),//泵号
        Attemper:ko.observable(),//操作员
        //调度  ??
        Remark:ko.observable(),//备注
        SurplusTransp_id: ko.observable(),//  剩转号
        //运距??
    };

    this.gridTransList =
      {
          size: { w: 4, h: 40 },
          url: '/api/Mms/Dispatch/GetTransportByContId',
          pagination: false,
          queryParams: {
              contId: '-11111111111'
          },
          method: "GET"
      };
    this.gridTransList.onClickRow = function ()
    {
        var row = self.gridTransList.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个参数！');
       // self.CurrModel.ChouseId(""); //拌楼号
        self.CurrModel.Transp_ID(row.Transp_ID);//运输单号
        self.CurrModel.CheckDateTime(row.CheckDateTime);//开单时间
        self.CurrModel.Produce_Amount(row.Produce_Amount);
        self.CurrModel.Amount(row.Amount);
        self.CurrModel.Other(row.Other);
        self.CurrModel.surplus(row.surplus);
        self.CurrModel.Driver(row.Driver);
        self.CurrModel.Trans_In_Value(row.Trans_In_Value);
        self.CurrModel.Pump_No(row.Pump_No);
        self.CurrModel.Attemper(row.Attemper);
        self.CurrModel.Remark(row.Remark);
        self.CurrModel.SurplusTransp_id(row.SurplusTransp_id);
        self.opType = 'updated';
    };
    this.ReSetCurrModel = function ()
    {
        //self.CurrModel.Cont_ID("");
        //self.CurrModel.Confect_ID("");
        //self.CurrModel.Task_ID("");
        //self.CurrModel.CFirstCarTime('2017-12-16 01:01:00');  //
        //self.CurrModel.CProjectName("");
        //self.CurrModel.LastCarTime('2017-12-16 01:01:00');
        //self.CurrModel.CClientName("");
        //self.CurrModel.Cinterva("");
        //self.CurrModel.CPosition("");
        //self.CurrModel.Cwjj("");
        //self.CurrModel.Cysfl("");
        //self.CurrModel.Cydfl("");
        //self.CurrModel.Cqd("");
        //self.CurrModel.Ctld("");
        //self.CurrModel.Cyscs("");
        self.CurrModel.Hous_ID(""); //拌楼号
        self.CurrModel.Transp_ID("");//运输单号
        self.CurrModel.CheckDateTime("");//开单时间
        self.CurrModel.Produce_Amount("");
        self.CurrModel.Amount("");
        self.CurrModel.Other("");
        self.CurrModel.surplus("");
        self.CurrModel.Driver("");
        self.CurrModel.Trans_In_Value("");
        self.CurrModel.Pump_No("");
        self.CurrModel.Attemper("");
        self.CurrModel.Remark("");
        self.CurrModel.SurplusTransp_id("");
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
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Dispatch/GetTranspID?hous_id=' + self.CurrModel.Hous_ID() + "&task_ID=" + self.CurrModel.Task_ID(),
            success: function (d) {
                self.ReSetCurrModel();
                self.CurrModel.Transp_ID(d);
                self.opType = 'inserted';
            }
        });
        ;
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        self.gridEdit.deleterow();
    }
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个参数！');
        self.CurrModel.Cont_ID(row.合同编号);
        self.selContId(row.合同编号);
        self.CurrModel.Confect_ID(row.配比编号);
        self.CurrModel.Task_ID(row.任务单编号);
        self.DisplayModel.CFirstCarTime(row.第一车时间);  //
        self.DisplayModel.CProjectName(row.ProjectName);
        self.DisplayModel.LastCarTime(row.最后一车时间);
        self.DisplayModel.CClientName(row.ClientName);
        self.DisplayModel.Cinterva(!row.interva ? "0" : row.interva);
        self.DisplayModel.CPosition(row.结构部位);
        self.DisplayModel.Cwjj(row.wjj);
        self.DisplayModel.Cysfl(row.累计方量);
        self.DisplayModel.Cydfl(row.预定方量);
        self.DisplayModel.Cqd(row.强度等级);
        self.DisplayModel.Ctld(row.坍落度);
        self.DisplayModel.Cyscs(row.累计车次);
        self.CurrModel.Hous_ID(row.House_id);
        self.CurrModel.Transp_ID("");
        //self.gridTransList.datagrid('reload');
        var queryParams =
       {
           contId: row.合同编号
       };
        self.gridTransList.datagrid('reload', queryParams);
        //self.gridEdit.begin()
    };
    this.grid.onClickRow = this.editClick;
    this.saveClick = function ()
    {
        var post = new Object();
        post.list = new Object();
        post.list[self.opType] = []; //inserted  updated
        var data = ko.toJS(self.CurrModel);
        data.surplus = data.surplus == null ? 0 : data.surplus;
        data.Trans_In_Value = data.Trans_In_Value == null ? 0 : data.Trans_In_Value;
        data.Produce_Amount = data.Produce_Amount == "" ? 0 : data.Produce_Amount;
        data.Other = data.Other == "" ? 0 : data.Other;
        data.surplus = data.surplus == "" ? 0 : data.surplus;
        data.Trans_In_Value = data.Trans_In_Value == "" ? 0 : data.Trans_In_Value;
        if (data.Amount == "")
            data.Amount = 0;
        post.list[self.opType].push(data);
        com.ajax({
            type: 'POST',
            url: '/api/Mms/Dispatch/edit',
            data: JSON.stringify(post),
            success: function (d) {
                data = d;
                self.opType = "updated";
                com.message('success', '保存成功！');
                var queryParams =
                   {
                       contId: self.CurrModel.Cont_ID()
                   };
                self.gridTransList.datagrid('reload', queryParams);
            }
        });
    };
    setTimeout(function ()
    {
        $('#tt').tabs({
            border: false,
            onSelect: function (title, index) {
                switch(index){
                    case 0:
                        $("#frmProduct").attr("src","/Mms/Product");
                        break;
                    case 1:
                        $("#frmProduct1").attr("src", "/Mms/Product");
                        break;
                }
                console.log(title +index+ ' is selected');
            }
        });
    }, 2000);
    //this.grid.onDblClickRow = this.editClick;
};
