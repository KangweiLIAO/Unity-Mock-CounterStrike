using UnityEngine;

public class Enermy : MonoBehaviour {
    public float health = 100f;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
