using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidWobble : MonoBehaviour
{
    private Transform _transform;
    private Material _fluidMaterial;

    [SerializeField] private float fluidPercent;
    [SerializeField] private float foamSize;

    [SerializeField] private float recoverySpeed;
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMax;

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
        _fluidMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Update()
    {
        _fluidMaterial.SetFloat("_FluidPercent", Mathf.Clamp01(fluidPercent));
        _fluidMaterial.SetFloat("_FoamSize", Mathf.Clamp01(foamSize));

        _wobbleAddX = Mathf.Lerp(_wobbleAddX, 0, recoverySpeed * Time.deltaTime);
        _wobbleAddZ = Mathf.Lerp(_wobbleAddZ, 0, recoverySpeed * Time.deltaTime);

        _wave = Mathf.Sin(2 * Mathf.PI * wobbleSpeed * Time.time);
        _wobbleX = _wobbleAddX * _wave;
        _wobbleZ = _wobbleAddZ * _wave;

        _fluidMaterial.SetFloat("_WobbleX", _wobbleX);
        _fluidMaterial.SetFloat("_WobbleZ", _wobbleZ);

        _velocity = _lastPosition - _transform.position;
        _angularVelocity = _lastRotation - _transform.rotation.eulerAngles;

        _wobbleAddX += _velocity.z * wobbleMax;
        _wobbleAddZ += _velocity.x * wobbleMax;

        _wobbleAddX = Mathf.Clamp(_wobbleAddX, -wobbleMax, wobbleMax);
        _wobbleAddZ = Mathf.Clamp(_wobbleAddZ, -wobbleMax, wobbleMax);

        _lastPosition = _transform.position;
        _lastRotation = _transform.rotation.eulerAngles;
    }
}