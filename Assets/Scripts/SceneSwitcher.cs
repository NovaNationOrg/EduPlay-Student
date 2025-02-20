using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher 
{
   public static void SwitchGameScene(string sceneName) { SceneManager.LoadSceneAsync(sceneName); }
   public static void SwitchScanScene() { SceneManager.LoadSceneAsync("EduPlayStudent"); }
}
