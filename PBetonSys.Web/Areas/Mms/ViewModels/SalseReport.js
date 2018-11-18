/**
* 模块名：MMS
* 程序名: SalseReport.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 60 },
        url: '/api/Mms/SalseReport/GetSalseReportList',
        //data: { BegMonthDate: '', EndDatetime: '' },
        queryParams: ko.observable(),
        //pagination: false
        pagination: true,
        onLoadSuccess: function () {
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
            url: '/api/mms/SalseReport/GetTotalSalseReportList',
            success: function (d) {
                if (d.Amount < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['Amount']);
                    rowTota2 += parseFloat(rows[i]['Money']);
                    rowTota3 += parseFloat(rows[i]['OtherMoney']);

                }
                self.grid.datagrid("appendRow", {
                    Price: '<b>小计：</b>', Amount: rowTotal, Money: rowTota2, OtherMoney: rowTota3
                });
                debugger;
                self.grid.datagrid("appendRow", {
                    Price: '<b>合计：</b>', Amount: d.Amount, Money: d.Money, OtherMoney: d.OtherMoney
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
