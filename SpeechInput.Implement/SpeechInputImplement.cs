using NAudio.Wave;
using SpeechInput.Common.Interfaces;
using SpeechInput.Common.Models;

namespace SpeechInput.Implement
{
    public class SpeechInputImplement : ISpeechInput
    {
        private IWaveIn waveIn;
        public SpeechInputImplement(IStt stt)
        {
            this.Stt = stt;
        }
        public IStt Stt { get; set; }

        public OperationResult<string> EndRecordAndGetText()
        {
            throw new NotImplementedException();
        }

        public OperationResult ResetAudio()
        {
            waveIn = new WaveInEvent();
        }

        public OperationResult StartRecord()
        {
            
        }
    }
}
