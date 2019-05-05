function nodeLoad(store, operation, options) {
    var node = operation.node;
    App.direct.NodeLoad(store.storeId, node.getId(), {
        success: function (result) {
            node.set('loading', false);
            node.set('loaded', true);
            var data = Ext.decode(result);
            node.appendChild(data, undefined, true);
            node.expand();
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('加载错误', errorMsg);
        }
    });
    return false;
};
function onTabClose(item) {
    try {
        var window = item.body.dom.firstChild.contentWindow;
        if (window.onTabClose) {
            var msg = window.onTabClose();
            if (msg.length > 0) {
                alert(msg);
                return false;
            }
        }
    } catch (ex) { }
    return true;
}
function onTabsRemove(tabs, item) {
    return onTabClose(item);
}
function addTab(id, title, url, closable) {
    if (!url) {
        return;
    }
    tabp = App.mainTabPanel;
    var tabid = "id=" + id;
    var tab = tabp.getComponent(tabid);
    if (!tab) {
        tab = tabp.add({
            id: tabid,
            title: title,
            closable: closable,
            /*html: "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>",*/
            loader: {
                url: url,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: "正在加载 " + title + "..."
                }
            },
            listeners: {
                beforeclose: { fn: function (item) { return onTabClose(item) } }
            },
            autoScroll: false
        });
    }
    tabp.setActiveTab(tab);
}
function loadPage(record, e) {
    if (!record) {
        return;
    }
    if (!record.data.href) {
        return;
    }
    if (e) {
        e.stopEvent();
    }
    try {
        eval(record.data.href)
    } catch (e) {
        addTab(record.getId(), record.data.text, record.data.href, true);
    }
};

var dologout = function (btn) {
    if (btn != "yes") {
        return;
    }
    var top = window;
    while (true) {
        if (top.parent) {
            if (top == top.parent) {
                break;
            }
            top = top.parent;
        } else {
            break;
        }
    }
    top.top.location.href = thisRootUrl + "Logout.htm";
}
function logout(sender) {
    Ext.Msg.confirm("提示", '您确定退出系统吗？', function (btn) { dologout(btn) });
    return false;
}
function refresh(sender) {
    var tab = App.mainTabPanel.getActiveTab();
    if (tab) {
        tab.reload(true);
        //var html = tab.getBody().dom.innerHTML;
        //tab.getBody().update(html);
    }
    return false;
}

function GetNode(nodes, nodeinfo) {
    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];
        if (node.data.leaf) {
            if ((node.data.text == nodeinfo) || (node.data.id == nodeinfo)) {
                return node;
            }
        } else {
            var resultnode = GetNode(node.childNodes, nodeinfo);
            if (resultnode) {
                return resultnode;
            }
        }

    }
}

function OpenMenu(menu) {
    menu = menu.replace("id=", "");
    loadPage(GetNode(App.mainTreePanel.getRootNode().childNodes, menu));
}

function help(sender) {
    var menu = sender.text;
    OpenMenu(menu)
    return false;
}
function user(sender) {
    App.Manager_System_SysUser_MyUser_Window.show();
    return false;
}
function task(sender) {
    App.Manager_Plugins_Main_SysTask_Window.show();
    return false;
}


function aboutRDTeam(sender) {
    var myDate = new Date(); 
    var window = App.Manager_System_SysUser_aboutRDTeam_Window;
    var url = "aboutRDTeam.html?dt=" + myDate.getTime();
    var html = "<iframe src='" + url + "' width=990 height=600 scrolling=no  frameborder=0></iframe>";
    if (window.getBody()) {
        window.getBody().update(html);
    } else {
        window.html = html;
    }
    window.show();
    return false;
}

function clickMenu(sender) {
    var menu = sender.text;
    OpenMenu(menu)
    return false;
}

Ext.create("Ext.window.Window", {
    id: "Manager_System_SysUser_MyUser_Window",
    hidden: true,
    width: 360,
    height: 300,
    bodyStyle: "background-color: #fff;",
    closable: true,
    html: "<iframe src='SysUser/MyUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    title: "设置用户信息",
    modal: true
})

var tempWin = Ext.create("Ext.window.Window", {
    id: "Manager_Plugins_Main_SysTask_Window",
    hidden: true,
    width: 765,
    height: 400,
    bodyStyle: "background-color: #fff;",
    closable: true,
    html: "<iframe src='SysTask/TaskRemind.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    title: "待办任务提醒",
    modal: true
});
tempWin.on('beforeclose', function () {
    App.direct.SetTaskCssColor({
        success: function (result) {
            if (result == "true") {
            } else {
                clearTimeout(iTime);
                $("#LinkButton4").css('color', "black");
            }
        },
        showFailureWarning: false
    });

}, this);

Ext.create("Ext.window.Window", {
    id: "Manager_System_SysUser_aboutRDTeam_Window",
    hidden: true,
    width: 990,
    height: 600,
    html: "<iframe src='aboutRDTeam.html' width=990 height=600 scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "优秀的研发团队",
    modal: true
})

var OnTimer = function () {
    App.direct.OnTimer({
        success: function (result) {
            App.StatusBar1.setText(result);
        },
        showFailureWarning: false
    });
}
var setTheme = function (color) {
    App.direct.GetThemeUrl(color, {
        success: function (result) {
            //
            Ext.net.ResourceMgr.setTheme(result);
            App.mainTabPanel.items.each(function (el) {
                if (!Ext.isEmpty(el.iframe)) {
                    if (el.getBody().Ext) {
                        el.getBody().Ext.net.ResourceMgr.setTheme(result, color.toLowerCase());
                    }
                }
            });
        },
        failture: function (result) { alert(result); }
    });
}
Ext.onReady(function () {
    var int = self.setInterval("OnTimer()", 50000);
    if (Ext.net.ResourceMgr.theme == "gray") document.getElementById('sp1').style.border = '2px solid'; else document.getElementById('sp2').style.border = '2px solid';
    colorful();
})
var i = 0;
var iTime;
function getColor() {
    i++;
    switch (i) {
        case 1: return "#ff0000";
        case 2: return "#ff6600";
        case 3: return "#3366cc";
        default: return "black";
    }
}
function colorful() {
    if ($("#hiddenTaskCount").val() == "true") {
        $("#LinkButton4").css('color', getColor());
        if (i == 3) i = 0;
        iTime = setTimeout('colorful()', 1000);
    } else {
        clearTimeout(iTime);
    }
}
