using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ParticleSystem))]
public class ParticulePlexus : MonoBehaviour {

	public float maxDistance = 1.0f;

	public int maxConnections = 5;
	public int maxLineRenderes = 100;

	new ParticleSystem particuleSystem;
	ParticleSystem.Particle[] particules;

	ParticleSystem.MainModule particuleSystemMainModule;

	public LineRenderer lineRendererTemplate;
	List<LineRenderer> lineRenderes = new List<LineRenderer>();

	Transform _transform;

	// Use this for initialization
	void Start () {
		particuleSystem= GetComponent<ParticleSystem>();
		particuleSystemMainModule = particuleSystem.main;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int maxParticules = particuleSystemMainModule.maxParticles;
		if (particules == null || particules.Length < maxParticules) {
			particules = new ParticleSystem.Particle[maxParticules];
		}
		int lrIndex = 0;
		int lineRendererCount = lineRenderes.Count;
		if (lineRendererCount > maxLineRenderes)
		{
			for (int i = maxLineRenderes; i < lineRendererCount; i++)
			{
				Destroy(lineRenderes[i].gameObject);
			}

			int removedCount = lineRendererCount - maxLineRenderes;
			lineRenderes.RemoveRange(maxLineRenderes, removedCount);

			lineRendererCount -= removedCount;
		}
		if (maxConnections > 0 && maxLineRenderes > 0) {
			particuleSystem.GetParticles (particules);
			int particuleCount = particuleSystem.particleCount;
			float maxDistanceSqr = maxDistance * maxDistance;
			ParticleSystemSimulationSpace simulationSpace = particuleSystemMainModule.simulationSpace;
			switch (simulationSpace) {
			case ParticleSystemSimulationSpace.Local: 		
				_transform = transform;
				break;
			case ParticleSystemSimulationSpace.Custom:
				_transform = particuleSystemMainModule.customSimulationSpace;
				break;
			case ParticleSystemSimulationSpace.World:	
				_transform = transform;
				break;
			default :
				throw new System.NotSupportedException (string.Format ("Unsupported Simulation Space '{0}'.", System.Enum.GetName (typeof(ParticleSystemSimulationSpace), particuleSystemMainModule)));
			}
			for (int i = 0; i < particuleCount; i++) {
				if (lrIndex == maxLineRenderes) {
					break;
				}
				Vector3 p1_position = particules [i].position;
				int connections = 0;
				for (int j = i + 1; j < particuleCount; j++) {
					Vector3 p2_position = particules [j].position;//
					float distanceSqr = Vector3.SqrMagnitude (p1_position - p2_position);
					if (distanceSqr <= maxDistanceSqr) {
						LineRenderer lr;
						if (lrIndex == lineRendererCount) {
							lr = Instantiate (lineRendererTemplate, _transform, false);
							lineRenderes.Add (lr);
							lineRendererCount++;
						}
						lr = lineRenderes [lrIndex];
						lr.enabled = true;
						lr.useWorldSpace = simulationSpace == ParticleSystemSimulationSpace.World ? true : false;
						lr.SetPosition (0, p1_position);
						lr.SetPosition (1, p2_position);
						lrIndex++;
						connections++;
						if (connections == maxConnections || lrIndex == maxLineRenderes) {
							break;
						}
					}
				}
			}
		}
		for (int i = lrIndex; i < lineRendererCount; i++) {
			lineRenderes [i].enabled = false;
		}
	}
}
//https://www.youtube.com/watch?v=ruNPkuYT1Ck