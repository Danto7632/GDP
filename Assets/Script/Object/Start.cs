using UnityEngine;
using UnityEngine.SceneManagement;

public class Start: MonoBehaviour {
    public string sceneNameToLoad = "MainMap";

    public void LoadSceneOnClick() {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
