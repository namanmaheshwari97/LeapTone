using UnityEngine;
using System.Collections;
using Leap;

public class Note : FingerModel {
	Controller controller;	// Leap Controller  initiation.

	void InitFinger () {
		controller = new Controller ();
		Enabling ();
		Debug.Log("Start ends here...");
	}

	public void Enabling(){

		// Enables the Given gestures..
		controller.EnableGesture (Gesture.GestureType.TYPE_CIRCLE);
		controller.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP);
		controller.EnableGesture (Gesture.GestureType.TYPE_SCREEN_TAP);
		controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);

		controller.Config.SetFloat ("Gesture.Circle.MinRadius", 10.0f);
		controller.Config.SetFloat ("Gesture.Circle.MinArc", .5f);
		controller.Config.Save ();

	}

	public override void UpdateFinger(){
	Frame frame = controller.Frame ();
	GestureList gesturelist = frame.Gestures ();
	for (int i = 0; i < gesturelist.Count; i++) {
		if(GetLeapFinger().IsExtended)
				Debug.Log ("Hand is Right!");
		}
	}
}


public class ControlNotePlay : Note{
	void Start(){
		Debug.Log ("Start here");
		InitFinger ();
	}

	void Update(){
		UpdateFinger ();
	}
}