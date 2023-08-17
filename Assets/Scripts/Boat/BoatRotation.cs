using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boatPrefab; 
    [SerializeField]
    private int numberOfBoats = 5; 
    [SerializeField]
    private float boatSpacing = 20f; 

    [SerializeField]
    private Vector3 centerPosition; 
    [SerializeField]
    private float squareSize = 10f; 

    [SerializeField]
    private float rotationSpeed = 1f; // Speed at which the boats rotate

    private List<GameObject> boats = new List<GameObject>();

    private void Start()
    {
        // Find the parent GameObject named "Sharks"
        GameObject sharksParent = GameObject.Find("Sharks");

        if (sharksParent == null)
        {
            Debug.LogError("Could not find the 'Sharks' parent GameObject.");
            return;
        }

        for (int i = 0; i < numberOfBoats; i++)
        {
            Vector3 startPos = centerPosition + new Vector3(squareSize / 2f, 0f, squareSize / 2f);
            startPos += Vector3.right * (i * boatSpacing);

            // Instantiate the boat under the "Sharks" parent GameObject
            GameObject boat = Instantiate(boatPrefab, startPos, Quaternion.identity);
            boat.transform.SetParent(sharksParent.transform);
            boats.Add(boat);
        }
    }

    private void Update()
    {

        float time = Time.time;
        for (int i = 0; i < boats.Count; i++)
        {
            float xOffset = Mathf.Sin(time + i) * squareSize / 2f;
            float zOffset = Mathf.Cos(time + i) * squareSize / 2f;
            Vector3 targetPosition = centerPosition + new Vector3(xOffset, 0f, zOffset);
            boats[i].transform.position = Vector3.Lerp(boats[i].transform.position, targetPosition, Time.deltaTime);

            Vector3 directionToTarget = targetPosition - boats[i].transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            boats[i].transform.rotation = Quaternion.RotateTowards(boats[i].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
