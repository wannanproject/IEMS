//查询物料信息
Ext.create("Ext.window.Window", {
//查询带回窗体
    id: "McUI_SearchBox_SearchInLocData_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchInLocData.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchInLocData_Request = function (record) {
    //返回值处理
    App.txtAddSLocName.setValue(record.data.LOC_NAME);
    App.txtAddSLocNo.setValue(record.data.LOC_NO);
}

var TriggerFieldSearchInLocDatas = function () {//查询
    var window = App.McUI_SearchBox_SearchInLocData_Window;
    window.show();
}