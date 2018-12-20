using System;

namespace Common
{
    [Serializable]
    public class CUnapply : Message
    {
        public CUnapply() : base(Command.C_UNAPPLY) { }
        public string to_unapply;
    }
}
