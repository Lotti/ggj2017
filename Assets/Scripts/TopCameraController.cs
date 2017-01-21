using UnityEngine;
using TouchScript;
using TouchScript.Gestures;

[RequireComponent(typeof(Camera))]
public class TopCameraController : MonoBehaviour {
	private Camera TopCamera;
	public ScreenTransformGesture OneFingerMoveGesture;
	private Rigidbody body;

	public LayerMask touchInputMask;
	public float PanSpeed = 0.1f;

	void Awake() {
		TopCamera = this.GetComponent<Camera> ();
		body = TopCamera.GetComponent<Rigidbody> ();
	}

	private float defaultZ;
	void Start () {
		defaultZ = TopCamera.transform.localPosition.z;
	}

	private void OnEnable() {
		if (TouchManager.Instance != null) {
			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
			OneFingerMoveGesture.Transformed += oneFingerTransformHandler;
		}
	}

	private void OnDisable() {
		if (TouchManager.Instance != null) {
			TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
			OneFingerMoveGesture.Transformed -= oneFingerTransformHandler;
		}
	}

	private void touchesBeganHandler (object sender, TouchEventArgs e) {
		if (e.Touches.Count == 1) {
			Ray ray = TopCamera.ScreenPointToRay (e.Touches [0].Position);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, touchInputMask)) {
				GameObject target = hit.transform.gameObject;
				target.SendMessage ("OnLight", hit.point, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private Vector3 moveTo;
	private bool needToMove = false;
	private void oneFingerTransformHandler(object sender, System.EventArgs e) {
		Vector3 v = TopCamera.transform.localPosition;
		Vector3 d = OneFingerMoveGesture.DeltaPosition * PanSpeed;

		moveTo = new Vector3 (v.x + d.x, v.y + d.y, defaultZ);
		Debug.Log ("move: v" + v +", d"+ d+ ", r"+moveTo);
		Debug.Log ("forward: " + transform.forward);
		needToMove = true;
		//body.MovePosition (moveTo);
		//transform.localPosition = moveTo;
	}

	void FixedUpdate() {
		if (needToMove) {
			needToMove = false;
			body.MovePosition (moveTo);
		}
	}
}