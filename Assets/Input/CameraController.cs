// written by: Thomas Stewart, Michael Quinn
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Shane Barry, Metin Erman

using UnityEngine;
using System.Collections;

/// <summary>
/// CameraController is a MonoBehavior that is used to control a Camera GameObject.
/// This allows for zooming in and out, keeps the camera in the correct bounds and 
/// centered over the Players. 
/// </summary>
public class CameraController : MonoBehaviour
{
    public Camera gameCamera;
    public Rect defaultScreenSize = new Rect(Vector2.zero, new Vector2(40, 30));
    private float preferedSize;

    void Start()
    {
        gameCamera = GetComponent<Camera>();
        preferedSize = gameCamera.orthographicSize;
    }

    void Update()
    {
        float aspectRatio = gameCamera.aspect;

        Vector3 position = transform.position;
        Rect levelBounds;
        float xUpperLimit = float.MinValue;
        float xLowerLimit = float.MaxValue;
        float yUpperLimit = float.MinValue;
        float yLowerLimit = float.MaxValue;
        float size;

        //if a Level exists, control the camera baised on it
        if (Level.current != null)
        {
            levelBounds = Level.current.gameBounds;

            //find the farthest in each direction the Players are
            //and save them as the limits the camea must show
            foreach (Player item in Level.current.players)
            {
                if (item != null && item.active)
                {
                    if (item.position.x > xUpperLimit)
                        xUpperLimit = item.position.x;
                    if (item.position.x < xLowerLimit)
                        xLowerLimit = item.position.x;
                    if (item.position.y > yUpperLimit)
                        yUpperLimit = item.position.y;
                    if (item.position.y < yLowerLimit)
                        yLowerLimit = item.position.y;
                }
            }

            //Allow the Players zoom the camera in and out
            foreach (PlayerControls item in Controls.get().players)
            {
                if (item.zoomOut)
                    preferedSize *= 1 + Options.get().cameraZoomSpeed;
                if (item.zoomIn)
                    preferedSize /= 1 + Options.get().cameraZoomSpeed;
            }
        }
        //if a Level does not exist, use the defaults to control the camra
        else
        {
            levelBounds = defaultScreenSize;
        }

        //if the limits have not been changed because there is not Level or Players in the Level
        //then just keep the camera where it is
        if (xUpperLimit == float.MinValue)
        {
            xUpperLimit = position.x;
            xLowerLimit = position.x;
            yUpperLimit = position.y;
            yLowerLimit = position.y;
        }

        //make sure the camera is not zoomed too far in or out
        if (2f * preferedSize > levelBounds.height)
            preferedSize = levelBounds.height / 2f;
        else if (aspectRatio * 2f * preferedSize > levelBounds.width)
            preferedSize = levelBounds.width / (aspectRatio * 2f);
        else if (preferedSize < Options.get().cameraEdgeBufferSize)
            preferedSize = Options.get().cameraEdgeBufferSize;

        size = preferedSize;

        //change the limits to put a buffer space around the edges of where the camera will see
        //so that the Players are not right on the edge of the screen
        xUpperLimit += Options.get().cameraEdgeBufferSize;
        xLowerLimit -= Options.get().cameraEdgeBufferSize;
        yUpperLimit += Options.get().cameraEdgeBufferSize;
        yLowerLimit -= Options.get().cameraEdgeBufferSize;

        //make sure the limites are out outside the Level's bounds
        if (xUpperLimit > levelBounds.xMax)
            xUpperLimit = levelBounds.xMax;
        if (xLowerLimit < levelBounds.xMin)
            xLowerLimit = levelBounds.xMin;
        if (yUpperLimit > levelBounds.yMax)
            yUpperLimit = levelBounds.yMax;
        if (yLowerLimit < levelBounds.yMin)
            yLowerLimit = levelBounds.yMin;

        //the camera will be positioned in the center of the limits of what it will show
        position.x = (xUpperLimit - xLowerLimit) / 2.0f + xLowerLimit;
        position.y = (yUpperLimit - yLowerLimit) / 2.0f + yLowerLimit;

        //make sure the camera is zoomed out far enough to be able to show all of the limits
        if (xUpperLimit - xLowerLimit > size * 2f * aspectRatio)
            size = (xUpperLimit - xLowerLimit) / (2f * aspectRatio);
        if (yUpperLimit - yLowerLimit > size * 2f)
            size = (yUpperLimit - yLowerLimit) / 2f;

        //make sure the camera is not showing outside of the Level's bounds
        //shift the camera's position if it is
        if (position.x + size * aspectRatio > levelBounds.xMax)
            position.x -= position.x + size * aspectRatio - levelBounds.xMax;
        if (position.x - size * aspectRatio < levelBounds.xMin)
            position.x += levelBounds.xMin - position.x + size * aspectRatio;
        if (position.y + size > levelBounds.yMax)
            position.y -= position.y + size - levelBounds.yMax;
        if (position.y - size < levelBounds.yMin)
            position.y += levelBounds.yMin - position.y + size;

        //set the Level's background to be the center of the Level
        if (Level.current != null)
            Level.current.backgroundPosition = position;

        //set the camrea settings to the values calculated
        gameCamera.orthographicSize = size;
        transform.position = position;
    }
}
