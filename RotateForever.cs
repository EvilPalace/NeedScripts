using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour {

	public enum Space
	{
		Vector_up,
		transform_up,
		Custom_Point,
		Custom_Axis
	}

	public bool isRotate = true;
	// 周期
	[Range(0.1f,10)]
	public float timeOfRotateOnePeriod = 1;
	// 使用本地坐标还是世界坐标
	public Space space = Space.Custom_Axis;
	[SerializeField]
	private Transform down;
	[SerializeField]
	private Transform up;
	[SerializeField]
	private Vector3 axis;

	// 每次转动的角度
	private float anglePer;
	// 转动的总的角度
	private float allAngle = 360;
	// 每帧的时间
	private float timePer = 0.016f;
	// 初始旋转角度
	private Quaternion initialRotation;


	// Use this for initialization
	void Start () {
		
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		anglePer = allAngle * timePer / timeOfRotateOnePeriod;
		if (isRotate) {
			Vector3 axis;
			switch (space) {
			case Space.Vector_up:
				axis = Vector3.up;
				break;
			case Space.transform_up:
				axis = transform.up;
				break;
			case Space.Custom_Point:
				axis = up.position - down.position;
				break;
			case Space.Custom_Axis:
				axis = this.axis;
				break;
			default:
				axis = Vector3.up;
				break;
			}
			transform.RotateAround (transform.position, axis, anglePer);
		}
	}
}
