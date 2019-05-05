//查询物料信息
Ext.create("Ext.window.Window", {
//查询带回窗体
    id: "McUI_SearchBox_SearchBinMaterial_Window",
    height: 460,
    hidden: true,
    width: 560,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchBinMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_SearchBinMaterial_Request = function (record) {//返回值处理
    if (!App.winAdd.hidden) {
        App.txtAddLineMaterialName.setValue(record.data.MATER_NAME);
        App.txtAddLineMaterialNo.setValue(record.data.MATER_NO);
        App.txtAddLineMaxQty.setValue(record.data.BIN_COUNT);
    }
    else if (!App.winModify.hidden) {
        App.txtModifyLineMaterialName.setValue(record.data.MATER_NAME);
        App.txtModifyLineMaterialNo.setValue(record.data.MATER_NO);
        App.txtModifyLineMaxQty.setValue(record.data.BIN_COUNT);
    }
    else {
        App.txtAddLineMaterialName.setValue(record.data.MATER_NAME);
        App.txtAddLineMaterialNo.setValue(record.data.MATER_NO);
        App.txtAddLineMaxQty.setValue(record.data.BIN_COUNT);
    }
}

var TriggerFieldSearchMaterial = function () {//查询
    var window = App.McUI_SearchBox_SearchBinMaterial_Window;
    //window.setTitle("请选择物料");
    //var html = "<iframe src='" + thisRootUrl + "McUI/SearchBox/SearchBinMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    //if (window.getBody()) {
    //    window.getBody().update(html);
    //} else {
    //    window.html = html;
    //}
    window.show();
}