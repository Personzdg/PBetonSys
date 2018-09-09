/**
* 模块名：MMS
* 程序名: Resource.js
**/

var viewModel = function () {
    var self = this;
    this.form = ko.mapping.fromJS(data.form);
    this.grid = {
        size: { w: 4, h: 50 },
        url: '/api/Mms/Resource/GetResourceList',
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
            url: '/api/mms/Resource/GetTotalResourceList',
            success: function (d) {
                if (d.a_suttle < 1) {
                    return false;
                }
                var rowTotal = 0;
                var rowTota2 = 0;
                var rowTota3 = 0;
           
                var rows = self.grid.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    rowTotal += parseFloat(rows[i]['a_gross']);
                    rowTota2 += parseFloat(rows[i]['a_empty']);
                    rowTota3 += parseFloat(rows[i]['a_suttle']);
                
                }
                self.grid.datagrid("appendRow", {
                    silo_id: '<b>小计：</b>', a_gross: rowTotal, a_empty: rowTota2, a_suttle: rowTota3
                });
                debugger;
                self.grid.datagrid("appendRow", {
                    silo_id: '<b>合计：</b>', a_gross: d.a_gross, a_empty: d.a_empty, a_suttle: d.a_suttle
                });

            }
        });
    }

    this.downloadClick = function (vm, event) {
        com.exporter(self.grid).download($(event.currentTarget).attr("suffix"));
    };
};
