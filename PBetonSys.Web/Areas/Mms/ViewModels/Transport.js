/**
* 模块名：MMS
* 程序名: Transport.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 100 },
        url: '/api/Mms/Product/GetTransportList',
        //data: { BegMonthDate: '', EndDatetime: '' },
        queryParams: ko.observable(),
        //pagination: false
        pagination: true,
        onLoadSuccess: function () {
            self.IniTotal();
        },
        loadFilter: function (d) {
            d.rows = utils.copyProperty(d.rows, 'Transp_ID', '_id');
            return d;
        }
    };
    this.grid.queryParams(ko.toJS(self.form)); //
    this.searchClick = function () {
        var param = ko.toJS(this.form);
        this.grid.queryParams(param);
    }


    this.IniTotal = function () {
        com.ajax({
            type: 'GET',
            data: ko.mapping.toJS(self.form),
            url: '/api/mms/Product/GetTotalTransportList',
            success: function (d) {
                if (d.Produce_Amount <1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['Produce_Amount']);
                    rowTota2 += parseFloat(rows[i]['amount']);
                    rowTota3 += parseFloat(rows[i]['Other']);
                    rowTota4 += parseFloat(rows[i]['Number']);
                }
                //self.grid.datagrid("appendRow", {
                //    Vehicle_id: '<b>小计：</b>', Produce_Amount: rowTotal, amount: rowTota2, Other: rowTota3, Number: rowTota4
                //});
                //debugger;
                self.grid.datagrid("appendRow", {
                    Vehicle_id: '<b>合计：</b>', Produce_Amount: d.Produce_Amount, amount: d.amount, Other: d.Other, Number: d.Number
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
