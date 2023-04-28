using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 8f;
    private float jumpingPower = 18f;
    private bool isFacingRight;

    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
