using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectTransform : MonoBehaviour
{

    public void SetTransform(GameObject targetObject)
    {
        gameObject.transform.position = targetObject.transform.position;
        gameObject.transform.rotation = targetObject.transform.rotation;
    }
}
