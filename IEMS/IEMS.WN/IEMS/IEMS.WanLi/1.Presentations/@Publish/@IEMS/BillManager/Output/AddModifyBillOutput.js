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
    App.txtAddLineListNo.setValue(App.txtListNo.getValue()); //出库单号
    App.txtAddLineLineId.setValue(gridMainLineLength());     //单行行号
    App.txtAddLineRequireQty.setValue(1);                    //单据数量
    App.txtAddLineRcvQty.setValue(0);                        //待发数量
    App.txtAddLineShipQty.setValue(0);                       //任务数量
    App.txtAddLineActQty.setValue(0);                        //实发数量
    App.txtShowAddLineMaterial.setValue("");                 //物料信息
    App.txtAddLineMaterial.setValue("");                     //物料信息
    App.TxtAddLineBinNo.setValue("");                        //开始库位
    App.txtAddLineGuid.setValue("");                         //产品GUID号
    App.txtAddLinePalletRfid.setValue("");                   //工装编号
    App.txtAddLineOutTaskFlag.setValue(1);                   //行任务执行
    SelecttxtAddLineEndEquipType();
    App.winAdd.show();
}
//添加行信息保存
var saveAddLineWindows = function () {

    var LINE_ID = App.txtAddLineLineId.getValue();   //单行行号
    if (LINE_ID == "") {
        Ext.Msg.alert("失败", "单行行号不能为空！");
        return;
    }

    var ShowAddLineMaterial = App.txtShowAddLineMaterial.getValue();
    if (ShowAddLineMaterial == "") {
        Ext.Msg.alert("失败", "请选择物料信息！");
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
    App.txtAddLineRcvQty.setValue(App.txtAddLineRequireQty.getValue());   //待发数量
    r.data.LINE_ID = App.txtAddLineLineId.getValue();                     //单行行号
    r.data.LINE_STATUS = App.txtAddLineBillStatus.getValue();             //单据行状态
    r.data.MATERIAL_NO = App.txtAddLineMaterial.getValue();               //物料信息
    r.data.MATER_NAME = App.txtShowAddModifyLineMaterial.getValue();      //物料信息
    r.data.REQUIRE_QTY = App.txtAddLineRequireQty.getValue();             //单据数量
    r.data.RCV_QTY = App.txtAddLineRcvQty.getValue();    //待发数量
    r.data.SHIP_QTY = App.txtAddLineShipQty.getValue();  //任务数量
    r.data.ACT_QTY = App.txtAddLineActQty.getValue();    //已生成任务数量
    r.data.LIMIT_BIN_NO = App.TxtAddLineBinNo.getValue();//开始库位
    r.data.ELOC_NO = App.txtAddLineEndEquip.getValue();  //目的库位
    r.data.ORDER_NO = App.txtAddLineListNo.getValue();   //出库单号
    r.data.PRODUCT_GRADE = App.txtAddLineGrade.getValue();          //等级
    r.data.LIMIT_PALLET_ID = App.txtAddLinePalletRfid.getValue();   //工装编号
    r.data.LINE_TASK_FLAG = App.txtAddLineOutTaskFlag.getValue();   //行任务执行
    r.data.LIMIT_PRODUCT_GUID = App.txtAddLineGuid.getValue();      //物料GUID
    r.data.ORDER_LINE_GUID = App.txtAddLineLineGuid.getValue();     //行Guid
    store.data.add(r);
    refreshRowNum(store);
    App.winAdd.close();
}

//获得基础信息的store方法
var getBillHeaderStore = function () {
    var OrderPriority = 50;
    var SourceType=0;
    var OrderType = "100030";
    var OrderTypeModule="O";

    return {
        OrderNo: App.txtListNo.getValue(),
        Status: App.txtBillStatus.getValue(),
        OrderPriority: OrderPriority,
        SourceType: SourceType,
        OrderType: OrderType,
        OrderTypeModule: OrderTypeModule
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
                 store.data.removeAt(i);
             }
             else {
                 store.data.items[i].data.DELETE_FLAG = "Y";
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

    var showModifyLineWindows = function (record) {
        App.txtModifyLineListNo.setValue(record.data.ORDER_NO);       //出库单号
        App.txtModifyLineLineId.setValue(record.data.LINE_ID);        //单行行号
        App.txtModifyLineBillStatus.setValue(record.data.LINE_STATUS);//单据状态
        App.txtModifyLineRequireQty.setValue(record.data.REQUIRE_QTY);//单据数量
        App.txtModifyLineBinNo.setValue(record.data.LIMIT_BIN_NO);    //开始库位
        App.txtModifyLineEndEquipType.setValue(App.txtAddLineEndEquipType.getValue());  //目的站台类型
        App.txtModifyLineEndEquip.setValue(App.txtAddLineEndEquip.getValue());          //目的站台
        App.txtModifyLineEndEquip.setValue(record.data.ELOC_NO);      //目的站台
        App.txtModifyLineMaterial.setValue(record.data.MATERIAL_NO);  //物料信息
        App.txtShowAddModifyLineMaterial.setValue(record.data.MATER_NAME);  //物料信息
        App.txtModifyLineGuid.setValue(record.data.LIMIT_PRODUCT_GUID);     //物料Guid
        App.txtModifyLineGrade.setValue(record.data.PRODUCT_GRADE);         //等级
        App.txtModifyLinePalletRfid.setValue(record.data.LIMIT_PALLET_ID);  //工装编号
        App.txtModifyLineShipQty.setValue(record.data.SHIP_QTY);            //任务数量
        App.txtModifyLineRcvQty.setValue(record.data.RCV_QTY);              //待发数量
        App.txtModifyLineActQty.setValue(record.data.ACT_QTY);              //已完成任务数量
        App.txtModifyLineOutTaskFlag.setValue(record.data.LINE_TASK_FLAG);  //行任务执行
        SelecttxtModifyLineEndEquipType();
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
        r.data.LINE_STATUS = App.txtModifyLineBillStatus.getValue();             //单据行状态
        r.data.MATERIAL_NO = App.txtShowAddModifyLineMaterial.getValue();        //物料信息
        r.data.MATER_NAME = App.txtModifyLineMaterial.getValue();                //物料信息
        r.data.REQUIRE_QTY = App.txtModifyLineRequireQty.getValue();             //单据数量
        r.data.RCV_QTY = App.txtModifyLineRcvQty.getValue();    //待发数量
        r.data.SHIP_QTY = App.txtModifyLineShipQty.getValue();  //任务数量
        r.data.ACT_QTY = App.txtModifyLineActQty.getValue();    //已生成任务数量
        r.data.LIMIT_BIN_NO = App.txtModifyLineBinNo.getValue();//开始库位
        r.data.ELOC_NO = App.txtModifyLineEndEquip.getValue();  //目的库位
        r.data.ORDER_NO = App.txtModifyLineListNo.getValue();   //出库单号
        r.data.PRODUCT_GRADE = App.txtModifyLineGrade.getValue();          //等级
        r.data.LIMIT_PALLET_ID = App.txtModifyLinePalletRfid.getValue();   //工装编号
        r.data.LINE_TASK_FLAG = App.txtModifyLineOutTaskFlag.getValue();   //行任务执行
        r.data.LIMIT_PRODUCT_GUID = App.txtModifyLineGuid.getValue();      //物料GUID
        r.data.ORDER_LINE_GUID = App.txtModifyLineLineGuid.getValue();     //行Guid
        if (r.data.DELETE_FLAG == "Y") {
            r.data.DELETE_FLAG = "N";
        }
        refreshRowNum(store);
        App.winModify.close();
    }


