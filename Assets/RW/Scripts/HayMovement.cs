using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMovement : MonoBehaviour
{
    public float movementSpeed;
    public float limitHay;

    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
    }
    private void UpdateMovement()
    {
        float horizontalinput = Input.GetAxisRaw("Horizontal");
        if (horizontalinput < 0 && transform.position.x > -limitHay)
        {
            transform.Translate(transform.right * (-movementSpeed) * Time.deltaTime);
        }
        else if (horizontalinput > 0 && transform.position.x < limitHay)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }
    
}

