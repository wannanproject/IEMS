using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace IEMS.Main.AppBiz
{
    using MSTL.DbAccess;
    using IEMS.Main.Entity;
    using IEMS.Main.DbRI;
    using IEMS.Main.DbCI;

    internal class LoginLogManager : ILoginLogManager
    {
        private ISsbUserService userBasicService = TableViewServiceFactory.CreateInstance<ISsbUserService>();
        private IUserService userBusinessService = DbCIServiceFactory.CreateInstance<IUserService>();
        private ISslLoginLogService logBaicService = TableViewServiceFactory.CreateInstance<ISslLoginLogService>();

        #region 私有方法
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private SsbUser getUser(SsbUser user)
        {
            SsbUser result = this.userBasicService.GetEntityList(user).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 根据用户名称和密码进行登录（去掉了异常处理）
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="isPageLogin"></param>
        /// <returns></returns>
        private SsbUser Login(SsbUser loginUser, bool isPageLogin)
        {
            SsbUser result = null;
            string loginPass = loginUser.UserPwd;
            loginUser.UserPwd = null;
            var user = getUser(loginUser);
            if (user == null)
            {
                return result;
            }
            if (string.IsNullOrWhiteSpace(user.UserPwd))
            {
                return result;
            }
            var decrypt = AppBizFactory.CreateInstance<IMcPassword>();
            string spassword = decrypt.DecryptString(user.UserPwd, string.Empty, Encoding.ASCII);
            if (loginPass.Trim() == spassword.Trim())
            {
                if (isPageLogin)
                {
                    LoginSuccess(user);
                }
                result = user;
                return result;
            }
            return result;
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="user"></param>
        private void LoginSuccess(SsbUser user)
        {
            Logout(false, user);
            SslLoginLog loginlog = new SslLoginLog();
            loginlog.UserId = user.ObjId;
            loginlog.LoginTime = DateTime.Now;
            loginlog.LogoutTime = null;
            loginlog.LoginIp = HttpContext.Current.Request.UserHostAddress;
            if (loginlog.LoginIp == "::1")
            {
                loginlog.LoginIp = "127.0.0.1";
            }
            TableViewServiceFactory.CreateInstance<ISslLoginLogService>().Insert(loginlog);
        }
        private void LoginSuccessForWinCE(SsbUser user)
        {
            LogoutForWinCE(false, user);
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            SslLoginLog loginlog = new SslLoginLog();
            loginlog.UserId = user.ObjId;
            loginlog.LoginTime = DateTime.Now;
            loginlog.LogoutTime = null;
            loginlog.LoginIp = endpoint.Address; ;
            if (loginlog.LoginIp == "::1")
            {
                loginlog.LoginIp = "172.0.0.1";
            }
            TableViewServiceFactory.CreateInstance<ISslLoginLogService>().Insert(loginlog);
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="clearAuthentication"></param>
        /// <param name="user"></param>
        private void Logout(bool clearAuthentication, SsbUser user)
        {

            string IP = HttpContext.Current.Request.UserHostAddress;


            if (IP == "::1")
            {
                IP = "127.0.0.1";
            }
            SslLoginLog log = new SslLoginLog();
            log.UserId = user == null ? -99 : user.ObjId;
            log.LogoutIp = IP;
            log.LogoutTime = DateTime.Now;
            this.UpdateUserLoginLog(log);
            if (clearAuthentication)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearAuthentication"></param>
        /// <param name="user"></param>
        private void LogoutForWinCE(bool clearAuthentication, SsbUser user)
        {
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string IP = endpoint.Address;


            if (IP == "::1")
            {
                IP = "127.0.0.1";
            }
            SslLoginLog log = new SslLoginLog();
            log.UserId = user == null ? -99 : user.ObjId;
            log.LoginIp = IP;
            log.LogoutTime = DateTime.Now;
            this.UpdateUserLoginLog(log);
            if (clearAuthentication)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
            }
        }
        /// <summary>
        /// 修改用户推出登录时信息
        /// </summary>
        /// <param name="log"></param>
        public void UpdateUserLoginLog(SslLoginLog log)
        {
            var service = TableViewServiceFactory.CreateInstance<ISslLoginLogService>();
            service.Update(log, new SslLoginLog() { UserId = log.UserId });
        }
        #endregion

        /// <summary>
        /// 根据用户名称和密码进行登录（去掉了异常处理）
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public SsbUser Login(SsbUser loginUser)
        {
            return Login(loginUser, true);
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public void Logout(int userId)
        {
            SsbUser user = new SsbUser();
            user.ObjId = userId;
            user = getUser(user);
            Logout(true, user);
        }

    }
}
