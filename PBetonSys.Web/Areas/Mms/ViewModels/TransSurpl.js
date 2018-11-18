/**
* 模块名：MMS
* 程序名: TransSurpl.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 55 },
        url: '/api/Mms/TransSurpl/GetTransSurplList',
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
            url: '/api/mms/TransSurpl/GetTotalTransSurpl',
            success: function (d) {
                if (d.trans_out_value < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['trans_out_value']);
                    rowTota2 += parseFloat(rows[i]['Trans_in_value']);
                 
                }
                //self.grid.datagrid("appendRow", {
                //    施工单位: '<b>合计：</b>', 预订方量: rowTotal, 生产方量: rowTota2, 签收方量: rowTota3, 累计车次: rowTota4
                //});
                self.grid.datagrid("appendRow", { 转出工程名称: '<b>合计：</b>', trans_out_value: d.trans_out_value, Trans_in_value: d.Trans_in_value });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };


};
