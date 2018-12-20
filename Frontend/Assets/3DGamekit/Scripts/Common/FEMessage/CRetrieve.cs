using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CRetrieve : Message
    {
        public CRetrieve() : base(Command.C_RETRIEVE) { }
        public string to_retrieve;
    }
}
