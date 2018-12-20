﻿/*
 * message code define
 * if you are changing this file, mind to rebuild both Frontend and Backend later
 */

namespace Common
{
    public enum Command
    {
        NONE,
        SBEGIN = 0,
        S_PLAYER_ENTER,
        S_PLAYER_ATTRIBUTE,
        S_MARKET_ATTRIBUTE,
        S_WORLD_MARKET_ATTRIBUTE,
        S_GET_INVENTORY,
        S_ITEM_SPAWN,
        S_SPAWN,
        S_PLAYER_RESPAWN,
        S_PLAYER_ACTION,
        S_ENTITY_DESTROY,
        S_PLAYER_MOVE,
        S_SPRITE_MOVE,
        S_JUMP,
        S_ATTACK,
        S_EQUIP_WEAPON,
        S_HIT,
        S_TAKE_ITEM,
        S_EXIT,
        S_SPRITE_DIE,
        S_PLAYER_DIE,
        S_TIP_INFO,
        SEND,

        CBEGIN,
        C_LOGIN,
        C_REGISTER,
        C_PLAYER_ENTER,
        C_BUY,
        C_WORLD_MARKET_BUY,
        C_APPLY,
        C_UNAPPLY,
        C_SELL,
        C_GET_INVENTORY,
        C_GET_WORLD_MARKET,
        C_PLAYER_ATTACK,
        C_PLAYER_JUMP,
        C_PLAYER_MOVE,
        C_PLAYER_TAKE,
        C_POSITION_REVISE,
        C_ENEMY_CLOSING,
        C_DAMAGE,
        CEND,

        DEBUGGING, // THE FOLLOWING MESSEGES ARE FOR DEBUGGING
        C_FIND_PATH,
        S_FIND_PATH,

        CMD_END, // DO NOT GREATER THAN UINT_MAX !!!
    }
}
