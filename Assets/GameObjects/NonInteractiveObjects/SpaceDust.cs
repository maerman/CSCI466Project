using UnityEngine;
using System.Collections;

public class SpaceDust : NonInteractiveObject
{
    public Vector2 imageResolution = new Vector2(1920, 1080);

    protected override void destructableObjectCollision(DestructableObject other)
    {
        
    }

    protected override void indestructableObjectCollision(IndestructableObject other)
    {
        
    }

    protected override void playerCollision(Player other)
    {
        
    }

    protected override void startNonInteractiveObject()
    {
        drawMode = SpriteDrawMode.Tiled;
        GetComponent<SpriteRenderer>().size = new Vector2(level.gameBounds.width * 2, level.gameBounds.height * 2);

        scale = Vector2.one;
        position = level.gameBounds.center;

        imageResolution /= 100f;
    }

    protected override void updateNonInteractiveObject()
    {
        //make sure it always lines up no matter where it is put

        Vector2 pos = position;

        if (level.gameBounds.center.x - pos.x > imageResolution.x)
        {
            pos.x += imageResolution.x;
        }
        else if (level.gameBounds.center.x - pos.x < -imageResolution.x)
        {
            pos.x -= imageResolution.x;
        }

        if (level.gameBounds.center.y - pos.y> imageResolution.y)
        {
            pos.y += imageResolution.y;
        }
        else if (level.gameBounds.center.y - pos.y < -imageResolution.y)
        {
            pos.y -= imageResolution.y;
        }

        position = pos;
    }
}
