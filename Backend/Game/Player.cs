﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Backend.Game
{
    public class Player : Creature
    {
        public IChannel connection;
        public string user;
        public string token;

        public string InteligenceValue;
        public string SpeedValue;
        public string LevelValue;
        public string AttackValue;
        public string DefenseValue;
        public int gold_coins;
        public int silver_coins;

        public string speed_item="";
        public string attack_item = "";
        public string defense_item = "";
        public string inteligence_item = "";

        public Dictionary<String, String> all_item_type = new Dictionary<string, string>();
        public Dictionary<String, int> all_item_value = new Dictionary<string, int>();
        public Dictionary<String, int> all_gold_price = new Dictionary<string, int>();
        public Dictionary<String, int> all_silver_price = new Dictionary<string, int>();
        public Dictionary<String, String> all_item_durable = new Dictionary<string, String>();

        public Dictionary<String, int> Inventory = new Dictionary<string, int>();
        public Dictionary<String, String> selling_Inventory = new Dictionary<string, string>();

        public Dictionary<String, String> world_market_item_name = new Dictionary<string, String>();
        public Dictionary<String, String> world_market_item_seller = new Dictionary<string, String>();
        public Dictionary<String, int> world_market_item_price = new Dictionary<string, int>();

        private Weapon m_weapon;

        public Weapon Weapon { get { return m_weapon; } }
        public Player(IChannel channel)
        {
            connection = channel;
            channel.SetContent(this);
        }
        override public void OnHit(Creature enemy, int hpDec)
        {
            if (currentHP == 0)
                return;

            if (IsInvulnerable())
                return;

            m_lastHitTS = DateTime.Now;
            hpDec = currentHP - hpDec < 0 ? currentHP : hpDec;

            SHit hit = new SHit();
            hit.decHP = hpDec;
            hit.sourceId = enemy != null ? enemy.entityId : 0;
            hit.targetId = this.entityId;
            Broadcast(hit);

            currentHP = currentHP - hpDec;
            if (currentHP == 0)
            {
                OnDie();
                World.Instance.DelayInvoke(5, OnReSpawn);
            }
        }

        public override void AddEntity(Entity entity)
        {
            base.AddEntity(entity);
        }

        public override bool RemoveEntity(int id, out Entity entity)
        {
            return base.RemoveEntity(id, out entity);
        }


        public override void OnReSpawn()
        {
            // TODO read from last savepoint
            // read from database
            V3 pos = DefaultData.pos;
            Position = V3ToPoint3d(pos);
            currentHP = maxHP;

            SPlayerReSpawn msg = new SPlayerReSpawn();
            msg.entityId = entityId;
            msg.HP = maxHP;
            msg.position = pos;
            Broadcast(msg);
        }

        override public DEntity ToDEntity()
        {
            DEntity entity = base.ToDEntity();
            return entity;
        }

        override public void FromDEntity(DEntity entity)
        {
            scene = "Level1";
            name = "Ellen";
            base.FromDEntity(entity);
        }

        virtual public void OnEnterScene(Scene scene)
        {

        }

        virtual public void OnLeaveScene(Scene scene)
        {

        }

        virtual public void OnEquiped(Item item)
        {

        }

        virtual public void OnUnEquiped(Item item)
        {

        }

        override public void OnDie()
        {
            SPlayerDie msg = new SPlayerDie();
            msg.entityId = entityId;
            msg.isMine = false;
            Broadcast(msg, true);

            SPlayerDie msg1 = new SPlayerDie();
            msg1.entityId = entityId;
            msg1.isMine = true;
            connection.Send(msg1);
        }

        virtual public void OnBirth()
        {

        }

        public void SendSpawn(DEntity entity)
        {
            SSpawn msg = new SSpawn();
            msg.entity = entity;
            msg.isMine = entity.entityID == entityId;
            connection.Send(msg);
        }

        override public void Spawn()
        {
        }

        override public void Vanish()
        {

        }

        public void TakeItem(Item target)
        {
            SPlayerTake msgTake = new SPlayerTake();

            if (target.forClone)
            {
                Entity clone = World.CreateEntityByName(target.name);
                clone.forClone = false;
                if (target == null)
                    return;
                msgTake.clone = true;
                msgTake.itemID = clone.entityId;
                msgTake.name = clone.name;
                target = (Item)clone;
            }
            else
            {
                msgTake.clone = false;
                msgTake.itemID = target.entityId;
                msgTake.name = target.name;
            }

            if (!(target is Item))
            {
                return;
            }
            //msgTake.itemId;
            AddEntity(target);
            connection.Send(msgTake);
            if (target is Weapon && m_weapon == null)
            {
                EquipWeapon((Weapon)target);
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (m_weapon != null)
                return;

            m_weapon = weapon;
            SEquipWeapon msgEquip = new SEquipWeapon();
            msgEquip.playerID = this.entityId;
            msgEquip.itemName = this.m_weapon.name;
            msgEquip.itemID = this.m_weapon.entityId;

            Broadcast(msgEquip);
        }

        public bool IsEquipedWeapon()
        {
            return m_weapon != null;
        }

        public void SendEquipWeapon(Player player)
        {
            SEquipWeapon msgEquip = new SEquipWeapon();
            msgEquip.playerID = player.entityId;
            msgEquip.itemName = player.m_weapon.name;
            msgEquip.itemID = player.m_weapon.entityId;
            connection.Send(msgEquip);
        }
    }
}
