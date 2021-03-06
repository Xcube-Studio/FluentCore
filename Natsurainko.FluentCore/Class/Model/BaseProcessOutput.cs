using Natsurainko.FluentCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Natsurainko.FluentCore.Class.Model
{
    public class BaseProcessOutput : IProcessOutput
    {
        public string Raw { get; private set; }

        public BaseProcessOutput(string output) => this.Raw = output;

        public string GetPrintValue() => this.Raw;

        public void Print() => Console.WriteLine(this.Raw);
    }
}
