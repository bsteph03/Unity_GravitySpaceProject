using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image backgroundImage;
	private Image joystickImage;

	private Vector3 inputVector;

	// Use this for initialization
	void Start () {
		backgroundImage = GetComponent<Image> ();
		joystickImage = transform.GetChild (0).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void OnDrag(PointerEventData ped) {
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (backgroundImage.rectTransform, ped.position, ped.pressEventCamera, out pos)) 
		{
			pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.y);

			Debug.Log ("OnDrag()_VirtualJoystick : " + pos);

			inputVector = new Vector3 (pos.x, 0, pos.y);//inputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1); // sii centre pas (0;0)
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//Debug.Log ("2 : " + pos);

			joystickImage.rectTransform.anchoredPosition = 
				new Vector3(inputVector.x * (backgroundImage.rectTransform.sizeDelta.x/4)
					, inputVector.z * (backgroundImage.rectTransform.sizeDelta.y/4));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped) {
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
	}
	public float Vertical()
	{
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis ("Vertical");
	}
}
//https://www.youtube.com/watch?v=uSnZuBhOA2U