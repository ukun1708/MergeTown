using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelIdleAnimation : MonoBehaviour
{
    private float value;

    void Update()
    {
        value += Time.deltaTime * 8;

        var y = Mathf.Cos(value) * .5f * Time.deltaTime;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + y, transform.localScale.z);
    }
}
