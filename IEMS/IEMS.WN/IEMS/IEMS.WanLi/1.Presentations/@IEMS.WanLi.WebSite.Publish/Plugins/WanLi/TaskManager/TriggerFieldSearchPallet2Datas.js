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
    App.txtAddPalletName2.setValue(record.data.PALLET_NAME);
    App.txtAddPallet2.setValue(record.data.PALLET_NO);
}

var TriggerFieldSearchPallet2Datas = function () {//查询
    var window = App.McUI_SearchBox_SearchPallet2Data_Window;
    window.show();
}