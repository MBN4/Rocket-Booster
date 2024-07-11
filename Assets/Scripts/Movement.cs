using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            else
            {
                audioSource.Stop();
            }
        }
       


       // else if (Input.GetKey(KeyCode.DownArrow))
        //{
          //  rb.AddRelativeForce(-Vector3.up * Time.deltaTime * mainThrust);
        //}
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);
        }
        
       else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationThrust);
        }
        
       void ApplyRotation(float rotationThisFrame){
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
            rb.freezeRotation = false;
        }
    }
}
