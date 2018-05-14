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
        url: '/api/Mms/Task/GetNewTakList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            if (d && d.row) {
                d.rows = utils.copyProperty(d.rows, 'Task_id', '_id');
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
    this.addClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个任务！');
        self.opentaskdialog("新开配比" + row.Task_id, row, function (vm, win) {
            if (com.formValidate(win)) {
                self.save("inserted", vm, win);
            }
        });
    };
    /**
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个任务！');
        self.opentaskdialog("编辑任务" + row._id, row, function (vm, win) {
            if (com.formValidate(win)) {
                self.save("updated", vm, win);
            }
        });
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个任务！');
        com.message('confirm', '确认要删除选中的任务吗？', function (b) {
            if (b) {
                var post = { list: { deleted: [{ Task_id: row.Task_id }] } };
                com.ajax({
                    type: 'POST',
                    url: '/api/Mms/Task/edit',
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
    **/

    this.save = function (type, vm, win) {
        //if (true)   //下面那些东西检测遇到错误给这样的提示实例
        //{
        //    com.message("error", "错误提示");
        //    return false;
        //}
        var post = new Object();
        post.list = new Object();
        post.list[type] = [];
        var data = ko.toJS(vm.form);
        if (data.Amount == "")
            data.Amount = 0;
        //post.list[type].push(data);
        post.form = data;
        post.list["inserted"] = vm.griddetail.datagrid("getRows");
        var rows = vm.griddetail.datagrid("getRows");
        if (post.list["inserted"].length > 0)
        {
            for (var ii = 0; ii < post.list["inserted"].length; ii++)
            {
                var Theory_Value =rows[ii].Theory_Value;
                post.list["inserted"][ii].Confect_ID = vm.form.Confect_ID();
                post.list["inserted"][ii].MTCode =' 0';// rows[ii].MTCode;
                post.list["inserted"][ii].ZBCode =' 0';// rows[ii].ZBCode;
                post.list["inserted"][ii].Provide_ID =' 0';// rows[ii].Provide_ID;
                post.list["inserted"][ii].MT_Value = 1; // rows[ii].MT_Value;
                post.list["inserted"][ii].Ratio = 0;// rows[ii].Ratio;
                post.list["inserted"][ii].Theory_Value = Theory_Value;
                post.list["inserted"][ii].Confect_Detail_ID = '0';
                post.list["inserted"][ii].T1 = 0;
                post.list["inserted"][ii].Tol = 0; 
                post.list["inserted"][ii].range_From = 0;
                post.list["inserted"][ii].MTSize = '0'; 
                post.list["inserted"][ii].NewMax = 0;
                post.list["inserted"][ii].NewMin = 0; 
            }
        }
        debugger;
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
            width: 1000,
            height: 480,
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
                this.comboboxPumpType = {
                    value: model.Pump_vehicle==null?null:self.removeEmptyValue(model.Pump_vehicle.replace(';', ',').split(',')),
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
                    Confect_ID: ko.observable(model.Task_id.replace('T', 'P')),
                    Task_id: ko.observable(model.Task_id),
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
                this.confirmClick = function () {
                    fnConfirm(this, win);
                };
                //计算事件
                this.calcClick = function ()
                {
                 


                    var rows = that.griddetail.datagrid("getRows");
                    var value = 0;
                    var LiLun_value = 0;
                    var MtType = '';
                    var Sequence = 0;
                    var Ratio = 0;
                    var Water = 0;
                    var sumWater = 0;
                    for (var i = 0, l = rows.length; i < l; i++) {

                        
                         LiLun_value = rows[i].Theory_Value;  // 怎么读取griddetail列值
                         MtType = rows[i].MTType;  // 材料类型
                         Sequence = rows[i].Sequence;  // 序号
                         Ratio = rows[i].Ratio/100;
                         Water = LiLun_value * Ratio;
                         sumWater = sumWater + Water;
                         if (Sequence <= 4) {
                             if (LiLun_value > 0) { value = LiLun_value + sumWater; }
                             else { value = 0;}
                            

                         }
                       else {
                         if (LiLun_value == 0) {
                             value = 0
                         }
                         else {
                             value = LiLun_value;

                         }
                         }
                        if (MtType == '水') {

                            if (LiLun_value > 0) { value = LiLun_value - sumWater }


                        }



                        that.griddetail.datagrid('updateRow', { index: i, row: { range_From: "0", MT_Value: value } });
                        }
                };
                this.cancelClick = function () {
                    win.dialog('close');
                };
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
                        //size: { w: 8, h:15 },
                        queryParams: {
                            Inside_ID: "",
                            HousID: model.House_id
                        },
                        method: "GET",
                        contentType: "application/json",
                        url: '/api/Mms/S_Confect/GetLookupS_ConfectDetail?rad=' + Math.random(),
                        onLoadSuccess: function ()
                        {
                            if (model.House_id == '01')
                            {
                                that.griddetail.datagrid('showColumn', 'Hous1Name');
                            } else if (model.House_id == '02')
                            {
                                that.griddetail.datagrid('showColumn', 'Hous2Name');
                            } else if (model.House_id == '03') {
                                that.griddetail.datagrid('showColumn', 'Hous3Name');
                            } else if (model.House_id == '04') {
                                that.griddetail.datagrid('showColumn', 'Hous4Name');
                            }
                        }
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
};
