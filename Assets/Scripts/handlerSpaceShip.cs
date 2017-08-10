using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handlerSpaceShip : MonoBehaviour {

	[SerializeField]GameObject _spaceShip;
	[SerializeField]GameObject _planet;

	[SerializeField]VirtualJoystick _Joystick;

	[SerializeField]public Text _monitorText;
	public int _health = 100;
	public int _fuel = 1000;
	public int _speed = 5; //Vitesse de déplacement
	public int _gravity = 0; //Gravité : -9.8f Terre

	public bool isGravity = false;
	bool stop = false;
	public bool canMove = true;

	// Use this for initialization
	void Start () {
		this.updateMonitorText ();
		this.canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (!isGravity) {
				// Move the object forward along its z axis 1 unit/second.
				this._spaceShip.transform.Translate (Vector3.forward * Time.deltaTime * _speed);

				//Move directional
				if (_Joystick.Horizontal () != 0 || _Joystick.Vertical () != 0) {
					this._spaceShip.transform.Translate (new Vector3 ((_Joystick.Horizontal () * Time.deltaTime * _speed), (_Joystick.Vertical () * Time.deltaTime * _speed), 0f));
					this.lossFuel (1);
				}
			} else {
				Vector3 posShip = this._spaceShip.transform.position;
				if (!stop) {
					if (posShip.x > -3) {
						this._spaceShip.transform.Translate (Vector3.left * Time.deltaTime * _speed);
					} else {
						this._spaceShip.transform.Translate (Vector3.right * Time.deltaTime * _speed);
					}
					if (posShip.y > 17) {
						this._spaceShip.transform.Translate (Vector3.down * Time.deltaTime * _speed);
					} else {
						this._spaceShip.transform.Translate (Vector3.up * Time.deltaTime * _speed);
					}
					if (posShip.z < 460) {
						this._spaceShip.transform.Translate (Vector3.forward * Time.deltaTime * _speed);
					} else {
						stop = true;
					}
				} else { //rotate
					Vector3 rotationMask = new Vector3 (0, 1, 0); //which axes to rotate around
					this._spaceShip.transform.RotateAround (_planet.transform.position,
						rotationMask, (float)this._speed * Time.deltaTime);
					if (this._spaceShip.transform.localEulerAngles.y > -90.0f) {
						this._spaceShip.transform.localEulerAngles = new Vector3 (this._spaceShip.transform.localEulerAngles.x, this._spaceShip.transform.localEulerAngles.y - Time.deltaTime * _speed, this._spaceShip.transform.localEulerAngles.z);
					}
				}
			}
		}
	}

	public void lossFuel(int fuel)
	{
		this._fuel = this._fuel - fuel;
		this.updateMonitorText ();
	}
	public void lossHealth(int health)
	{
		this._health = this._health - health;
		this.updateMonitorText ();
	}
	public void updateSpeed(int speed)
	{
		this._speed = speed;
		this.updateMonitorText ();
	}
	public void updateGravity(int gravity)
	{
		this._gravity = gravity;
		this.updateMonitorText ();
	}
	void updateMonitorText()
	{
		string _text = "Health : " + this._health.ToString () + "\n" + "Fuel : " + this._fuel.ToString () + "\n" + "Speed : " + this._speed.ToString ()  + "\n" + "Gravity : " + this._gravity.ToString ();
		this._monitorText.text = _text;
	}

}
//https://forum.unity3d.com/threads/rotate-an-object-around-another-object.59289/
//https://www.youtube.com/watch?v=C0uJ4sZelio