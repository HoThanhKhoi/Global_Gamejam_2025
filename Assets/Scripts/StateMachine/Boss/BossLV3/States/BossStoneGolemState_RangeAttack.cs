﻿using System.Collections;
using UnityEngine;

public class BossStoneGolemState_RangeAttack : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_RangeAttack(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = owner.ProjectileCooldown - animationLength;
        owner.ShowAttackIndicatorOnPlayer(true);
    }

    public override void Update()
    {
        base.Update();

        if (TimeOut())
        {
            if (owner.IsProjectileCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.ArmorBuff, 1f);
            }
            else
            {
                stateTimer = owner.ProjectileCooldown;
                owner.FaceToPlayer();
                PlayAnimationFromFrame(4);
            }
        }
    }

    public override void AnimationTrigger(int index)
    {
        base.AnimationTrigger(index);

        owner.Rb.bodyType = RigidbodyType2D.Dynamic;
        owner.SpawnArmProjectile();
    }

    public override void Exit()
    {
        base.Exit();

        owner.ShowAttackIndicatorOnPlayer(false);
    }
}