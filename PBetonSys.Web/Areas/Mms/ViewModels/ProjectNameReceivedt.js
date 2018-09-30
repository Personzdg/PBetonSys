/**
* 模块名：MMS
* 程序名: ProjectNameReceivedt.js
**/

var viewModel = function () {
    var self = this;
    this.form = data.form;
    this.dataSource = data.dataSource;
    this.grid = {
        size: { w: 4, h: 60 },
        url: '/api/Mms/Receive/GetProjectNameReceivedt',
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
            url: '/api/mms/Receive/GetTotalProjectNameReceivedt',
            success: function (d) {
                if (d.方量 < 1) {
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
                var rowTota11 = 0;
                var rowTota12 = 0;
                var rowTota13 = 0;
                var rowTota14 = 0;
                var rowTota15 = 0;

                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                  
                    rowTotal += parseFloat(rows[i]['方量']);
                    rowTota2 += parseFloat(rows[i]['总胶量']);
                    rowTota3 += parseFloat(rows[i]['summoney']);
                    rowTota4 += parseFloat(rows[i]['泵费']);
                    rowTota5 += parseFloat(rows[i]['运费']);
                    rowTota6 += parseFloat(rows[i]['mtvalue1_1']);
                    rowTota7 += parseFloat(rows[i]['mtvalue2']);
                    rowTota8 += parseFloat(rows[i]['mtvalue3']);
                    rowTota9 += parseFloat(rows[i]['mtvalue4']);
                    rowTota10 += parseFloat(rows[i]['mtvalue5']);
                    rowTota11 += parseFloat(rows[i]['mtvalue6']);
                    rowTota12 += parseFloat(rows[i]['mtvalue7']);
                    rowTota13 += parseFloat(rows[i]['mtvalue8']);
                    rowTota14 += parseFloat(rows[i]['mtvalue9']);
                 
                    //rowTota15 += parseFloat(rows[i]['qty']);

                }
                //self.grid.datagrid("appendRow", {
                //    Mstring5: '<b>小计：</b>', qty: rowTota15, num1: rowTotal, num2: rowTota2, num3: rowTota3, num4: rowTota4, num5: rowTota5, num6: rowTota6, num7: rowTota7, num8: rowTota8, num9: rowTota9, num10: rowTota10, num11: rowTota11, num12: rowTota12, num13: rowTota13, sumNum: rowTota14
                //});

                self.grid.datagrid("appendRow", { 工程名称: '<b>合计：</b>', 方量: d.方量, 总胶量: d.总胶量, summoney: d.summoney, 泵费: d.泵费, 运费: d.运费, mtvalue1_1: d.mtvalue1_1, mtvalue1_2: d.mtvalue1_2, mtvalue2_1: d.mtvalue2_1, mtvalue2_2: d.mtvalue2_2, mtvalue2: d.mtvalue2, mtvalue3: d.mtvalue3, mtvalue4: d.mtvalue4, mtvalue5: d.mtvalue5, mtvalue6: d.mtvalue6, mtvalue7: d.mtvalue7, mtvalue8: d.mtvalue8, mtvalue9: d.mtvalue9 });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
