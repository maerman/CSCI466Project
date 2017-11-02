using UnityEngine;
using System.Collections;

public class SpaceDust : NonInteractiveObject
{
    protected override void destroyNonInteractiveObject()
    {

    }

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
        position = level.gameBounds.center;
    }

    protected override void updateNonInteractiveObject()
    {
        if (size.x > level.gameBounds.width / 2f)
        {
            float toScale = level.gameBounds.width / 2f / spriteSize.x;
            scale = new Vector2(toScale, toScale);
        }
        if (size.y > level.gameBounds.height / 2f)
        {
            float toScale = level.gameBounds.height / 2f / spriteSize.y;
            scale = new Vector2(toScale, toScale);
        }


        Vector2 pos = position;

        if (level.gameBounds.center.x - pos.x > spriteSize.x * scale.x)
        {
            pos.x += spriteSize.x * scale.x;
        }
        else if (level.gameBounds.center.x - pos.x < -spriteSize.x * scale.x)
        {
            pos.x -= spriteSize.x * scale.x;
        }

        if (level.gameBounds.center.y - pos.y> spriteSize.y * scale.y)
        {
            pos.y += spriteSize.y * scale.y;
        }
        else if (level.gameBounds.center.y - pos.y < -spriteSize.y * scale.y)
        {
            pos.y -= spriteSize.y * scale.y;
        }

        position = pos;
    }
}
