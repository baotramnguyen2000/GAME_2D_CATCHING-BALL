using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin = -7.7f, xMax = 7.7f;
    }

    public float moveSpeed = 15;
    public Boundary boundary;
    public AudioClip plusSound;
    public AudioClip soundLose;
    public AudioClip soundPresent;
    public Sprite playerSmall;
    public Sprite playerNormal;


    AudioSource audio;
    Rigidbody rb;
    BoxCollider boxCol;
    SpriteRenderer spriteRender;
    GameController gc;
    UIController ui;
    float xDir;
    bool isSmall;
    bool isReverse;
    float timeCounterSmall = 3f;
    float timeCounterReverse = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = FindObjectOfType<GameController>();
        ui = FindObjectOfType<UIController>();
        audio = GetComponent<AudioSource>();
        boxCol = GetComponent<BoxCollider>();
        spriteRender = GetComponent<SpriteRenderer>();
        isSmall = false;
        isReverse = false;

        
    }
    private void Update()
    {
        //ktr player co bi thu nho ko
        if (isSmall)
        {
            //Cho dem nguoc thoi gian ke tu luc bi thu nho (timecounter = 3 | dem nguoc 3 giay)
            timeCounterSmall -= Time.deltaTime;
            
            if (timeCounterSmall < 0)
            {
                boundary.xMin = -7.7f;
                boundary.xMax = 7.7f;
                Vector3 sizeNormal = new Vector3(7.1f, 1.18f);
                gc.presentChangeSizePlayer(spriteRender, boxCol, sizeNormal, playerNormal);
                Debug.Log("Finish  Counter Change Size");
                timeCounterSmall = 3f;
                isSmall = false;
            }
        }
        if (isReverse)
        {
            timeCounterReverse -= Time.deltaTime;

            if (timeCounterReverse < 0)
            {
                Debug.Log("Finish  Counter Reverse");
                timeCounterReverse = 3f;
                isReverse = false;
            }
        }
    }
    // FixedUpdate
    void FixedUpdate()
    {
        if (isReverse)
        {
            movePlayer(-1);
        }
        else if (!isReverse)
        {
            movePlayer(1);
        }
    }
    void movePlayer(int valueReverse)
    {
        xDir = Input.GetAxis("Horizontal") * valueReverse;
        
        Vector3 movement = new Vector3(xDir, 0f,0f);

        rb.velocity = movement * moveSpeed;


        //Xet gioi han cho player
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            -4f,
            0f);
        //transform.position += movement * moveSpeed *Time.deltaTime;
    }
    //List ball 
    public class Item
    {
        public int score { get; set; }
        public string tag { get; set; }
    }
    List<Item> listBall = new List<Item>(){
            new Item() { score=1,tag="ball1"},
            new Item() { score=2,tag="ball2"},
            new Item() { score=3,tag="ball3"},
            new Item() { score=4,tag="ball4"},
            new Item() { score=5,tag="ball5"},
        };
    //trigger
    private void OnTriggerEnter(Collider other)
    {
        //Player cham ball
        foreach(Item ball in listBall)
        {
            if (other.gameObject.CompareTag(ball.tag))
            {
                Destroy(other.gameObject);
                gc.increateScore(ball.score);
                audio.PlayOneShot(plusSound);
            }
        }
        //Player cham Boom
        if (other.gameObject.CompareTag("Boom"))
        {
            Destroy(other.gameObject);
            audio.PlayOneShot(soundLose);
            gc.setGameOver(true);
        }

        //Player cham present
        if (other.gameObject.CompareTag("Present"))
        {
            audio.PlayOneShot(soundPresent,2f);

            Destroy(other.gameObject);

            //Random present
            int presentRandom = Random.Range(0, 5);
            //int presentRandom = 4;
            if (presentRandom == 0)
            {
                ui.setNotify("Slower Player", Color.red);
                ui.FadeOut();
                gc.presentChangeSpeedPlayer(-4);
                Debug.Log("Present Slow");
            }
            else if(presentRandom == 1)
            {
                ui.setNotify("Faster Player", Color.green);
                ui.FadeOut();
                gc.presentChangeSpeedPlayer(4);
                Debug.Log("Presnt Fast ");
            }
            else if(presentRandom == 2)
            {
                ui.setNotify("Player Small",Color.green);
                ui.FadeOut();
                isSmall = true;
                boundary.xMin = -9.3f;
                boundary.xMax = 9.35f;
                Vector3 sizeSmall = new Vector3(2.5f, 1.29f);
                gc.presentChangeSizePlayer(spriteRender,boxCol,sizeSmall,playerSmall);
               
                Debug.Log("Small Player");
            }
            else if(presentRandom == 3)
            {
                ui.setNotify("Reverse Move", Color.red);
                ui.FadeOut();
                isReverse = true;
                Debug.Log("Reverse move ");
            }
            else if(presentRandom == 4)
            {
                ui.setNotify("Reset score", Color.red);
                ui.FadeOut();
                gc.presentResetScore();
            }
        }
    }
}
