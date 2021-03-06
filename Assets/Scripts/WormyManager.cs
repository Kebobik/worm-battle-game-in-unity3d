using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormyManager : MonoBehaviour
{
    Wormy[] wormies;
    public Transform wormyCamera;

    public static WormyManager singleton;
    public static bool isGameOver;
    private int currentWormy;

    float currentTime = 0f;
    float startingTime = 0f;


    [SerializeField] Text countdownText;

    void Start()
    {
        isGameOver = false;
        currentTime = startingTime;
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;

        wormies = GameObject.FindObjectsOfType<Wormy>();
        wormyCamera = Camera.main.transform;

        for (int i = 0; i < wormies.Length; i++)
        {
            wormies[i].wormId = i;
        }
    }

    public void NextWorm()
    {
        StartCoroutine(NextWormCoroutine());
    }

    public IEnumerator NextWormCoroutine()
    {
        var nextWorm = currentWormy + 1;
        currentWormy = -1;

        yield return new WaitForSeconds(1);

        currentWormy = nextWorm;
        if (currentWormy >= wormies.Length)
        {
            
            currentWormy = 0;
        }

       wormyCamera.SetParent(wormies[currentWormy].transform);
        wormyCamera.localPosition = Vector3.zero + Vector3.back * 10;
    }


    public bool IsMyTurn(int i)
    {   
        return i == currentWormy;
    }

   
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {                     
            singleton.NextWorm();
            currentTime = 30;
        }
          if(Input.GetKeyDown(KeyCode.Q))
        {   
            currentTime = 30;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTime = 30;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentTime = 30;
        }
        if (Input.GetMouseButton(1))
        {
            currentTime = 30;
        }
        
    }
}
