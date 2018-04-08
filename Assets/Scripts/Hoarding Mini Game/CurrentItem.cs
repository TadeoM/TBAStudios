using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour {

    [SerializeField] private Text timerText;
    [SerializeField] private Text itemText;
    [SerializeField] private OrganizeBox.ItemType type;
    [SerializeField] private int itemsLeft;
    private HoarderHand hoarder;
    private List<OrganizeBox> targets;
    private Dictionary<Vector2, OrganizeBox> boxLocationMap;
    private OrganizeBox target;
    private Vector2 size;
    private Vector3 originalPosition;
    private bool inMotion;
    private GameTimer gameTimer;
    private List<Sprite> recyclingSprites;
    private List<Sprite> clothingSprites;
    private List<Sprite> paperSprites;
    private List<Sprite> trashSprites;
    private List<Sprite> electronicsSprites;
    private HashSet<Sprite> alreadyUsed;
    private bool gameOver;
    [SerializeField] private int SPEED;
    [SerializeField] private GameObject spriteTree;
    [SerializeField] private GameObject boxOverlay;

    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }

    // Doing this because C# is dumb 
    int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    // Use this for initialization
    void Start() {

        size = new Vector2(3, 2);
        targets = new List<OrganizeBox>();
        recyclingSprites = new List<Sprite>();
        clothingSprites = new List<Sprite>();
        paperSprites = new List<Sprite>();
        trashSprites = new List<Sprite>();
        electronicsSprites = new List<Sprite>();
        alreadyUsed = new HashSet<Sprite>();
        foreach(Transform child in spriteTree.transform)
        {
            string category = child.name;
            Debug.Log(category);
            foreach(Transform sprite in child)
            {
                if(category == "Recycling")
                {
                    recyclingSprites.Add(sprite.GetComponent<SpriteRenderer>().sprite);
                }
                else if(category == "Electronics")
                {
                    electronicsSprites.Add(sprite.GetComponent<SpriteRenderer>().sprite);
                }
                else if(category == "Trash")
                {
                    trashSprites.Add(sprite.GetComponent<SpriteRenderer>().sprite);
                }
                else if(category == "Clothing")
                {
                    clothingSprites.Add(sprite.GetComponent<SpriteRenderer>().sprite);
                }
                else if(category == "Paper")
                {
                    paperSprites.Add(sprite.GetComponent<SpriteRenderer>().sprite);
                }
            }
        }
        boxLocationMap = new Dictionary<Vector2, OrganizeBox>();
        GameObject[] targetsTest;
        targetsTest = GameObject.FindGameObjectsWithTag("Box");
        hoarder = GameObject.FindGameObjectWithTag("Hand").GetComponent<HoarderHand>();
        gameTimer = GameObject.FindGameObjectWithTag("HoarderTime").GetComponent<GameTimer>();
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
    void Update()
    {
        timerText.text = "0:";
        if (gameTimer.TimeLeft < 10)
        {
            timerText.text += "0";
        }
        timerText.text += gameTimer.TimeLeft.ToString();
        itemText.text = itemsLeft.ToString();
        if (itemsLeft == 0 || gameTimer.TimeLeft == 0)
        {
            gameOver = true;
            /*
            if (itemsLeft < 5)
            {
                GameManager.Instance.CurrentNPCScript.ChangeMentalState((int)GameManager.Kindness.Best);
            }
            GameManager.Instance.CurrentNPCScript.ChangeMentalState((int)GameManager.Kindness.Good);
            */
            gameTimer.StopTimer();
        }
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
            posChange.y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            posChange.y += 1;
        }
        Vector2 newPos;
        newPos.y = Mod((int)(target.Position.y + posChange.y), (int)size.y);
        newPos.x = Mod((int)(target.Position.x + posChange.x), 3);
        if (newPos.x == 1 && newPos.y == 1)
        {
            if (posChange.x == -1)
            {
                newPos.x -= 1;
            }
            else if (posChange.x == 1)
            {
                newPos.x += 1;
            }
        }
        Debug.Log(newPos);
        target.GetComponent<SpriteRenderer>().color = Color.white;
        target = boxLocationMap[newPos];
        if (!hoarder.HandMoving)
        {
            hoarder.Target = target;
        }
        boxOverlay.GetComponent<Transform>().position = target.GetComponent<Transform>().position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (target.Type == this.type)
            {
                itemsLeft--;
                target.addItem();
            }
            inMotion = true;
        }

        if (GameOver)
        {
            LevelManager.Instance.LoadScene(Level.MainGame);
            GameManager.Instance.playedJoe = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding");
        OrganizeBox collision = other.GetComponent<OrganizeBox>();
        if(collision == null)
        {
            return;
        }
        if (collision.Position.Equals(target.Position))
        {
            Debug.Log("Colliding with correct object");
            GetComponent<Transform>().position = originalPosition;
            type = (OrganizeBox.ItemType)Random.Range(0, 5);
            Sprite newSprite;
            bool alreadyAdded = false;
            switch (type)
            {
                case OrganizeBox.ItemType.CLOTHES:
                    newSprite = clothingSprites[Random.Range(0, clothingSprites.Count)];
                    GetComponent<SpriteRenderer>().sprite = newSprite;
                    alreadyAdded = alreadyUsed.Add(newSprite);
                    break;
                case OrganizeBox.ItemType.ELECTRONICS:
                    newSprite = electronicsSprites[Random.Range(0, electronicsSprites.Count)];
                    GetComponent<SpriteRenderer>().sprite = newSprite;
                    alreadyAdded = alreadyUsed.Add(newSprite);
                    break;
                case OrganizeBox.ItemType.RECYCLING:
                    newSprite = recyclingSprites[Random.Range(0, recyclingSprites.Count)];
                    GetComponent<SpriteRenderer>().sprite = newSprite;
                    alreadyAdded = alreadyUsed.Add(newSprite);
                    break;
                case OrganizeBox.ItemType.PAPER:
                    newSprite = paperSprites[Random.Range(0, paperSprites.Count)];
                    GetComponent<SpriteRenderer>().sprite = newSprite;
                    alreadyAdded = alreadyUsed.Add(newSprite);
                    break;
                case OrganizeBox.ItemType.TRASH:
                    newSprite = trashSprites[Random.Range(0, trashSprites.Count)];
                    GetComponent<SpriteRenderer>().sprite = newSprite;
                    alreadyUsed.Add(newSprite);
                    break;
            }
            inMotion = false;
        }
    }

    public void removeItem()
    {
        itemsLeft--;
    }

    public void addItem()
    {
        itemsLeft++;
    }
}
