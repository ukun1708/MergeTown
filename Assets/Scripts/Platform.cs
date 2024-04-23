using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Platform : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    public House house;

    public Box box;
    private void Awake()
    {
        transform.localScale = Vector3.zero;

        gameManager.platformList.Add(this);
    }
}
