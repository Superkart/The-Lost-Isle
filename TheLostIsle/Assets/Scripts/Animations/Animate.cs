/*
Filename: Animate.cs
Author: Musa Elqaq
Last Edited On: 4/11/2025

Purpose: Automate the animations used on the player in relation to what keys they press.
*/

using System;
using UnityEngine;

public class Animate : MonoBehaviour {
    private Animator anim;

    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Set the trigger value to True for the parameter Dance.
            anim.SetTrigger("Jump");
        }

        // Set the trigger value to True for Walking if the player presses the "w" key
        if (Input.GetKeyDown(KeyCode.W)) {
            anim.SetTrigger("Walk");

            // Set the trigger value to True for Sprinting if player is both walking and pressing shift
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
                anim.SetTrigger("Sprint");
            }
        }
    }

    // todo
    /// <summary>
    /// When true, the player is currently on a stair case, so the stair animation(s) should be used
    /// </summary>
    /// <returns>True or False - True if on a staircase, false if not</returns>
    Boolean isOnStairs() {
        return false;
    }
}