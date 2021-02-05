using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact/Person")]
public class Person : InteractChoice
{
    public Person(string keyword)
    {
        this.keyword = keyword;
    }
}

/*
	[SerializeField] private InteractableObject[] baseInteractableObjectsInRoom;
	
	private InteractableObject[] interactableObjectsInRoom;
	public InteractableObject[] InteractableObjectsInRoom
	{
		get { return interactableObjectsInRoom; }
	}

	[SerializeField] private InteractableObject[] basePeopleInRoom;
	
	private InteractableObject[] peopleInRoom;

	public InteractableObject[] PeopleInRoom
	{
		get { return peopleInRoom;  }
	}

	public void SetInteractableObjectsInRoom(InteractableObject[] objects)
	{
		interactableObjectsInRoom = objects;
	}

	public void SetPeopleInRoom(InteractableObject[] objects)
	{
		peopleInRoom = objects;
	}
	
	// using https://answers.unity.com/questions/1664323/how-are-you-resetting-your-scriptable-objects-betw.html
	
	// The use case of the following methods depends on the name lists to be in the same order as the object lists

	private void OnEnable()
	{
		interactableObjectsInRoom = baseInteractableObjectsInRoom;
		peopleInRoom = basePeopleInRoom;
	}
	
	public void OnAfterDeserialize() 
	{
	}
*/