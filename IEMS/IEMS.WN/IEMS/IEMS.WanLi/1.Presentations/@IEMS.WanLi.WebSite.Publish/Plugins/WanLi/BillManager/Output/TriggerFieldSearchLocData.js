//查询物料信息
Ext.create("Ext.window.Window", {
//查询带回窗体
    id: "McUI_SearchBox_SearchOutLocData_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchOutLocData.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchOutLocData_Request = function (record) {//返回值处理
    if (!App.winAdd.hidden) {
        App.txtAddLineEndLocName.setValue(record.data.LOC_NAME);
        App.txtAddLineEndLocNo.setValue(record.data.LOC_NO);
    }
    else if (!App.winModify.hidden) {
        App.txtModifyLineEndLocName.setValue(record.data.LOC_NAME);
        App.txtModifyLineEndLocNo.setValue(record.data.LOC_NO);
    }
    else {
        App.txtAddLineEndLocName.setValue(record.data.LOC_NAME);
        App.txtAddLineEndLocNo.setValue(record.data.LOC_NO);
    }
}

var TriggerFieldSearchOutLocData = function () {//查询
    var window = App.McUI_SearchBox_SearchOutLocData_Window;
    window.show();
}