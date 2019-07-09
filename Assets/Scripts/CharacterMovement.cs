﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Transform CameraTransform;
	public CharacterStatus characterStatus;
	public Animator anim;


	public float vertical;
	public float horizontal;
	public float moveAmount;
	public float rotationSpeed;

	public Vector3 rotationDirection;
	public Vector3 moveDirection;

	public void MoveUpdate()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal)); 

		anim.SetFloat("vertical", vertical, 0.15f, Time.deltaTime);

		Vector3 moveDir = CameraTransform.forward* vertical;
		moveDir += CameraTransform.right * horizontal;
		moveDir.Normalize();
		moveDirection = moveDir;
		rotationDirection = CameraTransform.forward;

		rotationNormal();
		characterStatus.isGround = Ground();
	}

	public void rotationNormal()
	{
		if(!characterStatus.isAiming)
		{
			rotationDirection = moveDirection;
		}

		Vector3 targetDir = rotationDirection;
		targetDir.y = 0;

		if(targetDir == Vector3.zero)
		targetDir = transform.forward;

		Quaternion lookDir = Quaternion.LookRotation(targetDir);
		Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
		transform.rotation = targetRot;
	}

	public bool Ground()
	{
		Vector3 origin = transform.position;
		origin.y += 0.6f;
		Vector3 dir = -Vector3.up;
		float dis = 0.7f;
		RaycastHit hit;
		if(Physics.Raycast(origin, dir, out hit, dis))
		{
			Vector3 tp = hit.point;
			transform.position = tp;
			return true;
		}
		return false;
	}
}
