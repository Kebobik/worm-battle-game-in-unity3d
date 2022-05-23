using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    private void Start()
    {
        Time.timeScale = 1;
       
    }
   public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    
}
