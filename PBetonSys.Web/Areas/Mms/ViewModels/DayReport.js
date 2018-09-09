/**
* 模块名：MMS
* 程序名: DayReport.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 4 },
        url: '/api/Mms/DayReport/GetDayReportList',
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
            url: '/api/mms/DayReport/GetTotalDayReport',
            success: function (d) {
                if (d.预订方量 < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['预订方量']);
                    rowTota2 += parseFloat(rows[i]['生产方量']);
                    rowTota3 += parseFloat(rows[i]['签收方量']);
                    rowTota4 += parseFloat(rows[i]['累计车次']);
                }
                self.grid.datagrid("appendRow", {
                    施工单位: '<b>合计：</b>', 预订方量: rowTotal, 生产方量: rowTota2, 签收方量: rowTota3, 累计车次: rowTota4
                });
                //self.grid.datagrid("appendRow", { 施工单位: '<b>合计：</b>', 预订方量: d.预订方量, 生产方量: d.生产方量, 签收方量: d.签收方量, 累计车次: d.累计车次 });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };


};
