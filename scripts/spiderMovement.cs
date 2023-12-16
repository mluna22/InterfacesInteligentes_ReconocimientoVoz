using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderMovement : MonoBehaviour
{
    Vector3 jumpStart;
    Vector3 jumpEnd;
    Vector3 flipStart;
    Vector3 flipEnd;
    Vector3 heightStart;
    Vector3 heightEnd;
    bool isFlipped = false;
    Vector3 jumpForce = new Vector3(0, 3, 0);

    public void Jump()
    {
        jumpStart = transform.position;
        jumpEnd = transform.position + jumpForce;
        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        float time = 0;
        while (time < 0.5f)
        {
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time > 0)
        {
            transform.position = Vector3.Lerp(jumpStart, jumpEnd, time);
            time -= Time.deltaTime;
            yield return null;
        }
    }

    public void Flip()
    {
        flipStart = transform.eulerAngles;
        flipEnd = transform.eulerAngles + new Vector3(360, 0, 0);
        heightStart = transform.position;
        heightEnd = transform.position + new Vector3(0, isFlipped ? -1 : 1, 0);
        StartCoroutine(FlipCoroutine());
    }

    IEnumerator FlipCoroutine()
    {
        float time = 0;
        while (time < 0.5f)
        {
            transform.eulerAngles = Vector3.Lerp(flipStart, flipEnd, time);
            transform.position = Vector3.Lerp(heightStart, heightEnd, time);
            time += Time.deltaTime;
            yield return null;
        }
        isFlipped = !isFlipped;
    }
}
