using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ElementAddElement : Element
{
	List<Element> elementQueue;
	Element ele;
	
	public ElementAddElement(List<Element> elementQueue, Element ele) 
	{ 
		this.elementQueue = elementQueue;
		this.ele = ele;
	}
	
	public override void onActive()
	{
		Element.addElement(elementQueue, ele);
		finished = true;
	}
}

public class SegmentDisruptQueue : Element
{
	List<Element> elementQueue;
	Element ele;
	
	public SegmentDisruptQueue(List<Element> elementQueue, Element ele) 
	{ 
		this.elementQueue = elementQueue;
		this.ele = ele;
	}
	
	public override void onActive()
	{
		Element.disruptQueue(elementQueue, ele);
		finished = true;
	}
}

public class ElementNoop : Element
{
	int life = 0;
	
	public ElementNoop(int stunTicks)
	{ 
		life = stunTicks; 
	}
	
	public override void update()
	{
		life --;
		if(life <= 0)
			finished = true;
	}
}

// PLAYER MOVEMENT -----------------------------------





public class ElementMoveOverTime : Element
{
	int total_life = 0;
	int life = 0;
	GameObject my_object;
	Vector3 destination;
	Vector3 initial_position;
	bool local;
	
	public ElementMoveOverTime(int stunTicks, Vector3 dest, Vector3 initial, GameObject my_object, bool local)
	{ 
		this.total_life = stunTicks;
		this.my_object = my_object;
		this.destination = dest;
		this.initial_position = initial;
		this.local = local;
	}
	
	public override void update()
	{
		life ++;
		float ratio = (float)life / total_life;
		
		if(local)
			my_object.transform.localPosition = Vector3.Lerp(initial_position, destination, ratio);
		else
			my_object.transform.position = Vector3.Lerp(initial_position, destination, ratio);
		
		if(ratio >= 1.0f)
			finished = true;
	}
}

public class ElementWarpObject : Element
{
	GameObject my_object;
	Vector3 destination;
	Quaternion direction;
	bool local;
	
	public ElementWarpObject(Vector3 dest, Vector3 dir, GameObject my_object, bool local)
	{ 
		this.my_object = my_object;
		this.destination = dest;
		Quaternion q = new Quaternion();
		q.eulerAngles = dir;
		direction = q;
		this.local = local;
	}
	
	public override void onActive()
	{
		if(local)
			my_object.transform.localPosition = destination;
		else
			my_object.transform.position = destination;
		my_object.transform.rotation = direction;
		finished = true;
	}
}

/*public class ElementLoadScene : Element
{
	string scene_name;
	
	public ElementLoadScene(string scene_name)
	{ 
		this.scene_name = scene_name;
	}
	
	public override void onActive()
	{
		Application.LoadLevel(scene_name);
	}
}*/

public class ElementSetTextMesh : Element
{
	TextMesh my_mesh;
	string my_message;
	
	public ElementSetTextMesh(TextMesh t, string s)
	{ 
		this.my_mesh = t;
		this.my_message = s;
	}
	
	public override void onActive()
	{
		my_mesh.text = my_message;
		finished = true;
	}
}

public class ElementOnOffCamera : Element
{
	Camera cam;
	bool on;
	
	public ElementOnOffCamera(Camera cam, bool on)
	{ 
		this.cam = cam;
		this.on = on;
	}
	
	public override void onActive ()
	{
		cam.enabled = on;
		finished = true;
	}
}

public class ElementInstantiateObject : Element
{
	GameObject prefab;
	Vector3 pos;
	
	public ElementInstantiateObject(GameObject prefab, Vector3 position)
	{ 
		this.prefab = prefab;
		this.pos = position;
	}
	
	public override void onActive ()
	{
		MonoBehaviour.Instantiate(prefab, pos, Quaternion.identity);
		finished = true;
	}
}