/**
* 模块名：MMS
* 程序名: ProvideResource.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 3, h: 60 },
        url: '/api/Mms/Resource/GetProvideResourceList',
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
    this.grid.queryParams(ko.toJS(self.form));
    this.searchClick = function () {
        var param = ko.toJS(this.form);
        this.grid.queryParams(param);
    }


    this.IniTotal = function () {
        com.ajax({
            type: 'GET',
            data: ko.mapping.toJS(self.form),
            url: '/api/mms/Resource/GetTotalResourceList',
            success: function (d) {
                if (d.a_suttle < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
                var rowTota4 = 0;
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['a_gross']);
                    rowTota2 += parseFloat(rows[i]['a_empty']);
                    rowTota3 += parseFloat(rows[i]['a_suttle']);
                    rowTota4 += parseFloat(rows[i]['a_money']);

                }
                //self.grid.datagrid("appendRow", {
                //    silo_id: '<b>小计：</b>', a_gross: rowTotal, a_empty: rowTota2, a_suttle: rowTota3, a_money: rowTota4
                //});
                //debugger;
                self.grid.datagrid("appendRow", {
                    silo_id: '<b>合计：</b>', a_gross: d.a_gross, a_empty: d.a_empty, a_suttle: d.a_suttle, a_suttleSJ: d.a_suttleSJ, a_money: d.a_money, FS: d.FS, FSMoney: d.FSMoney,
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
