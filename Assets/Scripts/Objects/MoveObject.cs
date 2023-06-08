using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 1;
    public Vector3 moveLocalPosition;
    public new Rigidbody rigidbody;

    private bool isMove = false;

    private void Update()
    {
        if (!isMove) return;
        if (Vector3.Distance(transform.localPosition, moveLocalPosition) <= .1f)
        {
            isMove = false;
            return;
        }
        rigidbody.MovePosition((transform.position + transform.right * Time.fixedDeltaTime * moveSpeed));
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (isMove) return;
        if (collision.collider.tag.Equals(Constants.PlayerTag))
        {
            isMove = true;
        }
    }
}