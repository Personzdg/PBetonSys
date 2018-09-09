/**
* 模块名：MMS
* 程序名: Product.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 4 },
        url: '/api/Mms/ProjectAlltab/GetProjectAlltabList',
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
            url: '/api/mms/ProjectAlltab/GetTotalProjectAlltabList',
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
                var rowTota15 = 0;

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTota15 += parseFloat(rows[i]['本日销售方量']);
                    rowTotal += parseFloat(rows[i]['本日销售金额']);
                    rowTota2 += parseFloat(rows[i]['本月销售方量']);
                    rowTota3 += parseFloat(rows[i]['本月销售金额']);
                    rowTota4 += parseFloat(rows[i]['累计销售方量']);
                    rowTota5 += parseFloat(rows[i]['累计销售金额']);
                    rowTota6 += parseFloat(rows[i]['本月实际收款']);
                    rowTota7 += parseFloat(rows[i]['本月累计收款']);
                    rowTota8 += parseFloat(rows[i]['其他扣除']);
                    rowTota9 += parseFloat(rows[i]['累计欠款']);

                }
                self.grid.datagrid("appendRow", {
                    合同编号: '<b>合计：</b>',
                    本日销售方量: rowTota15,
                    本日销售金额: rowTotal,
                    本月销售方量: rowTota2,
                    本月销售金额: rowTota3,
                    累计销售方量: rowTota4,
                    累计销售金额: rowTota5,
                    本月实际收款: rowTota6,
                    本月累计收款: rowTota7,
                    其他扣除: rowTota8,
                    累计欠款: rowTota9
                });

                //self.grid.datagrid("appendRow", {
                //    合同编号: '<b>合计：</b>',
                //    本日销售方量: d.本日销售方量,
                //    本日销售金额: d.本日销售金额,
                //    本月销售方量: d.本月销售方量,
                //    累计销售方量: d.累计销售方量,
                //    累计销售金额: d.累计销售金额,
                //    本月实际收款: d.本月实际收款,
                //    本月累计收款: d.本月累计收款,
                //    其他扣除: d.其他扣除,
                //    累计欠款: d.累计欠款
                //});

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
