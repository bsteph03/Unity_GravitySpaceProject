using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class stepsGame : MonoBehaviour {

	[SerializeField]handlerSpaceShip handlerSpaceShip;

	[SerializeField] GameObject MeteoreGroup;
	[SerializeField] GameObject LightningGroup;
	[SerializeField] GameObject FireAirGroup;

	[SerializeField] GameObject moonObject;
	[SerializeField] GameObject moonLight;
	[SerializeField] GameObject moonHalo;
	[SerializeField] GameObject explosion;

	[SerializeField] GameObject plexusGroup;

	[SerializeField] AudioSource playSound;

	[SerializeField]GameObject _Joystick;
	[SerializeField]GameObject _button;
	[SerializeField]GameObject _lostText;

	[SerializeField]shaderEffectCamera _shaderEffectCamera;

	private bool shield = false;

	public void exitGame()
	{
		handlerSpaceShip.canMove = false;
		_lostText.SetActive (true);
		//Application.Quit ();You are Lost in Space !
	}
	public void FinishGame()
	{
		handlerSpaceShip.canMove = false;
		_lostText.GetComponentInChildren<Text>().text = "You made it !";
		_lostText.SetActive (true);
		//Application.Quit ();
	}

	public void setMeteoreLevel(bool active)
	{
		MeteoreGroup.SetActive (active);
	}

	public void setStormLevel(bool active)
	{
		LightningGroup.SetActive (active);
		FireAirGroup.SetActive (active);
	}

	public void setPlanetLevel(bool active)
	{
		if (active) {
			LightningGroup.SetActive (false);
			FireAirGroup.SetActive (false);
			Light moonObjectLight = moonObject.GetComponent<Light> ();
			moonObjectLight.color = Color.red;
			Light moonHaloLight = moonHalo.GetComponent<Light> ();
			moonHaloLight.color = Color.red;
			moonHaloLight.intensity = 0.35f;
			moonHalo.SetActive (true);
			Light moonLightDirection = moonLight.GetComponent<Light> ();
			moonLightDirection.color = Color.red;
			explosion.SetActive (true);
			moonLight.SetActive (true);
			moonHalo.SetActive (true);
		} else {
			moonHalo.SetActive (false);
			moonLight.SetActive (false);
		}
	}

	public void setVortex(bool active)
	{
		plexusGroup.SetActive (active);
	}

	public void setJoystick(bool active)
	{
		_Joystick.SetActive (active);
	}

	public void setButton(bool active, string text)
	{
		if (active) {
			_button.SetActive (active);
			_button.GetComponentInChildren<Text>().text = text;
			_button.name = text;
			//Button btn = (Button) _button;
			//btn.onClick.AddListener(Button_Clicked());

			//Text t = _button.GetComponentInChildren<Text> ();
			//t.text = "hello";
		} else {
			_button.SetActive (active);
		}
	}

	public void Button_Clicked()
	{
		string name = EventSystem.current.currentSelectedGameObject.name;
		if (name.Equals ("Bouclier")) {
			shield = true;
			setButton (false,null);
		}
		if (name.Equals ("Propulseur")) {
			updateShip (false, 0, 50);
		}
	}

	public IEnumerator waitForShield(float time)
	{
		yield return new WaitForSeconds (time);
		if (!shield) {
			exitGame ();
		}
	}

	public void updateShip(bool gravity, int gravityValue, int speed)
	{
		if (gravity) {
			handlerSpaceShip.isGravity = true;
			handlerSpaceShip.updateGravity(gravityValue);
		} else {
			handlerSpaceShip.isGravity = false;
			handlerSpaceShip.updateGravity(0);
		}
		handlerSpaceShip.updateSpeed (speed);
	}

	public void playDialog(string name)
	{
		string dialog = "Dialog/" + name;//"Call_of_Duty_Black_Ops_III_OST_-_I_Live_Orchestral"
		AudioClip clip = Resources.Load<AudioClip>(dialog);
		playSound.clip = clip;
		playSound.loop = false;
		playSound.Play ();
	}

	public void setShaderEffect(bool active)
	{
		_shaderEffectCamera.enabled = active;
		//otherObject.GetComponent<NameOfScript>().enabled = false;
	}
}