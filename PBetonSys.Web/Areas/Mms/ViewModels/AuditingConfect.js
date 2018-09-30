/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function (data) {
    var self = this;
    self.myList = [];
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 40 },
        url: '/api/Mms/Confect/GetConfectList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            if (d && d.row) {
                d.rows = utils.copyProperty(d.rows, 'Confect_ID', '_id');
            }
            return d;
        }
    };
    this.grid.queryParams(data.form);
    this.gridEdit = new com.editGridViewModel(this.grid);

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

    /**    ------------**/
    //this.addClick = function () {
    //    var row = self.grid.datagrid('getSelected');
    //    if (!row) return com.message('warning', '请先选择一个配比！');
    //    self.opentaskdialog("开配比审核" + row.Task_id, row, function (vm, win) {
    //        if (com.formValidate(win)) {
    //            self.save("inserted", vm, win);
    //        }
    //    });
    //};
    
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个配比！');
        self.opentaskdialog("编辑配比" + row._id, row, function (vm, win) {
            if (com.formValidate(win)) {
                self.save("updated", vm, win);
            }
        });
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个配比！');
        com.message('confirm', '确认要删除选中的任务吗？', function (b) {
            if (b) {
                var post = { list: { deleted: [{ Confect_ID: row.Confect_ID }] } };
                com.ajax({
                    type: 'POST',
                    url: '/api/Mms/Confect/edit',
                    data: JSON.stringify(post),
                    success: function (d) {
                        data = d;
                        com.message('success', '保存成功！');
                        self.grid.queryParams({});
                    }
                });
            }
        });
    }
    

    this.save = function (type, vm, win) {
        var post = new Object();
        post.list = new Object();
        post.list[type] = [];
        var data = ko.toJS(vm.form);
        if (data.Amount == "")
            data.Amount = 0;
        post.list[type].push(data);
        com.ajax({
            type: 'POST',
            url: '/api/Mms/Confect/edit',
            data: JSON.stringify(post),
            success: function (d) {
                data = d;
                com.message('success', '保存成功！');
                win.dialog('close');
                self.grid.queryParams({});
            }
        });
    };

    this.opentaskdialog = function (title, model, fnConfirm) {
        com.dialog({
            title: title,
            width: 900,
            height: 500,
            html: "#task-template",
            viewModel: function (win) {
                var that = this;
                this.lookupClick = function () {
                    mms.com.selectS_Confect(self, model, function (data) {
                        that.form.Inside_Code(data.inside_id);
                        var queryParams = {
                            Inside_ID: data.inside_id,
                            HousID: model.House_id
                        };
                        that.griddetail.datagrid('reload', queryParams);
                        //that.gridDetail('reload', queryParams);
                        //that.IniMyConfectDetailList(data.inside_id, model.House_id);
                    });
                };
                this.comboboxHouse = {
                    valueField: 'Value',
                    textField: 'Text',
                    data: self.comboboxHouseData
                };
                this.comboboxProject = {
                    valueField: 'Cont_ID',
                    textField: 'ProjectName',
                    data: self.comboboxProjectData
                };
                this.comboboxClient = {
                    valueField: 'Clinet_id',
                    textField: 'Name',
                    data: self.comboboxClientData
                };
                this.comboboxPlace = {
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxPlaceData
                };
                this.comboboxStrong = {
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxStrongData
                };
                this.comboboxFall = {
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxFallData
                };
                debugger;
                this.comboboxPumpType = {
                    value: model.Pump_vehicle == null ? null : self.removeEmptyValue(model.Pump_vehicle.replace(';', ',').split(',')),
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxPumpTypeData,
                    onChange: function (n, o) {
                        that.form.Pump_vehicle(n.join(','));
                    }
                };
                this.comboboxWjj = {
                    value: self.removeEmptyValue(model.Wjj ? model.Wjj.replace(';', ',').split(',') : []),
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxWjjData,
                    onChange: function (n, o) {
                        that.form.Wjj(n.join(','));
                    }
                };
                this.dispalyform =
                    {
                        Clin_ID: ko.observable(model.Clin_ID)  // 这部个显示的时候是需要的。但保存时Confect表里没这个字段不需要保存
                        //Pump_vehicle: ko.observable(model.Pump_vehicle.replace(';', ',')) // 这部个显示的时候是需要的。但保存时Confect表里没这个字段不需要保存
                    };
                this.form = {
                    Confect_ID: ko.observable(model.Confect_ID.replace('T', 'P')),
                    //Task_id: ko.observable(model.Task_id),
                    Hous_id: ko.observable(model.House_id),
                    //AuditingFlag: ko.observable(model.AuditingFlag),
                    Cont_ID: ko.observable(model.Cont_ID),

                    Inside_Code: ko.observable(),
                    Place: ko.observable(model.Place),
                    Strong: ko.observable(model.Strong),
                    Amount: ko.observable(model.Amount),
                    Fall: ko.observable(model.Fall),
                    Pump: ko.observable(model.Pump),
                    State: ko.observable(false),

                    Wjj: ko.observable(model.Wjj ? model.Wjj.replace(';', ',') : undefined),
                    //LinkName: ko.observable(model.LinkName),
                    //Telephon: ko.observable(model.Telephon),
                    ViseName: ko.observable(model.ViseName),
                    CheckDateTime: ko.observable(model.CheckDateTime),
                    //Provide_DateTime: ko.observable(model.Provide_DateTime == null ? undefined : model.Provide_DateTime),
                    Remark: ko.observable(model.Remark)
                };

                //that.grid = {
                //    size: { w: 4, h: 40 },
                //    url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail?Inside_ID=' + this.form.Inside_Code + "&HousID=" + this.form.Hous_id,
                //    queryParams: ko.observable(),
                //    pagination: false
                //};
                this.confirmClick = function () {
                    fnConfirm(this, win);
                };

                this.cancelClick = function () {
                    win.dialog('close');
                };
                //      var detailcols = [[
                //{ title: '序号', field: 'Sequence', sortable: true, align: 'left', width: 60 },
                //{ title: '1#线', field: 'Hous1Name', sortable: true, align: 'left', width: 60 },
                //{ title: '2#线', field: 'Hous2Name', sortable: true, align: 'left', width: 60 },
                //{ title: '3#线', field: 'Hous3Name', sortable: true, align: 'left', width: 60 },
                //{ title: '4#线', field: 'Hous4Name', sortable: true, align: 'left', width: 60 },
                //{ title: '最小值', field: 'range_From', sortable: true, align: 'left', width: 40 },
                //{ title: '设定配比值', field: 'Theory_value', sortable: true, align: 'left', width: 80 },
                //{ title: '含水率', field: 'Ratio', sortable: true, align: 'left', width: 80 },
                //{ title: '施工配比值', field: 'MT_Value', sortable: true, align: 'left', width: 80 },
                //{ title: '最大值', field: 'Range_To', sortable: true, align: 'left', width: 80 },
                //{ title: '材料种类', field: 'MTType', sortable: true, align: 'left', width: 80 },
                //{ title: '材料规格', field: 'MTSize', sortable: true, align: 'left', width: 80 },
                //{ title: '供应商名称', field: 'Provide_ID', sortable: true, align: 'left', width: 80 }
                //]];

                this.griddetail =
                    {
                        iconCls: 'icon icon-list',
                        nowrap: true,           //折行
                        rownumbers: false,       //行号
                        striped: true,          //隔行变色
                        singleSelect: false,     //单选
                        remoteSort: false,       //后台排序
                        pagination: false,      //翻页
                        fit: false,
                        size: { w: 5, h: 15 },
                        queryParams: {
                            Inside_ID: "",
                            HousID: model.House_id
                        },
                        method: "GET",
                        contentType: "application/json",
                        url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail?rad=' + Math.random(),

                    };
                this.gridEdit = new com.editGridViewModel(that.griddetail);
                this.griddetail.onDblClickRow = that.gridEdit.begin;
                this.griddetail.onClickRow = that.gridEdit.ended;
            }
        });
    };

    this.removeEmptyValue = function (arr) {
        var n = [];
        for (var i = 0; i < arr.length; i++) {
            if (arr[i])
                n.push(arr[i]);
        }
        if (n.length == 0)
            return null;
        return n;
    };
    //comboboxClient
    this.initComboData = function () {
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'House_id' },
            success: function (d) {
                self.comboboxHouseData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Contract/GetComboContract',
            success: function (d) {
                self.comboboxProjectData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Clinet/GetComboClinet',
            success: function (d) {
                self.comboboxClientData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'Place' },
            success: function (d) {
                self.comboboxPlaceData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'Strong' },
            success: function (d) {
                self.comboboxStrongData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'Fall' },
            success: function (d) {
                self.comboboxFallData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'pumpType' },
            success: function (d) {
                self.comboboxPumpTypeData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'wjj' },
            success: function (d) {
                self.comboboxWjjData = d;
            }
        });
    };

    this.initComboData();

    this.grid.onDblClickRow = this.addClick;


    //this.gridDetail = $.extend(true,{},self.grid,{
    //    url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail',
    //    queryParams: {
    //        Inside_ID: 'C15-1',
    //        HousID: '01'
    //    },
    //    pagination:false
    //});
    //设置grid列


    //this.IniMyConfectDetailList = function (Inside_ID, HousID) {
    //    debugger;
    //    //设置grid列
    //    var detailcols = [[
    //            { title: '序号', field: 'Sequence', sortable: true, align: 'left', width: 60 },
    //            { title: '1#线', field: 'Hous1Name', sortable: true, align: 'left', width: 60 },
    //            { title: '2#线', field: 'Hous2Name', sortable: true, align: 'left', width: 60 },
    //            { title: '3#线', field: 'Hous3Name', sortable: true, align: 'left', width: 60 },
    //            { title: '4#线', field: 'Hous4Name', sortable: true, align: 'left', width: 60 },
    //            { title: '最小值', field: 'range_From', sortable: true, align: 'left', width: 40 },
    //            { title: '设定配比值', field: 'Theory_value', sortable: true, align: 'left', width: 80 },
    //            { title: '含水率', field: 'Ratio', sortable: true, align: 'left', width: 80 },
    //            { title: '施工配比值', field: 'MT_Value', sortable: true, align: 'left', width: 80 },
    //            { title: '最大值', field: 'Range_To', sortable: true, align: 'left', width: 80 },
    //            { title: '材料种类', field: 'MTType', sortable: true, align: 'left', width: 80 },
    //            { title: '材料规格', field: 'MTSize', sortable: true, align: 'left', width: 80 },
    //            { title: '供应商名称', field: 'Provide_ID', sortable: true, align: 'left', width: 80 }
    //    ]];
    //    var queryParams2222 = {
    //        Inside_ID: 'C15-1',
    //        HousID: '01'
    //    };
    //    var grid222222 = $('#griddetaillist');
    //    grid222222.datagrid({
    //        iconCls: 'icon icon-list',
    //        nowrap: true,           //折行
    //        rownumbers: false,       //行号
    //        striped: true,          //隔行变色
    //        singleSelect: false,     //单选
    //        remoteSort: false,       //后台排序
    //        pagination: false,      //翻页
    //        fit: false,
    //        queryParams: queryParams2222,
    //        method: "GET",
    //        contentType: "application/json",
    //        height: 310,
    //        url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail?rad=' + Math.random(),
    //        columns: detailcols
    //    });



    //    //com.ajax({
    //    //    type: 'GET',
    //    //    url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail?Inside_ID=' + Inside_ID + "&HousID=" + HousID,
    //    //    success: function (d) {
    //    //        //self.myList = d;
    //    //        for (var i = 0; i < d.length; i++)
    //    //        {
    //    //            vm.griddetaillist.addnew(d[i]);
    //    //            //$('#griddetaillist').addnew(d[i]);
    //    //        }
    //    //    }
    //    //});
    //};

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
