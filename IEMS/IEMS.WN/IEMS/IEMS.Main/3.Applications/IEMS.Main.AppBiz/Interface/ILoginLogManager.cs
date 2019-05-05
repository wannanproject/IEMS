namespace IEMS.Main.AppBiz
{
    using IEMS.Main.Entity;

    public interface ILoginLogManager : IAppBizManager
    {
        /// <summary>
        /// 根据用户名称和密码进行登录（去掉了异常处理）
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        SsbUser Login(SsbUser loginUser);

        /// <summary>
        /// 用户退出
        /// </summary>
        void Logout(int userId);
    }
}
