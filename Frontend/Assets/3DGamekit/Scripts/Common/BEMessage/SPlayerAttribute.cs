using System;
using TMPro;

namespace Common
{
    [Serializable]
    public class SPlayerAttribute : Message
    {
        public SPlayerAttribute() : base(Command.S_PLAYER_ATTRIBUTE) { }
        public string name;
        public string InteligenceValue;
        public string SpeedValue;
        public string LevelValue;
        public string AttackValue;
        public string DefenseValue;
        public Int16 gold_coins;
    }
}
