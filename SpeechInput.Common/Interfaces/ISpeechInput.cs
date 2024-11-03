using SpeechInput.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechInput.Common.Interfaces
{
    public interface ISpeechInput
    {
        /// <summary>
        /// 音频转文字对象
        /// </summary>
        IStt Stt { get; set; }
        /// <summary>
        /// 重置音频
        /// </summary>
        /// <returns></returns>
        OperationResult ResetAudio();
        /// <summary>
        /// 开始录音
        /// </summary>
        /// <returns></returns>
        OperationResult StartRecord();
        /// <summary>
        /// 结束录音
        /// </summary>
        /// <returns></returns>
        OperationResult<string> EndRecordAndGetText();
    }
}
