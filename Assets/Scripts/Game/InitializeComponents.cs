using UnityEngine;
using System.Collections;

// Class to initialize anything that is not attached to a MonoBehavior, such as other systems.
public class InitializeComponents : MonoBehaviour
{
	void Start ()
	{
		InputManagement.init();
	}
}
