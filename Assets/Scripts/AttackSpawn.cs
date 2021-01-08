using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    private float _lastAngle = 0;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;

        Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPosition.x;
        mousePosition.y -= objectPosition.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (angle != _lastAngle)
        {
            float newAngle = angle - _lastAngle;
            transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), newAngle);
        }
        _lastAngle = angle;
    }
}

