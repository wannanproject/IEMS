//查询物料信息
Ext.create("Ext.window.Window", {
//查询带回窗体
    id: "McUI_SearchBox_SearchSrmData_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchSrmData.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchSrmData_Request = function (record) {
    //返回值处理
    App.txtSrnName.setValue(record.data.CRN_NAME);
    App.txtSrmNo.setValue(record.data.CRN_NO);
    App.txtMinCol.setValue(record.data.MIN_BAY);
    App.txtMaxCol.setValue(record.data.MAX_BAY);
}

var TriggerFieldSearchSrmData = function () {//查询
    var window = App.McUI_SearchBox_SearchSrmData_Window;
    window.show();
}