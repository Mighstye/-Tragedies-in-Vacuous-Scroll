using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("YaotomeMisaki");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
