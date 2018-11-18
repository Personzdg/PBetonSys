/**
* 模块名：MMS
* 程序名: PumpCartCost.js
**/

var viewModel = function () {
    var self = this;
    debugger;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 60 },
        url: '/api/Mms/SalseReport/GetPumpCartCostList',
        //data: { BegMonthDate: '', EndDatetime: '' },
        queryParams: ko.observable(),
        //pagination: false
        pagination: true,
        onLoadSuccess: function () {
            debugger;
            self.IniTotal();
        },
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'id', '_id');
            return d;
        }

    };
    this.grid.queryParams(data.form);
    this.searchClick = function () {
        var param = ko.toJS(this.form);
        this.grid.queryParams(param);
    }


    this.IniTotal = function () {
        com.ajax({
            type: 'GET',
            data: ko.mapping.toJS(self.form),
            url: '/api/mms/SalseReport/GetTotalPumpCartCostList',
            success: function (d) {
                if (d.Amount < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
          

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['Amount']);
                    rowTota2 += parseFloat(rows[i]['Summoney']);
               

                }
                self.grid.datagrid("appendRow", {
                    Pump_No: '<b>小计：</b>', Amount: rowTotal, Summoney: rowTota2
                });
                debugger;
                self.grid.datagrid("appendRow", {
                    Pump_No: '<b>合计：</b>', Amount: d.Amount, Summoney: d.Summoney
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
