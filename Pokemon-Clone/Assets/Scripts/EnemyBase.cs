using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    public override void StartIdle()
    {
        base.StartIdle();

        // anything here is called when entering the idle state
    }

    public override void Idle()
    {
        base.Idle();

        // anything here is called during the idle state through update()

        // enemy movement code here?
    }

    public override void StartBattle()
    {
        base.StartBattle();

        // anything here is called when entering the battle state
    }

    public override void Battle()
    {
        base.Battle();

        // anything here is called during the battle state through update()

        if (IsTurn)
        {
            Debug.Log("It is the " + name + "'s turn");

            // Fight
            BattleManager.Instance.Attack(this);
        }
    }
}
