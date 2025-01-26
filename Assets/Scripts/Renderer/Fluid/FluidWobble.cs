using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FluidWobble : MonoBehaviour
{
    private Transform _transform;
    private BoxCollider _collider;
    private Material _fluidMaterial;

    [SerializeField] private float fluidPercent;
    [SerializeField] private float foamPercent;

    [SerializeField] private float recoverySpeed;
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMax;

    private float _scaleY;

    private float _wobbleAddX;
    private float _wobbleAddZ;

    private float _wave;
    private float _wobbleX;
    private float _wobbleZ;

    private Vector3 _lastPosition;
    private Vector3 _lastRotation;

    private Vector3 _velocity;
    private Vector3 _angularVelocity;

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<BoxCollider>();
        _fluidMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Update()
    {

        _scaleY = Vector3.Project(_transform.rotation * _collider.size, Vector3.up).y;
        _fluidMaterial.SetFloat("_ScaleY", _scaleY);

        _fluidMaterial.SetFloat("_FluidPercent", Mathf.Clamp01(fluidPercent));
        _fluidMaterial.SetFloat("_FoamPercent", Mathf.Clamp01(foamPercent));

        _wobbleAddX = Mathf.Lerp(_wobbleAddX, 0, recoverySpeed * Time.deltaTime);
        _wobbleAddZ = Mathf.Lerp(_wobbleAddZ, 0, recoverySpeed * Time.deltaTime);

        _wave = Mathf.Sin(2 * Mathf.PI * wobbleSpeed * Time.time);
        _wobbleX = _wobbleAddX * _wave;
        _wobbleZ = _wobbleAddZ * _wave;

        _fluidMaterial.SetFloat("_WobbleX", _wobbleX);
        _fluidMaterial.SetFloat("_WobbleZ", _wobbleZ);

        _velocity = _lastPosition - _transform.position;
        _angularVelocity = _lastRotation - _transform.rotation.eulerAngles;

        _wobbleAddX += _velocity.z * 4 * wobbleMax;
        _wobbleAddZ += _velocity.x * 4 * wobbleMax;

        _wobbleAddX = Mathf.Clamp(_wobbleAddX, -wobbleMax, wobbleMax);
        _wobbleAddZ = Mathf.Clamp(_wobbleAddZ, -wobbleMax, wobbleMax);

        _lastPosition = _transform.position;
        _lastRotation = _transform.rotation.eulerAngles;
    }
}