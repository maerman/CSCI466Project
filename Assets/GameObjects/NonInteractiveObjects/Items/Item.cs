using UnityEngine;
using System.Collections;

public abstract class Item : NonInteractiveObject
{
    protected override void startNonInteractiveObject()
    {

    }

    protected override void updateNonInteractiveObject()
    {

    }

    public abstract void updateItem(bool used, Player player);
}
