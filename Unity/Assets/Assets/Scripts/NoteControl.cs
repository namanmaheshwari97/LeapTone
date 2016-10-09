using UnityEngine;
using System.Collections;
using Leap;


public class NoteControl : MonoBehaviour {

//	[RequireComponent(typeof(AudioSource))]

	Controller controller;
	private int ch = 0;
	private HandModel m_HandModel;
	private Vector3 palmnormal;
	public float startingPitch = 4;
	private float change;
	public int timeToDecrease = 5;
	AudioSource audio;
	private bool indxextend;


	// Use this for initialization
	void Start () {
		
		var audio1 = GameObject.FindWithTag("audio1");
		controller = new Controller ();
		m_HandModel = transform.GetComponent<HandModel> ();
		controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", 200.0f);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", 750f);
		controller.Config.Save ();


		//Audio starts here...

		audio = audio1.GetComponent<AudioSource>();
		startingPitch = audio.pitch ;
		change = audio.pitch;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Frame frame = controller.Frame ();
		GestureList gesturelist = frame.Gestures ();
		palmnormal = m_HandModel.GetPalmNormal ();

		for (int i = 0; i < gesturelist.Count; i++) {
			Gesture gesture = gesturelist [i];
			change = 0;
			if (m_HandModel.GetLeapHand ().IsRight) {
				if (indxextend = m_HandModel.fingers [1].GetLeapFinger ().IsExtended) {
					if (palmnormal.y < 0) {				//when palmnormal.y > 0, it means the palm is facing up.
						Debug.Log ("The hand is facing down");
						if (gesture.Type == Gesture.GestureType.TYPESWIPE) {
							SwipeGesture Swipe = new SwipeGesture (gesture);
							Vector swipeDirection = Swipe.Direction;
							if (swipeDirection.y < 0) {
								Debug.Log ("Tempo Down");
								change += 2f;
								ch = 1;
								if (audio.pitch > 0)
									audio.pitch -= Time.deltaTime * change / timeToDecrease;
								else
									Debug.Log ("The pitch is below zero. Exitting application.");
							}
						}
					}
					else if (palmnormal.y > 0) {				// when palmnormal.y < 0, it means the palm is facing down.
						Debug.Log("The hand is facing up");
						if (gesture.Type == Gesture.GestureType.TYPESWIPE) {
							SwipeGesture Swipe = new SwipeGesture (gesture);
							Vector swipeDirection = Swipe.Direction;
							if (swipeDirection.y > 0) {
								Debug.Log ("Tempo Up");
								change -= 2f;
								ch = 0;
								if (audio.pitch > 0)
									audio.pitch -= Time.deltaTime * change / timeToDecrease;
								else
									Debug.Log ("The pitch is below zero. Exitting application.");
							}
						}		
					}
				}
			}
		}
	}
}