using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteManager : MonoBehaviour
{

    #region singleton
    public static MinuteManager instance = null;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
		else 
		{
			instance = this;
			instance.InitiativeList = new Queue<GameObject>();
			instance.EnemyIL = new Queue<GameObject>();
			instance.Initiative = new Dictionary<GameObject, EventDic>();
		}
    }
    #endregion

    private bool ticking = false;
    private float Timer = 0f;
    //private float EMTimer = 0f;


    

    [SerializeField, Range(0.5f, 15f)] private float tickDelay = 3f;
    //[SerializeField] private KeyCode tickNow = KeyCode.Space;

    public delegate void Tick();
    public static event Tick tick;
    public static event Tick untick;

    

    //public System.Delegate[] getInvocationList()
    //{
    //    if (tick != null) return tick.GetInvocationList();
    //    else return new System.Delegate[0];
    //}

    // Used for getting the list in the TickManager UI
    
    public float getTickDelay() { return tickDelay;  }
    public void setTickDelay(float tDelay) { tickDelay = tDelay; }
    public void StartTicking(float tDelay, TickMode tMode = TickMode.Chaos) { ticking = true; tickDelay = tDelay; tickMode = tMode; }
    public void StartTicking() { ticking = true; }

    public void tickNow()
    {
        if (ticking)
        {
            Timer -= tickDelay;
            if (Timer < 0) Timer = 0;
            DoTick();
        }
    }
    public void backTick()
    {
        if (ticking)
        {
            Timer -= tickDelay;
            if (Timer < 0) Timer = 0;
            UnDoTick();
        }
    }

    private void UnDoTick()
    {
        //Debug.Log("Tick Tock");
        if (untick != null)
            untick();
        
    }
    private void DoTick()
    {
        //Debug.Log("Tick Tock");
        roundTracker++;
        if (tick != null)
            tick();
        
    }

    // Use this for initialization
    void Start()
    {
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ticking)
        {
            Timer += Time.deltaTime;
            if (Timer >= tickDelay && tickDelay != 0f)
            {
                while (Timer >= tickDelay)
                    Timer -= tickDelay;

                DoTick();
            }
            //if (Input.GetKeyDown(tickNow))
            //{
            //    Timer -= tickDelay;
            //    if (Timer < 0) Timer = 0;
            //    if (tick != null) tick();
            //}
        }
    }
}
