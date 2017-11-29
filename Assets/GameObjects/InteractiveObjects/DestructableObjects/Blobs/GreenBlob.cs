using UnityEngine;
using System.Collections;

public class Shoot : BlobBehaviour
{
    private float shootTimeSecs;
    private int shootTimer = 100;
    private float shotSize;
    private float shotSpeed;


    public Shoot(float shootTimeSecs, float shotSize, float shotSpeed)
    {
        this.shootTimeSecs = shootTimeSecs;
        this.shotSize = shotSize;
        this.shotSpeed = shotSpeed;
    }

    public override bool combine(BlobBehaviour other)
    {
        if (other.GetType() == typeof(Shoot))
        {
            Shoot theOther = (Shoot)other;

            float amountThis = magnitude / (magnitude + other.magnitude);

            magnitude += other.magnitude;

            shotSpeed = shotSpeed * amountThis + theOther.shotSpeed * (1 - amountThis);
            shotSize = shotSize * amountThis + theOther.shotSize * (1 - amountThis);
            shootTimeSecs = shootTimeSecs * amountThis + theOther.shootTimeSecs * (1 - amountThis);

            return true;
        }
        else
        {
            return false;
        }
    }

    public override void update(Blob thisBlob)
    {
        if (thisBlob.scale.x > shotSize)
        {
            shootTimer--;

            if (shootTimer <= 0)
            {
                shootTimer = (int)(shootTimeSecs * Level.current.updatesPerSec / magnitude);

                SpaceObject target = thisBlob.closestObject<SpaceObject>(Level.current.getTypes(true, true, false, false), false);

                if (target != null)
                {
                    thisBlob.turnTowards(target);

                    Vector3 aimAt = SpaceObject.intersectPosTime(target, shotSpeed, thisBlob.position + new Vector2(0, thisBlob.scale.x * 3).rotate(thisBlob.angle));

                    if (aimAt.z < 0)
                    {
                        aimAt = target.position;
                    }

                    float theAngle = thisBlob.angleToAbsolute(aimAt);
                    Blob current = (Blob)Level.current.createObject("BlobPF", thisBlob.position + new Vector2(0, thisBlob.scale.x * 3).rotate(thisBlob.angle), theAngle,
                        new Vector2(0, shotSpeed).rotate(theAngle), thisBlob.angularVelocity + magnitude, shotSize);

                    float shotPortion = shotSize / thisBlob.scale.x;
                    float thisPortion = 1 - shotPortion;

                    current.mass = thisBlob.mass * shotPortion;
                    current.health = thisBlob.health * shotPortion;
                    current.team = thisBlob.team;
                    current.color = thisBlob.color;

                    foreach (BlobBehaviour item in thisBlob.behaviors)
                    {
                        BlobBehaviour temp = item.clone();

                        temp.magnitude *= shotPortion;
                        current.behaviors.AddFirst(temp);

                        item.magnitude *= thisPortion;
                    }

                    float area = (thisBlob.scale.x * thisBlob.scale.x / 4 * Mathf.PI);
                    area -= (shotSize * shotSize / 4 * Mathf.PI); 
                    float theScale = Mathf.Sqrt(area * 4 / Mathf.PI);

                    thisBlob.scale = new Vector2(theScale, theScale);
                    thisBlob.mass *= thisPortion;
                    thisBlob.health *= thisPortion;
                    thisBlob.maxHealth -= current.health;
                }
            }
        }
    }
}

public class GreenBlob : Blob
{
    public float shootTimeSecs = 2;
    public float shotSize = 0.25f;
    public float shotSpeed = 15;

    protected override void startBlob()
    {
        color = new Color(0, 1, 0, color.a);

        behaviors.AddFirst(new Shoot(shootTimeSecs, shotSize, shotSpeed));
    }
}
