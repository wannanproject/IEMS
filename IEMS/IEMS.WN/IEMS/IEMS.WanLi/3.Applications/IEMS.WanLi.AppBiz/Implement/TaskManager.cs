using MSTL.DbAccess;
using MSTL.LogAgent;
using MSTL.ResultStruct.McException;
using IEMS.WanLi.Entity;
using IEMS.WanLi.DbRI;
using IEMS.WanLi.DbCI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IEMS.WanLi.AppBiz
{
    internal class TaskManager : ITaskManager
    {
        #region 系统日志  log
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
        #endregion

        #region GetBarTaskData
        public PageResult GetWbsTaskData(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<ITaskService>();
            return service.GetWbsTaskData(pageResult);
        }
        #endregion

        #region GetWbsTask
        public WbsTask GetWbsTask(long taskNo)
        {
            var service = TableViewServiceFactory.CreateInstance<IWbsTaskService>();
            return service.GetEntityList(new WbsTask() { TaskNo = taskNo }).FirstOrDefault();
        }
        #endregion

        #region CancelSelectTask
        public string CancelSelectTask(WbsTask[] tasks,string FinishDesc, int TaskNo)
        {
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            var procService = ProcedureServiceFactory.CreateInstance<IProcTaskFinishService>();
            transaction.BeginTransaction();
            try
            {     
                procService.ExcuteProcedure(new ProcTaskFinish() { InTaskNo = TaskNo, InFinishDesc= FinishDesc });
                transaction.CompleteTransaction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                transaction.RollbackTransaction();
                throw ex;
            }
            return "";
        }
        #endregion

        #region CompleteSelectTask

      
        #endregion

        #region NonloadHandleSpSelectTask 空出库处理
        #endregion

        #region ResetTaskEmerge 重置任务优先级
        //重置任务优先级之前，先判断任务步骤是否为0
        private bool verifyResetTaskEmergeData(WbsTask[] tasks)
        {
            foreach (var task in tasks)
            {
                if (task.TaskStep != "01")
                {
                    var message = string.Format("作业序号：[{0}]的工作任务已在动作中,无法调整优先顺序!", task.TaskNo);
                    throw new VerifyException(message);
                }
            }
            return true;
        }

        //重置任务优先级
        public int[] ResetTaskEmerge(WbsTask[] tasks, int emerge)
        {
            #region 单据验证
            if (!verifyResetTaskEmergeData(tasks))
            {
                throw new VerifyException("任务验证失败!");
            }
            #endregion
            var result = new List<int>();
            var transaction = TransactionServiceFatory.CreateInstance<ITransactionService>();
            var service = TableViewServiceFactory.CreateInstance<IWbsTaskService>();
            transaction.BeginTransaction();
            try
            {
                foreach (var task in tasks)
                {
                    service.Update(new WbsTask() { Emergency = emerge },
                        new WbsTask() { TaskNo = task.TaskNo, TaskStep = "01" });
                }
                transaction.CompleteTransaction();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                transaction.RollbackTransaction();
                throw ex;
            }
            return result.ToArray();
        }
        #endregion

        #region  手动创建任务
        public bool CreateTaskOperation(string palletid,string locNo)
        {
            try
            {
                var service = DbCIServiceFactory.CreateInstance<ITaskService>();
                return service.ExecPROC_0304_CMD_STEP_ADJUST(palletid, locNo);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion

        #region  获取任务信息
        public PageResult GetTaskData(PageResult pageResult)
        {
            var service = DbCIServiceFactory.CreateInstance<ITaskService>();
            return service.GetTaskData(pageResult);
        }
        #endregion
        
        #region 堆垛机任务维护
        /// <summary>
        /// 堆垛机指令重发
        /// </summary>
        /// <param name="Objid"></param>
        /// <returns></returns>
        public int ResendWbsTaskCmd(string Objid)
        {
            try
            {
                if (string.IsNullOrEmpty(Objid) || Objid.Equals("0"))
                {
                    return 0;
                }
                var service = TableViewServiceFactory.CreateInstance<IWbsTaskCmdService>();
                return service.Update(new WbsTaskCmd() { CmdStep = "00" }, new WbsTaskCmd() {ObjId = int.Parse(Objid) });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return 0;
            }
        }
        /// <summary>
        /// 堆垛机指令结束
        /// </summary>
        /// <param name="Objid"></param>
        /// <returns></returns>
        public bool FinishWbsTaskCmd(string Objid)
        {
            try
            {
                if (string.IsNullOrEmpty(Objid) || Objid.Equals("0"))
                {
                    return false;
                }
                var service = ProcedureServiceFactory.CreateInstance<IProcWmsFinishCmdService>();
                service.ExcuteProcedure(new ProcWmsFinishCmd() { IObjid = int.Parse(Objid) });
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// 堆垛机指令删除
        /// </summary>
        /// <param name="Objid"></param>
        /// <returns></returns>
        public bool DeleteWbsTaskCmd(string Objid)
        {
            try
            {
                if (string.IsNullOrEmpty(Objid) || Objid.Equals("0"))
                {
                    return false;
                }
                var service = ProcedureServiceFactory.CreateInstance<IProcWmsDeleteCmdService>();
                service.ExcuteProcedure(new ProcWmsDeleteCmd() { IObjid = int.Parse(Objid) });
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// 任务删除
        /// </summary>
        /// <param name="Objid"></param>
        /// <returns></returns>
        public bool DeleteWbsTask(string TaskNo)
        {
            try
            {
                if (string.IsNullOrEmpty(TaskNo))
                {
                    return false;
                }
                var service = ProcedureServiceFactory.CreateInstance<IProcWmsDeleteTaskService>();
                service.ExcuteProcedure(new ProcWmsDeleteTask() { ITaskNo = int.Parse(TaskNo) });
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 任务请求
        /// </summary>
        /// <param name="palletNo1"></param>
        /// <param name="palletNo2"></param>
        /// <param name="sLocNo"></param>
        /// <returns></returns>
        public string RequestTaskCmd(string palletNo1, string palletNo2, string sLocNo)
        {
            var taskSeq = SequenceServiceFactory.CreateInstance<ISeqTproc0100TaskRequestService>();
            var taskTable = TableViewServiceFactory.CreateInstance<ITproc0100TaskRequestService>();
            var taskSever = ProcedureServiceFactory.CreateInstance<IProc0100TaskRequestService>();

            var cmdSeq = SequenceServiceFactory.CreateInstance<ISeqTproc0200CmdRequestService>();
            var cmdTable = TableViewServiceFactory.CreateInstance<ITproc0200CmdRequestService>();
            var cmdSever = ProcedureServiceFactory.CreateInstance<IProc0200CmdRequestService>();

            try
            {
                var taskId = taskSeq.NEXTVAL;
                taskTable.Insert(new Tproc0100TaskRequest()
                {
                    ObjId = taskId,
                    PalletNo1 = palletNo1,
                    PalletNo2 = palletNo2,
                    SlocNo = sLocNo,
                    OrderTypeNo = "100064"
                });
                taskSever.ExcuteProcedure(new Proc0100TaskRequest() { IParamObjid = taskId });
                var dtRequestTask = taskTable.GetEntityList(new Tproc0100TaskRequest() { ObjId = taskId }).FirstOrDefault();
                if (dtRequestTask.ErrCode != 0)
                {
                    return dtRequestTask.ErrDesc;
                }
                var cmdId = cmdSeq.NEXTVAL;
                cmdTable.Insert(new Tproc0200CmdRequest()
                {
                    ObjId = cmdId,
                    TaskNo = int.Parse(dtRequestTask.TaskNo.ToString().Trim()),
                    CurrLocNo = sLocNo
                });
                cmdSever.ExcuteProcedure(new Proc0200CmdRequest() { IParamObjid = cmdId });
                var dtRequestCmd = cmdTable.GetEntityList(new Tproc0200CmdRequest() { ObjId = cmdId }).FirstOrDefault();
                if (dtRequestCmd.ErrCode != 0)
                {
                    return dtRequestCmd.ErrDesc;
                }
                
                return string.Empty;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

    }
}