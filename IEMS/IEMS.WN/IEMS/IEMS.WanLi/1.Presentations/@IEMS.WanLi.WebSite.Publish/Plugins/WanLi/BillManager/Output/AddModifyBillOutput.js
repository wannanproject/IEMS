//获取单据行号
var gridMainLineLength = function () {
    var maxlineId = 0;
    try {
        var store = App.gridMain.store;
        for (var i = 0; i < store.data.items.length; i++) {
            var lineId = store.data.items[i].data.LINE_ID;
            if (lineId > maxlineId) {
                maxlineId = lineId;
            }
        }
    }
    catch (e) {
    }
    return maxlineId + 1;
}

//刷新行号
var refreshRowNum = function (store) {
    for (var i = 0; i < store.data.items.length; i++) {
        store.data.items[i].index = i;
    }
    store.loadRecords(store.data.items, true);
};

//初始化添加显示
var showAddLineWindows = function () {
    if (App.txtAddLineBillStatus) {
        App.txtAddLineBillStatus.setValue(App.txtBillStatus.getRawValue());       //单据状态
    }
    if (App.txtAddLineBillStatus) {
        App.txtAddLineBillStatus.setValue(App.txtBillStatus.getRawValue());       //单据状态
    }
    App.txtAddLineOrderNo.setValue(App.txtOrderNo.getValue());  //出库单号
    App.txtAddLineLineId.setValue(gridMainLineLength());        //单行行号
    App.txtAddLineRequireQty.setValue(1);                       //单据数量
    App.txtAddLineMaterialName.setValue("");                    //物料信息
    App.txtAddLineMaterialNo.setValue("");                      //物料信息
    App.txtAddLineBinNo.setValue("");                           //开始库位
    App.txtAddLineGuid.setValue("");                            //产品GUID号
    App.txtAddLinePalletRfid.setValue("");                      //工装编号
    App.txtAddLineEndLocNo.setValue("");                        //目的站台
    App.txtAddLineEndLocName.setValue("");                      //目的站台
    App.txtAddLineStartDate.setValue("");                       //开始时间
    App.txtAddLineEndDate.setValue("");                         //结束时间
    App.winAdd.show();
}
//添加行信息保存
var saveAddLineWindows = function () {

    var LINE_ID = App.txtAddLineLineId.getValue();   //单行行号
    if (LINE_ID == "") {
        Ext.Msg.alert("失败", "单行行号不能为空！");
        return;
    }

    var store = App.gridMain.getStore();
    var items = store.items;
    for (var i = 0; i < store.data.items.length; i++) {
        if (LINE_ID == store.data.items[i].data.LINE_ID) {
            Ext.Msg.alert("失败", '单行行号不能重复！');
            return;
        }
    }
    var modelName = store.model.getName();
    var r = Ext.create(modelName);
    r.data.LINE_ID = App.txtAddLineLineId.getValue();                       //单行行号
    r.data.MATERIAL_NO = App.txtAddLineMaterialNo.getValue();               //物料信息
    r.data.MATER_NAME = App.txtAddLineMaterialName.getValue();              //物料信息
    r.data.REQUIRE_QTY = App.txtAddLineRequireQty.getValue();               //单据数量
    r.data.LIMIT_BIN_NO = App.txtAddLineBinNo.getValue();                   //开始库位
    r.data.ELOC_NO = App.txtAddLineEndLocNo.getValue();                     //目的库位
    r.data.ELOC_NAME = App.txtAddLineEndLocName.getValue();                 //目的库位
    r.data.ORDER_NO = App.txtAddLineOrderNo.getValue();                     //出库单号
    r.data.PRODUCT_GRADE = App.txtAddLineGrade.getValue();                  //等级
    r.data.LIMIT_PALLET_ID = App.txtAddLinePalletRfid.getValue();           //工装编号
    r.data.LIMIT_PRODUCT_GUID = App.txtAddLineGuid.getValue();              //物料GUID
    r.data.LIMIT_DATE_START = App.txtAddLineStartDate.getValue();           //物料GUID
    r.data.LIMIT_DATE_END = App.txtAddLineEndDate.getValue();               //物料GUID
    r.data.DELETE_FLAG = "A";
    store.data.add(r);
    refreshRowNum(store);
    App.winAdd.close();
}

//获得基础信息的store方法
var getBillHeaderStore = function () {
    var ORDER_PRIORITY = 50;
    var SOURCE_TYPE = 0;
    var ORDER_TYPE_NO = "100030";
    var ORDER_TYPE_MODULE = "O";
    return {
        ORDER_NO: App.txtOrderNo.getValue(),
        ORDER_STATUS: App.txtBillStatus.getValue(),
        ORDER_PRIORITY: ORDER_PRIORITY,
        SOURCE_TYPE: SOURCE_TYPE,
        ORDER_TYPE_NO: ORDER_TYPE_NO,
        ORDER_TYPE_MODULE: ORDER_TYPE_MODULE
    }
}

var storeToJson = function (store) {
    if (!store) {
        return '';
    }
    if (store.type == "store") {
        arr = new Array();
        Ext.each(store.data.items, function (record) {
            arr.push(record.data);
        });
        return Ext.encode(arr);
    }
    else {
        return Ext.encode(store);
    }
}

