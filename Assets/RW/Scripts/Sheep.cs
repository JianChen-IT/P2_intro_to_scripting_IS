﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    // Start is called before the first frame update
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;
    public AudioSource audio_drop;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;


    private SheepSpawner sheepSpawner;

    public float heartOffset;
    public GameObject heartPrefab;

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        //transform.position = new Vector3(0, 0, runSpeed * Time.deltaTime);
    }
    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        audio_drop.Play();
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
    }
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;
        Destroy(gameObject, gotHayDestroyDelay);
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }
}
