/**
* 模块名：MMS
* 程序名: Product.js
**/

var viewModel = function () {
    var self = this;
    self.records = ko.observableArray();
    self.iniGroups = ko.observableArray();
    self.map = {};
    this.Init = function(){
        com.ajax({
            type: 'Get',
            url: '/api/Mms/Product/GetProductList',
            data: {},
            success: function (d) {
                self.IniGrupData(d.rows);
                //ko.mapping.fromJS(d.rows, {},self.records);
                //self.records = ko.observableArray(d.rows);
            }
        });
    };
    this.IniGrupData = function (rows) 
    {
        for (var i = 0; i < rows.length; i++) {
            var ai = rows[i];
            if (!self.map[ai.Cont_id]) {
                self.iniGroups.push({
                    Cont_id: ai.Cont_id,
                    datas: [ai]
                });
                self.map[ai.Cont_id] = ai;
            } else {
                for (var j = 0; j < self.iniGroups().length; j++) {
                    var dj = self.iniGroups()[j];
                    if (dj.Cont_id == ai.Cont_id) {
                        dj.datas.push(ai);
                        break;
                    }
                }
            }
        }
        console.log(self.iniGroups());
    };
    this.Init();
    this.searchClick = function ()
    {
        this.Init();
    }
};
