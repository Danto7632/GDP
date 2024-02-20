using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMain: MonoBehaviour {
    public string sceneNameToLoad = "StartMap";

    public void LoadSceneOnClick() {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
