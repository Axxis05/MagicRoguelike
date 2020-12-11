using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _zOffset;
    [SerializeField]
    private float _yOffset;
    private Vector3 _offsetVector;
    private float _cameraAngle = 0.0f;
    private float _addedYOffset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _zOffset = -8.0f;
        _yOffset = Mathf.Tan(_cameraAngle*Mathf.PI/180) * 8;                                     //15 degrees converted to radians by multiplying by PI/180. Tan(angle)*adjacent = oposite
        _yOffset += _addedYOffset;                                                               //Added offset for better field of view
        _offsetVector = new Vector3(0, _yOffset, _zOffset);
        Vector3 cameraPosition = _player.transform.position + _offsetVector;
        transform.position = cameraPosition;
        transform.RotateAround(cameraPosition, new Vector3(1, 0, 0), _cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _offsetVector;
    }
}