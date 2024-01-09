using UnityEngine;
using Cinemachine;

public class Rig : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public Camera MainCamera { get { return _mainCamera; } }
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    public CinemachineVirtualCamera VirtualCamera { get { return _virtualCamera; } }
    [SerializeField] private GameObject _cameraPointer;
    public GameObject CameraPointer { get { return _cameraPointer; } }
    
    [SerializeField] private Vector2 _xMovementConstraints;
    [SerializeField] private Vector2 _zMovementConstraints;

    [SerializeField] private Vector2 _orthographicSizeConstraints;
    [SerializeField] private float _orthographicSizeDelta;    
    [SerializeField] private float _cameraAngularSpeed;
    [SerializeField] private float _cameraLinearSpeed;

    public void RotateCamera(float angle)
    {
        _cameraPointer.transform.Rotate(Vector3.up, angle * _cameraAngularSpeed);
    }
    public void SetConstraints(Vector2 xConstraints, Vector2 zConstraints)
    {
        _xMovementConstraints = xConstraints;
        _zMovementConstraints = zConstraints;
    }
    public void IncreaseOrthographicSize(float amount) 
    {
        if (_virtualCamera.m_Lens.OrthographicSize >= _orthographicSizeConstraints.y) { return; }
        _virtualCamera.m_Lens.OrthographicSize += amount * _orthographicSizeDelta * Time.deltaTime;
        
    }
    public void DecreaseOrthographicSize(float amount) 
    {
        if (_virtualCamera.m_Lens.OrthographicSize <= _orthographicSizeConstraints.x) { return; }
        _virtualCamera.m_Lens.OrthographicSize -= amount * _orthographicSizeDelta * Time.deltaTime;
    }

    public void MoveCamera(Vector2 movement)
    {

        _cameraPointer.transform.Translate(movement.x * _cameraLinearSpeed * Time.deltaTime, 0f, movement.y * _cameraLinearSpeed * Time.deltaTime);
        if (_cameraPointer.transform.position.x < _xMovementConstraints.x) 
        {
            _cameraPointer.transform.position = new Vector3(_xMovementConstraints.x, _cameraPointer.transform.position.y, _cameraPointer.transform.position.z); 
        }
        else if (_cameraPointer.transform.position.x > _xMovementConstraints.y)
        {
            _cameraPointer.transform.position = new Vector3(_xMovementConstraints.y, _cameraPointer.transform.position.y, _cameraPointer.transform.position.z);
        }
        if (_cameraPointer.transform.position.z < _zMovementConstraints.x)
        {
            _cameraPointer.transform.position = new Vector3(_cameraPointer.transform.position.x, _cameraPointer.transform.position.y, _zMovementConstraints.x);
        }
        else if (_cameraPointer.transform.position.z > _xMovementConstraints.y)
        {
            _cameraPointer.transform.position = new Vector3(_cameraPointer.transform.position.x, _cameraPointer.transform.position.y, _zMovementConstraints.y);
        }
    }

}
