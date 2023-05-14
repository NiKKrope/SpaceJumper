using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGrenadeScript : MonoBehaviour
{
    Rigidbody m_RigidBody;
    public LayerMask playerMask;
    GameObject player;
    float pushForce = 10f;
    float explosionRadius = 5f;
    float explosionForce = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        m_RigidBody = GetComponent<Rigidbody>();
        m_RigidBody.AddForce((transform.forward * pushForce) + transform.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer == 10) {
            if (Physics.CheckSphere(transform.position, explosionRadius, playerMask)) {
                player.GetComponent<PlayerMovementScript>().AddExplosionForce((-transform.position + player.transform.position) * explosionForce);
            }
            gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            Invoke("DeleteSelf", 0.25f);
        }
    }

    private void DeleteSelf() {
        Destroy(gameObject);
    }
}
