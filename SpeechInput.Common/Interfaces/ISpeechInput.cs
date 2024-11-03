using SpeechInput.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechInput.Common.Interfaces
{
    public interface ISpeechInput: IDisposable
    {
        /// <summary>
        /// 音频转文字对象
        /// </summary>
        IStt Stt { get; set; }
        /// <summary>
        /// 重置音频
        /// </summary>
        /// <returns></returns>
        Task<OperationResult> ResetAudio();
        /// <summary>
        /// 开始录音
        /// </summary>
        /// <returns></returns>
        Task<OperationResult> StartRecord();
        /// <summary>
        /// 结束录音
        /// </summary>
        /// <returns></returns>
        Task<OperationResult<string>> EndRecordAndGetText();
        /// <summary>
        /// 可用改变事件
        /// </summary>
        event Action<bool> AvaliableChanged;
        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsAvaliable { get; }
    }
}
