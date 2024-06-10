using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilityTimer;

    [SerializeField]
    private GameObject[] skins;
    private GameObject skin;

    [SerializeField]
    private Material[] trailMaterials;

    [SerializeField]
    private Sprite[] abilityImgs;

    private float time;
    private bool WaitingForCooldown = false;
    private float abilityDuration;
    private bool abilityCooldown = false;

    public GameObject player;
    [SerializeField]
    private GameManager gm;
    private GameManager.Character chosenHero;

    //--------------
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam;
    public LayerMask whatIsGrappleable;
    [Header("Swinging")]
    private float maxSwingDistance = 9f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    private Character character = null;

    void Start()
    {
        chosenHero = gm.CharacterType();
        switch (chosenHero)
        {
            case GameManager.Character.panda:
                character = new Panda(abilityImgs[0], skins[0], trailMaterials[0], this);
                break;
            case GameManager.Character.fox:
                character = new Fox(abilityImgs[0], skins[1], trailMaterials[1], this);
                break;
            case GameManager.Character.spider:
                character = new Spider(abilityImgs[0], skins[2], trailMaterials[1], this);
                break;
            default:
                break;
        }

        skin = Instantiate(character.skin, this.transform); 
        transform.GetComponent<TrailRenderer>().material = character.trailMat; 
        skin.transform.SetParent(this.transform); 

        ResetCooldown();

    }

    // Update is called once per frame
    void Update()
    {

        if (WaitingForCooldown == false && abilityCooldown == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                character.Ability();
            }

        }
        else if (WaitingForCooldown == true) //длительность после истощения абилки
        {
            time -= Time.deltaTime;
            abilityTimer.text = Mathf.FloorToInt(time).ToString();
            if (Mathf.FloorToInt(time) <= 0)
                ResetCooldown();
        }
        else if (abilityCooldown == true) //длительность активной абилки
        {
            if (Input.GetButtonUp("Fire1"))
            {
                character.InterruptAbility();
            }

            abilityDuration -= Time.deltaTime;
            abilityTimer.text = "-";
            if (abilityDuration <= 0)
            {
                character.ResetAbility();
                WaitingForCooldown = true;
                abilityCooldown = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (chosenHero == GameManager.Character.spider)
        {
            if (!joint) return;

            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 2f);

            lr.SetPosition(0, cam.transform.position);
            lr.SetPosition(1, swingPoint);
        }
    }

    private void ResetCooldown()
    {
        WaitingForCooldown = false;
        abilityTimer.text = "E";
        time = character.cooldownDuration;
        abilityDuration = character.abilityDuration;
    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    // class Character and its children classes
    public abstract class Character : MonoBehaviour
    {
        public int deathCounter = 0;
        public float abilityDuration = 5;
        public float cooldownDuration = 5;

        public Sprite abilityImg;
        public GameObject skin;
        public Material trailMat;


        public virtual void Ability() { }
        public virtual void ResetAbility() { }
        public virtual void InterruptAbility() { } //e.g. for spider
    }


    public class Fox : Character
    {
        CharacterManager cm;
        public Fox(Sprite abilityImage, GameObject skinObject, Material trailMat, CharacterManager cm)
        {
            abilityImg = abilityImage;
            skin = skinObject;
            this.trailMat = trailMat;
            this.cm = cm;

            abilityDuration = 4f;
        }

        public override void Ability()
        {
            cm.player.GetComponent<newMovement>().forwardForce *= 1.75f;
            cm.player.GetComponent<newMovement>().sidewaysForce *= 1.75f;

            cm.abilityCooldown = true;
        }

        public override void ResetAbility()
        {
            cm.player.GetComponent<newMovement>().forwardForce /= 1.75f;
            cm.player.GetComponent<newMovement>().sidewaysForce /= 1.75f;
        }
    }


    public class Panda : Character
    {
        CharacterManager cm;
        public Panda(Sprite abilityImage, GameObject skinObject, Material trailMat, CharacterManager cm)
        {
            abilityImg = abilityImage;
            skin = skinObject;
            this.trailMat = trailMat;
            this.cm = cm;

            abilityDuration = 3f;
        }

        public override void Ability()
        {
            cm.gameObject.GetComponent<SphereCollider>().material.bounciness = 0.4f;
            cm.gameObject.transform.localScale = new Vector3(1, 1, 1f);
            cm.abilityCooldown = true;
            cm.player.GetComponent<newMovement>().forwardForce *= 1.25f;
            cm.player.GetComponent<newMovement>().sidewaysForce *= 1.25f;
        }

        public override void ResetAbility()
        {
            cm.gameObject.GetComponent<SphereCollider>().material.bounciness = 0f;
            cm.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            cm.player.GetComponent<newMovement>().forwardForce /= 1.25f;
            cm.player.GetComponent<newMovement>().sidewaysForce /= 1.25f;
        }
    }


    public class Spider : Character
    {
        CharacterManager cm;

        public Spider(Sprite abilityImage, GameObject skinObject, Material trailMat, CharacterManager cm)
        {
            deathCounter = base.deathCounter;
            abilityDuration = 1.5f;
            cooldownDuration = 2.5f;

            abilityImg = abilityImage;
            skin = skinObject;
            this.trailMat = trailMat;
            this.cm = cm;
        }


        public override void Ability()
        {
            if (cm.joint != null) return; //if already swinging, no need to check for points

            RaycastHit sphereCastHit;
            Physics.SphereCast(cm.cam.transform.position, cm.predictionSphereCastRadius, cm.cam.transform.up, out sphereCastHit,
                cm.maxSwingDistance, cm.whatIsGrappleable);

            RaycastHit raycastHit;
            Physics.Raycast(cm.cam.transform.position, cm.cam.transform.up, out raycastHit, cm.maxSwingDistance, cm.whatIsGrappleable);

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
                cm.predictionPoint.gameObject.SetActive(true);
                cm.predictionPoint.position = realHitPoint;
                cm.abilityCooldown = true;
            }
            else
            {
                cm.predictionPoint.gameObject.SetActive(false);
            }

            cm.predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

            if (cm.predictionHit.point == Vector3.zero) return;

            cm.swingPoint = cm.predictionHit.point;
            cm.joint = cm.player.gameObject.AddComponent<SpringJoint>();
            cm.joint.autoConfigureConnectedAnchor = false;
            cm.joint.connectedAnchor = cm.swingPoint;

            float distanceFromPoint = Vector3.Distance(cm.player.transform.position, cm.swingPoint);

            //the distance grapple will try to keep from grapple point.
            cm.joint.maxDistance = distanceFromPoint * 0.8f;
            cm.joint.minDistance = distanceFromPoint * 0.25f;

            //customize values as you like
            cm.joint.spring = 4.5f;
            cm.joint.damper = 7f;
            cm.joint.massScale = 4.5f;

            cm.lr.positionCount = 2;
            cm.currentGrapplePosition = cm.gunTip.position;
        }

        public override void ResetAbility()
        {
            cm.lr.positionCount = 0;
            if (cm.joint is not null)
                Destroy(cm.joint);

        }
        public override void InterruptAbility() //stopSwing
        {
            cm.lr.positionCount = 0;
            if (cm.joint is not null)
                Destroy(cm.joint);
            cm.WaitingForCooldown = true;
            cm.abilityCooldown = false;
        }
    }
}
