using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTriggers : MonoBehaviour {

	stepsGame sg;

	private bool endPlanet = false;

	void Start () {
		sg = this.gameObject.GetComponentInParent<stepsGame>();
		sg.playDialog ("Dialog1");
	}

	void OnTriggerEnter()
	{
		if (this.gameObject.transform.parent.name.Equals ("playableGame")) {
			sg.exitGame ();
		}
		else if (this.gameObject.transform.parent.name.Equals ("levelGame")) {
			switch (this.gameObject.name) {
				case "Meteore":
					sg.playDialog ("Dialog2");
					sg.setMeteoreLevel (true);
					break;
				case "endMeteore":
					sg.playDialog ("Dialog3");
					sg.setMeteoreLevel (false);
					break;
				case "Storm":
					sg.setStormLevel (true);
					sg.setShaderEffect (true);
					break;
				case "endStorm":
				    sg.playDialog ("Dialog4");
					sg.setStormLevel(false);
				    sg.setShaderEffect (false);
					break;
				case "Planet":
				    sg.playDialog ("Dialog5");
					sg.setPlanetLevel (true);
					sg.setJoystick (false);
					sg.updateShip (true, 50, 20);
					break;
				case "midPlanet":
					sg.setButton (true, "Bouclier");
				    StartCoroutine (sg.waitForShield (4.5f));
					break;
				case "endPlanet":
					if (!endPlanet) {
						endPlanet = true;
						sg.playDialog ("Dialog6");
						sg.setPlanetLevel (false);
						sg.setVortex (true);
						sg.setButton (true, "Propulseur");
					}
					break;
				case "Vortex":
				    sg.playDialog ("Dialog7");
					sg.setVortex (false);
					sg.FinishGame();
					break;
				case "outLost":
					sg.setVortex (false);
					sg.exitGame();
					break;
			}
		}	
	}
}
