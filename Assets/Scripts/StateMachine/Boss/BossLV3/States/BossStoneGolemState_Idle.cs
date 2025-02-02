﻿using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Idle : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Idle(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = owner.IdleTime;
    }

    public override void Update()
    {
        base.Update();

        owner.FaceToPlayer();

        if (TimeOut())
        {
            if (!owner.IsProjectileCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.RangeAttack);
            }
            else if(!owner.IsZipShootCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.Zip);
            }
            else if(!owner.IsLaserCastCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.Glowing);
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            EnemyDamageableComponent damageable = owner.GetComponent<EnemyDamageableComponent>();

            damageable.TakeDamage(damageable.PlayerDamage);

            stateMachine.ChangeState(BossStoneGolemStateMachine.State.Hurt);
        }
    }
}
