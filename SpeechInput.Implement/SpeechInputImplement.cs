using NAudio.Wave;
using SpeechInput.Common.Interfaces;
using SpeechInput.Common.Models;

namespace SpeechInput.Implement
{
    public class SpeechInputImplement : ISpeechInput
    {
        private IWaveIn? waveIn;
        WaveFileWriter writer;
        MemoryStream memoryStream;
        private bool _isAvaliable = false;
        private CancellationTokenSource checkAvaliableTokenSource = new CancellationTokenSource();

        public event Action<bool> AvaliableChanged;

        public SpeechInputImplement(IStt stt)
        {
            this.Stt = stt;
            ResetAudio();
            CheckAvaliable();
        }
        ~SpeechInputImplement()
        {
            this.Dispose();
        }
        public IStt Stt { get; set; }

        public bool IsAvaliable => _isAvaliable;

        public async Task<OperationResult<string>> EndRecordAndGetText()
        {
            waveIn.StopRecording();

            var bytes = memoryStream.ToArray();

            return await Stt.SpeechToText(bytes);
        }

        public async Task<OperationResult> ResetAudio()
        {
            if (waveIn != null)
            {
                waveIn.DataAvailable -= WaveIn_DataAvailable;
            }
            waveIn = new WaveInEvent();
            waveIn.DataAvailable += WaveIn_DataAvailable;
            memoryStream = new MemoryStream();
            writer = new WaveFileWriter(memoryStream, waveIn.WaveFormat);
            return await Task.FromResult(OperationResult.OK());
        }

        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        public async Task<OperationResult> StartRecord()
        {
            waveIn.StopRecording();
            return await Task.FromResult(OperationResult.OK());
        }
        private async Task CheckAvaliable()
        {
            while (!checkAvaliableTokenSource.IsCancellationRequested)
            {
                await Task.Delay(1000);
                var result =await Stt.TestConnection();
                if (result)
                {
                    if (!_isAvaliable)
                    {
                        _isAvaliable = true;
                        AvaliableChanged?.Invoke(true);
                    }
                }
                else
                {
                    if (_isAvaliable)
                    {
                        _isAvaliable = false;
                        AvaliableChanged?.Invoke(false);
                    }
                }
            }
        }

        public void Dispose()
        {
            waveIn.StopRecording();
            checkAvaliableTokenSource.Cancel();
        }
    }
}
