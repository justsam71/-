    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilityTimer;

    [SerializeField]
    private GameObject[] skins;
    private GameObject skin;

    [SerializeField]
    private Material[] trailMaterials;

    private float time;
    private bool timerCooldown;
    private float abilityDuration;
    private bool abilityCooldown;

    [SerializeField]
    private GameObject player;
    private GameManager gm;
    private GameManager.Character chosenHero;

    //--------------
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam;
    public LayerMask whatIsGrappleable;
    [Header("Swinging")]
    private float maxSwingDistance = 20f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        chosenHero =  gm.CharacterType();

        if (chosenHero == GameManager.Character.panda)
        {
            skin = Instantiate(skins[0], player.transform);
            player.transform.GetComponent<TrailRenderer>().material = trailMaterials[0]; 
        }
        else if (chosenHero == GameManager.Character.fox)
        {
            skin = Instantiate(skins[1], player.transform);
            player.transform.GetComponent<TrailRenderer>().material = trailMaterials[1];
        }
        else if (chosenHero == GameManager.Character.spider)
        {
            skin = Instantiate(skins[2], player.transform);
            player.transform.GetComponent<TrailRenderer>().material = trailMaterials[1];
        }

        skin.transform.SetParent(player.transform);

    }

    void Update()
    {
        if ((int)chosenHero == 2)
        {
            if (Input.GetButtonDown("Fire1")) StartSwing();
            if (Input.GetButtonUp("Fire1")) StopSwing();

            CheckForSwingPoints();

            
        }

        if (timerCooldown == false && abilityCooldown == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Ability();
            }
        }
        else if (timerCooldown == true)
        {
            time -= Time.deltaTime;
            abilityTimer.text = Mathf.FloorToInt(time).ToString();
            if (Mathf.FloorToInt(time) <= 0)
                Reset();
        }   
        else if (abilityCooldown == true)
        {
            abilityDuration -= Time.deltaTime;
            abilityTimer.text = "-";
            if (abilityDuration <= 0)
            {
                if ((int)chosenHero == 0) //panda
                {
                    this.GetComponent<SphereCollider>().material.bounciness = 0f;
                    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    player.GetComponent<newMovement>().forwardForce /= 1.25f;
                    player.GetComponent<newMovement>().sidewaysForce /= 1.25f;
                }
                if ((int)chosenHero == 1) //fox
                {
                    player.GetComponent<newMovement>().forwardForce /= 1.75f;
                    player.GetComponent<newMovement>().sidewaysForce /= 1.75f;
                }
                timerCooldown = true;
                abilityCooldown = false;
            }
        }


    }

    private void Reset()
    {
        timerCooldown = false;
        abilityTimer.text = "E";
        time = 5f;
        abilityDuration = 3;
    }

    void Ability()
    {
        if ((int) chosenHero == 0) //panda
        {
            this.GetComponent<SphereCollider>().material.bounciness = 0.4f;
            this.transform.localScale = new Vector3(1, 1, 1f);
            abilityCooldown = true;
            player.GetComponent<newMovement>().forwardForce *= 1.25f;
            player.GetComponent<newMovement>().sidewaysForce *= 1.25f;
        }
        else if ((int) chosenHero == 1) //fox

        {
            player.GetComponent<newMovement>().forwardForce *= 1.75f;
            player.GetComponent<newMovement>().sidewaysForce *= 1.75f;

            abilityCooldown = true;
        }
    }

    void StartSwing()
    {
        if (predictionHit.point == Vector3.zero) return;
        
        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.transform.position, swingPoint);

        //the distance grapple will try to keep from grapple point.
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        //customize values as you like
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }
    void StopSwing()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void CheckForSwingPoints()
    {
        if (joint != null) return; //if already swinging, no need to check for points

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.transform.position, predictionSphereCastRadius, cam.transform.up, out sphereCastHit,
            maxSwingDistance, whatIsGrappleable);
        
        RaycastHit raycastHit;
        Physics.Raycast(cam.transform.position, cam.transform.up, out raycastHit, maxSwingDistance, whatIsGrappleable);

        Vector3 realHitPoint;
        //Option1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;
        //Option2 - Predicted Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;
        //Option3
        else
            realHitPoint = Vector3.zero;

        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    private void LateUpdate()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 2f);

        lr.SetPosition(0, cam.transform.position);
        lr.SetPosition(1, swingPoint);
    }
}
