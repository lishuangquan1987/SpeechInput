using SpeechInput.Common.Interfaces;
using SpeechInput.Common.Models;

namespace SpeechInput.Stt.Baidu
{
    public class BaiduStt : IStt
    {
        public Task<OperationResult> Init(InitContext context)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<string>> SpeechToText(byte[] data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TestConnection()
        {
            throw new NotImplementedException();
        }
    }
}
