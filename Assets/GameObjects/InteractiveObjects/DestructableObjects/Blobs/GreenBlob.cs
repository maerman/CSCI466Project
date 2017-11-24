using UnityEngine;
using System.Collections;

public class ShootBehavior : BlobBehaviour
{
    private float shootTimeSecs;
    private int shootTimer = 100;
    private float shootSize;
    private float shootSpeed;


    public ShootBehavior()
    {

    }

    public void combine(ShootBehavior other)
    {

    }

    public override void update(Blob thisBlob)
    {

    }
}

public class GreenBlob : Blob
{
    protected override void startBlob()
    {
        color = new Color(0, 1, 0, color.a);

        behaviors.AddFirst(new ShootBehavior());
    }
}
