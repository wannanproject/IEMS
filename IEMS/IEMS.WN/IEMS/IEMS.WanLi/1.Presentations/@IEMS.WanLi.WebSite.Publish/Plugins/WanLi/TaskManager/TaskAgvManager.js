var saveAddWindows = function () {
    var palletId1 = App.txtAddPalletNo1.getValue();
    var palletId2 = App.txtAddPalletNo2.getValue();
    var sLocNo = App.txtAddSLocNo.getValue();
    var eLocNo = App.txtAddELocNo.getValue();
    var sLocName = App.txtAddSLocName.getValue();
    var eLocName = App.txtAddELocName.getValue();
    if (sLocNo == null || sLocNo == "") {
        Ext.Msg.alert("错误", '请选择起始站点!');
        return;
    }
    if (eLocNo == null || eLocNo == "") {
        Ext.Msg.alert("错误", '请选择目标站点!');
        return;
    }
    if ((palletId1 == null || palletId1 == "") && (palletId2 == null || palletId2 == "")) {
        Ext.Msg.alert("错误", '请选择入库工装!');
        return;
    }
    App.direct.AddAgvTask(palletId1, palletId2, sLocNo, eLocNo, {
        success: function (result) {
            if (result == "") {
                Ext.Msg.alert('成功', "AGV任务添加成功！");
            }
            else {
                Ext.Msg.alert('错误', result);
            }
            App.winAdd.close();
            gridMainFresh();
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('异常', errorMsg);
        }
    });
    return false;
};

var commandcolumn_click = function (command, record) {
    if (command.toLowerCase() == "delete") {
        commandcolumn_click_confirm(command, record);
    }
    return false;
};
var commandcolumn_click_confirm = function (command, record) {
    if (command.toLowerCase() == "delete") {
        Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) {
            if (btn == "yes") {
                deleteRecordData(record);
            }
        });
    }
    return false;
};
var deleteRecordData = function (record) {
    var taskNo = record.data.TASK_NO;
    App.direct.DeleteAgvTask(taskNo, {
        success: function (result) {
            if (result == "") {
                Ext.Msg.alert('成功', "AGV任务删除成功！");
            }
            else {
                Ext.Msg.alert('错误', result);
            }
            gridMainFresh();
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('异常', errorMsg);
        }
    });
}


//初始化添加显示
var showAddWindow = function () {
    App.txtAddSLocName.setValue("");
    App.txtAddSLocNo.setValue("");
    App.txtAddELocName.setValue("");
    App.txtAddELocNo.setValue("");
    App.txtAddPalletNo1.setValue("");
    App.txtAddPalletName1.setValue("");
    App.txtAddPalletNo2.setValue("");
    App.txtAddPalletName2.setValue("");
    App.winAdd.show();
}



Ext.create("Ext.window.Window", {
    //查询带回窗体
    id: "McUI_SearchBox_SearchAgvSLocData_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchAgvSLocData.aspx' width=550 height=430 scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择起始站台",
    modal: true
});

var McUI_SearchBox_SearchAgvSLocData_Request = function (record) {//返回值处理
    if (!App.winAdd.hidden) {
        App.txtAddSLocName.setValue(record.data.LOC_NAME);
        App.txtAddSLocNo.setValue(record.data.LOC_NO);
    }
    else {
        App.txtSearcheSLocName.setValue(record.data.LOC_NAME);
        App.txtSearcheSLocNo.setValue(record.data.LOC_NO);
    }
}

var TriggerFieldSearchAgvSLocData = function () {//查询
    var window = App.McUI_SearchBox_SearchAgvSLocData_Window;
    window.show();
}


Ext.create("Ext.window.Window", {
    //查询带回窗体
    id: "McUI_SearchBox_SearchAgvELocData_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchAgvELocData.aspx' width=550 height=430 scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择结束站台",
    modal: true
});

var McUI_SearchBox_SearchAgvELocData_Request = function (record) {//返回值处理
    if (!App.winAdd.hidden) {
        App.txtAddELocName.setValue(record.data.LOC_NAME);
        App.txtAddELocNo.setValue(record.data.LOC_NO);
    }
    else {
        App.txtSearcheELocName.setValue(record.data.LOC_NAME);
        App.txtSearcheELocNo.setValue(record.data.LOC_NO);
    }
}

var TriggerFieldSearchAgvELocData = function () {//查询
    var window = App.McUI_SearchBox_SearchAgvELocData_Window;
    window.show();
}

//查询物料信息
Ext.create("Ext.window.Window", {
    //查询带回窗体
    id: "McUI_SearchBox_SearchPallet1Data_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchPallet1Data.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchPallet1Data_Request = function (record) {
    //返回值处理
    App.txtAddPalletName1.setValue(record.data.PALLET_NO);
    App.txtAddPalletNo1.setValue(record.data.PALLET_NO);
}

var TriggerFieldSearchPallet1Datas = function () {//查询
    var window = App.McUI_SearchBox_SearchPallet1Data_Window;
    window.show();
}

//查询物料信息
Ext.create("Ext.window.Window", {
    //查询带回窗体
    id: "McUI_SearchBox_SearchPallet2Data_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchPallet2Data.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchPallet2Data_Request = function (record) {
    //返回值处理
    App.txtAddPalletName2.setValue(record.data.PALLET_NO);
    App.txtAddPalletNo2.setValue(record.data.PALLET_NO);
}

var TriggerFieldSearchPallet2Datas = function () {//查询
    var window = App.McUI_SearchBox_SearchPallet2Data_Window;
    window.show();
}


var TriggerFieldAgvSLocData = function (a, b, c, d, e, f) {
    if (c == 0) {
        App.txtSearcheSLocNo.setValue("");
        App.txtSearcheSLocName.setValue("");
    }
    else {
        var window = App.McUI_SearchBox_SearchAgvSLocData_Window;
        window.show();
    }
}
var TriggerFieldAgvELocData = function (a, b, c, d, e, f) {
    if (c == 0) {
        App.txtSearcheELocNo.setValue("");
        App.txtSearcheELocName.setValue("");
    }
    else {
        var window = App.McUI_SearchBox_SearchAgvELocData_Window;
        window.show();
    }
}