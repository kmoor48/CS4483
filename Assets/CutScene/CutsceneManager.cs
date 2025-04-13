//using UnityEngine;
//using UnityEngine.Video;
//using UnityEngine.SceneManagement;

//public class CutsceneManager : MonoBehaviour
//{
//    public VideoPlayer videoPlayer;
//    public string nextSceneName = "Kerem-Level1"; 

//    void Start()
//    {
//        videoPlayer.loopPointReached += OnVideoEnd; 
//        videoPlayer.Play();
//    }

//    void OnVideoEnd(VideoPlayer vp)
//    {
//        SceneManager.LoadScene(nextSceneName); 
//    }
//}

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No next scene found in build settings!");
        }
    }
}


