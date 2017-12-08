// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

public class TowardsTarget : BlobBehaviour
{
    private float moveSpeed;
    private float updateTargetSecs;
    private int updateTargetTimer = 100;
    private SpaceObject target;


    public TowardsTarget(float moveSpeed, float updateTargetSecs)
    {
        this.moveSpeed = moveSpeed;
        this.updateTargetSecs = updateTargetSecs;
    }

    public override bool combine(BlobBehaviour other)
    {
        if (other.GetType() == typeof(TowardsTarget))
        {
            TowardsTarget theOther = (TowardsTarget)other;

            float amountThis = magnitude / (magnitude + other.magnitude);

            magnitude += other.magnitude;

            moveSpeed = moveSpeed * amountThis + theOther.moveSpeed * (1 - amountThis);
            updateTargetSecs = updateTargetSecs * amountThis + theOther.updateTargetSecs * (1 - amountThis);

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sets its target to the closest enemy DestructableObject to thisBlob, updating the target every so often
    /// Moves thisBlob towars the target. 
    /// </summary>
    /// <param name="thisBlob">The Blob this behavior is attached to.</param>
    public override void update(Blob thisBlob)
    {
        updateTargetTimer--;
        if (updateTargetTimer <= 0)
        {
            updateTargetTimer = (int)(updateTargetSecs * Level.current.updatesPerSec);
            target = null;
        }

        if (target == null || !target.enabled)
        {
            target = thisBlob.closestObject<SpaceObject>(Level.current.getTypes(true, true, false, false), false);
        }
        else
        {
            thisBlob.moveTowards(target, moveSpeed * magnitude * thisBlob.difficultyModifier);
        }
    }
}

public class RedBlob : Blob
{
    public float moveSpeed = 2f;
    public float updateTargetSecs = 2f;

    protected override void startBlob()
    {
        color = new Color(1, 0, 0, color.a);

        behaviors.AddFirst(new TowardsTarget(moveSpeed, updateTargetSecs));
    }
}
