using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart: MonoBehaviour {
    public string sceneNameToLoad = "MainMap";

    public void LoadSceneOnClick() {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
