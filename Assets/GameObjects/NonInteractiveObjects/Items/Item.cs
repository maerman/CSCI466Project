using UnityEngine;
using System.Collections;

public abstract class Item : NonInteractiveObject
{
    private bool previousUse;
    private bool itemUpdated = true;
    protected Player holder;
    private int pickupDropTime = 100;
    private int pickupDropTimer;
    private float doubleClickTimeSecs = 0.5f;
    private int doubleClickTimer;

    protected override void destroyNonInteractiveObject()
    {
        dropItem();
        holder = null;
    }

    protected override void startNonInteractiveObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other) { }

    protected override void indestructableObjectCollision(IndestructableObject other) { }

    protected override void playerCollision(Player other) { }

    protected override void updateNonInteractiveObject()
    {
        if (holder != null)
        {
            if (!itemUpdated || !holder.enabled)
            {
                drop();
            }
        }
        itemUpdated = false;

        if (pickupDropTimer > 0)
        {
            pickupDropTimer--;
        }
    }

    protected abstract void pickupItem();
    public bool pickup(Player player)
    {
        if (player == null)
        {
            throw new System.Exception("Player value sent to Item.pickup() set to NULL");
        }
        else if (pickupDropTimer <= 0)
        {
            for (int i = 0; i < player.items.Length; i++)
            {
                if (player.items[i] == null)
                { 
                    return pickup(player, i);
                }
                else if (player.items[i] == this)
                {
                    break;
                }
            }     
        }

        return false;
    }

    public bool pickup(Player player, int itemSlot)
    {
        if (player == null)
        {
            throw new System.Exception("Player value sent to Item.pickup() set to NULL");
        }
        else if (itemSlot < 0 && itemSlot >= player.items.Length)
        {
            throw new System.Exception("ItemSlot value set to Item.pickup() is out of range: " + itemSlot.ToString());
        }
        else if (pickupDropTimer <= 0)
        {
            dropItem();
            player.items[itemSlot] = this;
            holder = player;
            pickupItem();
            enabled = false;
            pickupDropTimer = pickupDropTime;
            return true;
        }
        return false;
    }

    protected abstract void dropItem();
    public void drop()
    {
        if (holder != null && pickupDropTimer <= 0)
        {
            dropItem();
            for (int i = 0; i < holder.items.Length; i++)
            {
                if (holder.items[i] == this)
                {
                    holder.items[i] = null;
                }
            }
            position = holder.position;
            enabled = true;
            pickupDropTimer = pickupDropTime;
            holder = null;
        }
    }

    protected abstract void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse);
    public void holding(bool use)
    {
        if (holder == null)
        {
            throw new System.Exception("Item holder set to NULL in updateItem, call pickup() first");
        }
        else if (!holder.enabled)
        {
            drop();
        }
        else
        {
            bool start = false;
            bool end = false;
            bool dbl = false;
            if (use & !previousUse)
            {
                start = true;

                if (doubleClickTimer > 0)
                {
                    dbl = true;
                    doubleClickTimer = 0;
                }
                else
                {
                    doubleClickTimer = (int)(doubleClickTimeSecs * level.updatesPerSec);
                }
            }
            else if (doubleClickTimer > 0)
            {
                doubleClickTimer--;
            }
            if (!use & previousUse)
            {
                end = true;
            }

            holdingItem(use, start, end, dbl);
        }


        if (pickupDropTimer > 0)
        {
            pickupDropTimer--;
        }

        previousUse = use;
        itemUpdated = true;
    }

    public virtual void loadValues(string values)
    {

    }

    public virtual string getValues()
    {
        return "";
    }
}
