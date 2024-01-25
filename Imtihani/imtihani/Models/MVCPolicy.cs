using IbtecarFramework;
using IbtecarFramework.Exceptions;
using Imtihani.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text;

namespace Imtihani.Models
{
    public class MVCPolicy : BasePolicy
    {
        static string PolicyKey = "Policy";
        private const string ERROR_MSG_KEY = "ErrMsg";
        StringBuilder sb = new StringBuilder();
        Stopwatch sw = null;
        public ControllerBase Controller { get; set; }

        public MVCPolicy()
        {
            sw = new Stopwatch();
            //CallContext.SetData(PolicyKey, this);
        }

        public MVCPolicy(ControllerBase controller)
            : this()
        {
            this.Controller = controller;
        }

        public override string GetExceptionMessage(Exception exception)
        {
            string message = "";
            if (exception is BusinessException)
            {
                message = (exception as BusinessException).Message;
            }
            /*else if (exception.IsEntityException())
            {
                message = exception.GetEntityErrorMessage();
            }*/
            else
            {
                Exception ex = exception;
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                message = ex.Message;
            }
            return message;
        }

        public override string HandleException(Exception ex)
        {
            string message = GetExceptionMessage(ex);
            if (ex is BusinessException)
            {
            }
            else
            {
                LogException(ex);
            }

            //if (this.Controller != null && !this.Controller.ControllerContext.IsAjax())
            //{
            //    SetMessage(message, Enums.MessageTypes.Error);
            //}
            return message;
        }

        public override void Log(string message, Enums.MessageTypes Type = Enums.MessageTypes.Error)
        {
            LogHelper.Log(new Exception(message));
        }

        public override void LogException(Exception ex)
        {
            LogHelper.Log(ex);
        }

        //public override void SetMessage(string message, Enums.MessageTypes type)
        //{
        //    if (this.Controller != null)
        //    {
        //        var messageO = new MessageObject(message, type);
        //        Controller.TempData[ERROR_MSG_KEY] = messageO;
        //    }
        //}

        ~MVCPolicy()
        {
            sw.Stop();
            //CallContext.SetData(PolicyKey, null);
        }

        //private void WriteProfile(string value)
        //{
        //    Stopwatch sw = CallContext.GetData(PolicyKey) as Stopwatch;
        //    string val = value + " " + sw.ElapsedMilliseconds;
        //}

        //public static MVCPolicy GetFromContext()
        //{
        //    return CallContext.GetData(MVCPolicy.PolicyKey) as MVCPolicy;
        //}
    }
}
