using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
}