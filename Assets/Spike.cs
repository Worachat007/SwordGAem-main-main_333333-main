using UnityEngine;

public class Spike : MonoBehaviour
{
    public float Damage = 200f;
    public void OnTriggerEnter2D(Collider2D Collision)
    { 
        if (Collision.CompareTag("PlayerStats"))
        {
            Debug.Log("Player hit a spike");
            Collision.GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}