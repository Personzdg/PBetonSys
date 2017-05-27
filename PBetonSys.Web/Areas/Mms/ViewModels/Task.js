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

    //this.grid.onDblClickRow = this.gridEdit.begin;
    //this.grid.onClickRow = this.gridEdit.ended;
    //this.grid.OnAfterCreateEditor = function (editors, row) {
    //    if (row._isnew == undefined)
    //        com.readOnlyHandler('input')(editors.Clinet_id.target, true);
    //};

    this.refreshClick = function () {
        window.location.reload();
    };
    this.addClick = function () {
        com.ajax({
            type: 'GET',
            url: '/api/Mms/Task/getnewcode',
            success: function (d) {
                var defaults = { Task_id: d, Pump_vehicle: "" };
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
                self.grid.queryParams({});
            }
        });
    };

    this.opentaskdialog = function (title, model, fnConfirm) {
        com.dialog({
            title: title,
            width: 500,
            height: 420,
            html: "#task-template",
            viewModel: function (win) {
                var that = this;
                this.combotree = {
                    panelWidth: 450,
                    editable: true,
                    mode: 'local',
                    fitColumns: true,
                    idField: 'Cont_ID',
                    textField: 'Cont_ID',
                    data: self.combogridData,
                    columns: [[
                        { field: 'Cont_ID', title: '合同编号', width: 100 },
                        { field: 'ProjectName', title: '工程名称', width: 150 },
                        { field: 'Name', title: '施工单位', width: 150 }
                    ]],
                    filter: function (q, row) {
                        return row['Cont_ID'].indexOf(q) >= 0 && row['ProjectName'].indexOf(q) >= 0;
                    },
                    onSelect: function (i, row) {
                        that.form.Clin_ID(row.Clinet_id);
                    }
                };
                this.comboboxHouse = {
                    valueField: 'Code',
                    textField: 'DESCRIPTION',
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
                    valueField: 'DESCRIPTION',
                    textField: 'DESCRIPTION',
                    data: self.comboboxPlaceData
                };
                this.comboboxStrong = {
                    valueField: 'DESCRIPTION',
                    textField: 'DESCRIPTION',
                    data: self.comboboxStrongData
                };
                this.comboboxFall = {
                    valueField: 'DESCRIPTION',
                    textField: 'DESCRIPTION',
                    data: self.comboboxFallData
                };
                this.comboboxPumpType = {
                    value: self.removeEmptyValue(model.Pump_vehicle.replace(';', ',').split(',')),
                    valueField: 'DESCRIPTION',
                    textField: 'DESCRIPTION',
                    data: self.comboboxPumpTypeData,
                    onChange: function (n, o) {
                        that.form.Pump_vehicle(n.join(','));
                    }
                };

                this.form = {
                    Task_id: ko.observable(model.Task_id),
                    House_id: ko.observable(model.House_id),
                    AuditingFlag: ko.observable(model.AuditingFlag),
                    Cont_ID: ko.observable(model.Cont_ID),
                    Clin_ID: ko.observable(model.Clin_ID),
                    Place: ko.observable(model.Place),
                    Strong: ko.observable(model.Strong),
                    Amount: ko.observable(model.Amount),
                    Fall: ko.observable(model.Fall),
                    Pump: ko.observable(model.Pump),
                    Pump_vehicle: ko.observable(model.Pump_vehicle.replace(';', ',')),
                    LinkName: ko.observable(model.LinkName),
                    Telephon: ko.observable(model.Telephon),
                    ViseName: ko.observable(model.ViseName),
                    CheckDateTime: ko.observable(model.CheckDateTime),
                    Provide_DateTime: ko.observable(model.Provide_DateTime == null ? undefined : model.Provide_DateTime),
                    Remark: ko.observable(model.Remark)
                };

                this.confirmClick = function () {
                    fnConfirm(this, win);
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
            url: '/api/Sys/SysCode/GetHouseList',
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
            url: '/api/Sys/SysCode/GetPlaceList',
            success: function (d) {
                self.comboboxPlaceData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/SysCode/GetStrongList',
            success: function (d) {
                self.comboboxStrongData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/SysCode/GetFallList',
            success: function (d) {
                self.comboboxFallData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/SysCode/GetPumpTypeList',
            success: function (d) {
                self.comboboxPumpTypeData = d;
            }
        });
    };

    this.initComboData();

    this.grid.onDblClickRow = this.editClick;
};
