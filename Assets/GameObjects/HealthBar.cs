using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public static bool showHealth = true;
    public static float frontAlpha = 0.7f;
    public static float backAlpha = 0.5f;

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
        if (owner == null)
        {
            Destroy(this.gameObject);
        }
        else if (!owner.enabled || !owner.gameObject.activeInHierarchy || !showHealth)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = healthFront.transform.localScale;
            scale.x = owner.health / owner.maxHealth;
            healthFront.transform.localScale = scale;

            Color color = frontSpriteRenderer.color;
            color.a = frontAlpha;
            frontSpriteRenderer.color = color;

            color = backSpriteRenderer.color;
            color.a = backAlpha;
            backSpriteRenderer.color = color;

            Vector3 pos = owner.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}
