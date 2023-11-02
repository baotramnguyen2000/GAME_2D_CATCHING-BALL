using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public List<GameObject> balls;
    public List<GameObject> booms;
    public GameObject presents;
    public AudioClip btnSound;
    public float spawnTimeBall = 2;
    public float spawnTimeBoom = 2.6f;
    public float spawnTimePresent = 3.25f;
    public int level2 = 20;
    public int level3 = 30;


    UIController ui;
    AudioSource audioSource;
    PlayerController player;
    FadeScene fadeScene;
    //private
    float m_spawnTimeBall;
    float m_spawnTimeBoom;
    float m_spawnTimePresent;
    int score;
    bool isGameover;

    void Start()
    {
        ui = FindObjectOfType<UIController>();
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
        fadeScene = FindObjectOfType<FadeScene>();

        isGameover = false;
        m_spawnTimeBall = 0;
        m_spawnTimeBoom = 3;
        m_spawnTimePresent = 3;
        score = 0;

        Time.timeScale = 1;
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("best_score", 20);

    }

    // Update is called once per frame
    void Update()
    {
        //Ham tao ra Ball moi 1s
        m_spawnTimeBall -= Time.deltaTime;
        if (m_spawnTimeBall <= 0 && !isGameover)
        {
            int randomIndexBall = Random.Range(0, 5);
            SpawnObject(balls[randomIndexBall]);
     
            m_spawnTimeBall = spawnTimeBall;
        }
        //Ham tao ra Boom moi 2s
        m_spawnTimeBoom -= Time.deltaTime;
        if (m_spawnTimeBoom <= 0 && !isGameover)
        {
            int randomIndexBoom = Random.Range(0, 2);
            SpawnObject(booms[randomIndexBoom]);

            m_spawnTimeBoom = spawnTimeBoom;
        }
        //Ham tao ra present moi 3.25s kem voi ti le xuat hien moi 2s la 30%
        m_spawnTimePresent -= Time.deltaTime;
        if(m_spawnTimePresent <=0 && !isGameover)
        {
            int randomPercentage = Random.Range(0, 100);
            if(randomPercentage<= 30)
            {
                SpawnObject(presents);

            }
            m_spawnTimePresent = spawnTimePresent;
        }
        //Check gameover
        if (isGameover)
        {
            ui.setNotify("", Color.white);
            audioSource.Pause();

            Time.timeScale = 0;
            
            Debug.Log("Game Over");
            


            ui.isShowPanelGV(true);

            int best_score = PlayerPrefs.GetInt("best_score") | 0;

            if (score > best_score)
            {
                PlayerPrefs.SetInt("best_score", score);
                ui.isShowTextNew(true);
            }
            ui.setBestScoreTxt(best_score);
            ui.setYourScoreTxt(score);
        }
        if (score >= level2)
        {
            spawnTimeBall = 1.2f;
            spawnTimeBoom = 1.7f;
            spawnTimePresent = 2.75f;
        }
        if (score >= level3)
        {
            spawnTimeBall = 0.6f;
            spawnTimeBoom = 1.35f;
            spawnTimePresent = 2.2f;
        }
    }

    void SpawnObject(GameObject obj)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-9.8f, 10), 7.5f);

        if (obj)
        {
            Instantiate(obj, spawnPos, Quaternion.identity);
        }
    }
    public void increateScore(int count)
    {
        score+=count;
        ui.setScoreTxt(score);
    }

    public void setGameOver(bool status)
    {
        isGameover = status;
    }
    
    public void rePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void exitGame()
    {
        fadeScene.SetTriggerFadeOutGame2Menu();
    }

    public void presentChangeSpeedPlayer(int speed)
    {
        if (player.moveSpeed > 3)
        {
            player.moveSpeed +=speed ;
        }
    }
    public void presentChangeSizePlayer(SpriteRenderer spriteRender, BoxCollider col,Vector3 sizeBoxCollider,Sprite img)
    {
        spriteRender.sprite = img;
        col.size = sizeBoxCollider;
    }
    public void presentResetScore()
    {
        score = 0;
        ui.setScoreTxt(score);
    }
}
