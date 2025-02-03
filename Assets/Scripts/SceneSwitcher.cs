using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   public void switchScreen1()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
        SceneManager.LoadSceneAsync("SceneView2");
        
  
    }

    public void switchScreen2()
    {
        SceneManager.UnloadSceneAsync("SceneView2");
        SceneManager.LoadSceneAsync("SampleScene");
    }

}
