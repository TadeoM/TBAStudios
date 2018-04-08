using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoarderHand : MonoBehaviour {

    private int itemsToSteal = 10;
    private OrganizeBox target;
    private CurrentItem currItem;
    private Vector3 placehold1;
    private Vector3 placehold2;
    private Vector2 movingBackTarget;
    private GameTimer gameTimer;
    private Timer holdTimer;
    private List<int> handTimes;
    private Dictionary<int, bool> handTimesDone;
    private bool handMoving;
    private bool handMovingBack;
    [SerializeField] private int SPEED;
    [SerializeField] private GameObject currItemObject;

    public OrganizeBox Target
    {
        get { return target; }
        set { target = value; }
    }

    public bool HandMoving
    {
        get { return handMoving; }
        set { handMoving = value; }
    }

    // Use this for initialization
    void Start() {
        handMoving = false;
        handTimes = new List<int>();
        handTimesDone = new Dictionary<int, bool>();
        placehold1 = GameObject.FindGameObjectWithTag("Placeholder1").GetComponent<Transform>().position;
        placehold2 = GameObject.FindGameObjectWithTag("Placeholder2").GetComponent<Transform>().position;
        gameTimer = GameObject.FindGameObjectWithTag("HoarderTime").GetComponent<GameTimer>();
        currItem = currItemObject.GetComponent<CurrentItem>();
        holdTimer = new Timer();
        holdTimer.SetTimer(.5f);
        holdTimer.OnTimeUp += HandleTimeUp;
        for (int i = 0; i < itemsToSteal; i++)
        {
            int random = Random.Range(2, gameTimer.GameTime-5);
            while (handTimes.Contains(random) || handTimes.Contains(random-1) || handTimes.Contains(random+1))
            {
                random = Random.Range(2, gameTimer.GameTime-5);
            }
            Debug.Log(random);
            handTimes.Add(random);
            handTimesDone[random] = false;
        }
    }

    // Update is called once per frame
    void Update() {
        holdTimer.Update();
        if (target == null)
        {
            return;
        }
        Vector3 oldPosition = GetComponent<Transform>().position;
        oldPosition.x = target.GetComponent<Transform>().position.x;
        if (target.Position.y == 0)
        {
            oldPosition.y = placehold1.y;
        }
        else
        {
            oldPosition.y = placehold2.y;
        }
        if (!handMoving)
        {
            if(target.Position.y == 0)
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipY = false;
            }
            GetComponent<Transform>().position = oldPosition;
        }
        if (handMovingBack)
        {
            Vector3 moveTo = new Vector2();
            moveTo.y = oldPosition.y;
            moveTo.x = target.GetComponent<Transform>().position.x;
            movingBackTarget = new Vector2();
            movingBackTarget.x = moveTo.x;
            movingBackTarget.y = moveTo.y;
            Vector3 movePos = Vector3.MoveTowards(GetComponent<Transform>().position, moveTo, SPEED * Time.deltaTime);
            GetComponent<Transform>().position = movePos;
            if (GetComponent<SpriteRenderer>().bounds.Contains(movingBackTarget))
            {
                Debug.Log("Hand done moving back");
                handMoving = false;
                handMovingBack = false;
                handTimesDone[gameTimer.TimeLeft] = true;
            }
        }
        else if ((handTimes.Contains(gameTimer.TimeLeft)) || handMoving)
        {
            if(handTimesDone.ContainsKey(gameTimer.TimeLeft) && !handTimesDone[gameTimer.TimeLeft])
            {
                Vector3 movePos = Vector3.MoveTowards(GetComponent<Transform>().position, target.GetComponent<Transform>().position, SPEED * Time.deltaTime);
                GetComponent<Transform>().position = movePos;
                handMoving = true;
            }
        }
    }

    void HandleTimeUp()
    {
        handMovingBack = true;
        holdTimer.Reset();
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
            Debug.Log("Hand colliding with target");
            holdTimer.StartTimer();
            currItem.addItem();
        }
        if (collision.gameObject.name == "HandPlacehold1" || collision.gameObject.name == "HandPlacehold2")
        {

        }
    }
}
