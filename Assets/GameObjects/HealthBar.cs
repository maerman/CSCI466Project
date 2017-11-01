using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public static bool showHealth = true;

    public DestructableObject owner;

    //initilized in editor
    public GameObject healtBar;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (owner == null || !owner.destroyed)
        {
            Destroy(this);
        }
        else if (!owner.gameObject.activeInHierarchy || !showHealth)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = healtBar.transform.localScale;
            scale.x = owner.health / owner.maxHealth;
            healtBar.transform.localScale = scale;

            transform.position = owner.position;
        }
    }
}
