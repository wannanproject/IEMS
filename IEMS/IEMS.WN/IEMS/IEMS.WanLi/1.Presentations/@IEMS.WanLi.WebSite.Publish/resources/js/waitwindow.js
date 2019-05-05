
Ext.onReady(function () {
    Ext.create("Ext.window.Window", {
        id: "waitProgressWindow",
        height: 52,
        hidden: true,
        width: 312,
        resizable: false,
        items: [
        {
            id: "progressbar3e53f875beb14e6ea026f55a831b5d15",
            width: 300,
            xtype: "progressbar",
            value: 0.0
        }],
        bodyStyle: "background-color:rgb(232,232,232)",
        bodyPadding: 0,
        closable: false,
        iconCls: "#Hourglass",
        title: "正在执行，请稍候……",
        modal: true,
        listeners: {
            show: {
                fn: function () {
                    App.progressbar3e53f875beb14e6ea026f55a831b5d15.wait({
                        interval: 200,
                        duration: 600000, //10分钟
                        increment: 20,
                        fn: function () {
                            App.waitProgressWindow.close();
                        }
                    });
                }
            }
        }
    });
});