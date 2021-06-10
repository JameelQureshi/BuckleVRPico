using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour {
	
	public TextMesh scoreText;
	public TextMesh finalScoreText;
	public static int score; 
	[SerializeField]
	float fadeSpeed;
	public Image scorePanel;
	public Image finalScorePanel;
	// Use this for initialization
	void Start () {
		finalScorePanel.gameObject.SetActive (false);
		StartCoroutine (FadeEffect.FadeIn(scoreText,fadeSpeed));
		StartCoroutine (FadeEffect.FadeIn(scorePanel,fadeSpeed));
	}
	
	// Update is called once per frame
	void Update () {
		if(scoreText!=null){
			
			scoreText.text = "" + score;
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log ("Restarting");
			SceneManager.LoadScene ("Menu");
		}

//		if(OVRInput.Button.Back){
//			Debug.Log ("Restarting");
//			SceneManager.LoadScene ("Menu");
//		}
	}
	public void ShowFinalScore(){
		finalScoreText.text=score+"!";
	}
}
