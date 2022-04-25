using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    // Find Portal Position
    [SerializeField] Vector3 TargetPosition;
    [SerializeField] Vector3 PositionOffset;
    [SerializeField] RectTransform PointerRectTransform;

    private void Awake()
    {
        PointerRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        TargetPosition = new Vector3(245f, -2.3f, 0f);
        Vector3 ToPos = TargetPosition - PositionOffset;
        Vector3 FromPos = Camera.main.transform.position;
        FromPos.z = 0f;
        Vector3 dir = (ToPos - FromPos).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        PointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }



 // float angle = Mathf.Atan2(p2.y-p1.y, p2.x-p1.x)*180 / Mathf.PI;

}
