using SpeechInput.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechInput.Common.Interfaces
{
    public interface IStt
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<OperationResult> Init(InitContext context);
        /// <summary>
        /// 语音转文字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<OperationResult<string>> SpeechToText(byte[] data);
        /// <summary>
        /// 检查连接是否可用
        /// </summary>
        /// <returns></returns>
        Task<bool> TestConnection();
    }
}