//获得基础信息的store方法
var postBillData = function () {
    App.direct.SaveJsonInfo(storeToJson(getBillHeaderStore()), storeToJson(App.gridMain.store),
            {
                success: function (result) {
                    if (result == "") {
                        Ext.Msg.alert('成功', "数据保存成功！", function (btn) { closeThisWindow() });
                    } else {
                        Ext.Msg.alert("失败", result);
                    }
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert("错误", errorMsg);
                }
            });
}

//取消保存的订单
var closeThisWindow = function () {
    try {
        this.parent.App.Plugins_Wms_OutputWork_OutPutReceipt_AddOutPutReceipt_Window.close();
    }
    catch (e) {
    }
}
//修改和删除订单
var commandcolumn_click = function (command, record) {
    if (command.toLowerCase() == "edit") {
        showModifyLineWindows(record);
    }
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

//删除订单信息
var deleteRecordData = function (record) {
    var LINE_ID = record.data.LINE_ID;
    var store = App.gridMain.getStore();
    for (var i = 0; i < store.data.items.length; i++) {
        if (LINE_ID == store.data.items[i].data.LINE_ID) {
            if ((!store.data.items[i].data.DELETE_FLAG) || store.data.items[i].data.DELETE_FLAG == "N") {
                store.data.items[i].data.DELETE_FLAG = "Y";
            }
            else {
                store.data.removeAt(i);
            }
            break;
        }
    }
    refreshRowNum(store);
    return false;
};

var saveModifyLineWindows = function (command, record) {
    if (command.toLowerCase() == "edit") {
        commandcolumn_direct_edit(record);
    }
    return false;
};
//添加界面修改菜单
var showModifyLineWindows = function (record) {
    App.txtModifyLineOrderNo.setValue(record.data.ORDER_NO);            //出库单号
    App.txtModifyLineLineId.setValue(record.data.LINE_ID);              //单行行号
    App.txtModifyLineRequireQty.setValue(record.data.REQUIRE_QTY);      //单据数量
    App.txtModifyLineBinNo.setValue(record.data.LIMIT_BIN_NO);          //开始库位
    App.txtModifyLineEndLocNo.setValue(record.data.ELOC_NO);            //目的站台
    App.txtModifyLineEndLocName.setValue(record.data.ELOC_NAME);        //目的站台
    App.txtModifyLineMaterialNo.setValue(record.data.MATERIAL_NO);      //物料信息
    App.txtModifyLineMaterialName.setValue(record.data.MATER_NAME);     //物料信息
    App.txtModifyLineGuid.setValue(record.data.LIMIT_PRODUCT_GUID);     //物料Guid
    App.txtModifyLineGrade.setValue(record.data.PRODUCT_GRADE);         //等级
    App.txtModifyLinePalletRfid.setValue(record.data.LIMIT_PALLET_ID);  //工装编号
    App.txtModifyLineStartDate.setValue(record.data.LIMIT_DATE_START);  //起始日期
    App.txtModifyLineEndDate.setValue(record.data.LIMIT_DATE_END);      //结束日期
    App.txtModifyLineMaxQty.setValue(record.data.REQUIRE_QTY);          //单据数量
    App.winModify.show();
}

var saveModifyLineWindows = function (record) {
    var store = App.gridMain.store;
    var LINE_ID = App.txtModifyLineLineId.getValue();        //单行行号
    for (var i = 0; i < store.data.items.length; i++) {
        if (LINE_ID == store.data.items[i].data.LINE_ID) {
            r = store.data.items[i];
            break;
        }
    }
    r.data.LINE_ID = App.txtModifyLineLineId.getValue();                     //单行行号
    r.data.MATERIAL_NO = App.txtModifyLineMaterialNo.getValue();             //物料信息
    r.data.MATER_NAME = App.txtModifyLineMaterialName.getValue();            //物料信息
    r.data.REQUIRE_QTY = App.txtModifyLineRequireQty.getValue();             //单据数量
    r.data.LIMIT_BIN_NO = App.txtModifyLineBinNo.getValue();                 //开始库位
    r.data.ELOC_NO = App.txtModifyLineEndLocNo.getValue();                   //目的库位
    r.data.ELOC_NAME = App.txtModifyLineEndLocName.getValue();               //目的库位
    r.data.ORDER_NO = App.txtModifyLineOrderNo.getValue();                   //出库单号
    r.data.PRODUCT_GRADE = App.txtModifyLineGrade.getValue();                //等级
    r.data.LIMIT_PALLET_ID = App.txtModifyLinePalletRfid.getValue();         //工装编号
    r.data.LIMIT_PRODUCT_GUID = App.txtModifyLineGuid.getValue();            //物料GUID
    r.data.LIMIT_DATE_START = App.txtModifyLineStartDate.getValue();         //物料GUID
    r.data.LIMIT_DATE_END = App.txtModifyLineEndDate.getValue();             //物料GUID
    if (r.data.DELETE_FLAG == "Y") {
        r.data.DELETE_FLAG = "N";
    }
    refreshRowNum(store);
    App.winModify.close();
}



//历史查询根据DeleteFlag的值进行样式绑定
var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("DELETE_FLAG") == "Y") {
        return "x-grid-row-deleted";
    }
}