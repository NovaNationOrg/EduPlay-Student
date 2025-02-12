using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher 
{
   public static void SwitchScreen1()
    {
        SceneManager.LoadSceneAsync("SceneView2");
        
  
    }

    public static void SwitchScreen2()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

}
