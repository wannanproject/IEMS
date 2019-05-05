using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IEMS.Frame.WebGlobal
{
    /// <summary>
    /// 文件对比拷贝功能
    /// </summary>
    public class FileCopy
    {
        /// <summary>
        /// 删除插件中的Config文件
        /// </summary>
        /// <param name="dirMain"></param>
        /// <param name="dirPlugin"></param>
        public void DeleteConfigFile(DirectoryInfo dirPlugin)
        {
            var fiPlugin = dirPlugin.GetFiles();
            foreach (FileInfo fib in fiPlugin)
            {
                if (fib.Extension.Equals(".config", StringComparison.CurrentCultureIgnoreCase))
                {
                    fib.Delete();
                }
            }
            dirPlugin = dirPlugin.Parent;
            fiPlugin = dirPlugin.GetFiles();
            foreach (FileInfo fib in fiPlugin)
            {
                if (fib.Extension.Equals(".config", StringComparison.CurrentCultureIgnoreCase))
                {
                    fib.Delete();
                }
            }
        }
        /// <summary>
        /// 对比拷贝文件，并更新相同文件
        /// 将dirMain和dirPlugin进行对比
        /// dirPlugin中较新的文件更新到dirMain对应文件中，并删除dirPlugin的文件
        /// </summary>
        /// <param name="dirMain"></param>
        /// <param name="dirPlugin"></param>
        public void FileCompareCopy(DirectoryInfo dirMain, DirectoryInfo dirPlugin)
        {
            var fiMain = dirMain.GetFiles();
            var fiPlugin = dirPlugin.GetFiles();
            foreach (FileInfo fia in fiMain)
            {
                foreach (FileInfo fib in fiPlugin)
                {
                    if (fia.Name.Equals(fib.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (fib.LastWriteTime > fia.LastWriteTime)
                        {
                            File.Copy(fib.FullName, fia.FullName, true);
                        }
                        fib.Delete();
                        break;
                    }
                }
            }
            DeleteConfigFile(dirPlugin);
        }
    }
}
