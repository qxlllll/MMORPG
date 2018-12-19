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
        public int gold_coins;
        public int silver_coins;

        public string speed_item;
        public string attack_item;
        public string defense_item;
        public string inteligence_item;
    }
}
