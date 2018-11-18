/**
* 模块名：MMS
* 程序名: Receive.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 80 },
        url: '/api/Mms/Receive/GetReceiveData',
        //data: { BegMonthDate: '', EndDatetime: '' },
        queryParams: ko.observable(),
        //pagination: false
        pagination: true,
        onLoadSuccess: function () {
            self.IniTotal();
        },
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
            url: '/api/mms/Receive/GetTotalReceiveData',
            success: function (d) {
                if (d.qty < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4 = 0;
                var rowTota5 = 0;
                var rowTota6 = 0;
                var rowTota7 = 0;
                var rowTota8 = 0;
                var rowTota9 = 0;
                var rowTota10 = 0;
                var rowTota11= 0;
                var rowTota12= 0;
                var rowTota13= 0;
                var rowTota14= 0;
                var rowTota15 = 0;

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTota15 += parseFloat(rows[i]['qty']);
                    rowTotal += parseFloat(rows[i]['num1']);
                    rowTota2 += parseFloat(rows[i]['num2']);
                    rowTota3 += parseFloat(rows[i]['num3']);
                    rowTota4 += parseFloat(rows[i]['num4']);
                    rowTota5 += parseFloat(rows[i]['num5']);
                    rowTota6 += parseFloat(rows[i]['num6']);
                    rowTota7 += parseFloat(rows[i]['num7']);
                    rowTota8 += parseFloat(rows[i]['num8']);
                    rowTota9 += parseFloat(rows[i]['num9']);
                    rowTota10 += parseFloat(rows[i]['num10']);
                    rowTota11 += parseFloat(rows[i]['num11']);
                    rowTota12 += parseFloat(rows[i]['num12']);
                    rowTota13 += parseFloat(rows[i]['num13']);
                    rowTota14 += parseFloat(rows[i]['sumNum']);
                  
                }
                //self.grid.datagrid("appendRow", {
                //    Mstring5: '<b>小计：</b>', qty: rowTota15, num1: rowTotal, num2: rowTota2, num3: rowTota3, num4: rowTota4, num5: rowTota5, num6: rowTota6, num7: rowTota7, num8: rowTota8, num9: rowTota9, num10: rowTota10, num11: rowTota11, num12: rowTota12, num13: rowTota13, sumNum: rowTota14
                //});
               
                self.grid.datagrid("appendRow", { Mstring5: '<b>合计：</b>', qty: d.qty, num1: d.num1, num2: d.num2, num3: d.num3, num4: d.num4, num5: d.num5, num6: d.num6, num7: d.num7, num8: d.num8, num9: d.num9, num10: d.num110, num11: d.num11, num12: d.num12, num13: d.num13, sumNum: d.sumNum });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
