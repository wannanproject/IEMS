using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEMS.WanLi.VoDto
{
    public class BinData
    {
        public string BinNo { get; set; }

        private int stringToInt(string str, int offset, int len)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            if (str.Length < offset + len)
            {
                return 0;
            }
            var s = str.Substring(offset, len);
            var result = 0;
            int.TryParse(s, out result);
            return result;
        }
        /// <summary>
        /// 巷道
        /// </summary>
        public int X
        {
            get
            {
                return stringToInt(this.BinNo, 0, 2);
            }
        }
        /// <summary>
        /// 巷深
        /// </summary>
        public int Y
        {
            get
            {
                return stringToInt(this.BinNo, 2, 2);
            }
        }
        /// <summary>
        /// 层高
        /// </summary>
        public int Z
        {
            get
            {
                return stringToInt(this.BinNo, 4, 2);
            }
        }
        /// <summary>
        /// 库深
        /// </summary>
        public int H
        {
            get
            {
                return stringToInt(this.BinNo, 6, 2);
            }
        }
        /// <summary>
        /// 可用状态：0：不可用  1：可用
        /// </summary>
        public int UsedFlag { get; set; }
        /// <summary>
        /// 库位大小(H High高, M MIDDLE中, L Low低, :无)
        /// </summary>
        public string BinSize { get; set; }
        /// <summary>
        /// 库位状态（_ :空库，$ :库存）
        /// </summary>
        public string BinStatus { get; set; }
        /// <summary>
        /// 业务状态：  I :预约入库，O :预约出库，T:移库占用, E :空出库位 P :先入库位
        /// </summary>
        public string BinBizStatus { get; set; }
        /// <summary>
        /// 权重(被放置时的优先顺序,1最先,数字越大越后)
        /// </summary>
        public int BinWeight { get; set; }
        /// <summary>
        /// 储区(库位类别 A, B, C...)
        /// </summary>
        public string BinArea { get; set; }
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        public string CrnNo { get; set; }
        /// <summary>
        /// 库位组
        /// </summary>
        public string GroupNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 对应AGV编码
        /// </summary>
        public string BinAgvNo { get; set;}
        /// <summary>
        /// 对应PLC编码
        /// </summary>
        public string BinPlcNo { get; set; }
        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 货笼ID1
        /// </summary>
        public string PalletNo1 { get; set; }
        /// <summary>
        /// 货笼ID2
        /// </summary>
        public string PalletNo2 { get; set; }
        /// <summary>
        /// 存货编码
        /// </summary>
        public string MaterNo { get; set; }
        /// <summary>
        /// 存货名称
        /// </summary>
        public string MaterName { get; set; }
        /// <summary>
        /// 大类
        /// </summary>
        public string MaterMkind { get; set; }
        /// <summary>
        /// 小类
        /// </summary>
        public string MaterType { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string MaterSpec { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string MaterDesc { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public string ProductDate { get; set; }
        /// <summary>
        /// 工装数量
        /// </summary>
        public string Qty { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

    }
    public class StoreBinData
    { 
        public BinData MaxBin { get; set; }
        public BinData[] AllBin { get; set; }
    }
}
