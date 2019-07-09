using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Controller : MonoBehaviour
{
 public CharacterMovement characterMovement;

 public void FixedUpdate()
 {
  characterMovement.MoveUpdate();
 }
 
}
