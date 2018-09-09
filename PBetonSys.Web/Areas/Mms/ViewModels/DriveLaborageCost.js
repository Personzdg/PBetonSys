/**
* 模块名：MMS
* 程序名: DriveLaborageCost.js
**/

var viewModel = function () {
    var self = this;
    debugger;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 50 },
        url: '/api/Mms/SalseReport/GetDriveLaborageCostList',
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
            url: '/api/mms/SalseReport/GetTotalDriveLaborageCostList',
            success: function (d) {
                if (d.Amount < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4= 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['Amount']);
                    rowTota2 += parseFloat(rows[i]['Sum_Money']);
                    rowTota3 += parseFloat(rows[i]['SjAmount']);
                    rowTota4 += parseFloat(rows[i]['SJSum_money']);
                }
                self.grid.datagrid("appendRow", {
                    CheckDateTime: '<b>小计：</b>', Amount: rowTotal, Sum_Money: rowTota2, SjAmount: rowTota3, SJSum_money: rowTota4
                });
                debugger;
                self.grid.datagrid("appendRow", {
                    CheckDateTime: '<b>合计：</b>', Amount: d.Amount, Sum_Money: d.Sum_Money, SjAmount: d.SjAmount, SJSum_money: d.SJSum_money
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
