using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	public Canvas MainCanvas;

	void Awake(){
		MainCanvas.enabled = true;
	}
	public void PlayClick(){
		
		SceneManager.LoadScene ("Orchestral");
	}
}
