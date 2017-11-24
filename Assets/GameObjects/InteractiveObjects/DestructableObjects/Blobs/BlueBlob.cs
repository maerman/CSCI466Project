using UnityEngine;
using System.Collections;

public class PullOthers : BlobBehaviour
{
    private float pullSpeed;

    public PullOthers(float moveSpeed)
    {
        this.pullSpeed = moveSpeed;
    }

    public void combine(PullOthers other)
    {

    }

    public override void update(Blob thisBlob)
    {
        foreach (DestructableObject item in Level.currentLevel.destructables)
        {
            if (item.GetType().IsSubclassOf(typeof(Blob)) || item.GetType() == typeof(Blob))
            {
                item.moveTowards(thisBlob, pullSpeed * magnitude / thisBlob.distanceFrom(item));
            }
        }
    }
}

public class BlueBlob : Blob
{
    public float pullSpeed;

    protected override void startBlob()
    {
        color = new Color(0, 0, 1, color.a);

        behaviors.AddFirst(new PullOthers(pullSpeed));
    }
}
