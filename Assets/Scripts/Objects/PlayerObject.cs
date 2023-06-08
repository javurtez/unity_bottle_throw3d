using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerObject : MonoBehaviour
{
    public float lerpRotate = 1f;
    public Vector3 force;
    public Vector3 rotationForce;
    private new Rigidbody rigidbody;

    private bool canCheckRotation = true;
    private bool isInRest = true;
    public bool hasJumped = false;
    public bool hasDoubleJumped = false;
    private Vector3 normalRotation;

    public LayerMask groundLayerMask;
    public float distance = .2f;
    public Transform[] groundDetections;
    public bool isDead = false;

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isDead) return;
        if (LevelManager.Instance.isFinishLevel) return;
        if (PlayerManager.Instance.isSelectingBottle) return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TapOnScreen();
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Began)
            {
                TapOnScreen();
            }
        }
#endif
    }
    private void FixedUpdate()
    {
        if (isDead) return;
        if (LevelManager.Instance.isFinishLevel) return;
        if (PlayerManager.Instance.isSelectingBottle) return;

        if (canCheckRotation)
        {
            if (!isInRest)
            {
                foreach (Transform groundDetect in groundDetections)
                {
                    if (Physics.Raycast(groundDetect.position, Vector3.down, out RaycastHit hit, distance, groundLayerMask))
                    {
                        if (hit.collider)
                        {
                            hasDoubleJumped = false;
                            isInRest = true;
                            hasJumped = false;
                            normalRotation = hit.normal;
                            rigidbody.velocity = Vector3.zero;
                        }
                    }
                }
            }
        }
        if (isInRest && !hasJumped)
        {
            transform.up = Vector3.Lerp(transform.up, normalRotation, 0.4f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(Constants.GroundTag))
        {
            AudioManager.Instance.PlayCollision();
        }
    }

    private void TapOnScreen()
    {
        bool noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
        if (!noUIcontrolsInUse) return;
        PlayerManager.Instance.StartLevel();

        if (!hasDoubleJumped)
        {
            if (hasJumped)
            {
                foreach (Transform groundDetect in groundDetections)
                {
                    if (!Physics.Raycast(groundDetect.position, Vector3.down, out RaycastHit hit, distance, groundLayerMask))
                    {
                        hasDoubleJumped = true;
                    }
                }
            }

            Flip();
        }
    }
    public void Flip()
    {
        AudioManager.Instance.PlayFlip();

        canCheckRotation = false;
        isInRest = false;
        rigidbody.velocity = Vector3.zero;
        hasJumped = true;

        rigidbody.AddForce(force, ForceMode.Impulse);
        rigidbody.angularVelocity = Vector3.zero;
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, transform.eulerAngles.z, -360, lerpRotate).setOnUpdate((float f) =>
        {
            transform.eulerAngles = new Vector3(0, 0, f);
        });

        LeanTween.delayedCall(gameObject, .5f, () =>
        {
            canCheckRotation = true;
        });
    }
    public void Dead()
    {
        LeanTween.cancel(gameObject);
        isDead = true;
        hasDoubleJumped = false;
        hasJumped = false;
    }
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180)
                angle -= 360;  // convert all angles to -180..+180
            if (max > 180)
                max -= 360;
            if (min > 180)
                min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0)
            angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }
}