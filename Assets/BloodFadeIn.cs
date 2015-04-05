﻿using System;
using UnityEngine;

public class BloodFadeIn : MonoBehaviour 
{
	void FixedUpdate() 
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 1f, 1f, Math.Min(renderer.color.a + 0.002f, 1f));
	}
}
