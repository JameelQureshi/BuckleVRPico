using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuRay : MonoBehaviour {
	[SerializeField] private Transform m_End;     
	[SerializeField] private float m_Damping = 0.5f;  
	private const float k_DampingCoef = -20f;   
	[SerializeField] private LineRenderer m_Flare;    
	[SerializeField] private float m_DefaultLineLength = 70f;   
	[SerializeField] MenuManager menu;
	[SerializeField] Material defaultLaser;
	[SerializeField] Material selectLaser;

	bool isCharacterLocked;
	private bool isDeafaultLaser;
	RaycastHit hitInfo;
	Ray ray;	

	/// <For Buckling>
	GameObject currentRider;
	/// </summary>
	// Use this for initialization
	void Start () {
		m_End.GetComponent<LineRenderer> ().enabled = false;
		isCharacterLocked = false;
		isDeafaultLaser = true;
		float lineLength = m_DefaultLineLength;
		m_Flare.SetPosition(0, m_End.position);
		m_Flare.SetPosition(1, m_End.position + m_End.forward * lineLength);

		menu = FindObjectOfType<MenuManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.S))
		{
			menu.StartGame();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			menu.Begin();
		}

		float lineLength = m_DefaultLineLength;
		m_End.GetComponent<LineRenderer>().SetPosition(0, m_End.position);
		m_End.GetComponent<LineRenderer>().SetPosition(1, m_End.position + m_End.forward * lineLength);

		m_Flare.SetPosition(0, m_End.position);
		m_Flare.SetPosition(1, m_End.position + m_End.forward * lineLength);
		

		ray = GetRay ();
		if (Physics.Raycast (ray, out hitInfo)) {

			if(hitInfo.collider.tag=="SelectBG" || hitInfo.collider.tag=="click1" || hitInfo.collider.tag=="click2" || hitInfo.collider.tag=="click3"){
				Debug.Log ("Testing");
				if (!isCharacterLocked) {	
					if (Input.GetMouseButton (0)) {
						CharacterselectedLaser ();
						isCharacterLocked = true;
					}
					if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
					{
						CharacterselectedLaser();
						isCharacterLocked = true;
					}
					if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(1, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
					{
						CharacterselectedLaser();
						isCharacterLocked = true;
					}


				}
				
				currentRider = hitInfo.transform.parent.gameObject.transform.parent.gameObject;

				if(Input.GetMouseButton(0))
				{
					if(hitInfo.collider.tag=="click1")
					{
						currentRider.GetComponent<Spawner> ().EnableClick1 ();
					}

					if(hitInfo.collider.tag=="click2" && currentRider.GetComponent<Spawner> ().CheackClcik1() )
					{
						currentRider.GetComponent<Spawner> ().EnableClick2 ();
					}

					if(hitInfo.collider.tag=="click3" && currentRider.GetComponent<Spawner> ().CheackClcik2() )
					{	
						currentRider.GetComponent<Spawner> ().EnableClick3 ();
						MakeLaserNoCharacterSelected ();
					}
				}
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					if (hitInfo.collider.tag == "click1")
					{
						currentRider.GetComponent<Spawner>().EnableClick1();
					}

					if (hitInfo.collider.tag == "click2" && currentRider.GetComponent<Spawner>().CheackClcik1())
					{
						currentRider.GetComponent<Spawner>().EnableClick2();
					}

					if (hitInfo.collider.tag == "click3" && currentRider.GetComponent<Spawner>().CheackClcik2())
					{
						currentRider.GetComponent<Spawner>().EnableClick3();
						MakeLaserNoCharacterSelected();
					}
				}
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(1, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					if (hitInfo.collider.tag == "click1")
					{
						currentRider.GetComponent<Spawner>().EnableClick1();
					}

					if (hitInfo.collider.tag == "click2" && currentRider.GetComponent<Spawner>().CheackClcik1())
					{
						currentRider.GetComponent<Spawner>().EnableClick2();
					}

					if (hitInfo.collider.tag == "click3" && currentRider.GetComponent<Spawner>().CheackClcik2())
					{
						currentRider.GetComponent<Spawner>().EnableClick3();
						MakeLaserNoCharacterSelected();
					}
				}


			}
			else
			{
				MakeLaserNoCharacterSelected ();
				
				
				if(isDeafaultLaser){
					isDeafaultLaser = false;
					setDefaultLaser ();
				}
				if(currentRider!=null){
					currentRider.GetComponent<Spawner> ().UnSelectAll();
				}
			}
			
			if (hitInfo.collider.tag == "Start") {
				

				if (!isDeafaultLaser) {
					selectedLaser ();
					isDeafaultLaser = true;
				}
				
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					menu.StartGame();
				}
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					menu.StartGame();
				}


			} else if (hitInfo.collider.tag == "Begin") {
				
				if (!isDeafaultLaser) {
					selectedLaser ();
					isDeafaultLaser = true;
				}
				
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					menu.Begin();
				}
				if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(1, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
				{
					menu.Begin();
				}

			} else {
				setDefaultLaser ();
			}

		} else {
			if(currentRider!=null){
				currentRider.GetComponent<Spawner> ().UnSelectAll();
			}
			MakeLaserNoCharacterSelected ();
			currentRider = null;
			if(isDeafaultLaser){
				isDeafaultLaser = false;
				setDefaultLaser ();
			}

		}


	}
	public void MakeLaserNoCharacterSelected(){
		isCharacterLocked = false;
		m_Flare.enabled = true;
		m_End.GetComponent<LineRenderer> ().enabled = false;
	}
	public Ray GetRay(){
		return new Ray (m_End.position,m_End.forward);
	}
	void setDefaultLaser(){
		Debug.Log ("Default Laser");
		m_Flare.material = defaultLaser;
		m_Flare.widthMultiplier=0.3f;
	}
	void selectedLaser(){
		Debug.Log ("Selected Laser");
		m_Flare.widthMultiplier=0.8f;
		m_Flare.material = selectLaser;
	}
	public void CharacterselectedLaser(){
		Debug.Log ("Character Selected Laser");
		m_Flare.enabled = false;
		m_End.GetComponent<LineRenderer> ().enabled = true;
	}

}
