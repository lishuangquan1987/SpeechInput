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
        OperationResult Init(InitContext context);
        OperationResult<string> SpeechToText(string text);
    }
}
