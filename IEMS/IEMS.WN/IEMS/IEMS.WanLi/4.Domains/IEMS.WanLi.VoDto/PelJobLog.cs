using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IEMS.WanLi.VoDto
{
    /// <summary>
    /// 数据传输对象 - 作业日志
    /// </summary>
    public class PelJobLog
    {
        /// <summary>
        /// 序列
        /// </summary>
        public long ObjId { get; set; }
        /// <summary>
        /// 作业编码
        /// </summary>
        public string JobNo { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 1:执行中  2：执行完成  3:执行错误
        /// </summary>
        public int JobStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 秒钟
        /// </summary>
        public decimal UsedTime { get; set; }
    }
}
