//查询物料信息
Ext.create("Ext.window.Window", {
    //查询带回窗体
    id: "McUI_SearchBox_PsbMaterial_Window",
    height: 460,
    hidden: true,
    width: 368,
    html: "<iframe src='" + thisRootUrl + "McUI/SearchBox/PsbMaterial.aspx' width=360 height=460 scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: true
});

var McUI_SearchBox_PsbMaterial_Request = function (record) {//返回值处理
    if (!App.winAdd.hidden) {
        App.txtShowAddLineMaterial.setValue(record.data.MATER_NAME);
        App.txtAddLineMaterial.setValue(record.data.MATER_NO);
    }
    else if (!App.winModify.hidden) {
        App.txtShowAddModifyLineMaterial.setValue(record.data.MATER_NAME);
        App.txtModifyLineMaterial.setValue(record.data.MATER_NO);
    }
    else {
        App.txtShowAddLineMaterial.setValue(record.data.MATER_NAME);
        App.txtAddLineMaterial.setValue(record.data.MATER_NO);
    }
}

var TriggerFieldSerchMaterial = function () {//查询

    App.McUI_SearchBox_PsbMaterial_Window.show();
}