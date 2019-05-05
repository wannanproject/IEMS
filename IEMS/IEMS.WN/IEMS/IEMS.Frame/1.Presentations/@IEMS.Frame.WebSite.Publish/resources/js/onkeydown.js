document.onkeydown = documentOnkeydown;
function documentOnkeydown(e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (((event.keyCode == 8) &&                                                    //按BackSpace键，判断是否是文本框和密码框，如果是，不做处理；如果不是，停止该键的响应
       ((event.srcElement.type != "text" &&
       event.srcElement.type != "textarea" &&
       event.srcElement.type != "password") ||
       event.srcElement.readOnly == true)) ||
       ((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82))) ||    //Ctrl+N(新建窗口),Ctrl+R(刷新，相当于F5)   停止按组合键响应
       (event.keyCode == 116)) {                                                   //F5  停止按键F5刷新页面
        event.keyCode = 0;
        event.returnValue = false;
    }
    return true;
}

//点击弹出窗口，height=100%设置，google浏览器无法获取其高度方法
window.onload = function () {
    if (parent && parent.document) {
        var iframes = parent.document.getElementsByTagName("iframe");
        if (iframes) {
            for (var i = 0; i < iframes.length; i++) {
                if (iframes[i].contentWindow == window) {
                    iframes[i].height = iframes[i].parentElement.style.height;
                }
            }
        }
    }
};