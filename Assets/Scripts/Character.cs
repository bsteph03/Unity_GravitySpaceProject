using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	[SerializeField]GameObject _Joystick;
	[SerializeField]GameObject _monitorText;
	[SerializeField]GameObject _buttonContinue;
	[SerializeField]GameObject _persoCam;
	[SerializeField]GameObject _mainCam;
	[SerializeField]GameObject _persoPanel;
	[SerializeField]GameObject _nameLabel;
	[SerializeField]GameObject _inputField;
	[SerializeField]GameObject _perso1;
	[SerializeField]GameObject _perso2;
	[SerializeField]GameObject _vsLabel;

	[SerializeField]handlerSpaceShip handlerSpaceShip;

	private string nameCharacter = "";

	public void choose()
	{
		string name = EventSystem.current.currentSelectedGameObject.name;
		if (name.Equals ("Perso1")) {
			_perso2.SetActive (false);
			_vsLabel.SetActive (false);
			_buttonContinue.SetActive (true);
		} else {
			_perso1.SetActive (false);
			_vsLabel.SetActive (false);
			_buttonContinue.SetActive (true);
		}
	}

	public void Continue()
	{
			_Joystick.SetActive (true);
			_monitorText.SetActive (true);
			_buttonContinue.SetActive (false);
		    _persoPanel.SetActive (false);
			_nameLabel.SetActive (false);
			_inputField.SetActive (false);
			_perso1.SetActive (false);
			_perso2.SetActive (false);
			this.nameCharacter = _inputField.GetComponentInChildren<Text> ().text;
			_persoCam.SetActive (false);
			_mainCam.SetActive (true);
			handlerSpaceShip.canMove = true;
	}
}