using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element
{
	public bool finished = false;
	public virtual void onActive() {}
	public virtual void update() {}
	public virtual void onRemove() {}
    public virtual void onTrigger2D(Collider2D coll) {}

    public static void disruptQueue(List<Element> ele_queue, Element ele)
	{
		if(ele_queue.Count > 0)
		{
			ele_queue[0].onRemove();
			ele_queue[0] = ele;
		}
		else
			ele_queue.Add(ele);
		
		ele_queue[0].onActive();
	}

	public static void insertQueue(List<Element> ele_queue, Element ele)
	{
		if (ele_queue.Count > 0)
			ele_queue[0].onRemove();

		ele_queue.Insert (0, ele);
		
		ele_queue[0].onActive();
	}

	public static void removeQueue(List<Element> ele_queue)
	{	
		if (ele_queue.Count > 0)
			ele_queue [0].finished = true;
		else
			Debug.Log ("Tried to removeQueue when queue was empty");
	}
	
	
	public static void addElement(List<Element> ele_queue, Element ele)
	{
		ele_queue.Add(ele);
		if(ele_queue.Count == 1) // It was empty before.
			ele_queue[0].onActive();
	}
	
	public static void updateQueue(List<Element> ele_queue)
	{
		if(ele_queue.Count > 0)
		{
			ele_queue[0].update();
			
			if(ele_queue[0].finished)
			{
				ele_queue[0].onRemove();
				ele_queue.RemoveAt(0);
				if(ele_queue.Count > 0)
					ele_queue[0].onActive();
			}
		}
	}

    public static void trigger2DQueue(List<Element> ele_queue, Collider2D coll)
    {
        if (ele_queue.Count > 0) {
            ele_queue[0].onTrigger2D(coll);
        }
    }
}

