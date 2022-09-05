using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera interiorCamera;
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private GameObject playerGameObject;

    private void Awake()
    {
        interiorCamera.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interiorCamera.gameObject.SetActive(InvertBool(interiorCamera.gameObject.activeInHierarchy));

        playerGameObject.transform.position = destination.position;
    }

    private bool InvertBool(bool boolToInvert)
    {
        return !boolToInvert;
    }
}
