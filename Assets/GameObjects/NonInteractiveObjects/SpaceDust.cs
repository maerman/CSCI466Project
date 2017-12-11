// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// SpaceDust is a NonInteractiveObject thats tiled across Levels to give the users a sense
/// of how they are moving, since the background does not move. This adjusts its position by 
/// a multiple of its texture's size when the Level's bounds are more than the texture's size
/// </summary>
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
        //tile the texture
        drawMode = SpriteDrawMode.Tiled;

        //set the area to be tiled as twice the Level's size
        GetComponent<SpriteRenderer>().size = new Vector2(level.gameBounds.width * 2, level.gameBounds.height * 2);

        //put this at the center of the Level
        position = level.gameBounds.center;
    }

    protected override void updateNonInteractiveObject()
    {
        //changes the size of this if the Level's bounds changed, to keep it twice the size of the Level
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


        //move this SpaceDust's position by a multple of its textures's size if the gameBounds has moved too much
        Vector2 pos = position;

        if (level.gameBounds.center.x - pos.x > spriteSize.x * scale.x)
            pos.x += spriteSize.x * scale.x;
        else if (level.gameBounds.center.x - pos.x < -spriteSize.x * scale.x)
            pos.x -= spriteSize.x * scale.x;

        if (level.gameBounds.center.y - pos.y> spriteSize.y * scale.y)
            pos.y += spriteSize.y * scale.y;
        else if (level.gameBounds.center.y - pos.y < -spriteSize.y * scale.y)
            pos.y -= spriteSize.y * scale.y;

        position = pos;
    }
}
