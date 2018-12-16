using System;

namespace Common
{
    [Serializable]
    public class CApply : Message
    {
        public CApply() : base(Command.C_APPLY) { }
        public string to_apply;
    }
}
