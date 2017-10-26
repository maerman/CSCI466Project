using UnityEngine;
using System.Collections;

public class SpaceDust : NonInteractiveObject
{
    private Vector2 imageDimentions;

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
        scale = Vector2.one;
        drawMode = SpriteDrawMode.Simple;
        imageDimentions = dimentions;

        drawMode = SpriteDrawMode.Tiled;
        GetComponent<SpriteRenderer>().size = new Vector2(level.gameBounds.width * 2, level.gameBounds.height * 2);

        position = level.gameBounds.center;
    }

    protected override void updateNonInteractiveObject()
    {
        //make sure it always lines up no matter where it is put
        float x = (int)(position.x / imageDimentions.x) * imageDimentions.x;
        float y = (int)(position.y / imageDimentions.y) * imageDimentions.y;
        position = new Vector2(x, y);
    }
}
