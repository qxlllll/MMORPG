﻿using Common;
using Common.Data;

namespace Backend.Game
{
    public class Player : Creature
    {
        public IChannel connection;
        public string user;
        public string token;
        private Item m_weapon;

        public Player(IChannel channel)
        {
            connection = channel;
            channel.SetContent(this);
        }
        override public void UnderAttack(Creature creature)
        {
            base.UnderAttack(creature);
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

        virtual public void OnDie()
        {

        }

        virtual public void OnBirth()
        {

        }

        public void SendSpawn(DEntity entity)
        {
            SSpawn msg = new SSpawn();
            msg.entity = entity;
            msg.isMine = entity.entityID == entityID;
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
            SPlayerTakeItem msgTake = new SPlayerTakeItem();

            if (target.forClone)
            {
                Entity clone = World.CreateEntityByName(target.name);
                clone.forClone = false;
                if (target == null)
                    return;
                msgTake.clone = true;
                msgTake.itemID = clone.entityID;
                msgTake.name = clone.name;
                target = (Item)clone;
            }
            else
            {
                msgTake.clone = false;
                msgTake.itemID = target.entityID;
                msgTake.name = target.name;
            }

            if (!(target is Item))
            {
                return;
            }
            //msgTake.itemId;
            AddEntity(target);
            connection.Send(msgTake);
            if (target is Weapon)
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
            msgEquip.playerID = this.entityID;
            msgEquip.itemName = weapon.name;
            msgEquip.itemID = weapon.entityID;
            Broundcast(msgEquip);
        }
    }
}
