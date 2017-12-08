// written by: Thomas Stewart, Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// Items are NonInteractiveObjects that can be pickedup, held, used and dropped by Players. 
/// Different types of Items will do different things when used and can be used in different ways. 
/// </summary>
public abstract class Item : NonInteractiveObject
{
    private bool previousUse;
    private bool itemUpdated = true;
    protected Player holder;
    private int pickupDropTime = 100;
    private int pickupDropTimer;
    private float doubleClickTimeSecs = 0.5f;
    private int doubleClickTimer;

    //If this Item is being destroyed, drop it. 
    protected override void destroyNonInteractiveObject()
    {
        dropItem();
        holder = null;
    }

    protected override void startNonInteractiveObject()
    {
        drawDepth = 1;
    }

    protected override void destructableObjectCollision(DestructableObject other) { }

    protected override void indestructableObjectCollision(IndestructableObject other) { }

    protected override void playerCollision(Player other) { }

    protected override void updateNonInteractiveObject()
    {
        //If update is being called, this Item is active in the Level and
        //should ot be held, do drop it
        if (holder != null)
        {
            if (!itemUpdated || !holder.active)
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

    //called when a Player picksup this Item
    protected abstract void pickupItem();

    /// <summary>
    /// Picks up this Item and puts it in the given Player's first open Item slot
    /// </summary>
    /// <param name="player">Player to pickup this Item.</param>
    /// <returns>Return true if the Item was succesfully pickup, false otherwise.</returns>
    public bool pickup(Player player)
    {
        //Given Player should not be null
        if (player == null)
        {
            throw new System.Exception("Player value sent to Item.pickup() set to NULL");
        }
        //If enough time has passed since this Item was pickedup or dropped, proceed to pick it up
        else if (pickupDropTimer <= 0)
        {
            //Find the first open Item slot the given Player has
            for (int i = 0; i < player.items.Length; i++)
            {
                //If a slot is open, then pickup this Item
                if (player.items[i] == null)
                {
                    return pickup(player, i);
                }
                //If this item is already in this Player's invintory,
                //stop looking for a slot to put it in
                else if (player.items[i] == this)
                {
                    break;
                }
            }     
        }
        return false;
    }

    /// <summary>
    /// Picks up this Item and puts it in the given slot number in the given Player's item slots
    /// </summary>
    /// <param name="player">Player to pickup this Item.</param>
    /// <param name="itemSlot">Slot number to put the Item in.</param>
    /// <returns>Return true if the Item was succesfully pickup, false otherwise.</returns>
    public bool pickup(Player player, int itemSlot)
    {
        //Given Player should not be null
        if (player == null)
        {
            throw new System.Exception("Player value sent to Item.pickup() set to NULL");
        }
        //Make sure the specified itemSlot is within the correct bounds
        else if (itemSlot < 0 && itemSlot >= player.items.Length)
        {
            throw new System.Exception("ItemSlot value set to Item.pickup() is out of range: " + itemSlot.ToString());
        }
        //If enough time has passed since this Item was pickedup or dropped, proceed to pick it up
        else if (pickupDropTimer <= 0)
        {
            //Make sure this Item is not already held
            dropItem();

            //Pickup this Item
            player.items[itemSlot] = this;
            holder = player;
            pickupItem();

            //Pickedup items should not be active in the game
            active = false;

            //Reset the pickupDropTimer, so it is not accidently dropped right away
            pickupDropTimer = pickupDropTime;

            return true;
        }
        return false;
    }

    //Called when the holding Player drops this Item
    protected abstract void dropItem();
    public void drop()
    {
        if (holder != null && pickupDropTimer <= 0)
        {
            dropItem();

            //find this Item in the holding Player's Item list and remove it
            for (int i = 0; i < holder.items.Length; i++)
            {
                if (holder.items[i] == this)
                {
                    holder.items[i] = null;
                }
            }

            //drop this Item under the holder
            position = holder.position;

            //set this active in the game
            active = true;

            //Reset the pickupDropTimer, so it is not accidently dropped right away
            pickupDropTimer = pickupDropTime;

            //this no longer has a holder
            holder = null;
        }
    }

    //called each update that a Player is holding this Item
    protected abstract void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse);
    public void holding(bool use)
    {
        //if this is being called, holder should not be null
        if (holder == null)
        {
            throw new System.Exception("Item holder set to NULL in updateItem, call pickup() first");
        }
        //if holder is not active, drop this Item
        else if (!holder.active)
        {
            drop();
        }
        else
        {
            bool start = false;
            bool end = false;
            bool dbl = false;

            //find if this Item has just started to be used or is doubleUsed (doubleClick)
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

            //find if this Item has just stopped being used
            if (!use & previousUse)
            {
                end = true;
            }

            //update this Item's particular use behavior
            holdingItem(use, start, end, dbl);
        }

        //update status values
        if (pickupDropTimer > 0)
        {
            pickupDropTimer--;
        }
        previousUse = use;
        itemUpdated = true;
    }

    /// <summary>
    /// To be overriden if this Item has particular values that need to be loaded to describe
    /// this particular instance. The saved values are read from the given string. 
    /// </summary>
    /// <param name="values"></param>
    public virtual void loadValues(string values)
    {

    }

    /// <summary>
    /// To be overriden if this Item has particular values that need to be saved to describe
    /// this particular instance. The values to be saved will be return in a string.  
    /// </summary>
    /// <param name="values"></param>
    public virtual string getValues()
    {
        return "";
    }
}
