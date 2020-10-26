using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public GameObject gun;

    private GunScript ammoScript;

    public Text ammoText;

    private void Start()
    {
        ammoScript = gun.GetComponent<GunScript>();
    }

    private void Update()
    {
        ammoText.text = ammoScript.currentAmmo.ToString();
    }
}
