#region 'Using' info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class HarpoonFire : MonoBehaviour
{
    [SerializeField] HarpoonGun cannon;
    public Transform firepoint;
    public GameObject bullet; // The cannonball that fires.

    float timeBetween; // the time between shots (probably will be removed)
    public float startTimeBetween; // begins a countdown between shots

    public Slider Slider;
    public Vector3 Offset; // controls where the bar appears in-game
    public Color Low;
    public Color High;
    public GameObject self;

    public void ProgressBar()
    {
        Slider.gameObject.SetActive(timeBetween < startTimeBetween && timeBetween > 0 && self.activeInHierarchy);
        Slider.value = timeBetween;
        Slider.maxValue = 6; // set to the same value as the startTimeBetween

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }

    void Start()
    { timeBetween = startTimeBetween; } // will likely be removed, best for automatic fire

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E) && timeBetween <= 0) // && !cannon.HasFired
            { 
                Instantiate(bullet,firepoint.position,firepoint.rotation);
                timeBetween = startTimeBetween; // start the cooldown
                cannon.Fire();
            }
        }
    }

    private void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset); // keep progress bar above harpoon gun
        timeBetween -= Time.deltaTime;

        ProgressBar(); // update the progress bar every frame so it's accurate
    }
}