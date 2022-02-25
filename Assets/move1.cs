using System.Collections;
using System.Collections.Generic;
using BossBehaviour;
using Control;
using UnityEngine;

public class move1 : BossPhaseMovementFragment
{
    [SerializeField] private float speed = 5;
    private Vector3 moveVector = Vector3.right;
    [SerializeField] private int halfPeriodCount = 5;
    [SerializeField] private int currentHalfPeriodCounter;

    protected override void CustomFragmentStart()
    {
        currentHalfPeriodCounter = halfPeriodCount;
    }

    protected override void BossMovementUpdate()
    {
        
        if (bossController.transform.position.x > FieldBoundaries.instance.right ||
            bossController.transform.position.x < FieldBoundaries.instance.left)
        {
            var position = bossController.transform.position;
            position = new Vector3(
                Mathf.Clamp(
                    position.x,
                    FieldBoundaries.instance.left+0.1f,
                    FieldBoundaries.instance.right-0.1f), position.y,
                position.z);
            bossController.transform.position = position;

            moveVector = -moveVector;
            currentHalfPeriodCounter -= 1;
        }
        bossController.transform.position += Time.deltaTime * moveVector* speed;
        
    }

    protected override bool FragmentEnd()
    {
        return currentHalfPeriodCounter <= 0;
    }

    protected override void CustomFragmentEnd()
    {

    }
}
