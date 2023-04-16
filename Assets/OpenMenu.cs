using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;



public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer vid;
    void Start()
    {

        vid = GetComponent<VideoPlayer>();
        vid.Play();
        vid.loopPointReached += LoadScene;

    }

    // Update is called once per frame
    void Update()
    {
        //while (vid.isPlaying)
        //{

        //}
        
    }
    void LoadScene(VideoPlayer vid)
    {
        SceneManager.LoadScene("StartMenu");
    }
}


