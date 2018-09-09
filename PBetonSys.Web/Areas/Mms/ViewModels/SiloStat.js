/**
* 模块名：MMS
* 程序名: SiloStat.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 50 },
        url: '/api/Mms/SiloStat/GetSiloStatList',
        //data: { BegMonthDate: '', EndDatetime: '' },
        queryParams: ko.observable(),
         ////pagination: false
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
            url: '/api/mms/SiloStat/GetSNTotalSiloStat',
            success: function (d) {
                if (d.本日库存<1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['昨日库存']);
                    rowTota2 += parseFloat(rows[i]['当日入库']);
                    rowTota3 += parseFloat(rows[i]['当日出库']);
                    rowTota4 += parseFloat(rows[i]['本日库存']);
                }
                debugger;
                self.grid.datagrid("appendRow", {
                    silo_id: '<b>合计：</b>', 昨日库存: rowTotal, 当日入库: rowTota2, 当日出库: rowTota3, 本日库存: rowTota4
                });
                self.grid.datagrid("appendRow", { silo_id: '<b>水泥库存：</b>', 昨日库存: d.昨日库存, 当日入库: d.当日入库, 当日出库: d.当日出库, 本日库存: d.本日库存 });

              

            }
        });
    }
    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
