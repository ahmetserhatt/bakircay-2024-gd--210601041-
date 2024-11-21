using System.Collections.Generic;
using UnityEngine;

public class MatchMaker : MonoBehaviour
{
    public List<GameObject> PlacedObjects = new List<GameObject>();
    public GameObject PointA; // Obje yerleþtirme noktasý

    // Tetikleme alanýna bir obje girdiðinde çalýþýr
    private void OnTriggerEnter(Collider other)
    {
        if (PlacedObjects.Count == 0)
        {
            PlaceObjectAtPoint(other.gameObject);
        }
        else
        {
            HandleObjectInteraction(other.gameObject);
        }
    }

    // Tetikleme alanýndan bir obje çýktýðýnda çalýþýr
    private void OnTriggerExit(Collider other)
    {
        RemoveObjectIfPresent(other.gameObject);
    }

    /// <summary>
    /// Obje, yerleþtirme noktasýna taþýnýr ve listeye eklenir.
    /// </summary>
    private void PlaceObjectAtPoint(GameObject obj)
    {
        obj.transform.position = PointA.transform.position;
        obj.transform.rotation = PointA.transform.rotation;
        PlacedObjects.Add(obj);
    }

    /// <summary>
    /// Gelen obje ile yerleþtirilmiþ obje arasýndaki etkileþimi kontrol eder.
    /// </summary>
    private void HandleObjectInteraction(GameObject obj)
    {
        if (IsSameObject(obj))
        {
            DestroyMatchingObjects(obj);
        }
        else
        {
            PushObjectBack(obj);
        }
    }

    /// <summary>
    /// Yerleþtirilmiþ obje ile gelen objenin ayný olup olmadýðýný kontrol eder.
    /// </summary>
    private bool IsSameObject(GameObject obj)
    {
        return obj.name == PlacedObjects[0].name;
    }

    /// <summary>
    /// Eðer objeler ayný ise, her iki objeyi yok eder ve listeyi temizler.
    /// </summary>
    private void DestroyMatchingObjects(GameObject obj)
    {
        Debug.Log("Same objects, destroying...");
        Destroy(obj); // Gelen objeyi yok et
        Destroy(PlacedObjects[0]); // Yerleþik olan objeyi yok et
        PlacedObjects.Clear(); // Listeyi temizle
    }

    /// <summary>
    /// Eðer objeler farklý ise, objeyi geriye doðru iter.
    /// </summary>
    private void PushObjectBack(GameObject obj)
    {
        Debug.Log("Different objects, pushing back...");
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 pushDirection = obj.transform.position - PointA.transform.position;
            rb.AddForce(pushDirection.normalized * 5f, ForceMode.Impulse); // Kuvvet uygula
        }
    }

    /// <summary>
    /// Eðer obje listedeyse, listeden çýkarýr.
    /// </summary>
    private void RemoveObjectIfPresent(GameObject obj)
    {
        if (PlacedObjects.Contains(obj))
        {
            PlacedObjects.Remove(obj);
        }
    }
}
