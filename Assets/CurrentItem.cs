using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItem : MonoBehaviour {

    [SerializeField] private OrganizeBox.ItemType type;
    private int itemsLeft;
    private List<OrganizeBox> targets;
    private Dictionary<Vector2, OrganizeBox> boxLocationMap;
    private OrganizeBox target;
    private Vector2 size;
    private Vector3 originalPosition;
    private bool inMotion;
    [SerializeField] private int SPEED;

    // Doing this because C# is dumb 
    int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    // Use this for initialization
    void Start() {
        itemsLeft = 20;
        size = new Vector2(3, 2);
        targets = new List<OrganizeBox>();
        boxLocationMap = new Dictionary<Vector2, OrganizeBox>();
        GameObject[] targetsTest; 
        targetsTest = GameObject.FindGameObjectsWithTag("Box");
        foreach(GameObject gameObject in targetsTest){
            targets.Add(gameObject.GetComponent<OrganizeBox>());
        }
        foreach(OrganizeBox box in targets)
        {
            boxLocationMap.Add(box.Position, box);
        }
        target = boxLocationMap[new Vector2(0, 0)];
        originalPosition = gameObject.GetComponent<Transform>().position;
    }
	
	// Update is called once per frame
	void Update () {
        if (inMotion)
        {
            Vector3 movePos = Vector3.MoveTowards(GetComponent<Transform>().position, target.GetComponent<Transform>().position, SPEED * Time.deltaTime);
            GetComponent<Transform>().position = movePos;
            return;
        }
        Vector2 posChange = new Vector2();
        if (Input.GetKeyDown(KeyCode.A))
        {
            posChange.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            posChange.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            posChange.y += 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            posChange.y -= 1;
        }
        Vector2 newPos;
        newPos.x = Mod((int)(target.Position.x + posChange.x), (int)size.x);
        newPos.y = Mod((int)(target.Position.y + posChange.y), (int)size.y);
        Debug.Log(newPos.ToString());
        target.GetComponent<SpriteRenderer>().color = Color.red; 
        target = boxLocationMap[newPos];
        target.GetComponent<SpriteRenderer>().color = Color.blue;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(target.Type == this.type)
            {
                itemsLeft--;
                target.addItem();
            }
            else
            {
                itemsLeft++;
            }
            inMotion = true;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding");
        OrganizeBox collision = other.GetComponent<OrganizeBox>();
        if (collision.Position.Equals(target.Position))
        {
            Debug.Log("Colliding with correct object");
            type = (OrganizeBox.ItemType)Random.Range(0, 5);
            GetComponent<Transform>().position = originalPosition;
            inMotion = false;
        }
    }
}
