﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public LayerMask layerMask;
    [HideInInspector]
    public GameObject explosion;
    [HideInInspector]
    public AudioClip explosionSound;

    float rocketLife;
    float destroyAfter = 10;

    private void Update()
    {
        rocketLife += Time.deltaTime;
        if (rocketLife > destroyAfter)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        Collider[] hitColliders = Physics.OverlapSphere(contact.point, radius, layerMask);
        GameObject explosionInstance = (GameObject)Instantiate(explosion, contact.point, Quaternion.identity);
        explosionInstance.GetComponent<Explosion>().explosionSound = explosionSound;

        foreach (Collider col in hitColliders)
        {
            col.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(this.gameObject);
    }
}
