using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechInput.Common.Models
{
    /// <summary>
    /// 操作结果的封装类
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 操作失败时的错误信息
        /// </summary>
        public string? ErrorMsg { get; set; }
        public static OperationResult OK() => new OperationResult() { IsSuccess = true };

        public static OperationResult NG(string errMsg) => new OperationResult() { IsSuccess = false, ErrorMsg = errMsg };

        public static OperationResult NG(Exception e) => new OperationResult() { IsSuccess = false, ErrorMsg = e.Message };
    }

    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// 操作之后返回的数据
        /// </summary>
        public T? Data { get; set; }
        public static OperationResult<T> OK(T data) => new OperationResult<T>() { IsSuccess = true, Data = data };
        public static new OperationResult<T> NG(string errMsg) => new OperationResult<T>() { IsSuccess = false, ErrorMsg = errMsg };

        public static new OperationResult<T> NG(Exception e) => new OperationResult<T>() { IsSuccess = false, ErrorMsg = e.Message };
    }


}
