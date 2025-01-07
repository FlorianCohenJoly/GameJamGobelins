using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPoireau : MonoBehaviour
{
   public GameObject poireau;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Poireau collecté !");
            Destroy(poireau);

        }
    }
}
