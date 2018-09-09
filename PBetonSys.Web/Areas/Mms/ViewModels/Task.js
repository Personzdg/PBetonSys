/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 50 },
        url: '/api/Mms/Task/GetTaskList',
        queryParams: ko.observable(),
        pagination: true,
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'Task_id', '_id');
            return d;
        },
        onLoadSuccess: function () {
            self.IniTotal();
        },

    };
    this.grid.queryParams(data.form);
    this.gridEdit = new com.editGridViewModel(this.grid);

    this.searchClick = function () {
        var param = ko.toJS(this.form);
        this.grid.queryParams(param);
    };

    this.clearClick = function () {
        window.location.reload();
    };

    this.refreshClick = function () {
        window.location.reload();
    };
    this.addClick = function () {
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Task/getnewcode',
            success: function (d) {
                var defaults = { Task_id: d, Pump_vehicle: "", Wjj: "",AuditingFlag:false,Provide_DateTime:''};
                self.opentaskdialog("添加新任务", defaults, function (vm, win) {
                    if (com.formValidate(win)) {
                        self.save("inserted", vm, win);
                    }
                });
            }
        });
    };
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
            url: '/api/Mms/Task/edit',
            data: JSON.stringify(post),
            success: function (d) {
                data = d;
                com.message('success', '保存成功！');
                win.dialog('close');
                self.searchClick();
            }
        });
    };

    this.opentaskdialog = function (title, model, fnConfirm) {
        com.dialog({
            title: title,
            width: 500,
            height: 440,
            html: "#task-template",
            viewModel: function (win) {
                var that = this;
                this.lookupClick = function () {
                    mms.com.selectContract(self, null, function (data) {
                        that.form.Cont_ID(data.Cont_ID);
                        that.form.Clin_ID(data.Clinet_id);
                    });
                };
                this.comboboxHouse = {
                    valueField: 'Code',
                    textField: 'Text',
                    data: self.comboboxHouseData
                };
                this.comboboxProject = {
                    valueField: 'Cont_ID',
                    textField: 'ProjectName',
                    data: self.combogridData
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
                    value: model.Pump_vehicle == null ? null : self.removeEmptyValue(model.Pump_vehicle.replace(';', ',').split(',')),

                    
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxPumpTypeData,
                    onChange: function (n, o) {
                        that.form.Pump_vehicle(n.join(','));
                    }
                };
                this.comboboxWjj = {

                    //value: self.removeEmptyValue(model.Wjj ? model.Wjj.replace(';', ',').split(',') : []),
                    value: model.wjj == null ? null : self.removeEmptyValue(model.wjj.replace(';', ',').split(',')),
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxWjjData,
                    onChange: function (n, o) {
                        that.form.Wjj(n.join(','));
                    }
                };
                this.form = {
                    /**     Task_id: ko.observable(model.Task_id.replace('T','P'))**/
                        Task_id: ko.observable(model.Task_id),
                    House_id: ko.observable(model.House_id),
                    AuditingFlag: ko.observable(model.AuditingFlag),
                    //AuditingFlagDisplay:model.AuditingFlag?"block":"none",
                    Cont_ID: ko.observable(model.Cont_ID),
                    Clin_ID: ko.observable(model.Clin_ID),
                    Place: ko.observable(model.Place),
                    Strong: ko.observable(model.Strong),
                    Amount: ko.observable(model.Amount),
                    Fall: ko.observable(model.Fall),
                    Pump: ko.observable(model.Pump),
                    Pump_vehicle: ko.observable(model.Pump_vehicle ? model.Pump_vehicle.replace(';', ','):''),
                    Wjj: ko.observable(model.Wjj ? model.Wjj.replace(';', ',') : undefined),
                    LinkName: ko.observable(model.LinkName),
                    Telephon: ko.observable(model.Telephon),
                    ViseName: ko.observable(model.ViseName),
                    CheckDateTime: ko.observable(model.CheckDateTime),
                    Provide_DateTime: ko.observable(model.Provide_DateTime.replace("T"," ")),
                    Remark: ko.observable(model.Remark)
                };
                if (!this.form.House_id())
                {
                   
                    if (self.comboboxHouseData.length > 0)
                    {
                        this.form.House_id(self.comboboxHouseData[0].Code)
                    }
                }
                this.confirmClick = function () {
                    fnConfirm(that, win);
                };

                this.cancelClick = function () {
                    win.dialog('close');
                };
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
                self.combogridData = d;
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

    this.grid.onDblClickRow = this.editClick;


    this.IniTotal = function () {
        com.ajax({
            type: 'GET',
            data: ko.mapping.toJS(self.form),
            url: '/api/mms/Task/GetTotalTaskList',
            success: function (d) {
                if (d.Amount < 1) {
                    return false;
                }
                var rowTotal = 0;

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['Amount']);
                 
                }
                self.grid.datagrid("appendRow", {
                    Pump_vehicle: '<b>小计：</b>', Amount: rowTotal
                });
                //debugger;
                self.grid.datagrid("appendRow", {
                    Pump_vehicle: '<b>合计：</b>', Amount: d.Amount
                });


            }
        });
    }
    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };

};
