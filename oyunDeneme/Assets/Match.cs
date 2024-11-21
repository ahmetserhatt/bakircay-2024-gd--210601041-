using System.Collections.Generic;
using UnityEngine;

public class MatchMaker : MonoBehaviour
{
    public List<GameObject> PlacedObjects = new List<GameObject>();
    public GameObject PointA; // Obje yerle�tirme noktas�

    // Tetikleme alan�na bir obje girdi�inde �al���r
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

    // Tetikleme alan�ndan bir obje ��kt���nda �al���r
    private void OnTriggerExit(Collider other)
    {
        RemoveObjectIfPresent(other.gameObject);
    }

    /// <summary>
    /// Obje, yerle�tirme noktas�na ta��n�r ve listeye eklenir.
    /// </summary>
    private void PlaceObjectAtPoint(GameObject obj)
    {
        obj.transform.position = PointA.transform.position;
        obj.transform.rotation = PointA.transform.rotation;
        PlacedObjects.Add(obj);
    }

    /// <summary>
    /// Gelen obje ile yerle�tirilmi� obje aras�ndaki etkile�imi kontrol eder.
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
    /// Yerle�tirilmi� obje ile gelen objenin ayn� olup olmad���n� kontrol eder.
    /// </summary>
    private bool IsSameObject(GameObject obj)
    {
        return obj.name == PlacedObjects[0].name;
    }

    /// <summary>
    /// E�er objeler ayn� ise, her iki objeyi yok eder ve listeyi temizler.
    /// </summary>
    private void DestroyMatchingObjects(GameObject obj)
    {
        Debug.Log("Same objects, destroying...");
        Destroy(obj); // Gelen objeyi yok et
        Destroy(PlacedObjects[0]); // Yerle�ik olan objeyi yok et
        PlacedObjects.Clear(); // Listeyi temizle
    }

    /// <summary>
    /// E�er objeler farkl� ise, objeyi geriye do�ru iter.
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
    /// E�er obje listedeyse, listeden ��kar�r.
    /// </summary>
    private void RemoveObjectIfPresent(GameObject obj)
    {
        if (PlacedObjects.Contains(obj))
        {
            PlacedObjects.Remove(obj);
        }
    }
}
