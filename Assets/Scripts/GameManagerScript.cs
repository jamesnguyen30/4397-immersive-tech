using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ballGameObject;
    public Rigidbody ballRigidbody;
    public Camera arCamera;

    public Text messageText;
    public Text scoreText;

    public GameObject floor;
    private MeshRenderer floorMeshrender;
    private bool isTargetImageDetected = false;

    private int score = 0;
    public BoxCollider destinationBoxCollider;
    
    public Vector3 gravityVector;
    private float gravityForceMagnitude = 100;
    
    void Start()
    {
        ballRigidbody = ballGameObject.GetComponent<Rigidbody>();
        floorMeshrender = floor.GetComponent<MeshRenderer>();
        UpdateMessage("Initializing engine ... ");
        UpdateScore();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isTargetImageDetected = floorMeshrender.enabled;

        if (isTargetImageDetected)
        {
			UpdateMessage("Image Detected!");
            ballGameObject.gameObject.SetActive(true);
            GetGravityVector(ref gravityVector, arCamera.transform);
            ballRigidbody.AddForce(gravityVector * gravityForceMagnitude * Time.deltaTime);
        }
        else
        {
			UpdateMessage("Image Not Detected!");
            ballGameObject.gameObject.SetActive(false);
        }
    }

    public void GameOver(string message = "")
    {
        Debug.Log("Message " + message);
        messageText.text = message;
    }

    private void UpdateMessage(string message) {
        messageText.text = message;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score : " + score;
    }

    public void Score(int amount)
    {
        score += amount;
        UpdateScore();
    }
    

    private void GetGravityVector(ref Vector3 gravityVector, Transform arCamPosition)
    {
        // gravityVector          
        gravityVector = -1 * arCamPosition.transform.up;         
        Debug.DrawRay(arCamPosition.position, gravityVector, Color.green);
    }
}
