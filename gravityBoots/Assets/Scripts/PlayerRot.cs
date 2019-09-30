using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// C# translation from http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html
/// Author: UA @aldonaletto 
/// </summary>

// Prequisites: create an empty GameObject, attach to it a Rigidbody w/ UseGravity unchecked
// To empty GO also add BoxCollider and this script. Makes this the parent of the Player
// Size BoxCollider to fit around Player model.
[RequireComponent(typeof(AudioSource))]
public class PlayerRot : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 6; // move speed
    [SerializeField]
    private bool isWalking = true;
    [SerializeField]
    private GameObject cam; // camera
    [SerializeField]
    private float lerpSpeed = 10; // smoothing speed
    [SerializeField]
    private float gravity = 10; // gravity acceleration
    [SerializeField]
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    [SerializeField]
    private float jumpSpeed = 10; // vertical jump initial speed
    [SerializeField]
    private float jumpRange = 10; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private bool isJumping = false;
    private float vertSpeed = 0; // vertical jump current speed
    bool airborne = false;

    //[SerializeField]
    //private AudioClip[] footstepSounds;    // an array of footstep sounds that will be randomly selected from.
    //[SerializeField]
    //private AudioClip jumpSound;           // the sound played when character leaves the ground.
    //[SerializeField]
    //private AudioClip landSound;           // the sound played when character touches back on ground.
    //[SerializeField]
    //[Range(0f, 1f)] private float runstepLenghten;
    //[SerializeField]
    //private float m_StepInterval;
    //private float stepCycle;
    //private float nextStep;

    private Transform myTransform;
    private BoxCollider boxCollider; // drag BoxCollider ref in editor
    public bool bootsOn = true;
    //private AudioSource m_AudioSource;

    private void Start()
    {
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation

        boxCollider = GetComponent<BoxCollider>();
        // distance from transform.position to ground
        distGround = boxCollider.size.y - boxCollider.center.y;

        //stepCycle = 0f;
        //nextStep = stepCycle / 2f;
        //m_AudioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate()
    {
        if ((transform.eulerAngles.x > 1 || transform.eulerAngles.x < -1 || transform.eulerAngles.z > 1 || transform.eulerAngles.z < -1) && !bootsOn)
        {
            StartCoroutine("returnVertical");
        }
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * myNormal);

        // jump code - jump to wall or simple jump
        if (jumping) return; // abort Update while jumping to a wall

        Ray ray;
        RaycastHit hit;

        if (isJumping)
        { // jump pressed:
            ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out hit, jumpRange, 1 << 9) && bootsOn)
            { // wall ahead?
                //PlayJumpSound();
                JumpToWall(hit.point, hit.normal); // yes: jump to the wall
            }
            else if (isGrounded && !airborne)
            { // no: if grounded, jump up
                //PlayJumpSound();
                GetComponent<Rigidbody>().AddForce(jumpSpeed * myNormal);
                airborne = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (transform.eulerAngles.x > 1 || transform.eulerAngles.x < -1 || transform.eulerAngles.z > 1 || transform.eulerAngles.z < -1)
            {
                GetComponent<Rigidbody>().velocity += jumpSpeed * myNormal / 2;
                StartCoroutine("returnVertical");
            }
        }
        // update surface normal and isGrounded:
        ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit))
        { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            if (isGrounded && airborne)
            {
                //PlayLandingSound();
                airborne = false;
            }
            else if (!isGrounded)
            {
                airborne = true;
            }
            surfaceNormal = hit.normal;
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * -myNormal);
            }
        }
        else
        {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        // find forward direction with new myNormal:
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
        // align character to the new myNormal while keeping the forward direction:
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        float speed;


        // move the character forth/back with Vertical axis:
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        // move the character left/right with Horizontal axis:
        myTransform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        speed = moveSpeed;

        //ProgressStepCycle(speed);
    }

    private void LateUpdate()
    {
        isWalking = !Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetButton("Jump");
    }

    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        GetComponent<Rigidbody>().isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        GetComponent<Rigidbody>().isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }


    IEnumerator returnVertical()
    {
        yield return new WaitForSeconds(0.3f);

        JumpToWall(transform.position, new Vector3(0, 0, 0));
    }

    //private void PlayLandingSound()
    //{
    //    m_AudioSource.clip = landSound;
    //    m_AudioSource.Play();
    //    nextStep = stepCycle + .5f;
    //}

    //private void PlayJumpSound()
    //{
    //    m_AudioSource.clip = jumpSound;
    //    m_AudioSource.Play();
    //}

    //private void ProgressStepCycle(float speed)
    //{
    //    if (transform.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
    //    {
    //        stepCycle += (transform.GetComponent<Rigidbody>().velocity.sqrMagnitude + (speed * (isWalking ? 1f : runstepLenghten))) *
    //                     Time.fixedDeltaTime;
    //    }

    //    if (!(stepCycle > nextStep))
    //    {
    //        return;
    //    }

    //    nextStep = stepCycle + m_StepInterval;


    //    PlayFootStepAudio();
    //}


    //private void PlayFootStepAudio()
    //{
    //    if (isGrounded)
    //    {
    //        return;
    //    }
    //    // pick & play a random footstep sound from the array,
    //    // excluding sound at index 0
    //    int n = Random.Range(1, footstepSounds.Length);
    //    m_AudioSource.clip = footstepSounds[n];
    //    m_AudioSource.PlayOneShot(m_AudioSource.clip);
    //    // move picked sound to index 0 so it's not picked next time
    //    footstepSounds[n] = footstepSounds[0];
    //    footstepSounds[0] = m_AudioSource.clip;
    //}
}
