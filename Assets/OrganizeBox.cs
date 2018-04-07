using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeBox : MonoBehaviour {

    private ItemType type;
    private int numItems;
    [SerializeField] private Vector2 position; 

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Vector2 Position
    {
        get { return position; }
    }

   public enum ItemType {
        TRASH, RECYCLING, ELECTRONICS, PAPER, CLOTHES, SELL
    };

    public OrganizeBox(ItemType type, Vector2 position)
    {
        this.type = type;
        this.position = position; 
    }

	// Use this for initialization
	void Start () {
        numItems = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addItem()
    {
        numItems++;
    }

    public void removeItem()
    {
        numItems--;
    }
}
