using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Speed Boost")]
public class SpeedBoost : Ability
{
    [SerializeField] float boostAmt;
    [SerializeField] float boostDuration;

    MovementInterface movementInterface;



    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;

        movementInterface = OwningAbility.GetComponent<MovementInterface>();
        if(movementInterface != null)
        {
            movementInterface.SetMoveSpeed(movementInterface.GetMoveSpeed() + boostAmt);
            StartCorutine(ResetSpeed());
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        movementInterface.SetMoveSpeed(movementInterface.GetMoveSpeed() - boostAmt);
    }
}