using UnityEngine;
using System.Collections;

public class TowardsTarget : BlobBehaviour
{
    private float moveSpeed;
    private float updateTargetSecs;
    private int updateTargetTimer = 100;
    private DestructableObject target;


    public TowardsTarget(float moveSpeed, float updateTargetSecs)
    {
        this.moveSpeed = moveSpeed;
        this.updateTargetSecs = updateTargetSecs;
    }

    public void combine(TowardsTarget other)
    {

    }

    public override void update(Blob thisBlob)
    {
        updateTargetTimer--;
        if (updateTargetTimer <= 0)
        {
            updateTargetTimer = (int)(updateTargetSecs * Level.currentLevel.updatesPerSec);
            target = null;
        }

        if (target == null || !target.enabled)
        {
            target = (DestructableObject)thisBlob.closestObject<SpaceObject>(Level.currentLevel.getTypes(true, true, false, false), false);
        }
        else
        {
            thisBlob.moveTowards(target, moveSpeed * magnitude);
        }
    }
}

public class RedBlob : Blob
{
    public float moveSpeed;
    public float updateTargetSecs;

    protected override void startBlob()
    {
        color = new Color(1, 0, 0, color.a);

        behaviors.AddFirst(new TowardsTarget(moveSpeed, updateTargetSecs));
    }
}
