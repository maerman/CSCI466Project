// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// Healthbar is a MonoBehavior that controls a healbar image attached to a 
/// DestructableObject that is a visual representation of the DestructableObject's health
/// </summary>
public class HealthBar : MonoBehaviour
{
    public DestructableObject owner;

    //initilized in editor
    public GameObject healthFront;

    private SpriteRenderer frontSpriteRenderer, backSpriteRenderer;
    
    void Start()
    {
        backSpriteRenderer = GetComponent<SpriteRenderer>();
        frontSpriteRenderer = healthFront.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //if this has no owner then it should not exist, so destory it
        if (owner == null)
        {
            Destroy(this.gameObject);
        }
        //if the owner of this is not active, hide this
        else if (!owner.active || !owner.gameObject.activeInHierarchy)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0;
            transform.localScale = scale;
        }
        else
        {
            //update the scale of front image baised on the owner's health
            Vector3 scale = healthFront.transform.localScale;
            if (owner.maxHealth <= 0)
            {
                owner.destroyThis();
                Destroy(this.gameObject);
                return;
            }
            scale.x = owner.health / owner.maxHealth;
            healthFront.transform.localScale = scale;

            //update the alpha of the front image baised on Options
            Color color = frontSpriteRenderer.color;
            color.a = Options.get().healthBarAlpha;
            frontSpriteRenderer.color = color;

            //update the alpha of the back image baised on Options
            color = backSpriteRenderer.color;
            color.a = Options.get().healthBarAlpha / 2.0f;
            backSpriteRenderer.color = color;

            //move this HealthBar to always be ontop of its owner
            Vector3 pos = owner.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}
