using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _objective;
    [SerializeField] private float _velTraslacion;
    [SerializeField] private float _velRotacion;

    private void FixedUpdate()
    {
        Traslacion();
        Rotacion();
    }

    private void Traslacion()
    {
        var posObjective = _objective.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, posObjective, _velTraslacion * Time.deltaTime);
    }

    private void Rotacion()
    {
        var direction = _objective.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _velRotacion * Time.deltaTime);

    }
}
