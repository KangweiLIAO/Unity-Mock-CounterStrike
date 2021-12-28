using UnityEngine;

public class Gun : MonoBehaviour {
    public float damage = 10f;
    public float fireRange = 100f;
    public float fireRate = 10f;
    public float nextTimeToFire = 0; // ~= cooldown time

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire) { // if able to fire after cooldown time
            FireWeapon();
            nextTimeToFire = Time.time + 1f / fireRate; // Fire rate increase = smaller fire interval
        }
    }

    void FireWeapon() {
        muzzleFlash.Play();
        RaycastHit hit; // store hitted object information
        // Raycast(origin, direction) within "fireRange"; store hit game object info in "hit":
        bool isHit = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, fireRange);
        if (isHit) {
            // Debug.Log(hit.transform.name);
            Debug.DrawLine(fpsCam.transform.position, hit.point, Color.green, 5f);
            Enermy target = hit.transform.GetComponent<Enermy>();
            if (target != null) {
                target.TakeDamage(10f);
            }
        }
        // instantiate the impactEffect at the surface, such that the effect direction = hit surface norm direction
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // turns hit suface's normal direction into a Quaternion
        Destroy(impactGO, 2);
    }
}
