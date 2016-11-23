/*
Custom configuration created by Jonathan Raynor. 11/17/2106
jraynor@andrewandsons.com
*/

//Hide date filter display
    $("#sortPanel").hide();
//Colors the TRs the correct color depending on POLineStatusID
function onDataBound() {
    var grid = this;
    grid.tbody.find('>tr').each(function () {
        var dataItem = grid.dataItem(this);
        switch (dataItem.POStatusID)
        {
            case (3):
                $(this).addClass('rejected');
                break;
            case (4):
                $(this).addClass('delivered');
                break;
            case(5):
                $(this).addClass('cancelled');
                break;
            case (7):
                $(this).addClass('loading');
                break;
            case (15):
                $(this).addClass('intransit');
                break;
            default:
                $(this).addClass('normal');
        }

    });

    $("#grid tr.k-alt").removeClass("k-alt");
}

function CopyCustPOLine(gridData)
{
    var dataItem = this.dataItem($(gridData.currentTarget).closest("tr"));
    //console.log(dataItem.CustPOLineID);
    $.ajax({
        type: 'POST',
        data: {
            CustPOLineID: dataItem.CustPOLineID
        },
        url: '/Grid/DuplicateSale',
        dataType: 'json',
        success: function (ADUserName) {
            //Clear results

        }
    });

}


//Additional data posted to the AllSales method on the GridController
function clientData() {
    var userSelectedCommodity = $("#dropDownCommodities").data("kendoDropDownList").value();
    if (!userSelectedCommodity)
    {
        userSelectedCommodity = "All";
    }
    return {
        fromDate: $("#datePickerFrom").val(),
        toDate: $("#datePickerTo").val(),
        commodity: userSelectedCommodity
    }
}

$(document.body).keydown(function(e) {
    if (e.altKey && e.keyCode == 87) {
        $("#grid").data("kendoGrid").table.focus();
    }
});



$(document).ready(function () {

    var grid = $("#grid").data("kendoGrid");
    //function for updating data set for the grid depending on the users date picker selection.
    $("#btnSetUserFilters").click(function (e) {
        e.preventDefault();
        grid.dataSource.fetch();
    });
    //Date manipulation class
    var dateManipulation = function()
    {
        var date = new Date();
        return {
            monthStarts: new Date(date.getFullYear(), date.getMonth(), 1),
            monthEnds: new Date(date.getFullYear(), date.getMonth() + 1, 0)
        }
    }
    //function for updating data set for the grid depending on the users date picker selection.
    $("#btnClearUserFilters").click(function (e) {
        e.preventDefault();
        var date = dateManipulation();

        $("#datePickerFrom").data("kendoDatePicker").value(date.monthStarts);
        $("#datePickerTo").data("kendoDatePicker").value(date.monthEnds);
        $("#dropDownCommodities").data("kendoDropDownList").value("All");
        $("#sortPanel").toggle("slow");
        grid.dataSource.fetch();
    });

    //Resets all seach filters
    $("#clearFilters").click(function (e) {
          e.preventDefault();
          var datasource = $("#grid").data("kendoGrid").dataSource;
          datasource.filter([]);
    });
    $("#hideDateFilter").click(function () {
        $("#sortPanel").toggle("fast");
    });
    $("#showDateFilters").click(function (e) {
        e.preventDefault();
        $("#sortPanel").toggle("fast");
    });
});
