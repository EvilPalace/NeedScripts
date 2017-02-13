using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotateForever))]
public class RotateForeverEditor : Editor {
	private SerializedObject obj;

	private SerializedProperty isRotate;
	private SerializedProperty timeOfRotateOnePeriod;
	private SerializedProperty space;
	private SerializedProperty up;
	private SerializedProperty down;
	private SerializedProperty axis;

	void OnEnable()
	{
		obj = new SerializedObject (target);
		isRotate = obj.FindProperty ("isRotate");
		timeOfRotateOnePeriod = obj.FindProperty ("timeOfRotateOnePeriod");
		space = obj.FindProperty ("space");
		up = obj.FindProperty ("up");
		down = obj.FindProperty ("down");
		axis = obj.FindProperty ("axis");
	}

	public override void OnInspectorGUI()
	{
		obj.Update ();

		EditorGUILayout.PropertyField (isRotate, new GUIContent ("是否旋转"));
		EditorGUILayout.PropertyField (timeOfRotateOnePeriod, new GUIContent ("旋转一圈需要的时间"));
		EditorGUILayout.PropertyField (space, new GUIContent ("围绕的旋转轴"));
		EditorGUILayout.LabelField ("当前围绕轴为：");
		if (space.enumValueIndex == 0) {
			EditorGUILayout.LabelField ("      世界坐标系的上");
		}
		else if (space.enumValueIndex == 1) {
			EditorGUILayout.LabelField ("       它自身的上");
		}
		else if (space.enumValueIndex == 2) {
			EditorGUILayout.PropertyField (up, new GUIContent("上顶点"));
			EditorGUILayout.PropertyField (down, new GUIContent("下顶点"));
		} else if(space.enumValueIndex == 3){
			EditorGUILayout.PropertyField (axis);
		}

		obj.ApplyModifiedProperties();
	}

}
