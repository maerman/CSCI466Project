// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

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

    /// <summary>
    /// Every so often, shoots a shotBlob from itself at closest enemy DestructableObject
    /// </summary>
    /// <param name="thisBlob">The Blob this behavior is attached to.</param>
    public override void update(Blob thisBlob)
    {
        if (thisBlob.scale.x > shotSize)
        {
            shootTimer--;

            if (shootTimer <= 0)
            {
                //reset shootTimer
                shootTimer = (int)(shootTimeSecs * Level.current.updatesPerSec / magnitude / thisBlob.difficultyModifier);

                SpaceObject target = thisBlob.closestObject<SpaceObject>(Level.current.getTypes(true, true, false, false), false);

                if (target != null)
                {
                    thisBlob.turnTowards(target);

                    //find where the shotBlob should be shot to hit the target, given their current positions and velocities
                    Vector3 aimAt = SpaceObject.intersectPosTime(target, shotSpeed, thisBlob.position + new Vector2(0, thisBlob.scale.x * 3).rotate(thisBlob.angle));

                    //if the taget cannot be hit, just shoot straight at its current position
                    if (aimAt.z < 0)
                    {
                        aimAt = target.position;
                    }

                    //create the shotBlob with an the velocity to hit where it is being aimed at
                    float theAngle = thisBlob.angleToAbsolute(aimAt);
                    Blob shotBlob = (Blob)Level.current.createObject("BlobPF", thisBlob.position + new Vector2(0, thisBlob.scale.x * 3).rotate(thisBlob.angle), theAngle,
                        new Vector2(0, shotSpeed).rotate(theAngle), thisBlob.angularVelocity + magnitude, shotSize);

                    float shotPortion = shotSize / thisBlob.scale.x;
                    float thisPortion = 1 - shotPortion;

                    //make the shotBlob's health, mass and Behaviors proportional to how much of thisBlob is it taking
                    //also decrease thisBlob's behaviors baised on how much the shotBlob took from it
                    shotBlob.mass = thisBlob.mass * shotPortion;
                    shotBlob.health = thisBlob.health * shotPortion;
                    foreach (BlobBehaviour item in thisBlob.behaviors)
                    {
                        BlobBehaviour temp = item.clone();

                        temp.magnitude *= shotPortion;
                        shotBlob.behaviors.AddFirst(temp);

                        item.magnitude *= thisPortion;
                    }

                    shotBlob.team = thisBlob.team;
                    shotBlob.color = thisBlob.color;

                    //find the new size of thisBlob baised on how much area the shotBlob took from it
                    float area = (thisBlob.scale.x * thisBlob.scale.x / 4 * Mathf.PI);
                    area -= (shotSize * shotSize / 4 * Mathf.PI); 
                    float theScale = Mathf.Sqrt(area * 4 / Mathf.PI);

                    //decrease thisBlob's size, mass and health baised on how much the shotBlob took from it
                    thisBlob.scale = new Vector2(theScale, theScale);
                    thisBlob.mass *= thisPortion;
                    thisBlob.health *= thisPortion;
                    thisBlob.maxHealth -= shotBlob.health;
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
