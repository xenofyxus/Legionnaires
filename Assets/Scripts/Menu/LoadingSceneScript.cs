using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour {

	private bool loadScene = false;
	public GameObject loadingBar;

	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loadingText;
	private int textCount = 0;

	void Update() {

		if (loadScene == false) {
			loadScene = true;
			loadingText.text = "Regenerating King";
			StartCoroutine(LoadNewScene());

		}

		if (loadScene == true) {
			loadingBar.GetComponent<Image> ().fillAmount += Time.deltaTime / 10;	
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
		}

	}
	IEnumerator LoadNewScene() {
		yield return new WaitForSeconds(2);
		AsyncOperation async = Application.LoadLevelAsync(scene);

		while (!async.isDone) {
			textCount++;
			if (textCount < 10) {
				loadingText.text = "Respawning Minions";
			} else {
				loadingText.text = "Starting Game";
			}
			loadingBar.GetComponent<Image> ().fillAmount += Time.deltaTime;
			yield return null;
		}

	}
}
