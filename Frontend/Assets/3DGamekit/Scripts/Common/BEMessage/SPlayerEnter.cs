using System;
using TMPro;

namespace Common
{
    [Serializable]
    public class SPlayerEnter : Message
    {
        public SPlayerEnter() : base(Command.S_PLAYER_ENTER) { }
        public string user;
        public string token;
        public string scene;
        public bool exist_or_not;
    }
}
