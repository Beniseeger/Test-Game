using System.Collections;
using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject player;
    private Animator reloadAnim;
    public float range = 100f;
    public float fireRate = 15f;
    public float damage = 10f;

    //Ammo
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;

    public ParticleSystem muzzleFlash;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10.0f;

    private bool isReloading = false;

    public float lifeTime = 3.0f;

    private float nextTimeToFire = 0f;
    private AudioSource startShootingAudio;

    private void Start()
    {
        reloadAnim = player.GetComponent<Animator>();
        currentAmmo = maxAmmo;
        startShootingAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        //minus ammo
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.parent = gameObject.transform.parent;

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());




        bullet.transform.position = bulletSpawn.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        Debug.DrawLine(bullet.transform.position, getShootingDirection().normalized);
        bullet.GetComponent<Rigidbody>().AddForce(getShootingDirection().normalized * bulletSpeed, ForceMode.Impulse);

        bullet.transform.parent = null;

        startShootingAudio.Play();

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));

        //RaycastHit hit;
        //if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range))
        //{
        //    print("Getroffenes Objekt: " + hit.collider.gameObject.name);
        //    Target target = hit.transform.GetComponent<Target>();

        //    if (target != null)
        //    {
        //        target.TakeDamage(damage);
        //    }
        //}
    }


    private Vector3 getShootingDirection()
    {
        Vector3 linePointing = Vector3.zero;
        Vector3 localCord = Vector3.zero;
        Vector3 pos = new Vector3(200, 200, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        int layer_mask = ~LayerMask.GetMask("TerrainCollider");

        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            linePointing = new Vector3(hit.point.x, hit.point.y, hit.point.z)
            {
                y = hit.point.y + 0.5f
            };


            localCord = linePointing - bulletSpawn.position;

            float angle = Mathf.Atan(localCord.x / localCord.z) * Mathf.Rad2Deg;

            if (Mathf.Abs(angle) < 80 && localCord.z > 0)
            {
                return localCord;
            } 
            else
            {
                return new Vector3(localCord.x * 0.17f, localCord.y, Mathf.Abs(localCord.z));
            }

        }
        else
        {
            return Vector3.zero;
        }

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay) 
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadAnim.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        reloadAnim.SetBool("isReloading", false);
    }
}
