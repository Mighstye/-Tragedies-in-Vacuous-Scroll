using BossBehaviour;
using Control;
using DG.Tweening;
using UnityEngine;

public class move1 : BossPhaseMovementFragment
{
    [SerializeField] private float speed = 3;
    [SerializeField] private int halfPeriodCount = 5;
    [SerializeField] private int currentHalfPeriodCounter;
    private Vector3 moveVector = Vector3.right;

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
                    FieldBoundaries.instance.left + 0.1f,
                    FieldBoundaries.instance.right - 0.1f), position.y,
                position.z);
            bossController.transform.position = position;

            moveVector = -moveVector;
            currentHalfPeriodCounter -= 1;
            bossController.animationLib.AnimationMove(moveVector.x);
        }

        bossController.transform.position += Time.deltaTime * moveVector * speed;
    }

    protected override void BossTween(Sequence sequence)
    {
        var d = 1f;
        sequence.Append(bossController.transform.DOMoveX(FieldBoundaries.instance.left + 1f, 1)
                .OnStart(() => bossController.animationLib.AnimationMove(-1)))
            .Append(bossController.transform.DOMoveX(FieldBoundaries.instance.right - 1f, 2)
                .SetLoops(2, LoopType.Yoyo).OnStart(() => bossController.animationLib.AnimationMove(1))
                .OnStepComplete(() =>
                {
                    d = -d;
                    bossController.animationLib.AnimationMove(d);
                }));
    }

    protected override bool FragmentEnd()
    {
        return currentHalfPeriodCounter <= 0;
    }

    protected override void CustomFragmentEnd()
    {
    }
}