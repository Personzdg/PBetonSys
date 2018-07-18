/**
* 模块名：MMS
* 程序名: PriceInfo.js
**/

var viewModel = function (data) {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 100 },
        url: '/api/Mms/Gathering/GetGatheringList',
        queryParams: ko.observable(),
        pagination: true,
        onLoadSuccess: function (){
            self.IniTotal();  
        },
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'Gathering_ID', '_id');
            return d;
        }

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
            url: '/api/Mms/Gathering/GetNewCode',
            success: function (d) {
                var defaults = { Gathering_ID: d, ReceiveType: "", SalseName: "" };
                self.opentaskdialog("添加收款", defaults, function (vm, win) {
                    if (com.formValidate(win)) {
                        self.save("inserted", vm, win);
                    }
                });
            }
        });
    };
    this.editClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个收款单！');
        self.opentaskdialog("编辑收款" + row._id, row, function (vm, win) {
            if (com.formValidate(win)) {
                self.save("updated", vm, win);
            }
        });
    };
    this.deleteClick = function () {
        var row = self.grid.datagrid('getSelected');
        if (!row) return com.message('warning', '请先选择一个收款！');
        com.message('confirm', '确认要删除选中的收款吗？', function (b) {
            if (b) {
                var post = { list: { deleted: [{ Gathering_ID: row.Gathering_ID }] } };
                com.ajax({
                    type: 'POST',
                    url: '/api/Mms/Gathering/edit',
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
        if (data.ReceiveMoney == "")
            data.ReceiveMoney = 0;

        if (data.Other == "")
            data.Other = 0;

        post.list[type].push(data);
        com.ajax({
            type: 'POST',
            url: '/api/Mms/Gathering/edit',
            data: JSON.stringify(post),
            success: function (d) {
                data = d;
                com.message('success', '保存成功！');
                win.dialog('close');
                self.searchClick();
            }
        });
    };
    /////////////////////
    this.IniTotal = function () {
        com.ajax({
            type: 'GET',
            //ko.mapping.fromJS(data.form);
            //data: self.form,
            data: ko.mapping.fromJS(self.form),
            url: '/api/mms/Gathering/GetTotalReceiveMoney',
            success: function (d) {
                if (d < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['ReceiveMoney']);
                    rowTota2 += parseFloat(rows[i]['Other']);
                }
                self.grid.datagrid("appendRow", {
                    EngineePos: '<b>小计：</b>', ReceiveMoney: rowTotal, Name: '',
                    EngineePos: '<b>小计：</b>', Other: rowTota2, Name: ''
                });
                self.grid.datagrid("appendRow", {
                    EngineePos: '<b>合计：</b>', ReceiveMoney: d == null ? "0" : d, Name: '',
                    EngineePos: '<b>合计：</b>', Other    : d == null ? "0" : d, Name: ''
                });
            }
        });
    }


    ////////////////



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
                        that.form.SysCont_ID(data.Cont_ID);
                        that.form.Clinet_id(data.Clinet_id);
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
             
                this.comboboxSalseName = {
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxSalseNameData
                };
                this.comboboxReceiveType = {
                    valueField: 'Text',
                    textField: 'Text',
                    data: self.comboboxReceiveTypeData
                };
             
               
             
                debugger;
                this.form = {
                    /**     Task_id: ko.observable(model.Task_id.replace('T','P'))**/
                    Gathering_ID: ko.observable(model.Gathering_ID),
                    SysCont_id: ko.observable(model.SysCont_id),
                    CheckDateTime: ko.observable(model.CheckDateTime),
                    AffirmDateTime: ko.observable(model.AffirmDateTime),
                    ReceiveMoney: ko.observable(model.ReceiveMoney),
                    Other: ko.observable(model.Other),
                    ReceiveType: ko.observable(model.ReceiveType),
                    AffirmFlag: ko.observable(model.AffirmFlag),
                    ClerkName: ko.observable(model.ClerkName),
                    Remark: ko.observable(model.Remark),

                    SalseName: ko.observable(model.SalseName),
                    AffirmName: ko.observable(model.AffirmName),
                     GatheringPerson: ko.observable(model.GatheringPerson),
                     Bank: ko.observable(model.Bank),
                     Accounts: ko.observable(model.Accounts),
                     AcCode: ko.observable(model.AcCode)

                    //Provide_DateTime: ko.observable(model.Provide_DateTime == null ? '': model.Provide_DateTime),
                   
                };
               
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
            data: { CodeType: 'SalseName' },
            success: function (d) {
                self.comboboxSalseNameData = d;
            }
        });
        com.ajax({
            type: 'GET',
            url: '/api/Sys/code/getcombo',
            data: { CodeType: 'ReceiveType' },
            success: function (d) {
                self.comboboxReceiveTypeData = d;
            }
        });
     
    };

    this.initComboData();

    this.grid.onDblClickRow = this.editClick;
};
