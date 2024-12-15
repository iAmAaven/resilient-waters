using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PackageTracker : MonoBehaviour
{
    public Transform target;
    public TextMeshProUGUI timeLeftText;
    public GameObject gfx, fastDeliveryIcon;
    public Sprite normalTrackerSprite, ballTrackerSprite;
    [HideInInspector] public PackageItem packageItem;

    public float rotationSpeed = 2;
    public bool isPackageTracker;
    public bool isHomeIslandTracker = false;

    // PRIVATES
    private Transform playerPos;
    private Vector3 initialPosOfGraphics;
    private bool npcDetected = false, packageDetected = false, homeDetected = false;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        if (packageItem != null && packageItem.packageInfo != null)
        {
            fastDeliveryIcon.SetActive(packageItem.packageInfo.isFastDelivery);
        }

        if (gfx != null)
            initialPosOfGraphics = gfx.transform.localPosition;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isHomeIslandTracker)
        {
            if (coll.gameObject.tag == "HomeIsland")
            {
                homeDetected = true;
                return;
            }
        }
        if (!isPackageTracker)
        {
            if (coll.gameObject.tag == "NPC" && coll.gameObject == target.gameObject)
            {
                Debug.Log("NPC detected");
                npcDetected = true;
            }
        }
        else
        {
            if (coll.gameObject.tag == "Package" && coll.gameObject == target.gameObject)
            {
                Debug.Log("Package detected");
                packageDetected = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (isHomeIslandTracker)
        {
            if (coll.gameObject.tag == "HomeIsland")
            {
                homeDetected = false;
                return;
            }
        }
        if (!isPackageTracker)
        {
            if (coll.gameObject.tag == "NPC" && coll.gameObject == target.gameObject)
                npcDetected = false;
        }
        else
        {
            if (coll.gameObject.tag == "Package" && coll.gameObject == target.gameObject)
                packageDetected = false;
        }
    }

    void Update()
    {
        if (timeLeftText != null)
        {
            timeLeftText.text = packageItem.packageInfo.deliveryTime - packageItem.deliveryHours + "h";

            if (transform.eulerAngles.z < 270f && transform.eulerAngles.z > 90f)
            {
                timeLeftText.transform.localScale = new Vector3(-1, -1, 1);
            }
            else
            {
                timeLeftText.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (playerPos != null)
        {
            transform.position = playerPos.position;

            if (gfx != null)
            {
                if (isHomeIslandTracker)
                {
                    if (homeDetected == false)
                    {
                        gfx.transform.localPosition = Vector2.MoveTowards(gfx.transform.localPosition, initialPosOfGraphics, 15f * Time.deltaTime);
                        gfx.GetComponent<SpriteRenderer>().sprite = normalTrackerSprite;
                    }
                    else
                    {
                        gfx.transform.position = Vector2.MoveTowards(gfx.transform.position, target.position, 15f * Time.deltaTime);
                        gfx.GetComponent<SpriteRenderer>().sprite = ballTrackerSprite;
                    }
                    return;
                }

                if ((!isPackageTracker && npcDetected == false) || (isPackageTracker && packageDetected == false))
                {
                    gfx.transform.localPosition = Vector2.MoveTowards(gfx.transform.localPosition, initialPosOfGraphics, 15f * Time.deltaTime);
                    gfx.GetComponent<SpriteRenderer>().sprite = normalTrackerSprite;
                }
                else if ((!isPackageTracker && npcDetected == true) || (isPackageTracker && packageDetected == true))
                {
                    Debug.Log("Move to target");
                    gfx.transform.position = Vector2.MoveTowards(gfx.transform.position, target.position, 15f * Time.deltaTime);
                    gfx.GetComponent<SpriteRenderer>().sprite = ballTrackerSprite;
                }
            }
        }
    }
}